using System.Runtime.InteropServices;

namespace Stride.Renderer.Models;

/// <summary>
/// This struct simply reserves 32 bytes of space, making the <see cref="Paint"/>
/// struct fully blittable without illegal attributes.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 32)]
internal struct Reserved32Bytes;
