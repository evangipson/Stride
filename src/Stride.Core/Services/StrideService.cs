using Stride.Abstractions.Builders;
using Stride.Abstractions.Models;
using Stride.Abstractions.Services;
using Stride.Core.Builders;

namespace Stride.Core.Services;

public class StrideService(IApplicationRenderService applicationRenderService, IRenderService renderService) : IStrideService
{
    private readonly IApplicationRenderService _applicationRenderService = applicationRenderService ?? throw new ArgumentNullException(nameof(_applicationRenderService));
    private readonly IRenderService _renderService = renderService ?? throw new ArgumentNullException(nameof(renderService));

    private IApplicationBuilder? _applicationBuilder;
    private IApplication? _application;

    public IStrideService Create(string? appName = null, string? title = null, int? width = null, int? height = null)
    {
        _applicationBuilder = new ApplicationBuilder()
            .Create(appName)
            .WithWindow(title, width, height);

        return this;
    }

    public IStrideService WithDarkMode()
    {
        if (_applicationBuilder == null)
        {
            throw new ApplicationException("You must call Create() before attempting to set Stride application options.");
        }

        _applicationBuilder.WithDarkMode();
        return this;
    }

    public IStrideService WithBlur()
    {
        if (_applicationBuilder == null)
        {
            throw new ApplicationException("You must call Create() before attempting to set Stride application options.");
        }

        _applicationBuilder.WithBlur();
        return this;
    }

    public IStrideService WithTitleBar()
    {
        if (_applicationBuilder == null)
        {
            throw new ApplicationException("You must call Create() before attempting to set Stride application options.");
        }

        _applicationBuilder.WithTitleBar();
        return this;
    }

    public IStrideService WithTransparency()
    {
        if (_applicationBuilder == null)
        {
            throw new ApplicationException("You must call Create() before attempting to set Stride application options.");
        }

        _applicationBuilder.WithTransparency();
        return this;
    }

    public IStrideService Run()
    {
        if (_applicationBuilder == null)
        {
            throw new ApplicationException("You must call Create() before attempting to run the Stride application.");
        }

        _application = _applicationBuilder.Build();
        _renderService.Render(_application);
        return this;
    }
}
