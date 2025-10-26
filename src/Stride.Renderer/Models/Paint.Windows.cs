using System.Runtime.InteropServices;

namespace Stride.Renderer.Models;

/// <summary>
/// Used to represent necessary information for a window paint message.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Paint
{
    public nint HardwareDeviceContext;
    
    public bool Erase;
    
    public Rectangle Rectangle;
    
    public bool Restore;
    
    public bool Update;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] Reserved;
}
