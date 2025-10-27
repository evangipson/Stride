using Stride.Core.Builders;
using Stride.Renderer.Services;

// create an 800x600 Stride dark mode application
// with blur, transparency, and a title bar
var app = new ApplicationBuilder()
    .Create("Simple Stride App")
    .WithWindow("Main Window", 800, 600)
    .WithBlur()
    .WithTitleBar()
    .WithDarkMode()
    .WithTransparency()
    .Build();

// use a new application renderer
using var appRenderer = new ApplicationRenderService();

// create a new renderer and render the app
var renderer = new RenderService(appRenderer);
renderer.Render(app);