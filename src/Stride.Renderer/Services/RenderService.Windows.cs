using System.Runtime.InteropServices;
using Stride.Abstractions.Models;
using Stride.Abstractions.Services;
using Stride.Renderer.Constants;
using Stride.Renderer.Enums;
using Stride.Renderer.Models;

namespace Stride.Renderer.Services;

/// <summary>
/// An implementation of <see cref="IRenderService"/> for the Windows operating system.
/// </summary>
public partial class RenderService : ApplicationRenderService, IRenderService
{
    /// <summary>
    /// Handle to a GDI bitmap object.
    /// </summary>
    private nint _gdiBitmap;

    /// <summary>
    /// A handle to store the windows message procedure.
    /// </summary>
    private MessageLoop.WndProc? _windowsMessageProcedure;

    /// <inheritdoc/>
    public void Render(IApplication application)
    {
        // if there is no window on the application, throw
        if (application.Window == null)
        {
            throw new ApplicationException("Stride must have a window to render.");
        }

        // define and Register the window class
        string className = "StrideWindowClass";

        // capture the WindowMessageProcedure delegate
        _windowsMessageProcedure = WindowMessageProcedure;
        nint windowsMessageProcedurePointer = Marshal.GetFunctionPointerForDelegate(_windowsMessageProcedure);

        // create and populate a WindowMessenger
        // bgbrush can be something like GetStockObject(BLACK_BRUSH), but for now is nint.Zero
        WindowMessenger windowMessenger = new(windowsMessageProcedurePointer, Interop.Kernel.GetModuleHandle(null), nint.Zero, className);

        // call the registration function
        uint registrationErrorCode = uint.MinValue;
        ushort registered = Interop.User.RegisterClassEx(ref windowMessenger);
        if (registered == 0 && Interop.Kernel.GetLastError() != 1410)
        {
            // 1410 (0x576) is "Class already exists," which is fine, but any other error means a problem.
            throw new ApplicationException($"Window Class registration failed! Win32 Error Code: {registrationErrorCode}");
        }

        // get width & height from stride application
        var width = application.Window.Width.GetValueOrDefault(800);
        var height = application.Window.Height.GetValueOrDefault(600);

        // populate the inner canvas of the application for painting it later
        SetApplicationCanvas(width, height);

        // setup bitmap info for GDI
        BitmapInfo bitmapInfo = new(width, height, planes: 1, bitCount: 32);
        _gdiBitmap = Interop.Gdi.CreateDIBSection(nint.Zero, ref bitmapInfo, RenderingConstants.DIB_RGB_COLORS, out nint bitsPointer, nint.Zero, 0);

        // create the window
        nint windowPointer = Interop.User.CreateWindowEx((uint)WindowStyle.SystemMenu, className, application.Name ?? "Stride Application", RenderingConstants.BaseWindowStyle,
            RenderingConstants.UseDefaultWindowSize, RenderingConstants.UseDefaultWindowSize, width, height,
            nint.Zero, nint.Zero, nint.Zero, nint.Zero);
        if (windowPointer == nint.Zero)
        {
            throw new ApplicationException("Window creation failed.");
        }

        // set global alpha to allow some background to show through initially
        Interop.User.SetLayeredWindowAttributes(windowPointer, 0, 230, RenderingConstants.LWA_ALPHA);

        // remove the components that define the old title bar, and repaint (to get a mica look)
        nint currentStyle = Interop.User.SetWindowLongPtr(windowPointer, RenderingConstants.GWL_STYLE, nint.Zero);
        nint newStyle = (nint)((uint)currentStyle & ~RenderingConstants.StylesToRemove);
        Interop.User.SetWindowLongPtr(windowPointer, RenderingConstants.GWL_STYLE, newStyle);
        Interop.User.SetWindowPos(windowPointer, nint.Zero, 0, 0, 0, 0, RenderingConstants.SWP_FRAMECHANGED | RenderingConstants.SWP_NOMOVE | RenderingConstants.SWP_NOSIZE | RenderingConstants.SWP_NOZORDER);

        // apply modernization
        ApplyModernization(windowPointer);

        // display the window
        Interop.User.ShowWindow(windowPointer, (int)WindowMessage.Show);

        // start the message loop
        RunMessageLoop();
    }

