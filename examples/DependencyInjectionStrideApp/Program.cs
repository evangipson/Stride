using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stride.Abstractions.Services;
using Stride.Core.Extensions;
using Stride.Renderer.Extensions;

// create the application builder
var builder = Host.CreateApplicationBuilder(args);

// add the Stride services to dependency injection services container
builder.Services.AddStrideCore().AddStrideRenderer();

// get the Stride service
var provider = builder.Services.BuildServiceProvider();
var stride = provider.GetRequiredService<IStrideService>();

// create and run the Stride app
stride.CreateApp("Dependency Injected Stride App", 800, 600)
    .WithBlur()
    .WithTitleBar()
    .WithDarkMode()
    .Run();