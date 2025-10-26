using System.Runtime.InteropServices;

namespace Stride.Renderer.Models;

/// <summary>
/// Represents an (x, y) coordinate.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Point
{
    /// <summary>
    /// The X coordinate of a point on the screen.
    /// </summary>
    public int X;

    /// <summary>
    /// The Y coordinate of a point on the screen.
    /// </summary>
    public int Y;
}
