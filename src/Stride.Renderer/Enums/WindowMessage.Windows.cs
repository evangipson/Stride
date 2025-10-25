namespace Stride.Renderer.Enums;

/// <summary>
/// A collection of potential operating system window messages.
/// </summary>
internal enum WindowMessage : uint
{
    /// <summary>
    /// Windows operating system message to show a window.
    /// </summary>
    Show = 5,

    /// <summary>
    /// Windows operating system message signal that a window is being closed.
    /// </summary>
    Close = 0x0010,

    /// <summary>
    /// Windows operating system message signal that a window's background is being erased.
    /// </summary>
    EraseBackground = 0x0014,

    /// <summary>
    /// Windows operating system message to signal that a window paint has occured.
    /// </summary>
    Paint = 0x000F,

    /// <summary>
    /// Windows operating system message to signal that window composition has changed.
    /// </summary>
    CompositionChanged = 0x031E,
}
