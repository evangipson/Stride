namespace Stride.Abstractions.Models;

/// <summary>
/// An extension of <see cref="Component"/> which represents a Stride application window.
/// </summary>
public class Window : Component
{
    /// <summary>
    /// A flag that, when <see langword="true"/>, enables blur.
    /// </summary>
    public bool? Blur { get; set; }

    /// <summary>
    /// A flag that, when <see langword="true"/>, enables a title bar.
    /// </summary>
    public bool? TitleBar { get; set; }

    /// <summary>
    /// A flag that, when <see langword="true"/>, enables transparency.
    /// </summary>
    public bool? Transparent { get; set; }
}
