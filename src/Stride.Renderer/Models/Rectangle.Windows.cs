using System.Runtime.InteropServices;

namespace Stride.Renderer.Models;

[StructLayout(LayoutKind.Sequential)]
public struct Rectangle
{
    public int left;
    
    public int top;
    
    public int right;
    
    public int bottom;
}
