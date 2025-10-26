using System.Runtime.InteropServices;

namespace Stride.Renderer.Models;

/// <summary>
/// Represents an area on the screen.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Rectangle
{
    /// <summary>
    /// The left point of the rectangle.
    /// </summary>
    public int Left;
    
    /// <summary>
    /// The top point of the rectangle.
    /// </summary>
    public int Top;
    
    /// <summary>
    /// The right point of the rectangle.
    /// </summary>
    public int Right;
    
    /// <summary>
    /// The bottom point of the rectangle.
    /// </summary>
    public int Bottom;
}
