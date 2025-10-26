using System.Runtime.InteropServices;

namespace Stride.Renderer.Models;

/// <summary>
/// Represents margins for a frame.
/// </summary>
/// <param name="left">The leftmost width of a frame margin.</param>
/// <param name="right">The rightmost width of a frame margin.</param>
/// <param name="top">The top of a frame margin.</param>
/// <param name="bottom">The bottom of a frame margin.</param>
[StructLayout(LayoutKind.Sequential)]
internal struct Margins(int left, int right, int top, int bottom)
{
    /// <summary>
    /// The leftmost width of a frame margin.
    /// </summary>
    internal int LeftWidth = left;

    /// <summary>
    /// The rightmost width of a frame margin.
    /// </summary>
    internal int RightWidth = right;

    /// <summary>
    /// The top of a frame margin.
    /// </summary>
    internal int TopHeight = top;

    /// <summary>
    /// The bottom of a frame margin.
    /// </summary>
    internal int BottomHeight = bottom;
}
