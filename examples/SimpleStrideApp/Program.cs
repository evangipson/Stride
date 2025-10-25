using Stride.Core.Builders;
using Stride.Core.Factories;
using Stride.Renderer.Services;

var window = new WindowBuilder().Create("Main Window", 800, 600).Build();

var strideApp = new ApplicationFactory().CreateApplication("Simple Stride App", window);

using var appRenderer = new ApplicationRenderService();
var renderer = new RenderService(appRenderer);
renderer.Render(strideApp);