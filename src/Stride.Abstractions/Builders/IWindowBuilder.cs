using Stride.Abstractions.Models;

namespace Stride.Abstractions.Builders;

/// <summary>
/// Responsible for building a window for use in a Stride application.
/// </summary>
public interface IWindowBuilder
{
    /// <summary>
    /// Creates an <see cref="IWindowBuilder"/>.
    /// <para>Must be called before calling any other methods.</para>
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="width">The width of the window.</param>
    /// <param name="height">The height of the window.</param>
    /// <returns>The <see cref="IWindowBuilder"/>.</returns>
    IWindowBuilder Create(string? title = null, int? width = null, int? height = null);

    /// <summary>
    /// Sets the blur option for the window.
    /// </summary>
    /// <param name="blur">
    /// A flag that, when <see langword="true"/>, enables blur. Defaults
    /// to <see langword="true"/>.
    /// </param>
    /// <returns>The <see cref="IWindowBuilder"/>.</returns>
    IWindowBuilder WithBlur(bool? blur = true);

    /// <summary>
    /// Sets the title bar option for the window.
    /// </summary>
    /// <param name="titleBar">
    /// A flag that, when <see langword="true"/>, shows a title bar. Defaults
    /// to <see langword="true"/>.
    /// </param>
    /// <returns>The <see cref="IWindowBuilder"/>.</returns>
    IWindowBuilder WithTitleBar(bool? titleBar = true);

    /// <summary>
    /// Sets the transparency for the window.
    /// </summary>
    /// <param name="transparency">
    /// A flag that, when <see langword="true"/>, enables window transparency.
    /// Defaults to <see langword="true"/>.
    /// </param>
    /// <returns>The <see cref="IWindowBuilder"/>.</returns>
    IWindowBuilder WithTransparency(bool? transparency = true);

    /// <summary>
    /// Adds a component to the window.
    /// </summary>
    /// <param name="component">The component to add.</param>
    /// <returns>The <see cref="IWindowBuilder"/>.</returns>
    IWindowBuilder AddComponent(IComponent component);

    /// <summary>
    /// Builds the window with all customizations applied.
    /// </summary>
    /// <returns>The window.</returns>
    IWindow Build();
}
