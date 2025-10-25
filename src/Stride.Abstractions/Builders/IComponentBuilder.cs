using Stride.Abstractions.Models;

namespace Stride.Abstractions.Builders;

public interface IComponentBuilder
{
    IComponentBuilder Create(int? height, int? width);

    IComponentBuilder AddComponent(IComponent component);

    IComponent Build();
}
