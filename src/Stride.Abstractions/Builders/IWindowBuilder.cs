using Stride.Abstractions.Models;

namespace Stride.Abstractions.Builders;

public interface IWindowBuilder
{
    IWindowBuilder Create(string? title, int? height, int? width, bool? blur = false, bool? titleBar = false, bool? transparent = false);

    IWindowBuilder AddComponent(IComponent component);

    IWindow Build();
}
