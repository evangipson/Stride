using System.Runtime.InteropServices;

namespace Stride.Renderer.Models;

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
