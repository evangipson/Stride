using Stride.Abstractions.Builders;
using Stride.Abstractions.Factories;
using Stride.Abstractions.Models;
using Stride.Abstractions.Services;

namespace Stride.Core.Services;

public class StrideService(IApplicationFactory applicationFactory,
    IWindowBuilder windowBuilder,
    IApplicationRenderService applicationRenderService,
    IRenderService renderService) : IStrideService
{
    private readonly IApplicationFactory _applicationFactory = applicationFactory ?? throw new ArgumentNullException(nameof(applicationFactory));
    private readonly IWindowBuilder _windowBuilder = windowBuilder ?? throw new ArgumentNullException(nameof(windowBuilder));
    private readonly IApplicationRenderService _applicationRenderService = applicationRenderService ?? throw new ArgumentNullException(nameof(_applicationRenderService));
    private readonly IRenderService _renderService = renderService ?? throw new ArgumentNullException(nameof(renderService));

    private IApplication? _application;

    public IStrideService CreateApp(string appName, int width, int height)
    {
        var window = _windowBuilder.Create("Main Window", height, width).Build();
        _application = _applicationFactory.CreateApplication(appName, window);

        return this;
    }

    public IStrideService WithDarkMode()
    {
        if (_application?.Window == null)
        {
            throw new ApplicationException("You must call CreateApp() before attempting to set Stride application options.");
        }

        _application.DarkMode = true;
        return this;
    }

    public IStrideService WithBlur()
    {
        if (_application?.Window == null)
        {
            throw new ApplicationException("You must call CreateApp() before attempting to set Stride application options.");
        }

        _application.Window.Blur = true;
        return this;
    }

    public IStrideService WithTitleBar()
    {
        if (_application?.Window == null)
        {
            throw new ApplicationException("You must call CreateApp() before attempting to set Stride application options.");
        }

        _application.Window.TitleBar = true;
        return this;
    }

    public IStrideService Run()
    {
        if (_application?.Window == null)
        {
            throw new ApplicationException("You must call CreateApp() before attempting to run the Stride application.");
        }

        _renderService.Render(_application);

        return this;
    }
}
