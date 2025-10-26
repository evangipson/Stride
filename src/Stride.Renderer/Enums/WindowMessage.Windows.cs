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

    /// <summary>
    /// Windows operating system message to signal a size calculation message to a window,
    /// even when a window's size is not being changed.
    /// </summary>
    UpdateSize = 0x0020,

    /// <summary>
    /// Windows operating system message to signal retaining a window's current position.
    /// </summary>
    KeepCurrentPosition = 0x0002,

    /// <summary>
    /// Windows operating system message to signal retaining a window's current size.
    /// </summary>
    KeepCurrentSize = 0x0001,

    /// <summary>
    /// Windows operating system message to signal retaining a window's current layer.
    /// </summary>
    KeepCurrentLayer = 0x0004,
}
