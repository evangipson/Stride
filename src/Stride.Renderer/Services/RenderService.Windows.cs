using System.Runtime.InteropServices;
using Stride.Abstractions.Models;
using Stride.Abstractions.Services;
using Stride.Renderer.Constants;
using Stride.Renderer.Enums;
using Stride.Renderer.Extensions;
using Stride.Renderer.Models;

namespace Stride.Renderer.Services;

/// <summary>
/// An implementation of <see cref="IRenderService"/> for the Windows operating system.
/// </summary>
public partial class RenderService(IApplicationRenderService applicationRenderService) : IRenderService
{
    private readonly IApplicationRenderService _applicationRenderService = applicationRenderService ?? throw new ArgumentNullException(nameof(applicationRenderService));

    private const string _defaultWindowClassName = "StrideWindowClassName";
    private const string _defaultApplicationName = "Stride Application";
    private const int _defaultWindowWidth = 800;
    private const int _defaultWindowHeight = 600;

    // static field required for lifetime management
    private static MessageLoop.WindowProcedure? _windowsMessageProcedure;

    private string _appName = _defaultApplicationName;
    private int _windowWidth;
    private int _windowHeight;
    private bool _isTransparent;
    private bool _isDarkMode;
    private bool _hasTitleBar;
    private bool _hasBlur;
    private nint _gdiBitmap;

    /// <inheritdoc/>
    public void Render(IApplication application)
    {
        // throw if there is no window on the application
        if (application?.Window == null)
        {
            throw new ApplicationException("Stride must have a window to render.");
        }

        // populate Stride application fields
        _appName = application.Name ?? _defaultApplicationName;
        _windowWidth = application.Window.Width ?? _defaultWindowWidth;
        _windowHeight = application.Window.Height ?? _defaultWindowHeight;
        _isTransparent = application.Window.Transparent ?? false;
        _isDarkMode = application.DarkMode ?? false;
        _hasTitleBar = application.Window.TitleBar ?? false;
        _hasBlur = application.Window.Blur ?? false;

        Console.WriteLine("\nRendering Stride application with the following config:");
        Console.WriteLine($"  Application Name: {_appName}");
        Console.WriteLine($"  Window Width: {_windowWidth}");
        Console.WriteLine($"  Window Height: {_windowHeight}");
        Console.WriteLine($"  Transparency: {_isTransparent}");
        Console.WriteLine($"  Dark Mode: {_isDarkMode}");
        Console.WriteLine($"  Title Bar: {_hasTitleBar}");
        Console.WriteLine($"  Blur: {_hasBlur}\n");

        // create a window and get a handle to it, throw if no window could be created
        var windowHandle = CreateWindow(application);
        if (windowHandle == nint.Zero)
        {
            throw new ApplicationException("Window creation failed.");
        }

        // keep a reference to all the user data for this rendering service instance
        GCHandle gch = GCHandle.Alloc(this);
        Interop.User.SetWindowLongPtr(windowHandle, RenderingConstants.UserData, GCHandle.ToIntPtr(gch));

        // apply Stride customizations
        ApplyStrideCustomizations(windowHandle);

        // display the window
        Interop.User.ShowWindow(windowHandle, (int)WindowMessage.Show);

        // run an initial paint of the application
        _applicationRenderService.PaintApplication();

        // start the message loop
        RunMessageLoop();
    }

    private nint CreateWindow(IApplication application)
    {
        // create and populate a WindowMessenger
        _windowsMessageProcedure = WindowMessageProcedure;
        var windowsProcedurePointer = Marshal.GetFunctionPointerForDelegate(_windowsMessageProcedure);
        var instance = Interop.Kernel.GetModuleHandle(null);
        var backgroundColor = GetBackgroundColor();
        var backgroundHandle = Interop.Gdi.GetStockObject(backgroundColor);
        WindowMessenger messenger = new(windowsProcedurePointer, instance, backgroundHandle, _defaultWindowClassName);

        // call the registration function
        Interop.User.RegisterClassEx(ref messenger)
            .ThrowOnError(nameof(Interop.User.RegisterClassEx));

        // populate the GDI bitmap
        BitmapInfo bitmapInfo = new(_windowWidth, _windowHeight, planes: 1, bitCount: 32);
        _gdiBitmap = Interop.Gdi.CreateDIBSection(nint.Zero, ref bitmapInfo, RenderingConstants.InterpretColorsAsRGB, out _, nint.Zero, 0);

        // populate the inner canvas of the application for painting it later
        _applicationRenderService.SetApplicationCanvas(_windowWidth, _windowHeight);

        // build the window style based on Stride application and window options
        var baseWindowStyle = _hasTitleBar
            ? (uint)WindowStyle.Default
            : (uint)WindowStyle.Layered;
        var windowStyle = _hasTitleBar
            ? RenderingConstants.BaseWindowStyle
            : (uint)WindowStyle.Resizeable;

        // create the window and return a handle to it
        return Interop.User.CreateWindowEx(baseWindowStyle, _defaultWindowClassName, _appName, windowStyle, RenderingConstants.UseDefaultSize, RenderingConstants.UseDefaultSize, _windowWidth, _windowHeight);
    }

