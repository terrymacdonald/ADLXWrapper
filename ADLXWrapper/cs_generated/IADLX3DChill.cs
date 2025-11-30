using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLX3DChill : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DChill
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLX3DChill* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLX3DChill* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLX3DChill* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupported(IADLX3DChill* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsEnabled(IADLX3DChill* pThis, [NativeTypeName("adlx_bool *")] bool* isEnabled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetFPSRange(IADLX3DChill* pThis, ADLX_IntRange* range);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMinFPS(IADLX3DChill* pThis, [NativeTypeName("adlx_int *")] int* currentMinFPS);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMaxFPS(IADLX3DChill* pThis, [NativeTypeName("adlx_int *")] int* currentMaxFPS);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetEnabled(IADLX3DChill* pThis, [NativeTypeName("adlx_bool")] byte enable);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetMinFPS(IADLX3DChill* pThis, [NativeTypeName("adlx_int")] int minFPS);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetMaxFPS(IADLX3DChill* pThis, [NativeTypeName("adlx_int")] int maxFPS);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLX3DChill* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLX3DChill* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLX3DChill* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLX3DChill* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupported>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* isEnabled)
    {
        fixed (IADLX3DChill* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsEnabled>((IntPtr)(lpVtbl[4]))(pThis, isEnabled);
        }
    }

    public ADLX_RESULT GetFPSRange(ADLX_IntRange* range)
    {
        fixed (IADLX3DChill* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetFPSRange>((IntPtr)(lpVtbl[5]))(pThis, range);
        }
    }

    public ADLX_RESULT GetMinFPS([NativeTypeName("adlx_int *")] int* currentMinFPS)
    {
        fixed (IADLX3DChill* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMinFPS>((IntPtr)(lpVtbl[6]))(pThis, currentMinFPS);
        }
    }

    public ADLX_RESULT GetMaxFPS([NativeTypeName("adlx_int *")] int* currentMaxFPS)
    {
        fixed (IADLX3DChill* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMaxFPS>((IntPtr)(lpVtbl[7]))(pThis, currentMaxFPS);
        }
    }

    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        fixed (IADLX3DChill* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetEnabled>((IntPtr)(lpVtbl[8]))(pThis, enable);
        }
    }

    public ADLX_RESULT SetMinFPS([NativeTypeName("adlx_int")] int minFPS)
    {
        fixed (IADLX3DChill* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetMinFPS>((IntPtr)(lpVtbl[9]))(pThis, minFPS);
        }
    }

    public ADLX_RESULT SetMaxFPS([NativeTypeName("adlx_int")] int maxFPS)
    {
        fixed (IADLX3DChill* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetMaxFPS>((IntPtr)(lpVtbl[10]))(pThis, maxFPS);
        }
    }
}
