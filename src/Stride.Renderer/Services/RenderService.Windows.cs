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
public partial class RenderService(IApplicationRenderService applicationRenderService) : IRenderService
{
    private readonly IApplicationRenderService _applicationRenderService = applicationRenderService ?? throw new ArgumentNullException(nameof(applicationRenderService));

    /// <summary>
    /// Handle to a GDI bitmap object.
    /// </summary>
    private nint _gdiBitmap;

    /// <summary>
    /// A handle to store the windows message procedure.
    /// </summary>
    private MessageLoop.WindowProcedure? _windowsMessageProcedure;

    /// <inheritdoc/>
    public void Render(IApplication application)
    {
        // create a window and get a handle to it, throw if no window could be created
        var windowHandle = CreateWindow(application);
        if (windowHandle == nint.Zero)
        {
            throw new ApplicationException("Window creation failed.");
        }

        // apply any customization
        //ApplyCustomization(windowHandle);

        // apply modernization
        ApplyModernization(windowHandle, application);

        // display the window
        Interop.User.ShowWindow(windowHandle, (int)WindowMessage.Show);

        // start the message loop
        RunMessageLoop();
    }

    private nint CreateWindow(IApplication application)
    {
        // throw if there is no window on the application
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
        var background = application.DarkMode == true ? nint.Zero : Interop.Gdi.GetStockObject((int)WindowBrush.White);
        WindowMessenger windowMessenger = new(windowsMessageProcedurePointer, Interop.Kernel.GetModuleHandle(null), background, className);

        // call the registration function
        ushort registered = Interop.User.RegisterClassEx(ref windowMessenger);
        uint registrationErrorCode = Interop.Kernel.GetLastError();
        if (registered == 0 && registrationErrorCode != RenderingConstants.ClassAlreadyExistsError)
        {
            throw new ApplicationException($"Window Class registration failed! Win32 Error Code: {registrationErrorCode}");
        }

        // get width & height from stride application
        var width = application.Window.Width.GetValueOrDefault(800);
        var height = application.Window.Height.GetValueOrDefault(600);

        // populate the inner canvas of the application for painting it later
        _applicationRenderService.SetApplicationCanvas(width, height);

        // setup bitmap info for GDI
        BitmapInfo bitmapInfo = new(width, height, planes: 1, bitCount: 32);
        _gdiBitmap = Interop.Gdi.CreateDIBSection(nint.Zero, ref bitmapInfo, RenderingConstants.InterpretColorsAsRGB, out nint bitsPointer, nint.Zero, 0);

        // build the window style based on Stride application and window options
        var baseWindowStyle = (uint)WindowStyle.Default;
        var windowStyle = RenderingConstants.BaseWindowStyle;
        if (application.Window.TitleBar != true)
        {
            baseWindowStyle = (uint)WindowStyle.Layered;
            windowStyle = (uint)WindowStyle.Resizeable;
        }

        // create the window and return a handle to it
        return Interop.User.CreateWindowEx(baseWindowStyle, className, application.Name ?? "Stride Application", windowStyle,
            RenderingConstants.UseDefaultSize, RenderingConstants.UseDefaultSize, width, height, nint.Zero, nint.Zero, nint.Zero, nint.Zero);
    }

