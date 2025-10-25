namespace Stride.Abstractions.Services;

/// <summary>
/// Responsible for rendering the Stride application window and components.
/// </summary>
public interface IApplicationRenderService
{
    /// <summary>
    /// Sets up the Stride application canvas.
    /// <para>Intended to be called once during application render setup.</para>
    /// </summary>
    /// <param name="width">The width of the Stride application.</param>
    /// <param name="height">The height of the Stride application.</param>
    void SetApplicationCanvas(int width, int height);

    /// <summary>
    /// Paints the Stride application window and components.
    /// <para>Intended to be called during the application render loop.</para>
    /// </summary>
    void PaintApplication();

    /// <summary>
    /// Gets the canvas width.
    /// </summary>
    /// <returns>The Stride application canvas width.</returns>
    int GetCanvasWidth();

    /// <summary>
    /// Gets the canvas height.
    /// </summary>
    /// <returns>The Stride application canvas height.</returns>
    int GetCanvasHeight();
}
