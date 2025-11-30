using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLX3DFrameRateTargetControl : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DFrameRateTargetControl
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLX3DFrameRateTargetControl* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLX3DFrameRateTargetControl* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLX3DFrameRateTargetControl* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupported(IADLX3DFrameRateTargetControl* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsEnabled(IADLX3DFrameRateTargetControl* pThis, [NativeTypeName("adlx_bool *")] bool* isEnabled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetFPSRange(IADLX3DFrameRateTargetControl* pThis, ADLX_IntRange* range);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetFPS(IADLX3DFrameRateTargetControl* pThis, [NativeTypeName("adlx_int *")] int* currentFPS);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetEnabled(IADLX3DFrameRateTargetControl* pThis, [NativeTypeName("adlx_bool")] byte enable);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetFPS(IADLX3DFrameRateTargetControl* pThis, [NativeTypeName("adlx_int")] int maxFPS);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLX3DFrameRateTargetControl* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLX3DFrameRateTargetControl* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLX3DFrameRateTargetControl* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLX3DFrameRateTargetControl* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupported>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* isEnabled)
    {
        fixed (IADLX3DFrameRateTargetControl* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsEnabled>((IntPtr)(lpVtbl[4]))(pThis, isEnabled);
        }
    }

    public ADLX_RESULT GetFPSRange(ADLX_IntRange* range)
    {
        fixed (IADLX3DFrameRateTargetControl* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetFPSRange>((IntPtr)(lpVtbl[5]))(pThis, range);
        }
    }

    public ADLX_RESULT GetFPS([NativeTypeName("adlx_int *")] int* currentFPS)
    {
        fixed (IADLX3DFrameRateTargetControl* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetFPS>((IntPtr)(lpVtbl[6]))(pThis, currentFPS);
        }
    }

    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        fixed (IADLX3DFrameRateTargetControl* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetEnabled>((IntPtr)(lpVtbl[7]))(pThis, enable);
        }
    }

    public ADLX_RESULT SetFPS([NativeTypeName("adlx_int")] int maxFPS)
    {
        fixed (IADLX3DFrameRateTargetControl* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetFPS>((IntPtr)(lpVtbl[8]))(pThis, maxFPS);
        }
    }
}
