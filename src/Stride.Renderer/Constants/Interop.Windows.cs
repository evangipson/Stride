using System.Runtime.InteropServices;
using Stride.Renderer.Models;

namespace Stride.Renderer.Constants;

/// <summary>
/// A <see langword="static"/> collection of Windows interop P/Invoke definitions.
/// </summary>
internal static class Interop
{
    /// <summary>
    /// A <see langword="static"/> collection of <c>user32.dll</c> P/Invoke definitions.
    /// </summary>
    internal static class User
    {
        [DllImport("user32.dll")]
        internal static extern nint DefWindowProc(nint hWnd, uint msg, nint wParam, nint lParam);

        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(nint hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern ushort RegisterClassEx(ref WindowMessenger lpwcx);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern nint CreateWindowEx(uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y, int nWidth, int nHeight, nint hWndParent, nint hMenu, nint hInstance, nint lpParam);
        
        [DllImport("user32.dll")]
        internal static extern int GetMessage(out Message lpMsg, nint hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        /// <summary>
        /// Translates virtual-key messages into character messages. Required to receive WM_CHAR messages from WM_KEYDOWN.
        /// </summary>
        /// <param name="lpMsg"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern bool TranslateMessage(ref Message lpMsg);

        /// <summary>
        /// Dispatches a message to a window procedure. Calls WindowMessenger.lpfnWndProc.
        /// </summary>
        /// <param name="lpMsg"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern nint DispatchMessage(ref Message lpMsg);

        /// <summary>
        /// Explicitly quits the message loop.
        /// </summary>
        /// <param name="nExitCode"></param>
        [DllImport("user32.dll")]
        internal static extern void PostQuitMessage(int nExitCode);

        [DllImport("user32.dll")]
        internal static extern bool BeginPaint(nint hWnd, out Paint lpPaint);

        [DllImport("user32.dll")]
        internal static extern bool EndPaint(nint hWnd, ref Paint lpPaint);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern nint SetWindowLongPtr(nint hWnd, int nIndex, nint dwNewLong);

        [DllImport("user32.dll")]
        internal static extern bool SetWindowPos(nint hWnd, nint hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        internal static extern bool SetLayeredWindowAttributes(nint hwnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetWindowCompositionAttribute(nint hWnd, ref WindowCompositionAttributeData data);
    }

    /// <summary>
    /// A <see langword="static"/> collection of <c>dwmapi.dll</c> P/Invoke definitions.
    /// </summary>
    internal static class Dwm
    {
        [DllImport("dwmapi.dll")]
        internal static extern int DwmSetWindowAttribute(nint hwnd, int dwAttribute, ref int pvAttribute, int cbAttribute);

        [DllImport("dwmapi.dll", SetLastError = true)]
        internal static extern int DwmEnableBlurBehindWindow(nint hWnd, ref BlurBehind pBlurBehind);
    }

    /// <summary>
    /// A <see langword="static"/> collection of <c>kernel32.dll</c> P/Invoke definitions.
    /// </summary>
    internal static class Kernel
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern uint GetLastError();

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        internal static extern nint GetModuleHandle(string? lpModuleName);
    }

    /// <summary>
    /// A <see langword="static"/> collection of <c>gdi32.dll</c> P/Invoke definitions.
    /// </summary>
    internal static class Gdi
    {
        [DllImport("gdi32.dll", SetLastError = true)]
        internal static extern nint CreateCompatibleDC(nint hdc);

        [DllImport("gdi32.dll")]
        internal static extern bool DeleteDC(nint hdc);

        [DllImport("gdi32.dll")]
        internal static extern nint CreateDIBSection(nint hdc, ref BitmapInfo pbmi, uint iUsage, out nint ppvBits, nint hSection, uint dwOffset);

        [DllImport("gdi32.dll")]
        internal static extern nint SelectObject(nint hdc, nint hgdiobj);

        [DllImport("gdi32.dll")]
        internal static extern bool DeleteObject(nint hObject);

        [DllImport("gdi32.dll")]
        internal static extern bool BitBlt(nint hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, nint hdcSrc, int nXSrc, int nYSrc, uint dwRop);

        /// <summary>
        /// Retrieves a handle to one of the predefined stock pens, brushes, fonts, or palettes.
        /// </summary>
        /// <param name="i">The type of the stock object to be returned.</param>
        /// <returns>A handle to the requested stock object, or NULL on failure.</returns>
        [DllImport("gdi32.dll")]
        public static extern nint GetStockObject(int i);
    }
}
