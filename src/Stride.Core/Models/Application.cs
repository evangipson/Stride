using Stride.Abstractions.Models;

namespace Stride.Core.Models;

public class Application : IApplication
{
    public string? Name { get; set; }

    public bool? DarkMode { get; set; }

    public IWindow? Window { get; set; }
}
