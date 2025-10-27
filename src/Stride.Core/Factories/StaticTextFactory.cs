using Stride.Abstractions.Factories;
using Stride.Abstractions.Models;

namespace Stride.Core.Factories;

public class StaticTextFactory : IStaticTextFactory
{
    public StaticText CreateStaticText(string? content = null, int? size = null, string? title = null, Component? parent = null)
        => new()
        {
            Parent = parent,
            Title = string.IsNullOrWhiteSpace(title)
                ? "Static Text"
                : title,
            Content = content,
            Size = size,
        };
}
