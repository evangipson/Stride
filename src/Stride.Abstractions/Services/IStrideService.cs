namespace Stride.Abstractions.Services;

public interface IStrideService
{
    IStrideService CreateApp(string appName, int width, int height);

    IStrideService Run();
}
