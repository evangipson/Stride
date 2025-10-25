namespace Stride.Abstractions.Models;

public interface IApplication
{
    string? Name { get; set; }

    IWindow? Window { get; set; }
}
