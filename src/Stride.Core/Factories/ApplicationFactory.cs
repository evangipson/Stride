using Stride.Abstractions.Factories;
using Stride.Abstractions.Models;
using Stride.Core.Models;

namespace Stride.Core.Factories;

public class ApplicationFactory : IApplicationFactory
{
    public IApplication CreateApplication(string name, IWindow window)
        => new Application()
        {
            Name = name,
            Window = window
        };
}
