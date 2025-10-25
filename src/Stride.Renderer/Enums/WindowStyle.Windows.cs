namespace Stride.Renderer.Enums;

/// <summary>
/// A collection of potential styles for windows.
/// </summary>
internal enum WindowStyle : uint
{
    Caption = 0x00C00000,
    SystemMenu = 0x00080000,
    ThickFrame = 0x00040000,
    MinimizeBox = 0x00020000,
    MaximizeBox = 0x00010000,
}
