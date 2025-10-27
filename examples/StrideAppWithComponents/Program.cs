using Stride.Core.Builders;
using Stride.Core.Factories;
using Stride.Renderer.Services;

// create an 800x600 Stride dark mode application
// with blur, transparency, and a title bar
var appBuilder = new ApplicationBuilder()
    .Create("Stride App With Components")
    .WithWindow("Main Window", 800, 600)
    .WithBlur()
    .WithTitleBar()
    .WithDarkMode()
    .WithTransparency();

// create some Stride components
var staticText = new StaticTextFactory()
    .CreateStaticText("Hello from Stride");

// add the components to the application
appBuilder.AddComponent(staticText);

// build the application
var app = appBuilder.Build();

// use a new application renderer
using var appRenderer = new ApplicationRenderService();

// create a new renderer and render the app
var renderer = new RenderService(appRenderer);
renderer.Render(app);