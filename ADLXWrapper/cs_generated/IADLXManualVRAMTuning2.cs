using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXManualVRAMTuning2 : adlx::IADLXInterface")]
public unsafe partial struct IADLXManualVRAMTuning2
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXManualVRAMTuning2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXManualVRAMTuning2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXManualVRAMTuning2* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedMemoryTiming(IADLXManualVRAMTuning2* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSupportedMemoryTimingDescriptionList(IADLXManualVRAMTuning2* pThis, IADLXMemoryTimingDescriptionList** ppDescriptionList);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMemoryTimingDescription(IADLXManualVRAMTuning2* pThis, ADLX_MEMORYTIMING_DESCRIPTION* description);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetMemoryTimingDescription(IADLXManualVRAMTuning2* pThis, ADLX_MEMORYTIMING_DESCRIPTION description);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMaxVRAMFrequencyRange(IADLXManualVRAMTuning2* pThis, ADLX_IntRange* tuningRange);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMaxVRAMFrequency(IADLXManualVRAMTuning2* pThis, [NativeTypeName("adlx_int *")] int* freq);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetMaxVRAMFrequency(IADLXManualVRAMTuning2* pThis, [NativeTypeName("adlx_int")] int freq);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXManualVRAMTuning2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXManualVRAMTuning2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXManualVRAMTuning2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupportedMemoryTiming([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXManualVRAMTuning2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedMemoryTiming>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetSupportedMemoryTimingDescriptionList(IADLXMemoryTimingDescriptionList** ppDescriptionList)
    {
        fixed (IADLXManualVRAMTuning2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSupportedMemoryTimingDescriptionList>((IntPtr)(lpVtbl[4]))(pThis, ppDescriptionList);
        }
    }

    public ADLX_RESULT GetMemoryTimingDescription(ADLX_MEMORYTIMING_DESCRIPTION* description)
    {
        fixed (IADLXManualVRAMTuning2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMemoryTimingDescription>((IntPtr)(lpVtbl[5]))(pThis, description);
        }
    }

    public ADLX_RESULT SetMemoryTimingDescription(ADLX_MEMORYTIMING_DESCRIPTION description)
    {
        fixed (IADLXManualVRAMTuning2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetMemoryTimingDescription>((IntPtr)(lpVtbl[6]))(pThis, description);
        }
    }

    public ADLX_RESULT GetMaxVRAMFrequencyRange(ADLX_IntRange* tuningRange)
    {
        fixed (IADLXManualVRAMTuning2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMaxVRAMFrequencyRange>((IntPtr)(lpVtbl[7]))(pThis, tuningRange);
        }
    }

    public ADLX_RESULT GetMaxVRAMFrequency([NativeTypeName("adlx_int *")] int* freq)
    {
        fixed (IADLXManualVRAMTuning2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMaxVRAMFrequency>((IntPtr)(lpVtbl[8]))(pThis, freq);
        }
    }

    public ADLX_RESULT SetMaxVRAMFrequency([NativeTypeName("adlx_int")] int freq)
    {
        fixed (IADLXManualVRAMTuning2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetMaxVRAMFrequency>((IntPtr)(lpVtbl[9]))(pThis, freq);
        }
    }
}
