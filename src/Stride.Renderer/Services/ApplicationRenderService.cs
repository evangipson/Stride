using System.Runtime.InteropServices;
using SkiaSharp;
using Stride.Abstractions.Services;

namespace Stride.Renderer.Services;

/// <inheritdoc cref="IApplicationRenderService"/>
public class ApplicationRenderService : IApplicationRenderService, IDisposable
{
    private bool _disposedValue;

    /// <summary>
    /// A reference to the application's drawing area.
    /// </summary>
    protected SKBitmap? Bitmap;

    /// <summary>
    /// A managed object for <see cref="Bitmap"/>.
    /// </summary>
    protected GCHandle BitmapHandle;

    /// <summary>
    /// A reference to the application's surface.
    /// </summary>
    protected SKSurface? Surface;

    /// <summary>
    /// Populates <see cref="Bitmap"/>, <see cref="BitmapHandle"/>, and <see cref="Surface"/>.
    /// </summary>
    /// <param name="width">The width of the Stride application.</param>
    /// <param name="height">The height of the Stride application.</param>
    public void SetApplicationCanvas(int width, int height)
    {
        // create the SKBitmap and allocate memory
        Bitmap = new SKBitmap(width, height, SKColorType.Bgra8888, SKAlphaType.Premul);

        // pin the bitmap's pixel data in memory (CRITICAL for GDI interop)
        BitmapHandle = GCHandle.Alloc(Bitmap.Pixels, GCHandleType.Pinned);

        // create the SKSurface that wraps the pinned memory
        Surface = SKSurface.Create(Bitmap.Info, BitmapHandle.AddrOfPinnedObject(), Bitmap.RowBytes);
    }

    /// <summary>
    /// Paints the application.
    /// </summary>
    public void PaintApplication()
    {
        Surface?.Canvas.Clear(new SKColor(0x00, 0x00, 0x00, 0x20));

        // drawing code here
        Surface?.Canvas.DrawCircle(100, 100, 50, new SKPaint { Color = SKColors.Blue });

        Surface?.Canvas.Flush();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            // free unmanaged resources (unmanaged objects) and override finalizer
            BitmapHandle.Free();
            Surface?.Dispose();
            Bitmap?.Dispose();

            // set large fields to null
            _disposedValue = true;
        }
    }

    ~ApplicationRenderService()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
