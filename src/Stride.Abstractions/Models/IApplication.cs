namespace Stride.Abstractions.Models;

public interface IApplication
{
    string? Name { get; set; }

    bool? DarkMode { get; set; }

    IWindow? Window { get; set; }
}
