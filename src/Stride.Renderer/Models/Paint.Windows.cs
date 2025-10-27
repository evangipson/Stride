using System.Runtime.InteropServices;

namespace Stride.Renderer.Models;

/// <summary>
/// Used to represent necessary information for a window paint message.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct Paint
{
    internal nint HardwareDeviceContext;

    internal int Erase;

    internal int RectangleLeft;

    internal int RectangleTop;

    internal int RectangleRight;

    internal int RectangleBottom;

    internal int Restore;

    internal int Update;

    internal Reserved32Bytes Reserved;
}
