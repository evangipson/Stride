using Microsoft.Extensions.DependencyInjection;
using Stride.Abstractions.Builders;
using Stride.Abstractions.Services;
using Stride.Core.Builders;
using Stride.Core.Services;

namespace Stride.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStrideCore(this IServiceCollection services)
        => services.AddTransient<IApplicationBuilder, ApplicationBuilder>()
            .AddTransient<IWindowBuilder, WindowBuilder>()
            .AddSingleton<IStrideService, StrideService>();
}
