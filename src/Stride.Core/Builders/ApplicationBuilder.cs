using Stride.Abstractions.Builders;
using Stride.Abstractions.Models;
using Stride.Core.Constants;

namespace Stride.Core.Builders;

/// <inheritdoc cref="IApplicationBuilder"/>
public class ApplicationBuilder : IApplicationBuilder
{
    private Application? _application;
    private IWindowBuilder? _windowBuilder;

    public IApplicationBuilder Create(string? name = null)
    {
        _application = new Application();
        _windowBuilder = new WindowBuilder().Create();
        _application.Name = name ?? StrideConstants.DefaultApplicationName;
        return this;
    }

    public IApplicationBuilder WithWindow(string? title = null, int? width = null, int? height = null)
    {
        if (_application == null || _windowBuilder == null)
        {
            throw new ApplicationException("Must call Create() before changing any application properties.");
        }

        _windowBuilder.Create(title, width, height);

        return this;
    }

    public IApplicationBuilder WithDarkMode(bool? darkMode = true)
    {
        if (_application == null)
        {
            throw new ApplicationException("Must call Create() before changing any application properties.");
        }

        _application.DarkMode = darkMode;
        return this;
    }

    public IApplicationBuilder WithBlur(bool? blur = true)
    {
        if (_windowBuilder == null)
        {
            throw new ApplicationException("Must call Create() before changing any application properties.");
        }

        _windowBuilder.WithBlur(blur);
        return this;
    }

    public IApplicationBuilder WithTransparency(bool? transparency = true)
    {
        if (_windowBuilder == null)
        {
            throw new ApplicationException("Must call Create() before changing any application properties.");
        }

        _windowBuilder.WithTransparency(transparency);
        return this;
    }

    public IApplicationBuilder WithTitleBar(bool? titleBar = true)
    {
        if (_windowBuilder == null)
        {
            throw new ApplicationException("Must call Create() before changing any application properties.");
        }

        _windowBuilder.WithTitleBar(titleBar);
        return this;
    }

    public IApplicationBuilder AddComponent(Component component)
    {
        if (_windowBuilder == null)
        {
            throw new ApplicationException("Must call Create() before changing any application properties.");
        }

        _windowBuilder.AddComponent(component);
        return this;
    }

    public Application Build()
    {
        if (_application == null || _windowBuilder == null)
        {
            throw new ApplicationException("Must call Create() before building the application.");
        }

        _application.MainWindow = _windowBuilder.Build();
        return _application;
    }
}