    private nint WindowMessageProcedure(nint hWnd, uint msg, nint wParam, nint lParam)
        => (WindowMessage)msg switch
        {
            //WindowMessage.EraseBackground => 1,
            WindowMessage.Paint => HandlePaint(hWnd),
            //WindowMessage.CompositionChanged => ApplyModernization(hWnd),
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
        _applicationRenderService.PaintApplication();

        // create a memory device context compatible with the window's device context
        nint windowDeviceContext = ps.hdc;
        nint memoryDeviceContext = Interop.Gdi.CreateCompatibleDC(windowDeviceContext);

        // select the GDI bitmap (which shares memory with the SKBitmap) into the memory DC
        nint oldBitmap = Interop.Gdi.SelectObject(memoryDeviceContext, _gdiBitmap);

        // copy pixels from the memory device context to the window device context
        var canvasWidth = _applicationRenderService.GetCanvasWidth();
        var canvasHeight = _applicationRenderService.GetCanvasHeight();
        Interop.Gdi.BitBlt(windowDeviceContext, 0, 0, canvasWidth, canvasHeight, memoryDeviceContext, 0, 0, RenderingConstants.CopyEntireRaster);

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

    private static void ApplyCustomization(nint windowPointer)
    {
        // set global alpha to allow some background to show through initially
        Interop.User.SetLayeredWindowAttributes(windowPointer, 0, 230, RenderingConstants.SetLayeredWindowAlpha);

        // remove the components that define the old title bar, and repaint (to get a mica look)
        nint currentStyle = Interop.User.SetWindowLongPtr(windowPointer, RenderingConstants.UpdateWindowStyle, nint.Zero);
        nint newStyle = (nint)((uint)currentStyle & ~RenderingConstants.StylesToRemove);
        Interop.User.SetWindowLongPtr(windowPointer, RenderingConstants.UpdateWindowStyle, newStyle);
        Interop.User.SetWindowPos(windowPointer, nint.Zero, 0, 0, 0, 0, RenderingConstants.ForceWindowFrameDraw);
    }

    private static nint ApplyModernization(nint windowPointer, IApplication application)
    {
        // enable non-client rendering
        var ncRenderingEnabled = RenderingConstants.NonClientRenderingPolicy;
        Interop.Dwm.DwmSetWindowAttribute(windowPointer, (int)WindowAttribute.NonClientRenderingPolicy, ref ncRenderingEnabled, sizeof(int));

        // set dark mode
        if (application.DarkMode == true)
        {
            var immersiveDarkMode = 1;
            Interop.Dwm.DwmSetWindowAttribute(windowPointer, (int)WindowAttribute.UseDarkMode, ref immersiveDarkMode, sizeof(int));
        }

        // transient window backdrop
        var acrylicValue = RenderingConstants.DrawBackdropAsTransientWindow;
        Interop.Dwm.DwmSetWindowAttribute(windowPointer, (int)WindowAttribute.WindowBackdropMaterial, ref acrylicValue, sizeof(int));

        // enable window blur behind
        if (application.Window?.Blur == true)
        {
            ApplyWindowBlur(windowPointer, WindowAccent.AcrylicBlurBehind);
        }

        // set corner preference on the window
        var cornerPreference = (int)WindowAttribute.UseRoundedCorners;
        Interop.Dwm.DwmSetWindowAttribute(windowPointer, (int)WindowAttribute.UseRoundedCorners, ref cornerPreference, sizeof(int));

        return nint.Zero;
    }

    private static nint ApplyWindowBlur(nint windowPointer, WindowAccent windowAccent)
    {
        // try and enable blur using the "new" Mica/Acrylic approach for newer Windows (8 and higher) versions
        AccentPolicy policy = new()
        {
            WindowAccent = windowAccent,
            AccentFlags = 0,
            GradientColor = 0x00000000,
            AnimationId = 0
        };

        // pin the policy structure in memory to get a stable pointer
        var policyHandle = GCHandle.Alloc(policy, GCHandleType.Pinned);

        // create the data structure for the P/Invoke call
        WindowCompositionAttributeData data = new()
        {
            Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
            Data = policyHandle.AddrOfPinnedObject(),
            SizeOfData = Marshal.SizeOf<AccentPolicy>()
        };

        // try to apply the new acrylic/mica blur, fallback to the classic DWM blur behind on older Windows versions
        try
        {
            int compositionResult = Interop.User.SetWindowCompositionAttribute(windowPointer, ref data);
            uint windowCompositionErrorCode = Interop.Kernel.GetLastError();
            if (compositionResult == 0 && windowCompositionErrorCode != 0)
            {
                Console.WriteLine($"Acrylic/Mica blur failed with error code: {windowCompositionErrorCode}.");
                throw new ApplicationException();
            }
        }
        catch(ApplicationException)
        {
            BlurBehind blurBehind = new(RenderingConstants.EnableBlurBehind | RenderingConstants.BlurBehindEntireWindow);
            int result = Interop.Dwm.DwmEnableBlurBehindWindow(windowPointer, ref blurBehind);
            uint enableBlurBehindErrorCode = Interop.Kernel.GetLastError();
            if (result == 0 && enableBlurBehindErrorCode != 0)
            {
                Console.WriteLine($"{nameof(Interop.Dwm.DwmEnableBlurBehindWindow)} failed with error code: {enableBlurBehindErrorCode}.");
            }
        }
        finally
        {
            // always clean up the pinned memory
            if (policyHandle.IsAllocated)
            {
                policyHandle.Free();
            }
        }

        return nint.Zero;
    }
}
