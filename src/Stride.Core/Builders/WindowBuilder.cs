using Stride.Abstractions.Builders;
using Stride.Abstractions.Models;
using Stride.Core.Constants;

namespace Stride.Core.Builders;

/// <inheritdoc cref="IWindowBuilder"/>
public class WindowBuilder : IWindowBuilder
{
    private readonly Window _window = new();

    public IWindowBuilder Create(string? title = null, int? width = null, int? height = null)
    {
        _window.Title = title ?? StrideConstants.DefaultWindowTitle;
        _window.Width = width ?? StrideConstants.DefaultWindowWidth;
        _window.Height = height ?? StrideConstants.DefaultWindowHeight;
        _window.Parent = null;
        return this;
    }

    public IWindowBuilder WithBlur(bool? blur = true)
    {
        _window.Blur = blur;
        return this;
    }

    public IWindowBuilder WithTitleBar(bool? titleBar = true)
    {
        _window.TitleBar = titleBar;
        return this;
    }

    public IWindowBuilder WithTransparency(bool? transparency = true)
    {
        _window.Transparent = transparency;
        return this;
    }

    public IWindowBuilder AddComponent(Component component)
    {
        component.Parent = _window;
        _window.Components.Add(component);
        return this;
    }

    public Window Build() => _window;
}
