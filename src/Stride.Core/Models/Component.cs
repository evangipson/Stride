using Stride.Abstractions.Models;

namespace Stride.Core.Models;

public class Component : IComponent
{
    private IList<IComponent>? _components;

    public int? Width { get; set; }

    public int? Height { get; set; }

    public IComponent? Parent { get; set; }

    public IList<IComponent> Components
    {
        get => _components ??= [];
        set => _components = value;
    }
}
