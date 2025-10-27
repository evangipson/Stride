using Stride.Abstractions.Models;

namespace Stride.Abstractions.Factories;

public interface IStaticTextFactory
{
    StaticText CreateStaticText(string? content = null, int? size = null, string? title = null, Component? parent = null);
}
