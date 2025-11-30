using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUPresetTuning : adlx::IADLXInterface")]
public unsafe partial struct IADLXGPUPresetTuning
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXGPUPresetTuning* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXGPUPresetTuning* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXGPUPresetTuning* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedPowerSaver(IADLXGPUPresetTuning* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedQuiet(IADLXGPUPresetTuning* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedBalanced(IADLXGPUPresetTuning* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedTurbo(IADLXGPUPresetTuning* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedRage(IADLXGPUPresetTuning* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentPowerSaver(IADLXGPUPresetTuning* pThis, [NativeTypeName("adlx_bool *")] bool* isPowerSaver);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentQuiet(IADLXGPUPresetTuning* pThis, [NativeTypeName("adlx_bool *")] bool* isQuiet);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentBalanced(IADLXGPUPresetTuning* pThis, [NativeTypeName("adlx_bool *")] bool* isBalance);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentTurbo(IADLXGPUPresetTuning* pThis, [NativeTypeName("adlx_bool *")] bool* isTurbo);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentRage(IADLXGPUPresetTuning* pThis, [NativeTypeName("adlx_bool *")] bool* isRage);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetPowerSaver(IADLXGPUPresetTuning* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetQuiet(IADLXGPUPresetTuning* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetBalanced(IADLXGPUPresetTuning* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetTurbo(IADLXGPUPresetTuning* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetRage(IADLXGPUPresetTuning* pThis);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXGPUPresetTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXGPUPresetTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXGPUPresetTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupportedPowerSaver([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUPresetTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedPowerSaver>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedQuiet([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUPresetTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedQuiet>((IntPtr)(lpVtbl[4]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedBalanced([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUPresetTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedBalanced>((IntPtr)(lpVtbl[5]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedTurbo([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUPresetTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedTurbo>((IntPtr)(lpVtbl[6]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedRage([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUPresetTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedRage>((IntPtr)(lpVtbl[7]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsCurrentPowerSaver([NativeTypeName("adlx_bool *")] bool* isPowerSaver)
    {
        fixed (IADLXGPUPresetTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentPowerSaver>((IntPtr)(lpVtbl[8]))(pThis, isPowerSaver);
        }
    }

    public ADLX_RESULT IsCurrentQuiet([NativeTypeName("adlx_bool *")] bool* isQuiet)
    {
        fixed (IADLXGPUPresetTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentQuiet>((IntPtr)(lpVtbl[9]))(pThis, isQuiet);
        }
    }

    public ADLX_RESULT IsCurrentBalanced([NativeTypeName("adlx_bool *")] bool* isBalance)
    {
        fixed (IADLXGPUPresetTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentBalanced>((IntPtr)(lpVtbl[10]))(pThis, isBalance);
        }
    }

    public ADLX_RESULT IsCurrentTurbo([NativeTypeName("adlx_bool *")] bool* isTurbo)
    {
        fixed (IADLXGPUPresetTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentTurbo>((IntPtr)(lpVtbl[11]))(pThis, isTurbo);
        }
    }

    public ADLX_RESULT IsCurrentRage([NativeTypeName("adlx_bool *")] bool* isRage)
    {
        fixed (IADLXGPUPresetTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentRage>((IntPtr)(lpVtbl[12]))(pThis, isRage);
        }
    }

    public ADLX_RESULT SetPowerSaver()
    {
        fixed (IADLXGPUPresetTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetPowerSaver>((IntPtr)(lpVtbl[13]))(pThis);
        }
    }

    public ADLX_RESULT SetQuiet()
    {
        fixed (IADLXGPUPresetTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetQuiet>((IntPtr)(lpVtbl[14]))(pThis);
        }
    }

    public ADLX_RESULT SetBalanced()
    {
        fixed (IADLXGPUPresetTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetBalanced>((IntPtr)(lpVtbl[15]))(pThis);
        }
    }

    public ADLX_RESULT SetTurbo()
    {
        fixed (IADLXGPUPresetTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetTurbo>((IntPtr)(lpVtbl[16]))(pThis);
        }
    }

    public ADLX_RESULT SetRage()
    {
        fixed (IADLXGPUPresetTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetRage>((IntPtr)(lpVtbl[17]))(pThis);
        }
    }
}
