using System.Runtime.InteropServices;

namespace Stride.Renderer.Models;

/// <summary>
/// Represents necessary information for a window messaging system.
/// </summary>
/// <param name="callback">The window callback to execute.</param>
/// <param name="instance">A handle to the window instance.</param>
/// <param name="backgroundBrush">The background brush to use.</param>
/// <param name="className">The class name for the window.</param>
[StructLayout(LayoutKind.Sequential)]
public struct WindowMessenger(nint callback, nint instance, nint backgroundBrush, string className)
{
    /// <summary>
    /// The size of this structure, in bytes.
    /// <para>MUST be set to <c>Marshal.SizeOf{WindowMessenger}()</c>.</para>
    /// </summary>
    public uint Size = (uint)Marshal.SizeOf<WindowMessenger>();

    /// <summary>
    /// Class styles (e.g., CS_OWNDC for custom drawing context)
    /// </summary>
    public uint ClassStyle = 0;

    /// <summary>
    /// A pointer to the Window Procedure (callback function).
    /// </summary>
    public nint Callback = callback;

    /// <summary>
    /// The number of extra bytes to allocate for the class structure. (Usually 0)
    /// </summary>
    public int ExtraClassBytes;

    /// <summary>
    /// The number of extra bytes to allocate for the window instance. (Usually 0)
    /// </summary>
    public int ExtraWindowBytes;

    /// <summary>
    /// A handle to the instance that contains the window procedure. (Usually IntPtr.Zero)
    /// </summary>
    public nint Instance = instance;

    /// <summary>
    /// A handle to the class icon (e.g., LoadIcon(IntPtr.Zero, IDI_APPLICATION)).
    /// </summary>
    public nint Icon = nint.Zero;

    /// <summary>
    /// A handle to the class cursor (e.g., LoadCursor(IntPtr.Zero, IDC_ARROW)).
    /// </summary>
    public nint Cursor = nint.Zero;

    /// <summary>
    /// A handle to the class background brush (e.g., GetStockObject(WHITE_BRUSH)).
    /// </summary>
    public nint BackgroundBrush = backgroundBrush;

    /// <summary>
    /// A null-terminated string that specifies the resource name of the class menu. (Usually null)
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string? MenuName = null;

    /// <summary>
    /// A null-terminated string that names the window class. Used in CreateWindowEx.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string ClassName = className;

    /// <summary>
    /// A handle to a small icon associated with the window class. (For the taskbar)
    /// </summary>
    public nint SmallIcon = nint.Zero;
}
