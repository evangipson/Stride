namespace Stride.Renderer.Models;

/// <summary>
/// A <see langword="static"/> collection of <see langword="delegate"/> for message loops.
/// </summary>
public static class MessageLoop
{
    /// <summary>
    /// A <see langword="delegate"/> for any window procedure call.
    /// </summary>
    /// <param name="hWnd">A window handle or pointer.</param>
    /// <param name="msg">The message to send in the window procedure.</param>
    /// <param name="wParam">Additional width parameter information.</param>
    /// <param name="lParam">Additional height parameter information.</param>
    /// <returns>A handle to the window.</returns>
    public delegate nint WindowProcedure(nint hWnd, uint msg, nint wParam, nint lParam);
}
