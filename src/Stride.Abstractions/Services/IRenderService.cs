using Stride.Abstractions.Models;

namespace Stride.Abstractions.Services;

/// <summary>
/// Responsible for operating system specific rendering of the main Stride application window.
/// </summary>
public interface IRenderService
{
    /// <summary>
    /// Renders the <paramref name="application"/>.
    /// </summary>
    /// <param name="application">The Stride application to render.</param>
    void Render(Application application);
}
