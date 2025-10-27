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
    public nint Window;

    /// <summary>
    /// The message identifier (e.g., WM_PAINT, WM_LBUTTONDOWN).
    /// </summary>
    public uint MessageId;

    /// <summary>
    /// Additional message information (depends on the message type).
    /// </summary>
    public nint WidthInfo;

    /// <summary>
    /// Additional message information (depends on the message type).
    /// </summary>
    public nint LengthInfo;

    /// <summary>
    /// The time the message was posted.
    /// </summary>
    public uint Time;

    /// <summary>
    /// The X screen coordinate of the cursor when the message was posted.
    /// </summary>
    public int CursorPositionX;

    /// <summary>
    /// The Y screen coordinate of the cursor when the message was posted.
    /// </summary>
    public int CursorPositionY;

    /// <summary>
    /// Reserved, should be ignored.
    /// </summary>
    public uint Private;
}
