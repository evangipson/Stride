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
    public SKBitmap? Bitmap;

    /// <summary>
    /// A managed object for <see cref="Bitmap"/>.
    /// </summary>
    public GCHandle BitmapHandle;

    /// <summary>
    /// A reference to the application's surface.
    /// </summary>
    public SKSurface? Surface;

    public void SetApplicationCanvas(int width, int height)
    {
        // create the SKBitmap and allocate memory
        Bitmap = new SKBitmap(width, height, SKColorType.Bgra8888, SKAlphaType.Premul);

        // pin the bitmap's pixel data in memory (CRITICAL for GDI interop)
        BitmapHandle = GCHandle.Alloc(Bitmap.Pixels, GCHandleType.Pinned);

        // create the SKSurface that wraps the pinned memory
        Surface = SKSurface.Create(Bitmap.Info, BitmapHandle.AddrOfPinnedObject(), Bitmap.RowBytes);
    }

    public void PaintApplication()
    {
        Console.WriteLine($"{nameof(PaintApplication)} starting.");
        Surface?.Canvas.Clear(new SKColor(230, 230, 230, 255));

        // drawing code here
        Surface?.Canvas.DrawCircle(100, 100, 50, new SKPaint { Color = SKColors.Blue });

        Surface?.Canvas.Flush();
        Console.WriteLine($"{nameof(PaintApplication)} ending.");
    }

    public int GetCanvasWidth() => Bitmap?.Width ?? 0;

    public int GetCanvasHeight() => Bitmap?.Height ?? 0;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // dispose managed state (managed objects)
            }

            // free unmanaged resources (unmanaged objects) and override finalizer
            BitmapHandle.Free();
            Surface?.Dispose();
            Bitmap?.Dispose();

            // set large fields to null
            Bitmap = null;
            Surface = null;
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
