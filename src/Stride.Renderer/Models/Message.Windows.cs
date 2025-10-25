using System.Runtime.InteropServices;

namespace Stride.Renderer.Models;

/// <summary>
/// Represents a Windows message.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Message
{
    /// <summary>
    /// A handle to the window whose window procedure receives the message.
    /// </summary>
    public nint hwnd;

    /// <summary>
    /// The message identifier (e.g., WM_PAINT, WM_LBUTTONDOWN).
    /// </summary>
    public uint message;

    /// <summary>
    /// Additional message information (depends on the message type).
    /// </summary>
    public nint wParam;

    /// <summary>
    /// Additional message information (depends on the message type).
    /// </summary>
    public nint lParam;

    /// <summary>
    /// The time the message was posted.
    /// </summary>
    public uint time;

    /// <summary>
    /// The cursor position, in screen coordinates, when the message was posted.
    /// </summary>
    public Point pt;

    /// <summary>
    /// Reserved, should be ignored.
    /// </summary>
    public uint lPrivate;
}
