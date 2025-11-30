using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public partial struct IADLXDisplayBlanking
{
}

[NativeTypeName("struct IADLXDisplayBlanking : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayBlanking
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXDisplayBlanking* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXDisplayBlanking* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXDisplayBlanking* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupported(IADLXDisplayBlanking* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentBlanked(IADLXDisplayBlanking* pThis, [NativeTypeName("adlx_bool *")] bool* blanked);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentUnblanked(IADLXDisplayBlanking* pThis, [NativeTypeName("adlx_bool *")] bool* unBlanked);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetBlanked(IADLXDisplayBlanking* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetUnblanked(IADLXDisplayBlanking* pThis);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXDisplayBlanking* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXDisplayBlanking* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXDisplayBlanking* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayBlanking* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupported>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsCurrentBlanked([NativeTypeName("adlx_bool *")] bool* blanked)
    {
        fixed (IADLXDisplayBlanking* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentBlanked>((IntPtr)(lpVtbl[4]))(pThis, blanked);
        }
    }

    public ADLX_RESULT IsCurrentUnblanked([NativeTypeName("adlx_bool *")] bool* unBlanked)
    {
        fixed (IADLXDisplayBlanking* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentUnblanked>((IntPtr)(lpVtbl[5]))(pThis, unBlanked);
        }
    }

    public ADLX_RESULT SetBlanked()
    {
        fixed (IADLXDisplayBlanking* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetBlanked>((IntPtr)(lpVtbl[6]))(pThis);
        }
    }

    public ADLX_RESULT SetUnblanked()
    {
        fixed (IADLXDisplayBlanking* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetUnblanked>((IntPtr)(lpVtbl[7]))(pThis);
        }
    }
}
