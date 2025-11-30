using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUTuningServices1 : adlx::IADLXGPUTuningServices")]
public unsafe partial struct IADLXGPUTuningServices1
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXGPUTuningServices1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXGPUTuningServices1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXGPUTuningServices1* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUTuningChangedHandling(IADLXGPUTuningServices1* pThis, IADLXGPUTuningChangedHandling** ppGPUTuningChangedHandling);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsAtFactory(IADLXGPUTuningServices1* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* isFactory);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _ResetToFactory(IADLXGPUTuningServices1* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedAutoTuning(IADLXGPUTuningServices1* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedPresetTuning(IADLXGPUTuningServices1* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedManualGFXTuning(IADLXGPUTuningServices1* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedManualVRAMTuning(IADLXGPUTuningServices1* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedManualFanTuning(IADLXGPUTuningServices1* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedManualPowerTuning(IADLXGPUTuningServices1* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetAutoTuning(IADLXGPUTuningServices1* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppAutoTuning);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetPresetTuning(IADLXGPUTuningServices1* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppPresetTuning);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetManualGFXTuning(IADLXGPUTuningServices1* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppManualGFXTuning);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetManualVRAMTuning(IADLXGPUTuningServices1* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppManualVRAMTuning);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetManualFanTuning(IADLXGPUTuningServices1* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppManualFanTuning);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetManualPowerTuning(IADLXGPUTuningServices1* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppManualPowerTuning);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSmartAccessMemory(IADLXGPUTuningServices1* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXSmartAccessMemory** ppSmartAccessMemory);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXGPUTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXGPUTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXGPUTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT GetGPUTuningChangedHandling(IADLXGPUTuningChangedHandling** ppGPUTuningChangedHandling)
    {
        fixed (IADLXGPUTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUTuningChangedHandling>((IntPtr)(lpVtbl[3]))(pThis, ppGPUTuningChangedHandling);
        }
    }

    public ADLX_RESULT IsAtFactory([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* isFactory)
    {
        fixed (IADLXGPUTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsAtFactory>((IntPtr)(lpVtbl[4]))(pThis, pGPU, isFactory);
        }
    }

    public ADLX_RESULT ResetToFactory([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU)
    {
        fixed (IADLXGPUTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_ResetToFactory>((IntPtr)(lpVtbl[5]))(pThis, pGPU);
        }
    }

    public ADLX_RESULT IsSupportedAutoTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedAutoTuning>((IntPtr)(lpVtbl[6]))(pThis, pGPU, supported);
        }
    }

    public ADLX_RESULT IsSupportedPresetTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedPresetTuning>((IntPtr)(lpVtbl[7]))(pThis, pGPU, supported);
        }
    }

    public ADLX_RESULT IsSupportedManualGFXTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedManualGFXTuning>((IntPtr)(lpVtbl[8]))(pThis, pGPU, supported);
        }
    }

    public ADLX_RESULT IsSupportedManualVRAMTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedManualVRAMTuning>((IntPtr)(lpVtbl[9]))(pThis, pGPU, supported);
        }
    }

    public ADLX_RESULT IsSupportedManualFanTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedManualFanTuning>((IntPtr)(lpVtbl[10]))(pThis, pGPU, supported);
        }
    }

    public ADLX_RESULT IsSupportedManualPowerTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedManualPowerTuning>((IntPtr)(lpVtbl[11]))(pThis, pGPU, supported);
        }
    }

    public ADLX_RESULT GetAutoTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppAutoTuning)
    {
        fixed (IADLXGPUTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetAutoTuning>((IntPtr)(lpVtbl[12]))(pThis, pGPU, ppAutoTuning);
        }
    }

    public ADLX_RESULT GetPresetTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppPresetTuning)
    {
        fixed (IADLXGPUTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetPresetTuning>((IntPtr)(lpVtbl[13]))(pThis, pGPU, ppPresetTuning);
        }
    }

    public ADLX_RESULT GetManualGFXTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppManualGFXTuning)
    {
        fixed (IADLXGPUTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetManualGFXTuning>((IntPtr)(lpVtbl[14]))(pThis, pGPU, ppManualGFXTuning);
        }
    }

    public ADLX_RESULT GetManualVRAMTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppManualVRAMTuning)
    {
        fixed (IADLXGPUTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetManualVRAMTuning>((IntPtr)(lpVtbl[15]))(pThis, pGPU, ppManualVRAMTuning);
        }
    }

    public ADLX_RESULT GetManualFanTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppManualFanTuning)
    {
        fixed (IADLXGPUTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetManualFanTuning>((IntPtr)(lpVtbl[16]))(pThis, pGPU, ppManualFanTuning);
        }
    }

    public ADLX_RESULT GetManualPowerTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppManualPowerTuning)
    {
        fixed (IADLXGPUTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetManualPowerTuning>((IntPtr)(lpVtbl[17]))(pThis, pGPU, ppManualPowerTuning);
        }
    }

    public ADLX_RESULT GetSmartAccessMemory([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXSmartAccessMemory** ppSmartAccessMemory)
    {
        fixed (IADLXGPUTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSmartAccessMemory>((IntPtr)(lpVtbl[18]))(pThis, pGPU, ppSmartAccessMemory);
        }
    }
}
