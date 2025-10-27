using Microsoft.Extensions.DependencyInjection;
using Stride.Abstractions.Services;
using Stride.Renderer.Services;

namespace Stride.Renderer.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStrideRenderer(this IServiceCollection services)
        => services.AddTransient<IApplicationRenderService, ApplicationRenderService>()
            .AddTransient<IRenderService, RenderService>();
}
