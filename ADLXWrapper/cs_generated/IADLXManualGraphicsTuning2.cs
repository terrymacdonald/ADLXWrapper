using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXManualGraphicsTuning2 : adlx::IADLXInterface")]
public unsafe partial struct IADLXManualGraphicsTuning2
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXManualGraphicsTuning2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXManualGraphicsTuning2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXManualGraphicsTuning2* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUMinFrequencyRange(IADLXManualGraphicsTuning2* pThis, ADLX_IntRange* tuningRange);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUMinFrequency(IADLXManualGraphicsTuning2* pThis, [NativeTypeName("adlx_int *")] int* minFreq);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetGPUMinFrequency(IADLXManualGraphicsTuning2* pThis, [NativeTypeName("adlx_int")] int minFreq);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUMaxFrequencyRange(IADLXManualGraphicsTuning2* pThis, ADLX_IntRange* tuningRange);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUMaxFrequency(IADLXManualGraphicsTuning2* pThis, [NativeTypeName("adlx_int *")] int* maxFreq);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetGPUMaxFrequency(IADLXManualGraphicsTuning2* pThis, [NativeTypeName("adlx_int")] int maxFreq);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUVoltageRange(IADLXManualGraphicsTuning2* pThis, ADLX_IntRange* tuningRange);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUVoltage(IADLXManualGraphicsTuning2* pThis, [NativeTypeName("adlx_int *")] int* volt);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetGPUVoltage(IADLXManualGraphicsTuning2* pThis, [NativeTypeName("adlx_int")] int volt);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXManualGraphicsTuning2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXManualGraphicsTuning2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXManualGraphicsTuning2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT GetGPUMinFrequencyRange(ADLX_IntRange* tuningRange)
    {
        fixed (IADLXManualGraphicsTuning2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUMinFrequencyRange>((IntPtr)(lpVtbl[3]))(pThis, tuningRange);
        }
    }

    public ADLX_RESULT GetGPUMinFrequency([NativeTypeName("adlx_int *")] int* minFreq)
    {
        fixed (IADLXManualGraphicsTuning2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUMinFrequency>((IntPtr)(lpVtbl[4]))(pThis, minFreq);
        }
    }

    public ADLX_RESULT SetGPUMinFrequency([NativeTypeName("adlx_int")] int minFreq)
    {
        fixed (IADLXManualGraphicsTuning2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetGPUMinFrequency>((IntPtr)(lpVtbl[5]))(pThis, minFreq);
        }
    }

    public ADLX_RESULT GetGPUMaxFrequencyRange(ADLX_IntRange* tuningRange)
    {
        fixed (IADLXManualGraphicsTuning2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUMaxFrequencyRange>((IntPtr)(lpVtbl[6]))(pThis, tuningRange);
        }
    }

    public ADLX_RESULT GetGPUMaxFrequency([NativeTypeName("adlx_int *")] int* maxFreq)
    {
        fixed (IADLXManualGraphicsTuning2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUMaxFrequency>((IntPtr)(lpVtbl[7]))(pThis, maxFreq);
        }
    }

    public ADLX_RESULT SetGPUMaxFrequency([NativeTypeName("adlx_int")] int maxFreq)
    {
        fixed (IADLXManualGraphicsTuning2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetGPUMaxFrequency>((IntPtr)(lpVtbl[8]))(pThis, maxFreq);
        }
    }

    public ADLX_RESULT GetGPUVoltageRange(ADLX_IntRange* tuningRange)
    {
        fixed (IADLXManualGraphicsTuning2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUVoltageRange>((IntPtr)(lpVtbl[9]))(pThis, tuningRange);
        }
    }

    public ADLX_RESULT GetGPUVoltage([NativeTypeName("adlx_int *")] int* volt)
    {
        fixed (IADLXManualGraphicsTuning2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUVoltage>((IntPtr)(lpVtbl[10]))(pThis, volt);
        }
    }

    public ADLX_RESULT SetGPUVoltage([NativeTypeName("adlx_int")] int volt)
    {
        fixed (IADLXManualGraphicsTuning2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetGPUVoltage>((IntPtr)(lpVtbl[11]))(pThis, volt);
        }
    }
}
