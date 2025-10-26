namespace Stride.Renderer.Enums;

/// <summary>
/// A collection of potential styles for windows.
/// </summary>
internal enum WindowStyle : uint
{
    /// <summary>
    /// The window has generic left-aligned properties.
    /// </summary>
    Default = 0x00000000,

    /// <summary>
    /// The window is a layered window.
    /// </summary>
    Layered = 0x00080000,

    /// <summary>
    /// The window has a thin-line border.
    /// </summary>
    ThinBorder = 0x00800000,

    /// <summary>
    /// The window has a title bar (includes the <see cref="ThinBorder"/> style).
    /// </summary>
    TitleBar = 0x00C00000,

    /// <summary>
    /// The window has a horizontal scroll bar.
    /// </summary>
    HorizontalScroll = 0x00100000,

    /// <summary>
    /// The window has a vertical scroll bar.
    /// </summary>
    VerticalScroll = 0x00200000,

    /// <summary>
    /// The window is initially minimized.
    /// </summary>
    Minimized = 0x20000000,

    /// <summary>
    /// The window is initially maximized.
    /// </summary>
    Maximized = 0x01000000,

    /// <summary>
    /// The window has a window menu on its title bar.
    /// <para>The <see cref="TitleBar"/> style must also be specified.</para>
    /// </summary>
    SystemMenu = 0x00080000,

    /// <summary>
    /// The window has a sizing border.
    /// </summary>
    Resizeable = 0x00040000,
    
    /// <summary>
    /// The window has a minimize button.
    /// <para>Must include the <see cref="SystemMenu"/> style.</para>
    /// </summary>
    MinimizeBox = 0x00020000,

    /// <summary>
    /// The window has a maximize button.
    /// <para>Must include the <see cref="SystemMenu"/> style.</para>
    /// </summary>
    MaximizeBox = 0x00010000,
}
