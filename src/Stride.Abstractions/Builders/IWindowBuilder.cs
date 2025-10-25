using Stride.Abstractions.Models;

namespace Stride.Abstractions.Builders;

public interface IWindowBuilder
{
    IWindowBuilder Create(string? title, int? height, int? width);

    IWindowBuilder AddComponent(IComponent component);

    IWindow Build();
}
