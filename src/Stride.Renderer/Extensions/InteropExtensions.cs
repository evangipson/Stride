using System.Runtime.CompilerServices;
using Stride.Renderer.Constants;

namespace Stride.Renderer.Extensions;

/// <summary>
/// A <see langword="static"/> collection of methods to extend interop methods.
/// </summary>
internal static class InteropExtensions
{
    /// <summary>
    /// Will <see langword="throw"/> an <see cref="ApplicationException"/> if <see cref="Interop.Kernel.GetLastError"/>
    /// returns any non-zero value.
    /// </summary>
    /// <param name="nativeCallReturn">The <see langword="ushort"/> return of an interop method call.</param>
    /// <param name="caller">The calling method's name, defaults to <see langword="null"/>.</param>
    /// <exception cref="ApplicationException"></exception>
    internal static void ThrowOnError(this ushort nativeCallReturn, [CallerMemberName] string? caller = null)
    {
        var nativeErrorCode = Interop.Kernel.GetLastError();
        if (nativeCallReturn == 0 && nativeErrorCode != 0)
        {
            throw new ApplicationException($"{caller} failed, error code: {nativeErrorCode}.");
        }
    }

    /// <summary>
    /// Will <see langword="throw"/> an <see cref="ApplicationException"/> if <see cref="Interop.Kernel.GetLastError"/>
    /// returns any non-zero value.
    /// </summary>
    /// <param name="nativeCallReturn">The <see langword="int"/> return of an interop method call.</param>
    /// <param name="caller">The calling method's name, defaults to <see langword="null"/>.</param>
    /// <exception cref="ApplicationException"></exception>
    internal static void ThrowOnError(this int nativeCallReturn, [CallerMemberName] string? caller = null)
    {
        var nativeErrorCode = Interop.Kernel.GetLastError();
        if (nativeCallReturn == 0 && nativeErrorCode != 0)
        {
            throw new ApplicationException($"{caller} failed, error code: {nativeErrorCode}.");
        }
    }

    /// <summary>
    /// Will <see langword="throw"/> an <see cref="ApplicationException"/> if <see cref="Interop.Kernel.GetLastError"/>
    /// returns any non-zero value.
    /// </summary>
    /// <param name="nativeCallReturn">The <see cref="nint"/> return of an interop method call.</param>
    /// <param name="caller">The calling method's name, defaults to <see langword="null"/>.</param>
    /// <exception cref="ApplicationException"></exception>
    internal static void ThrowOnError(this nint nativeCallReturn, [CallerMemberName] string? caller = null)
    {
        var nativeErrorCode = Interop.Kernel.GetLastError();
        if (nativeCallReturn == 0 && nativeErrorCode != 0)
        {
            throw new ApplicationException($"{caller} failed, error code: {nativeErrorCode}.");
        }
    }
}
