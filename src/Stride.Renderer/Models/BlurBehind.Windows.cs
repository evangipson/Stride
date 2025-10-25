using System.Runtime.InteropServices;

namespace Stride.Renderer.Models;

[StructLayout(LayoutKind.Sequential)]
public struct BlurBehind(uint flags, bool? enable = null, nint? regionBlur = null, bool? transitionOnMaximized = null)
{
    public uint Flags = flags;

    public bool Enable = enable ?? true;

    public nint RegionBlur = regionBlur ?? nint.Zero;

    public bool TransitionOnMaximized = transitionOnMaximized ?? false;
}
