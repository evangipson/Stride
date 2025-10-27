namespace Stride.Abstractions.Models;

/// <summary>
/// Represents a Stride application.
/// </summary>
public class Application
{
    /// <summary>
    /// The name of the application, displayed in the title bar.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// A flag that, when <see langword="true"/>, forces dark mode.
    /// </summary>
    public bool? DarkMode { get; set; }

    /// <summary>
    /// The application's main window, where all child components are rendered to.
    /// </summary>
    public Window? MainWindow { get; set; }
}
