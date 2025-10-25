using Stride.Abstractions.Models;

namespace Stride.Abstractions.Factories;

public interface IApplicationFactory
{
    IApplication CreateApplication(string name, IWindow window);
}
