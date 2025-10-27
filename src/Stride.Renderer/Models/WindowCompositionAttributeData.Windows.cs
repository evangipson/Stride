using System.Runtime.InteropServices;

namespace Stride.Renderer.Models;

[StructLayout(LayoutKind.Sequential)]
internal struct WindowCompositionAttributeData
{
    /// <summary>
    /// An <see cref="AccentPolicy"/> value.
    /// </summary>
    internal int Attribute;
    
    /// <summary>
    /// Pointer to the <see cref="AccentPolicy"/> <see langword="struct"/>.
    /// </summary>
    internal nint Data;

    internal int SizeOfData;
}
