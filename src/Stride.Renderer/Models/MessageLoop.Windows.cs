namespace Stride.Renderer.Models;

public static class MessageLoop
{
    public delegate nint WndProc(nint hWnd, uint msg, nint wParam, nint lParam);
}
