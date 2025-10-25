namespace Stride.Abstractions.Models;

public interface IComponent
{
    int? Width { get; set; }

    int? Height { get; set; }

    IComponent? Parent { get; set; }

    IList<IComponent> Components { get; set; }
}
