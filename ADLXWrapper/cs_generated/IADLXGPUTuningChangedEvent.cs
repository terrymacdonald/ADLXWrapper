using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUTuningChangedEvent : adlx::IADLXChangedEvent")]
public unsafe partial struct IADLXGPUTuningChangedEvent
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXGPUTuningChangedEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXGPUTuningChangedEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXGPUTuningChangedEvent* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_SYNC_ORIGIN _GetOrigin(IADLXGPUTuningChangedEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPU(IADLXGPUTuningChangedEvent* pThis, IADLXGPU** ppGPU);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsAutomaticTuningChanged(IADLXGPUTuningChangedEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsPresetTuningChanged(IADLXGPUTuningChangedEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsManualGPUCLKTuningChanged(IADLXGPUTuningChangedEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsManualVRAMTuningChanged(IADLXGPUTuningChangedEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsManualFanTuningChanged(IADLXGPUTuningChangedEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsManualPowerTuningChanged(IADLXGPUTuningChangedEvent* pThis);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXGPUTuningChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXGPUTuningChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXGPUTuningChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        fixed (IADLXGPUTuningChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetOrigin>((IntPtr)(lpVtbl[3]))(pThis);
        }
    }

    public ADLX_RESULT GetGPU(IADLXGPU** ppGPU)
    {
        fixed (IADLXGPUTuningChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPU>((IntPtr)(lpVtbl[4]))(pThis, ppGPU);
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsAutomaticTuningChanged()
    {
        fixed (IADLXGPUTuningChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsAutomaticTuningChanged>((IntPtr)(lpVtbl[5]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsPresetTuningChanged()
    {
        fixed (IADLXGPUTuningChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsPresetTuningChanged>((IntPtr)(lpVtbl[6]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsManualGPUCLKTuningChanged()
    {
        fixed (IADLXGPUTuningChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsManualGPUCLKTuningChanged>((IntPtr)(lpVtbl[7]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsManualVRAMTuningChanged()
    {
        fixed (IADLXGPUTuningChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsManualVRAMTuningChanged>((IntPtr)(lpVtbl[8]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsManualFanTuningChanged()
    {
        fixed (IADLXGPUTuningChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsManualFanTuningChanged>((IntPtr)(lpVtbl[9]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsManualPowerTuningChanged()
    {
        fixed (IADLXGPUTuningChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsManualPowerTuningChanged>((IntPtr)(lpVtbl[10]))(pThis) != 0;
        }
    }
}
