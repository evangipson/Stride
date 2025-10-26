namespace Stride.Abstractions.Models;

public interface IWindow
{
    string? Title { get; set; }

    int? Width { get; set; }

    int? Height { get; set; }

    bool? Blur { get; set; }

    bool? TitleBar { get; set; }

    bool? Transparent { get; set; }

    IList<IComponent> Components { get; set; }
}
