using System.Runtime.InteropServices;

namespace Stride.Renderer.Models;

/// <summary>
/// Represents necessary information when manually blurring a window.
/// </summary>
/// <param name="flags">One or many bitwise OR flags that determine what kind of blur will be used.</param>
/// <param name="enable">An optional flag to enable the blur.</param>
/// <param name="regionBlur">An optional handle that determines the blur region, defaults to <see langword="null"/>.</param>
/// <param name="transitionOnMaximized">An optional flag that, when <see langword="true"/>, will transition the blur when a window is maximized.</param>
[StructLayout(LayoutKind.Sequential)]
public struct BlurBehind(uint flags, bool? enable = null, nint? regionBlur = null, bool? transitionOnMaximized = null)
{
    /// <summary>
    /// One or many bitwise OR flags that determine whta kind of blur will be used.
    /// </summary>
    public uint Flags = flags;

    /// <summary>
    /// A flag that, when <see langword="true"/>, registers the window handle to use blur behind.
    /// </summary>
    public bool Enable = enable ?? true;

    /// <summary>
    /// The region within the client area where the blur behind will be applied.
    /// <para>A <see langword="null"/> value will apply the blur behind the entire client area.</para>
    /// </summary>
    public nint? RegionBlur = regionBlur ?? null;

    /// <summary>
    /// A flag that, when <see langword="true"/>, will transition the blur when a window is maximized.
    /// </summary>
    public bool TransitionOnMaximized = transitionOnMaximized ?? false;
}
