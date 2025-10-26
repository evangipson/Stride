using Stride.Core.Builders;
using Stride.Core.Factories;
using Stride.Renderer.Services;

// create the window
var window = new WindowBuilder().Create("Main Window", 800, 600, blur: true, titleBar: true, transparent: true).Build();

// create the application
var app = new ApplicationFactory().CreateApplication("Simple Stride App", window, darkMode: true);

// use a new application renderer
using var appRenderer = new ApplicationRenderService();

// create a new renderer and render the app
var renderer = new RenderService(appRenderer);
renderer.Render(app);