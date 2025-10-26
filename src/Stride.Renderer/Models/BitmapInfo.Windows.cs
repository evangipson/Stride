using System.Runtime.InteropServices;

namespace Stride.Renderer.Models;

/// <summary>
/// Information about a bitmap, used when blitting a window.
/// </summary>
/// <param name="width">How wide the bitmap is, in pixels.</param>
/// <param name="height">How tall the bitmap is, in pixels.</param>
/// <param name="planes">An optional number of planes the bitmap has, defaults to <c>1</c>.</param>
/// <param name="bitCount">An optional number of bits the bitmap has, defaults to <c>32</c>.</param>
/// <param name="compression">An optional number that represents bitmap compression, defaults to <c>0</c>.</param>
/// <param name="imageSize">An optional total size of the bitmap, defaults to <c>0</c>.</param>
[StructLayout(LayoutKind.Sequential)]
public struct BitmapInfo(int width, int height, ushort? planes = null, ushort? bitCount = null, uint? compression = null, uint? imageSize = null)
{
    /// <summary>
    /// Header information about a bitmap.
    /// </summary>
    public BitmapInfoHeader Header = new(width, height, planes, bitCount, compression, imageSize);

    /// <summary>
    /// The amount of colors a bitmap has.
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
    public uint[]? Colors;
}
