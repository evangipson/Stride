namespace Stride.Abstractions.Models;

/// <summary>
/// An extension of <see cref="Component"/> which represents static text.
/// </summary>
public class StaticText : Component
{
    /// <summary>
    /// The displayed content for a static text component.
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// The size of text for a static text component.
    /// </summary>
    public int? Size { get; set; }
}
