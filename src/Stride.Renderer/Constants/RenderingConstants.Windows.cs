using Stride.Renderer.Enums;

namespace Stride.Renderer.Constants;

/// <summary>
/// A <see langword="static"/> collection of Windows-specific rendering values.
/// </summary>
internal static class RenderingConstants
{
    /// <summary>
    /// The default value to use when updating window style.
    /// </summary>
    internal const int UpdateWindowStyle = -16;

    /// <summary>
    /// Index used with SetWindowLongPtr or GetWindowLongPtr to access the extra 32/64-bit user data
    /// associated with the window.
    /// <para>
    /// Used to store the GCHandle pointer for the managed C# object instance.
    /// </para>
    /// </summary>
    internal const int UserData = -21;

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
    /// A window style that has minimize & maximize buttons, a title bar, and resizable borders.
    /// </summary>
    internal const uint BaseWindowStyle = (uint)(WindowStyle.TitleBar
        | WindowStyle.SystemMenu
        | WindowStyle.Resizeable
        | WindowStyle.MinimizeBox
        | WindowStyle.MaximizeBox);

    /// <summary>
    /// A flag that instructs the operating system to use rounded corners for a window.
    /// </summary>
    internal const int UseRoundedCorners = 2;

    /// <summary>
    /// Styles to remove from the base window before rendering.
    /// </summary>
    internal const uint StylesToRemove = 0;

    /// <summary>
    /// Instructs the operating system to calculate the default position and/or size.
    /// When used for the position (x and y), it cascades the window position slightly
    /// if multiple default windows are opened.
    /// </summary>
    internal const int UseDefaultSize = unchecked((int)0x80000000);

    /// <summary>
    /// A flag that instructs the operating system to apply the "Mica" effect to a window's
    /// non-client area.
    /// <para>Windows 11 only.</para>
    /// </summary>
    internal const int ApplyMicaToNonClientArea = 1029;

    /// <summary>
    /// Draw the backdrop material effect corresponding to a transient window behind the
    /// entire window bounds.
    /// <para>
    /// For Windows 11, this corresponds to Desktop Acrylic, also known as Background Acrylic,
    /// in its brightest variant.
    /// </para>
    /// </summary>
    internal const int DrawBackdropAsTransientWindow = 3;

    /// <summary>
    /// Desktop Window Manager constant for enabling blur composition.
    /// <para>Often used in older DWM setups.</para>
    /// </summary>
    internal const int EnableBlurComposition = 0x00000001;

    /// <summary>
    /// A flag to force a window to draw.
    /// <para>Used in the <c>SetWindowPos()</c> method.</para>
    /// </summary>
    internal const uint ForceWindowFrameDraw = (uint)(WindowMessage.UpdateSize
        | WindowMessage.KeepCurrentSize
        | WindowMessage.KeepCurrentPosition
        | WindowMessage.KeepCurrentLayer);

    /// <summary>
    /// A flag to enable blur behind a window.
    /// </summary>
    internal const uint EnableBlurBehind = 0x00000001;

    /// <summary>
    /// A flag to enable blurring the entire window.
    /// </summary>
    internal const uint BlurBehindEntireWindow = 0x00000002;

    /// <summary>
    /// A rendering flag to use when setting a window attribute that informs the window
    /// to use "Mica" rendering.
    /// <para>Windows 11 only.</para>
    /// </summary>
    internal const int MicaMainWindowEffect = 1;

    /// <summary>
    /// A rendering flag to use when setting a window attribute that informs the window
    /// it will be rendered using a "non-client" policy.
    /// </summary>
    internal const int NonClientRenderingPolicy = 2;

    /// <summary>
    /// A rendering flag to use when setting a window attribute that informs the window
    /// it will have it's corner preferences changed.
    /// </summary>
    internal const int WindowCornerPreference = 33;

    /// <summary>
    /// A rendering flag to use when setting a window corner preference attribute when
    /// rounded corners are desired.
    /// </summary>
    internal const int WindowCornerPreferenceRound = 2;

    /// <summary>
    /// A flag to use when setting alpha values on a layered window attribute.
    /// </summary>
    internal const uint SetLayeredWindowAlpha = 0x00000002;

    /// <summary>
    /// A flag that signals to interpret colors of a device indepdent bitmap (DIB) as RGB.
    /// </summary>
    internal const uint InterpretColorsAsRGB = 0;

    /// <summary>
    /// A raster operation code that instructs <c>BitBlt()</c> to copy the entire raster during blitting.
    /// </summary>
    internal const uint CopyEntireRaster = 0x00CC0020;

    /// <summary>
    /// Represents a "Class already exists" error when trying to register a window class.
    /// </summary>
    internal const int ClassAlreadyExistsError = 1410;
}
