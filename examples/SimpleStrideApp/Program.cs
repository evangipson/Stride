using Stride.Core.Builders;
using Stride.Core.Factories;
using Stride.Renderer.Services;

var window = new WindowBuilder().Create("Main Window", 800, 600, blur: true, titleBar: false).Build();

var strideApp = new ApplicationFactory().CreateApplication("Simple Stride App", window, darkMode: false);

using var appRenderer = new ApplicationRenderService();
var renderer = new RenderService(appRenderer);
renderer.Render(strideApp);