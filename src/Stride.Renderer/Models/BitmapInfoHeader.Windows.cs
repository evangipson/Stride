using System.Runtime.InteropServices;

namespace Stride.Renderer.Models;

/// <summary>
/// GDI structure needed for DIB (Device Independent Bitmap).
/// </summary>
/// <param name="width">How wide the bitmap is, in pixels.</param>
/// <param name="height">How tall the bitmap is, in pixels.</param>
/// <param name="planes">An optional number of planes the bitmap has, defaults to <c>1</c>.</param>
/// <param name="bitCount">An optional number of bits the bitmap has, defaults to <c>32</c>.</param>
/// <param name="compression">An optional number that represents bitmap compression, defaults to <c>0</c>.</param>
/// <param name="imageSize">An optional total size of the bitmap, defaults to <c>0</c>.</param>
[StructLayout(LayoutKind.Sequential)]
public struct BitmapInfoHeader(int width, int height, ushort? planes = null, ushort? bitCount = null, uint? compression = null, uint? imageSize = null)
{
    /// <summary>
    /// The total size of the bitmap info header.
    /// <para>Must be set to <c>Marshal.SizeOf{BitmapInfoHeader}()</c>.</para>
    /// </summary>
    public uint Size = (uint)Marshal.SizeOf<BitmapInfoHeader>();

    /// <summary>
    /// The total width of a bitmap, in pixels.
    /// </summary>
    public int Width = width;

    /// <summary>
    /// The total height of a bitmap, in pixels.
    /// </summary>
    public int Height = height;

    /// <summary>
    /// The number of planes a bitmap has, defaults to <c>1</c>.
    /// </summary>
    public ushort Planes = planes ?? 1;

    /// <summary>
    /// The total number of bits a bitmap has, defaults to <c>32</c>.
    /// </summary>
    public ushort BitCount = bitCount ?? 32;

    /// <summary>
    /// Represents the type of compression a bitmap has, defaults to <c>0</c>.
    /// </summary>
    public uint Compression = compression ?? 0;

    /// <summary>
    /// The total size of a bitmap, defaults to <c>0</c>.
    /// </summary>
    public uint ImageSize = imageSize ?? 0;

    /// <summary>
    /// The total number of pixels per horizontal meter of a bitmap.
    /// </summary>
    public int XPixelsPerMeter;

    /// <summary>
    /// The total number of pixels per vertical meter of a bitmap.
    /// </summary>
    public int YPixelsPerMeter;

    /// <summary>
    /// The total number of colors used in a bitmap.
    /// </summary>
    public uint ColorUsed;

    /// <summary>
    /// The total number of mandatory colors used in a bitmap.
    /// </summary>
    public uint ColorImportant;
}
