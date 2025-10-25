using Stride.Abstractions.Builders;
using Stride.Abstractions.Models;
using Stride.Core.Models;

namespace Stride.Core.Builders;

public class ComponentBuilder : IComponentBuilder
{
    private readonly Component _component;

    private ComponentBuilder()
    {
        _component = new();
    }

    public IComponentBuilder Create(int? height, int? width)
    {
        _component.Height = height;
        _component.Width = width;

        return this;
    }

    public IComponentBuilder AddComponent(IComponent component)
    {
        component.Parent = _component;
        _component.Components.Add(component);

        return this;
    }

    public IComponent Build() => _component;
}
