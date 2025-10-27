using System.Runtime.InteropServices;

namespace Stride.Renderer.Models;

/// <summary>
/// This struct simply reserves 256 bytes of space, making the <see cref="BitmapInfo"/>
/// struct fully blittable without illegal attributes.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 256)]
internal struct Reserved256Bytes;