using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUTuningChangedEvent1 : adlx::IADLXGPUTuningChangedEvent")]
public unsafe partial struct IADLXGPUTuningChangedEvent1
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXGPUTuningChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXGPUTuningChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXGPUTuningChangedEvent1* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_SYNC_ORIGIN _GetOrigin(IADLXGPUTuningChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPU(IADLXGPUTuningChangedEvent1* pThis, IADLXGPU** ppGPU);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsAutomaticTuningChanged(IADLXGPUTuningChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsPresetTuningChanged(IADLXGPUTuningChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsManualGPUCLKTuningChanged(IADLXGPUTuningChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsManualVRAMTuningChanged(IADLXGPUTuningChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsManualFanTuningChanged(IADLXGPUTuningChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsManualPowerTuningChanged(IADLXGPUTuningChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsSmartAccessMemoryChanged(IADLXGPUTuningChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSmartAccessMemoryStatus(IADLXGPUTuningChangedEvent1* pThis, [NativeTypeName("adlx_bool *")] bool* pEnabled, [NativeTypeName("adlx_bool *")] bool* pCompleted);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXGPUTuningChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXGPUTuningChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXGPUTuningChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        fixed (IADLXGPUTuningChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetOrigin>((IntPtr)(lpVtbl[3]))(pThis);
        }
    }

    public ADLX_RESULT GetGPU(IADLXGPU** ppGPU)
    {
        fixed (IADLXGPUTuningChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPU>((IntPtr)(lpVtbl[4]))(pThis, ppGPU);
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsAutomaticTuningChanged()
    {
        fixed (IADLXGPUTuningChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsAutomaticTuningChanged>((IntPtr)(lpVtbl[5]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsPresetTuningChanged()
    {
        fixed (IADLXGPUTuningChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsPresetTuningChanged>((IntPtr)(lpVtbl[6]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsManualGPUCLKTuningChanged()
    {
        fixed (IADLXGPUTuningChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsManualGPUCLKTuningChanged>((IntPtr)(lpVtbl[7]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsManualVRAMTuningChanged()
    {
        fixed (IADLXGPUTuningChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsManualVRAMTuningChanged>((IntPtr)(lpVtbl[8]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsManualFanTuningChanged()
    {
        fixed (IADLXGPUTuningChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsManualFanTuningChanged>((IntPtr)(lpVtbl[9]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsManualPowerTuningChanged()
    {
        fixed (IADLXGPUTuningChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsManualPowerTuningChanged>((IntPtr)(lpVtbl[10]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsSmartAccessMemoryChanged()
    {
        fixed (IADLXGPUTuningChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSmartAccessMemoryChanged>((IntPtr)(lpVtbl[11]))(pThis) != 0;
        }
    }

    public ADLX_RESULT GetSmartAccessMemoryStatus([NativeTypeName("adlx_bool *")] bool* pEnabled, [NativeTypeName("adlx_bool *")] bool* pCompleted)
    {
        fixed (IADLXGPUTuningChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSmartAccessMemoryStatus>((IntPtr)(lpVtbl[12]))(pThis, pEnabled, pCompleted);
        }
    }
}
