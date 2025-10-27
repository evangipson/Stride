namespace Stride.Renderer.Enums;

/// <summary>
/// A collection of potential extended styles for windows.
/// </summary>
internal enum ExtendedWindowStyle : uint
{
    /// <summary>
    /// The window has generic left-aligned properties.
    /// </summary>
    Default = 0x00000000,

    /// <summary>
    /// The window accepts drag-drop files.
    /// </summary>
    AcceptFiles = 0x00000010,

    /// <summary>
    /// Forces a top-level window onto the taskbar when the window is visible.
    /// </summary>
    AppWindow = 0x00040000,

    /// <summary>
    /// The window has a border with a sunken edge.
    /// </summary>
    ClientEdge = 0x00000200,

    /// <summary>
    /// Paints all descendants of a window in bottom-to-top painting order using double-buffering.
    /// <para>
    /// Bottom-to-top painting order allows a descendent window to have translucency (alpha) and
    /// transparency (color-key) effects, but only if the descendent window also has the
    /// <see cref="Transparent"/> bit set.
    /// </para>
    /// </summary>
    Composited = 0x02000000,

    /// <summary>
    /// The title bar of the window includes a question mark.
    /// <para>
    /// When the user clicks the question mark, the cursor changes to a question mark with a pointer.
    /// </para>
    /// </summary>
    ContextHelp = 0x00000400,

    /// <summary>
    /// The window itself contains child windows that should take part in dialog box navigation.
    /// <para>
    /// If this style is specified, the dialog manager recurses into children of this window when
    /// performing navigation operations such as handling the TAB key, an arrow key, or a keyboard
    /// mnemonic.
    /// </para>
    /// </summary>
    ControlParent = 0x00010000,

    /// <summary>
    /// The window is a layered window.
    /// </summary>
    Layered = 0x00080000,

    /// <summary>
    /// The window should not be painted until siblings beneath the window (that were created by
    /// the same thread) have been painted.
    /// <para>
    /// The window appears transparent because the bits of underlying sibling windows have already
    /// been painted.
    /// </para>
    /// </summary>
    Transparent = 0x00000020,
}
