using Stride.Core.Builders;
using Stride.Core.Factories;
using Stride.Renderer.Services;

var window = new WindowBuilder().Create("Main Window", 800, 600).Build();

var strideApp = new ApplicationFactory().CreateApplication("Simple Stride Application", window);

using var renderer = new RenderService();
renderer.Render(strideApp);