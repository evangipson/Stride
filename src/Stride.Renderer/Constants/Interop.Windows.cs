using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Stride.Renderer.Models;

namespace Stride.Renderer.Constants;

/// <summary>
/// A <see langword="static"/> collection of Windows platform invoke definitions.
/// </summary>
internal static partial class Interop
{
    /// <summary>
    /// A <see langword="static"/> collection of <c>user32.dll</c> compile-time platform invoke definitions.
    /// </summary>
    internal static partial class User
    {
        [LibraryImport("user32.dll", EntryPoint = "DefWindowProcW")]
        internal static partial nint DefWindowProc(nint hWnd, uint msg, nint wParam, nint lParam);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool ShowWindow(nint hWnd, int nCmdShow);

        [LibraryImport("user32.dll", EntryPoint = "RegisterClassExW", SetLastError = true)]
        internal static partial ushort RegisterClassEx(ref WindowMessenger lpwcx);

        [LibraryImport("user32.dll", EntryPoint = "CreateWindowExW", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
        internal static partial nint CreateWindowEx(uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y, int nWidth, int nHeight, nint hWndParent = 0, nint hMenu = 0, nint hInstance = 0, nint lpParam = 0);
        
        [LibraryImport("user32.dll", EntryPoint = "GetMessageW")]
        internal static partial int GetMessage(out Message lpMsg, nint hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        /// <summary>
        /// Translates virtual-key messages into character messages.
        /// <para>Required to receive <c>WM_CHAR</c> messages from <c>WM_KEYDOWN</c>.</para>
        /// </summary>
        /// <param name="lpMsg">The message to translate.</param>
        /// <returns>
        /// <see langword="true"/> if the message was translated, <see langword="false"/> otherwise.
        /// </returns>
        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool TranslateMessage(ref Message lpMsg);

        /// <summary>
        /// Dispatches a message to a window procedure.
        /// </summary>
        /// <param name="lpMsg">The message to dispatch.</param>
        /// <returns>A pointer which contains a status.</returns>
        [LibraryImport("user32.dll", EntryPoint = "DispatchMessageW")]
        internal static partial nint DispatchMessage(ref Message lpMsg);

        /// <summary>
        /// Explicitly quits the message loop.
        /// </summary>
        /// <param name="nExitCode"></param>
        [LibraryImport("user32.dll")]
        internal static partial void PostQuitMessage(int nExitCode);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool BeginPaint(nint hWnd, out Paint lpPaint);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool EndPaint(nint hWnd, ref Paint lpPaint);

        /// <summary>
        /// Sets information about the specified window. Used with GWLP_USERDATA to get a pointer (the GCHandle).
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="nIndex">The zero-based offset to the value to be retrieved (e.g., GWLP_USERDATA).</param>
        /// <param name="dwNewLong">The new information to set about the specified window.</param>
        /// <returns>The requested value.</returns>
        [LibraryImport("user32.dll", EntryPoint = "SetWindowLongPtrW", SetLastError = true)]
        internal static partial nint SetWindowLongPtr(nint hWnd, int nIndex, nint dwNewLong);

        /// <summary>
        /// Retrieves information about the specified window. Used with GWLP_USERDATA to retrieve a pointer (the GCHandle).
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="nIndex">The zero-based offset to the value to be retrieved (e.g., GWLP_USERDATA).</param>
        /// <returns>The requested value.</returns>
        [LibraryImport("user32.dll", EntryPoint = "GetWindowLongPtrW", SetLastError = true)]
        internal static partial nint GetWindowLongPtr(nint hWnd, int nIndex);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool SetWindowPos(nint hWnd, nint hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool SetLayeredWindowAttributes(nint hwnd, uint crKey, byte bAlpha, uint dwFlags);

        [LibraryImport("user32.dll", SetLastError = true)]
        internal static partial int SetWindowCompositionAttribute(nint hWnd, ref WindowCompositionAttributeData data);

        [LibraryImport("user32.dll", EntryPoint = "SetWindowTextW", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
        internal static partial int SetWindowText(nint hWnd, string lpString);

        /// <summary>
        /// Sets class information about the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="nIndex">The zero-based offset to the value to be retrieved (e.g., GWLP_USERDATA).</param>
        /// <param name="dwNewLong">The new class information to set about the specified window.</param>
        /// <returns>The requested value.</returns>
        [LibraryImport("user32.dll", EntryPoint = "SetClassLongPtrW", SetLastError = true)]
        internal static partial nint SetClassLongPtr(nint hWnd, int nIndex, nint dwNewLong);
    }

    /// <summary>
    /// A <see langword="static"/> collection of <c>dwmapi.dll</c> compile-time platform invoke definitions.
    /// </summary>
    internal static partial class Dwm
    {
        [LibraryImport("dwmapi.dll", SetLastError = true)]
        internal static partial int DwmSetWindowAttribute(nint hwnd, int dwAttribute, ref int pvAttribute, int cbAttribute);

        [LibraryImport("dwmapi.dll", SetLastError = true)]
        internal static partial int DwmEnableBlurBehindWindow(nint hWnd, ref BlurBehind pBlurBehind);

        [LibraryImport("dwmapi.dll", SetLastError = true)]
        internal static partial int DwmExtendFrameIntoClientArea(nint hWnd, ref Margins pMarInset);
    }

    /// <summary>
    /// A <see langword="static"/> collection of <c>kernel32.dll</c> compile-time platform invoke definitions.
    /// </summary>
    internal static partial class Kernel
    {
        [LibraryImport("kernel32.dll", SetLastError = true)]
        internal static partial uint GetLastError();

        [LibraryImport("kernel32.dll", EntryPoint = "GetModuleHandleW", StringMarshalling = StringMarshalling.Utf16)]
        internal static partial nint GetModuleHandle(string? lpModuleName);
    }

    /// <summary>
    /// A <see langword="static"/> collection of <c>gdi32.dll</c> compile-time platform invoke definitions.
    /// </summary>
    internal static partial class Gdi
    {
        [LibraryImport("gdi32.dll", SetLastError = true)]
        internal static partial nint CreateCompatibleDC(nint hdc);

        [LibraryImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool DeleteDC(nint hdc);

        [LibraryImport("gdi32.dll")]
        internal static partial nint CreateDIBSection(nint hdc, ref BitmapInfo pbmi, uint iUsage, out nint ppvBits, nint hSection, uint dwOffset);

        [LibraryImport("gdi32.dll")]
        internal static partial nint SelectObject(nint hdc, nint hgdiobj);

        [LibraryImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool DeleteObject(nint hObject);

        [LibraryImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool BitBlt(nint hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, nint hdcSrc, int nXSrc, int nYSrc, uint dwRop);

        /// <summary>
        /// Retrieves a handle to one of the predefined stock pens, brushes, fonts, or palettes.
        /// </summary>
        /// <param name="i">The type of the stock object to be returned.</param>
        /// <returns>A handle to the requested stock object, or NULL on failure.</returns>
        [LibraryImport("gdi32.dll")]
        internal static partial nint GetStockObject(int i);
    }
}
