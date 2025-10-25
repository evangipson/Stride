using Stride.Abstractions.Models;

namespace Stride.Core.Models;

public class Window : IWindow
{
    private IList<IComponent>? _components;

    public string? Title { get; set; }

    public int? Width { get; set; }

    public int? Height { get; set; }

    public IList<IComponent> Components
    {
        get => _components ??= [];
        set => _components = value;
    }
}
