namespace Stride.Renderer.Enums;

internal enum WindowAccent
{
    Disabled = 0,
    
    Gradient,
    
    TransparentGradient,
    
    /// <summary>
    /// Enables the traditional Acrylic/Blur.
    /// </summary>
    BlurBehind,
    
    /// <summary>
    /// Enables the modern Acrylic blur
    /// <para>Only supported on Windows 10 1803+.</para>
    /// </summary>
    AcrylicBlurBehind,
    
    Invalid
}
