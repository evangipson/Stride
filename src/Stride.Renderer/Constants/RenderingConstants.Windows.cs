using Stride.Renderer.Enums;

namespace Stride.Renderer.Constants;

/// <summary>
/// A <see langword="static"/> collection of Windows-specific rendering values.
/// </summary>
internal static class RenderingConstants
{
    // Used for SetWindowLongPtr
    internal const int GWL_STYLE = -16;
    internal const int GWL_EXSTYLE = -20;

    /// <summary>
    /// Signals an interactive dark mode.
    /// <para>Windows 10 1809+ only.</para>
    /// </summary>
    internal const int ImmersiveDarkMode = 20;

    /// <summary>
    /// Signals the base "mica" backdrop.
    /// <para>Windows 11 only.</para>
    /// </summary>
    internal const int SystemBackdropType = 38;

    /// <summary>
    /// Creates a window that has Minimize and Maximize buttons, a Title Bar, a System Menu,
    /// and a Thick Frame (resizable borders).
    /// </summary>
    internal const uint BaseWindowStyle = (uint)(WindowStyle.Caption
        | WindowStyle.SystemMenu
        | WindowStyle.ThickFrame
        | WindowStyle.MinimizeBox
        | WindowStyle.MaximizeBox);

    /// <summary>
    /// Styles to remove from the base window before rendering.
    /// </summary>
    internal const uint StylesToRemove = 0;

    /// <summary>
    /// Instructs the operating system to calculate the default position and/or size.
    /// When used for the position (x and y), it cascades the window position slightly
    /// if multiple default windows are opened.
    /// </summary>
    internal const int UseDefaultWindowSize = unchecked((int)0x80000000);

    internal const int DWMWA_MICA_EFFECT = 1029;

    internal const int DWMSBT_TRANSIENTWINDOW = 3;

    /// <summary>
    /// Desktop Window Manager constant for enabling blur composition (often used in older DWM setups)
    /// </summary>
    internal const int DWM_BLURBEHIND = 0x00000001;

    internal const uint SWP_FRAMECHANGED = 0x0020;
    internal const uint SWP_NOMOVE = 0x0002;
    internal const uint SWP_NOSIZE = 0x0001;
    internal const uint SWP_NOZORDER = 0x0004;

    // Common Stock Object Constants
    public const int WHITE_BRUSH = 0;
    public const int LTGRAY_BRUSH = 1;
    public const int GRAY_BRUSH = 2;
    public const int DKGRAY_BRUSH = 3;
    public const int BLACK_BRUSH = 4;
    // Use this for a transparent background brush
    public const int NULL_BRUSH = 5;
    public const int HOLLOW_BRUSH = NULL_BRUSH;
    public const int WHITE_PEN = 6;
    public const int BLACK_PEN = 7;
    public const int NULL_PEN = 8;

    // DWM Blur Behind flags
    internal const uint DWM_BB_ENABLE = 0x00000001;
    internal const uint DWM_BB_BLURREGION = 0x00000002;

    /// <summary>
    /// Mica effect for main windows.
    /// </summary>
    internal const int MicaMainWindowEffect = 1;

    internal const int NonClientRenderingPolicy = 2;

    internal const int NonClientRenderingPolicyEnabled = 2;

    internal const int WindowCornerPreference = 33;

    internal const int WindowCornerPreferenceRound = 2;

    // Flags: LWA_ALPHA (use bAlpha for transparency) or LWA_COLORKEY (use crKey for transparent color)
    internal const uint LWA_ALPHA = 0x00000002;

    internal const uint DIB_RGB_COLORS = 0;

    internal const uint SRCCOPY = 0x00CC0020;
}