    private nint WindowMessageProcedure(nint windowPointer, uint message, nint wParam, nint lParam)
        => (WindowMessage)message switch
        {
            WindowMessage.EraseBackground when _isTransparent => (nint)1,
            WindowMessage.Paint => HandlePaint(windowPointer),
            WindowMessage.Close => CloseWindow(windowPointer),
            // always call the default handler for unhandled messages!
            _ => Interop.User.DefWindowProc(windowPointer, message, wParam, lParam)
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
        Console.WriteLine($"{nameof(HandlePaint)} starting.");
        Interop.User.BeginPaint(windowPointer, out Paint ps);

        // if _gdiBitmap isn't initialized, no application painting can happen
        if (_gdiBitmap == nint.Zero)
        {
            Console.WriteLine($"GDI bitmap not initialized, {nameof(HandlePaint)} ending.");
            Interop.User.EndPaint(windowPointer, ref ps);
            return nint.Zero;
        }

        // paint the application (clears and renders any components)
        _applicationRenderService.PaintApplication();

        // create a memory device context compatible with the window's device context
        nint windowDeviceContext = ps.HardwareDeviceContext;
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
        Console.WriteLine($"{nameof(HandlePaint)} ending.");
        return nint.Zero;
    }

    private static nint CloseWindow(nint windowPointer)
    {
        // get the stored GCHandle for user data set in CreateWindow
        nint handlePtr = Interop.User.GetWindowLongPtr(windowPointer, RenderingConstants.UserData);
        if (handlePtr != nint.Zero)
        {
            // free the allocated handle
            GCHandle gch = GCHandle.FromIntPtr(handlePtr);
            gch.Free();
        }

        // post the quit message to the operating system
        Interop.User.PostQuitMessage(0);

        return nint.Zero;
    }

    private nint ApplyStrideCustomizations(nint windowPointer)
    {
        // enable non-client rendering and acrylic backdrop for transparent title bar area
        if (!_hasTitleBar)
        {
            var ncRenderingEnabled = RenderingConstants.NonClientRenderingPolicy;
            Interop.Dwm.DwmSetWindowAttribute(windowPointer, (int)WindowAttribute.NonClientRenderingPolicy, ref ncRenderingEnabled, sizeof(int))
                .ThrowOnError(nameof(Interop.Dwm.DwmSetWindowAttribute));
        }

        // set dark mode or system default theme
        var immersiveDarkMode = _isDarkMode ? 1 : 0;
        Interop.Dwm.DwmSetWindowAttribute(windowPointer, (int)WindowAttribute.UseDarkMode, ref immersiveDarkMode, sizeof(int))
            .ThrowOnError(nameof(Interop.Dwm.DwmSetWindowAttribute));

        // set corner preference on the window
        var cornerPreference = RenderingConstants.UseRoundedCorners;
        Interop.Dwm.DwmSetWindowAttribute(windowPointer, (int)WindowAttribute.RoundedCorners, ref cornerPreference, sizeof(int))
            .ThrowOnError(nameof(Interop.Dwm.DwmSetWindowAttribute));

        // apply transparency effects
        if (_isTransparent)
        {
            // set window backdrop material
            BlurBehind blurBehind = new(RenderingConstants.EnableBlurBehind | RenderingConstants.BlurBehindEntireWindow);
            Interop.Dwm.DwmEnableBlurBehindWindow(windowPointer, ref blurBehind)
                .ThrowOnError(nameof(Interop.Dwm.DwmEnableBlurBehindWindow));

            // extend the rendering area up to the title bar for transparent blurry apps without a title bar
            Margins margins = new(-1, -1, -1, -1);
            Interop.Dwm.DwmExtendFrameIntoClientArea(windowPointer, ref margins)
                .ThrowOnError(nameof(Interop.Dwm.DwmExtendFrameIntoClientArea));
        }

        // apply window blur
        if (_hasBlur)
        {
            BlurBehind blurBehind = new(RenderingConstants.EnableBlurBehind | RenderingConstants.BlurBehindEntireWindow);
            Interop.Dwm.DwmEnableBlurBehindWindow(windowPointer, ref blurBehind)
                .ThrowOnError(nameof(Interop.Dwm.DwmEnableBlurBehindWindow));

            ApplyWindowBlur(windowPointer, WindowAccent.AcrylicBlurBehind);
        }

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

        try
        {
            Interop.User.SetWindowCompositionAttribute(windowPointer, ref data)
                .ThrowOnError(nameof(Interop.User.SetWindowCompositionAttribute));
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

    private int GetBackgroundColor()
    {
        if (_isTransparent)
        {
            return (int)WindowBrush.Transparent;
        }

        if (_isDarkMode)
        {
            return (int)WindowBrush.DarkGray;
        }

        return (int)WindowBrush.LightGray;
    }
}
