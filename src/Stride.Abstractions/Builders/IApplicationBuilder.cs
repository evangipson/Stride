using Stride.Abstractions.Models;

namespace Stride.Abstractions.Builders;

/// <summary>
/// Responsible for building a Stride application.
/// </summary>
public interface IApplicationBuilder
{
    /// <summary>
    /// Creates an <see cref="IApplicationBuilder"/>.
    /// <para>Must be called before calling any other methods.</para>
    /// </summary>
    /// <param name="name">The name of the Stride application.</param>
    /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
    IApplicationBuilder Create(string? name = null);

    /// <summary>
    /// Creates a window for a Stride application.
    /// <para>Must be called before calling any other window customizations.</para>
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="height">The height of the window.</param>
    /// <param name="width">The width of the window.</param>
    /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
    IApplicationBuilder WithWindow(string? title = null, int? width = null, int? height = null);

    /// <summary>
    /// Sets the dark mode option for a Stride application.
    /// </summary>
    /// <param name="darkMode">
    /// A flag that, when <see langword="true"/>, enables dark mode.
    /// Defaults to <see langword="true"/>.
    /// </param>
    /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
    IApplicationBuilder WithDarkMode(bool? darkMode = true);

    /// <summary>
    /// Sets the blur option for a Stride application window.
    /// </summary>
    /// <param name="blur">
    /// A flag that, when <see langword="true"/>, enables blur. Defaults
    /// to <see langword="true"/>.
    /// </param>
    /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
    IApplicationBuilder WithBlur(bool? blur = true);

    /// <summary>
    /// Sets the transparency option for a Stride application window.
    /// </summary>
    /// <param name="transparency">
    /// A flag that, when <see langword="true"/>, enables transparency.
    /// Defaults to <see langword="true"/>.
    /// </param>
    /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
    IApplicationBuilder WithTransparency(bool? transparency = true);

    /// <summary>
    /// Sets the title bar option for a Stride application window.
    /// </summary>
    /// <param name="titleBar">
    /// A flag that, when <see langword="true"/>, enables the title bar.
    /// Defaults to <see langword="true"/>.
    /// </param>
    /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
    IApplicationBuilder WithTitleBar(bool? titleBar = true);

    /// <summary>
    /// Builds the application with all customizations applied.
    /// </summary>
    /// <returns>The Stride application.</returns>
    IApplication Build();
}
