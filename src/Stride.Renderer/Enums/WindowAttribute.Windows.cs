namespace Stride.Renderer.Enums;

/// <summary>
/// A collection of potential options used to set window attributes.
/// <para>
/// Used by the <c>DwmGetWindowAttribute()</c> and <c>DwmSetWindowAttribute()</c>
/// methods.
/// </para>
/// </summary>
internal enum WindowAttribute
{
    /// <summary>
    /// Discovers whether non-client rendering is enabled.
    /// </summary>
    NonClientRenderingEnabled,

    /// <summary>
    /// Sets the non-client rendering policy.
    /// </summary>
    NonClientRenderingPolicy,

    /// <summary>
    /// Enables or forcibly disables DWM transitions.
    /// </summary>
    ToggleTransitions,

    /// <summary>
    /// Enables content rendered in the non-client area to be visible on the frame.
    /// </summary>
    AllowNonClientPaint,

    /// <summary>
    /// Retrieves the bounds of the caption button area in the window-relative space.
    /// </summary>
    GetCaptionButtonArea,

    /// <summary>
    /// Specifies whether non-client content is right-to-left (RTL) mirrored.
    /// </summary>
    NonClientContentRightToLeft,

    /// <summary>
    /// Forces the window to display an iconic thumbnail or peek representation (a static bitmap),
    /// even if a live or snapshot representation of the window is available.
    /// <para>
    /// This value is normally set during a window's creation, and not changed throughout the
    /// window's lifetime.
    /// </para>
    /// </summary>
    ForceThumbnailPeek,

    /// <summary>
    /// Sets how Flip3D treats the window.
    /// </summary>
    SetFlip3d,

    /// <summary>
    /// Retrieves the extended frame bounds rectangle in screen space.
    /// </summary>
    GetExtendedFrameArea,

    /// <summary>
    /// The window will provide a bitmap for use as an iconic thumbnail or peek representation
    /// (a static bitmap) for the window.
    /// <para>
    /// Can be specified with <see cref="ForceThumbnailPeek"/>.
    /// </para>
    /// <para>
    /// This value is normally set during a window's creation and not changed throughout the
    /// window's lifetime.
    /// </para>
    /// <para>Only supported on Windows 7+.</para>
    /// </summary>
    HasPeekBitmap,

    /// <summary>
    /// Do not show peek preview for the window. The peek view shows a full-sized preview of the
    /// window when the mouse hovers over the window's thumbnail in the taskbar.
    /// <para>Only supported on Windows 7+.</para>
    /// </summary>
    DisallowPeek,

    /// <summary>
    /// Prevents a window from fading to a glass sheet when peek is invoked.
    /// <para>Only supported on Windows 7+.</para>
    /// </summary>
    ExcludePeek,

    /// <summary>
    /// Cloaks the window such that it is not visible to the user.
    /// <para>Only supported on Windows 7+.</para>
    /// </summary>
    Cloak,

    /// <summary>
    /// If the window is cloaked, provides one of the following values explaining why.
    /// <list type="bullet">
    /// <item>
    /// DWM_CLOAKED_APP (<c>0x00000001</c>): The window was cloaked by its owner application.
    /// </item>
    /// <item>
    /// DWM_CLOAKED_SHELL (<c>0x00000002</c>): The window was cloaked by the shell.
    /// </item>
    /// <item>
    /// DWM_CLOAKED_INHERITED (<c>0x00000004</c>): The cloak value was inherited from its
    /// owner window.
    /// </item>
    /// </list>
    /// </summary>
    GetCloakedReason,

    /// <summary>
    /// Freeze the window's thumbnail image with its current visuals. Do no further live updates
    /// on the thumbnail image to match the window's contents.
    /// </summary>
    FreezePeek,

    /// <summary>
    /// Passively updates the window mode.
    /// </summary>
    PassiveUpdateMode,

    /// <summary>
    /// Enables a non-UWP window to use host backdrop brushes.
    /// <para>
    /// If this flag is set, then the application that calls <c>Windows::UI::Composition</c>
    /// APIs can build transparency effects using the host backdrop brush.
    /// </para>
    /// <para>Only supported on Windows 11.</para>
    /// </summary>
    UseHostBackdropBrush,

    /// <summary>
    /// Allows the window frame for this window to be drawn in dark mode colors when the
    /// dark mode system setting is enabled.
    /// <para>Only supported on Windows 11.</para>
    /// </summary>
    UseDarkMode = 20,

    /// <summary>
    /// Specifies the rounded corner preference for a window.
    /// <para>Only supported on Windows 11.</para>
    /// </summary>
    RoundedCorners = 33,

    /// <summary>
    /// Specifies the color of the window border.
    /// <para>
    /// Specifying DWMWA_COLOR_NONE (<c>0xFFFFFFFE</c>) for the color will suppress the
    /// drawing of the window border. This makes it possible to have a rounded window with
    /// no border.
    /// </para>
    /// <para>
    /// Specifying DWMWA_COLOR_DEFAULT (<c>0xFFFFFFFF</c>) for the color will reset the
    /// window back to using the system's default behavior for the border color.
    /// </para>
    /// <para>Only supported on Windows 11.</para>
    /// </summary>
    SetBorderColor,

    /// <summary>
    /// Specifies the color of the title bar.
    /// <para>
    /// Specifying DWMWA_COLOR_DEFAULT (<c>0xFFFFFFFF</c>) for the color will reset the window
    /// back to using the system's default behavior for the caption color.
    /// </para>
    /// <para>Only supported on Windows 11.</para>
    /// </summary>
    SetTitleBarColor,

    /// <summary>
    /// Specifies the color of the title bar text.
    /// <para>
    /// Specifying DWMWA_COLOR_DEFAULT (<c>0xFFFFFFFF</c>) for the color will reset the window
    /// back to using the system's default behavior for the caption color.
    /// </para>
    /// <para>Only supported on Windows 11.</para>
    /// </summary>
    SetTitleBarTextColor,

    /// <summary>
    /// Retrieves the width of the outer border that the DWM would draw around this window.
    /// </summary>
    OuterBorderWidth,

    /// <summary>
    /// Retrieves or specifies the system-drawn backdrop material of a window, including behind
    /// the non-client area.
    /// <para>Only supported on Windows 11.</para>
    /// </summary>
    WindowBackdropMaterial,
}
