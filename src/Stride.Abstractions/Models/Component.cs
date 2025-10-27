namespace Stride.Abstractions.Models;

/// <summary>
/// An <see langword="abstract"/> base for any Stride component.
/// </summary>
public abstract class Component
{
    private IList<Component>? _components;

    /// <summary>
    /// The identifier of a component.
    /// </summary>
    public virtual Guid Id => Guid.NewGuid();

    /// <summary>
    /// The title of a component.
    /// </summary>
    public virtual string? Title { get; set; }

    /// <summary>
    /// The width of a component, in pixels.
    /// </summary>
    public virtual int? Width { get; set; }

    /// <summary>
    /// The height of a component, in pixels.
    /// </summary>
    public virtual int? Height { get; set; }

    /// <summary>
    /// The parent of a component.
    /// </summary>
    public virtual Component? Parent { get; set; }

    /// <summary>
    /// A list of child components.
    /// </summary>
    public virtual IList<Component> Components
    {
        get => _components ??= [];
        set => _components = value;
    }
}
