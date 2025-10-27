namespace Stride.Abstractions.Services;

public interface IStrideService
{
    IStrideService Create(string? appName = null, string? title = null, int? width = null, int? height = null);

    IStrideService WithDarkMode();

    IStrideService WithBlur();

    IStrideService WithTitleBar();

    IStrideService WithTransparency();

    IStrideService Run();
}
