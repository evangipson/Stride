# Stride
A cross-platform application framework written in .NET 9 that uses [fast, safe, and predictable source-generated interop code](#how-stride-has-fast-safe-and-predictable-interop) under the hood.

## Getting Started
Download this NuGet package, then create and run an app:

```csharp
using Stride.Core.Builders;
using Stride.Core.Factories;
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
```

This creates an application that looks like this:

![](assets/simple-stride-app.png)

You can also use Stride in a dependency injection application:

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stride.Abstractions.Services;
using Stride.Core.Extensions;
using Stride.Renderer.Extensions;

// create the application builder
var builder = Host.CreateApplicationBuilder(args);

// add the Stride services to dependency injection services container
builder.Services
    .AddStrideCore()
    .AddStrideRenderer();

// get the Stride service
var provider = builder.Services.BuildServiceProvider();
var stride = provider.GetRequiredService<IStrideService>();

// create, customize, and run the Stride app
stride.Create("Dependency Injected Stride App", 800, 600)
    .WithBlur()
    .WithTitleBar()
    .WithDarkMode()
    .WithTransparency()
    .Run();
```

## Support
Stride is supported on the following platforms.

|Platform|Supported|
|--------|:-------:|
|Windows | ✅ |
|MacOs   | ⚠️ |
|Linux   | ⚠️ |

## How Stride Has Fast, Safe, and Predictable Interop
Stride defines a collection of interop platform invoke definitions using the `[LibraryImport]` attribute instead of the `[DllImport]` attribute, to force source generation marshalling instead of relying on runtime marshalling.

This requires all custom `struct` entities to be fully blittable using native types (such as `nint`, `int`, `ushort`, `uint`), and requires some platform invoke definitions to use explicit `EntryPoint` parameters, when appropriate.

### RegisterClassEx example
Let's see a "normal" runtime platform invoke definition for the windows `RegisterClassEx` method:

```csharp
// naughty, naughty- incurs a runtime cost of marshalling the WindowMessenger struct!
[DllImport("user32.dll", SetLastError = true)]
public static extern ushort RegisterClassEx(ref WindowMessenger lpwcx);
```

This relies on a definition of a `WindowMessenger` `struct`:

```csharp
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct WindowMessenger(string? className)
{
    public uint Size = (uint)Marshal.SizeOf<WindowMessenger>();
    public uint ClassStyle;
    public nint Callback;
    public int ExtraClassBytes;
    public int ExtraWindowBytes;
    public nint Instance;
    public nint Icon;
    public nint Cursor;
    public nint BackgroundBrush;
    public nint MenuName;
    // uh oh- string is a non-native type! this member will
    // prevent compile-time source-generated marshalling.
    public string? ClassName = className;
    public nint SmallIcon;
}
```

First, `WindowMessenger` must be made up of only native types. This can be achieved by changing the type of `ClassName` from `string` to an `nint` pointer to the `string`:

```csharp
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct WindowMessenger(nint classNamePointer)
{
    public uint Size = (uint)Marshal.SizeOf<WindowMessenger>();
    public uint ClassStyle;
    public nint Callback;
    public int ExtraClassBytes;
    public int ExtraWindowBytes;
    public nint Instance;
    public nint Icon;
    public nint Cursor;
    public nint BackgroundBrush;
    public nint MenuName;
    // now takes a pointer to a string, because nint is a native type!
    public nint ClassName = classNamePointer;
    public nint SmallIcon;
}
```

When creating the `WindowMessenger`, the `string` must be pinned to get a pointer to it:
```csharp
using System.Runtime.InteropServices;

public class ExampleRenderService
{
    private const string _defaultWindowClassName = "WindowClassName";
    private GCHandle _handle;
    private nint _pointer;

    public void Render()
    {
        // get a pointer to the string
        var str = _defaultWindowClassName;
        _handle = GCHandle.Alloc(str, GCHandleType.Pinned);
        _pointer = Marshal.StringToHGlobalUni(str);

        // give WindowMessenger the pointer
        WindowMessenger messenger = new(_pointer);
    }

    // called later to free all resources when exiting
    private void CleanUp()
    {
        // free the pointer
        if (_pointer != nint.Zero)
        {
            Marshal.FreeHGlobal(_pointer);
            _pointer = nint.Zero;
        }

        // free the handle 
        if (_handle.IsAllocated)
        {
            _handle.Free();
        }
    }
}
```

Now the platform invoke definition can be source-generated at runtime like this:

```csharp
// that's better- now we can achieve source-generated marshalling of
// WindowMessenger during compile-time because it only has native types!
[LibraryImport("user32.dll", EntryPoint = "RegisterClassExW", SetLastError = true)]
public static partial ushort RegisterClassEx(ref WindowMessenger lpwcx);
```