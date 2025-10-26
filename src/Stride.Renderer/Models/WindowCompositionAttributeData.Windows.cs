using System.Runtime.InteropServices;
using Stride.Renderer.Enums;

namespace Stride.Renderer.Models;

[StructLayout(LayoutKind.Sequential)]
internal struct WindowCompositionAttributeData
{
    internal WindowCompositionAttribute Attribute;
    internal nint Data; // Pointer to the AccentPolicy struct
    internal int SizeOfData;
}
