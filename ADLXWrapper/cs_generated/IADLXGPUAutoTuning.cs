using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUAutoTuning : adlx::IADLXInterface")]
public unsafe partial struct IADLXGPUAutoTuning
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXGPUAutoTuning* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXGPUAutoTuning* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXGPUAutoTuning* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedUndervoltGPU(IADLXGPUAutoTuning* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedOverclockGPU(IADLXGPUAutoTuning* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedOverclockVRAM(IADLXGPUAutoTuning* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentUndervoltGPU(IADLXGPUAutoTuning* pThis, [NativeTypeName("adlx_bool *")] bool* isUndervoltGPU);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentOverclockGPU(IADLXGPUAutoTuning* pThis, [NativeTypeName("adlx_bool *")] bool* isOverclockGPU);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentOverclockVRAM(IADLXGPUAutoTuning* pThis, [NativeTypeName("adlx_bool *")] bool* isOverclockVRAM);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _StartUndervoltGPU(IADLXGPUAutoTuning* pThis, [NativeTypeName("adlx::IADLXGPUAutoTuningCompleteListener *")] IADLXGPUAutoTuningCompleteListener* pCompleteListener);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _StartOverclockGPU(IADLXGPUAutoTuning* pThis, [NativeTypeName("adlx::IADLXGPUAutoTuningCompleteListener *")] IADLXGPUAutoTuningCompleteListener* pCompleteListener);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _StartOverclockVRAM(IADLXGPUAutoTuning* pThis, [NativeTypeName("adlx::IADLXGPUAutoTuningCompleteListener *")] IADLXGPUAutoTuningCompleteListener* pCompleteListener);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXGPUAutoTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXGPUAutoTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXGPUAutoTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupportedUndervoltGPU([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUAutoTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedUndervoltGPU>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedOverclockGPU([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUAutoTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedOverclockGPU>((IntPtr)(lpVtbl[4]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedOverclockVRAM([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUAutoTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedOverclockVRAM>((IntPtr)(lpVtbl[5]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsCurrentUndervoltGPU([NativeTypeName("adlx_bool *")] bool* isUndervoltGPU)
    {
        fixed (IADLXGPUAutoTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentUndervoltGPU>((IntPtr)(lpVtbl[6]))(pThis, isUndervoltGPU);
        }
    }

    public ADLX_RESULT IsCurrentOverclockGPU([NativeTypeName("adlx_bool *")] bool* isOverclockGPU)
    {
        fixed (IADLXGPUAutoTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentOverclockGPU>((IntPtr)(lpVtbl[7]))(pThis, isOverclockGPU);
        }
    }

    public ADLX_RESULT IsCurrentOverclockVRAM([NativeTypeName("adlx_bool *")] bool* isOverclockVRAM)
    {
        fixed (IADLXGPUAutoTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentOverclockVRAM>((IntPtr)(lpVtbl[8]))(pThis, isOverclockVRAM);
        }
    }

    public ADLX_RESULT StartUndervoltGPU([NativeTypeName("adlx::IADLXGPUAutoTuningCompleteListener *")] IADLXGPUAutoTuningCompleteListener* pCompleteListener)
    {
        fixed (IADLXGPUAutoTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_StartUndervoltGPU>((IntPtr)(lpVtbl[9]))(pThis, pCompleteListener);
        }
    }

    public ADLX_RESULT StartOverclockGPU([NativeTypeName("adlx::IADLXGPUAutoTuningCompleteListener *")] IADLXGPUAutoTuningCompleteListener* pCompleteListener)
    {
        fixed (IADLXGPUAutoTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_StartOverclockGPU>((IntPtr)(lpVtbl[10]))(pThis, pCompleteListener);
        }
    }

    public ADLX_RESULT StartOverclockVRAM([NativeTypeName("adlx::IADLXGPUAutoTuningCompleteListener *")] IADLXGPUAutoTuningCompleteListener* pCompleteListener)
    {
        fixed (IADLXGPUAutoTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_StartOverclockVRAM>((IntPtr)(lpVtbl[11]))(pThis, pCompleteListener);
        }
    }
}
