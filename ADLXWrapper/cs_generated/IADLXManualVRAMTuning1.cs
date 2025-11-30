using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXManualVRAMTuning1 : adlx::IADLXInterface")]
public unsafe partial struct IADLXManualVRAMTuning1
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXManualVRAMTuning1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXManualVRAMTuning1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXManualVRAMTuning1* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedMemoryTiming(IADLXManualVRAMTuning1* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSupportedMemoryTimingDescriptionList(IADLXManualVRAMTuning1* pThis, IADLXMemoryTimingDescriptionList** ppDescriptionList);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMemoryTimingDescription(IADLXManualVRAMTuning1* pThis, ADLX_MEMORYTIMING_DESCRIPTION* description);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetMemoryTimingDescription(IADLXManualVRAMTuning1* pThis, ADLX_MEMORYTIMING_DESCRIPTION description);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetVRAMTuningRanges(IADLXManualVRAMTuning1* pThis, ADLX_IntRange* frequencyRange, ADLX_IntRange* voltageRange);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetVRAMTuningStates(IADLXManualVRAMTuning1* pThis, IADLXManualTuningStateList** ppVRAMStates);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetEmptyVRAMTuningStates(IADLXManualVRAMTuning1* pThis, IADLXManualTuningStateList** ppVRAMStates);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsValidVRAMTuningStates(IADLXManualVRAMTuning1* pThis, [NativeTypeName("adlx::IADLXManualTuningStateList *")] IADLXManualTuningStateList* pVRAMStates, [NativeTypeName("adlx_int *")] int* errorIndex);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetVRAMTuningStates(IADLXManualVRAMTuning1* pThis, [NativeTypeName("adlx::IADLXManualTuningStateList *")] IADLXManualTuningStateList* pVRAMStates);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXManualVRAMTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXManualVRAMTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXManualVRAMTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupportedMemoryTiming([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXManualVRAMTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedMemoryTiming>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetSupportedMemoryTimingDescriptionList(IADLXMemoryTimingDescriptionList** ppDescriptionList)
    {
        fixed (IADLXManualVRAMTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSupportedMemoryTimingDescriptionList>((IntPtr)(lpVtbl[4]))(pThis, ppDescriptionList);
        }
    }

    public ADLX_RESULT GetMemoryTimingDescription(ADLX_MEMORYTIMING_DESCRIPTION* description)
    {
        fixed (IADLXManualVRAMTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMemoryTimingDescription>((IntPtr)(lpVtbl[5]))(pThis, description);
        }
    }

    public ADLX_RESULT SetMemoryTimingDescription(ADLX_MEMORYTIMING_DESCRIPTION description)
    {
        fixed (IADLXManualVRAMTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetMemoryTimingDescription>((IntPtr)(lpVtbl[6]))(pThis, description);
        }
    }

    public ADLX_RESULT GetVRAMTuningRanges(ADLX_IntRange* frequencyRange, ADLX_IntRange* voltageRange)
    {
        fixed (IADLXManualVRAMTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetVRAMTuningRanges>((IntPtr)(lpVtbl[7]))(pThis, frequencyRange, voltageRange);
        }
    }

    public ADLX_RESULT GetVRAMTuningStates(IADLXManualTuningStateList** ppVRAMStates)
    {
        fixed (IADLXManualVRAMTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetVRAMTuningStates>((IntPtr)(lpVtbl[8]))(pThis, ppVRAMStates);
        }
    }

    public ADLX_RESULT GetEmptyVRAMTuningStates(IADLXManualTuningStateList** ppVRAMStates)
    {
        fixed (IADLXManualVRAMTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetEmptyVRAMTuningStates>((IntPtr)(lpVtbl[9]))(pThis, ppVRAMStates);
        }
    }

    public ADLX_RESULT IsValidVRAMTuningStates([NativeTypeName("adlx::IADLXManualTuningStateList *")] IADLXManualTuningStateList* pVRAMStates, [NativeTypeName("adlx_int *")] int* errorIndex)
    {
        fixed (IADLXManualVRAMTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsValidVRAMTuningStates>((IntPtr)(lpVtbl[10]))(pThis, pVRAMStates, errorIndex);
        }
    }

    public ADLX_RESULT SetVRAMTuningStates([NativeTypeName("adlx::IADLXManualTuningStateList *")] IADLXManualTuningStateList* pVRAMStates)
    {
        fixed (IADLXManualVRAMTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetVRAMTuningStates>((IntPtr)(lpVtbl[11]))(pThis, pVRAMStates);
        }
    }
}
