using Stride.Abstractions.Builders;
using Stride.Abstractions.Models;
using Stride.Core.Models;

namespace Stride.Core.Builders;

public class WindowBuilder : IWindowBuilder
{
    private readonly Window _window = new();

    public IWindowBuilder Create(string? title, int? width, int? height, bool? blur = false, bool? titleBar = false)
    {
        _window.Title = title;
        _window.Width = width;
        _window.Height = height;
        _window.Blur = blur;
        _window.TitleBar = titleBar;

        return this;
    }

    public IWindowBuilder AddComponent(IComponent component)
    {
        _window.Components.Add(component);

        return this;
    }

    public IWindow Build() => _window;
}
