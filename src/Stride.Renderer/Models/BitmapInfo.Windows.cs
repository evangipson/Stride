using System.Runtime.InteropServices;

namespace Stride.Renderer.Models;

[StructLayout(LayoutKind.Sequential)]
public struct BitmapInfo(int width, int height, ushort? planes = null, ushort? bitCount = null, uint? compression = null, uint? imageSize = null)
{
    public BitmapInfoHeader Header = new(width, height, planes, bitCount, compression, imageSize);

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
    public uint[]? Colors;
}
