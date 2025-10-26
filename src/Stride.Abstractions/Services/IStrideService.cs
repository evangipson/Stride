namespace Stride.Abstractions.Services;

public interface IStrideService
{
    IStrideService CreateApp(string appName, int width, int height);

    IStrideService WithDarkMode();

    IStrideService WithBlur();

    IStrideService WithTitleBar();

    IStrideService WithTransparency();

    IStrideService Run();
}
