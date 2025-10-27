using System.Runtime.InteropServices;

namespace Stride.Renderer.Models;

/// <summary>
/// Represents necessary information when manually blurring a window.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct BlurBehind
{
    /// <summary>
    /// One or many bitwise OR flags that determine whta kind of blur will be used.
    /// </summary>
    internal uint Flags;

    /// <summary>
    /// A flag that, when <c>1</c>, registers the window handle to use blur behind.
    /// </summary>
    internal int Enable;

    /// <summary>
    /// The region within the client area where the blur behind will be applied.
    /// <para>A <see cref="nint.Zero"/> value will apply the blur behind the entire client area.</para>
    /// </summary>
    internal nint RegionBlur;

    /// <summary>
    /// A flag that, when <c>1</c>, will transition the blur when a window is maximized.
    /// </summary>
    internal int TransitionOnMaximized;
}