    private nint WindowMessageProcedure(nint hWnd, uint msg, nint wParam, nint lParam)
        => (WindowMessage)msg switch
        {
            WindowMessage.EraseBackground => 1,
            WindowMessage.Paint => HandlePaint(hWnd),
            WindowMessage.CompositionChanged => ApplyModernization(hWnd),
            WindowMessage.Close => CloseWindow(),
            _ => Interop.User.DefWindowProc(hWnd, msg, wParam, lParam) // always call the default handler for unhandled messages!
        };

    private void RunMessageLoop()
    {
        while (Interop.User.GetMessage(out Message message, nint.Zero, 0, 0) != 0)
        {
            // translate key press messages (WM_KEYDOWN) into character messages (WM_CHAR).
            // this is important for handling text input correctly.
            Interop.User.TranslateMessage(ref message);

            // dispatch the message to the appropriate window's WindowProc function.
            // this is where Stride framework logic (rendering, input handling) begins.
            Interop.User.DispatchMessage(ref message);
        }

        // clean up application
        Console.WriteLine("Stride application shutting down.");
        Interop.Gdi.DeleteObject(_gdiBitmap);
    }

    private nint HandlePaint(nint windowPointer)
    {
        Interop.User.BeginPaint(windowPointer, out Paint ps);

        // if _gdiBitmap isn't initialized, no application painting can happen
        if (_gdiBitmap == nint.Zero)
        {
            Interop.User.EndPaint(windowPointer, ref ps);
            return nint.Zero;
        }

        // paint the application (clears and renders any components)
        PaintApplication();

        // create a memory device context compatible with the window's device context
        nint windowDeviceContext = ps.hdc;
        nint memoryDeviceContext = Interop.Gdi.CreateCompatibleDC(windowDeviceContext);

        // select the GDI bitmap (which shares memory with the SKBitmap) into the memory DC
        nint oldBitmap = Interop.Gdi.SelectObject(memoryDeviceContext, _gdiBitmap);

        // copy pixels from the memory device context to the window device context
        Interop.Gdi.BitBlt(windowDeviceContext, 0, 0, Bitmap!.Width, Bitmap!.Height, memoryDeviceContext, 0, 0, RenderingConstants.SRCCOPY);

        // clean up the GDI resources (prevent memory leaks)
        Interop.Gdi.SelectObject(memoryDeviceContext, oldBitmap);
        Interop.Gdi.DeleteDC(memoryDeviceContext);

        // end the paint message
        Interop.User.EndPaint(windowPointer, ref ps);
        return nint.Zero;
    }

    private static nint CloseWindow()
    {
        Interop.User.PostQuitMessage(0);
        return nint.Zero;
    }

    private static nint ApplyModernization(nint windowPointer)
    {
        // enable non-client rendering
        var ncRenderingEnabled = RenderingConstants.NonClientRenderingPolicyEnabled;
        Interop.Dwm.DwmSetWindowAttribute(windowPointer, RenderingConstants.NonClientRenderingPolicy, ref ncRenderingEnabled, sizeof(int));

        // set immersive dark mode
        var immersiveDarkMode = 1;
        Interop.Dwm.DwmSetWindowAttribute(windowPointer, RenderingConstants.ImmersiveDarkMode, ref immersiveDarkMode, sizeof(int));

        //var acrylicValue = DWMSBT_TRANSIENTWINDOW;
        //Interop.Dwm.DwmSetWindowAttribute(windowPointer, RenderingConstants.SystemBackdropType, ref acrylicValue, sizeof(int));

        // explicitly enable the blur effect
        BlurBehind blurBehind = new(RenderingConstants.DWM_BB_ENABLE | RenderingConstants.DWM_BB_BLURREGION);
        int result = Interop.Dwm.DwmEnableBlurBehindWindow(windowPointer, ref blurBehind);
        if (result != 0)
        {
            // if this fails, it often means the OS doesn't support the blur, or the window needs additional styling for it to apply.
            Console.WriteLine($"{nameof(Interop.Dwm.DwmEnableBlurBehindWindow)} failed with error code: {result}");
        }

        // set corner preference on the window
        var cornerPreference = RenderingConstants.WindowCornerPreferenceRound;
        Interop.Dwm.DwmSetWindowAttribute(windowPointer, RenderingConstants.WindowCornerPreference, ref cornerPreference, sizeof(int));

        return nint.Zero;
    }
}
