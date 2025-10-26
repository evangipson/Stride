using System.Runtime.InteropServices;
using Stride.Renderer.Enums;

namespace Stride.Renderer.Models;

[StructLayout(LayoutKind.Sequential)]
internal struct AccentPolicy
{
    internal WindowAccent WindowAccent;

    internal uint AccentFlags;

    /// <summary>
    /// RGB color with alpha bits for the tint.
    /// </summary>
    internal uint GradientColor;

    internal uint AnimationId;
}
