using System.Runtime.InteropServices;

namespace Stride.Renderer.Models;

/// <summary>
/// GDI Structures needed for DIB (Device Independent Bitmap).
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct BitmapInfoHeader(int width, int height, ushort? planes = null, ushort? bitCount = null, uint? compression = null, uint? imageSize = null)
{
    public uint Size = (uint)Marshal.SizeOf<BitmapInfoHeader>();

    public int Width = width;

    public int Height = height;

    public ushort Planes = planes ?? 1;

    public ushort BitCount = bitCount ?? 32;

    public uint Compression = compression ?? 0;

    public uint ImageSize = imageSize ?? 0;

    public int XPixelsPerMeter;

    public int YPixelsPerMeter;

    public uint ColorUsed;

    public uint ColorImportant;
}
