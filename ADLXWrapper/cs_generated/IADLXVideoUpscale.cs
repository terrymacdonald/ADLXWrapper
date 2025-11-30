using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXVideoUpscale : adlx::IADLXInterface")]
public unsafe partial struct IADLXVideoUpscale
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXVideoUpscale* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXVideoUpscale* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXVideoUpscale* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupported(IADLXVideoUpscale* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsEnabled(IADLXVideoUpscale* pThis, [NativeTypeName("adlx_bool *")] bool* isEnabled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSharpnessRange(IADLXVideoUpscale* pThis, ADLX_IntRange* range);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSharpness(IADLXVideoUpscale* pThis, [NativeTypeName("adlx_int *")] int* currentMinRes);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetEnabled(IADLXVideoUpscale* pThis, [NativeTypeName("adlx_bool")] byte enable);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetSharpness(IADLXVideoUpscale* pThis, [NativeTypeName("adlx_int")] int minSharp);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXVideoUpscale* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXVideoUpscale* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXVideoUpscale* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXVideoUpscale* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupported>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* isEnabled)
    {
        fixed (IADLXVideoUpscale* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsEnabled>((IntPtr)(lpVtbl[4]))(pThis, isEnabled);
        }
    }

    public ADLX_RESULT GetSharpnessRange(ADLX_IntRange* range)
    {
        fixed (IADLXVideoUpscale* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSharpnessRange>((IntPtr)(lpVtbl[5]))(pThis, range);
        }
    }

    public ADLX_RESULT GetSharpness([NativeTypeName("adlx_int *")] int* currentMinRes)
    {
        fixed (IADLXVideoUpscale* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSharpness>((IntPtr)(lpVtbl[6]))(pThis, currentMinRes);
        }
    }

    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        fixed (IADLXVideoUpscale* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetEnabled>((IntPtr)(lpVtbl[7]))(pThis, enable);
        }
    }

    public ADLX_RESULT SetSharpness([NativeTypeName("adlx_int")] int minSharp)
    {
        fixed (IADLXVideoUpscale* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetSharpness>((IntPtr)(lpVtbl[8]))(pThis, minSharp);
        }
    }
}
