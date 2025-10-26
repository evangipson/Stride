using System.Runtime.InteropServices;

namespace Stride.Renderer.Models;

/// <summary>
/// Used to represent necessary information for a window paint message.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Paint
{
    public nint hdc;
    
    public bool fErase;
    
    public Rectangle rcPaint;
    
    public bool fRestore;
    
    public bool fIncUpdate;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] rgbReserved;
}
