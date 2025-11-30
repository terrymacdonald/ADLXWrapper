using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXManualGraphicsTuning1 : adlx::IADLXInterface")]
public unsafe partial struct IADLXManualGraphicsTuning1
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXManualGraphicsTuning1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXManualGraphicsTuning1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXManualGraphicsTuning1* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUTuningRanges(IADLXManualGraphicsTuning1* pThis, ADLX_IntRange* frequencyRange, ADLX_IntRange* voltageRange);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUTuningStates(IADLXManualGraphicsTuning1* pThis, IADLXManualTuningStateList** ppGFXStates);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetEmptyGPUTuningStates(IADLXManualGraphicsTuning1* pThis, IADLXManualTuningStateList** ppGFXStates);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsValidGPUTuningStates(IADLXManualGraphicsTuning1* pThis, [NativeTypeName("adlx::IADLXManualTuningStateList *")] IADLXManualTuningStateList* pGFXStates, [NativeTypeName("adlx_int *")] int* errorIndex);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetGPUTuningStates(IADLXManualGraphicsTuning1* pThis, [NativeTypeName("adlx::IADLXManualTuningStateList *")] IADLXManualTuningStateList* pGFXStates);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXManualGraphicsTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXManualGraphicsTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXManualGraphicsTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT GetGPUTuningRanges(ADLX_IntRange* frequencyRange, ADLX_IntRange* voltageRange)
    {
        fixed (IADLXManualGraphicsTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUTuningRanges>((IntPtr)(lpVtbl[3]))(pThis, frequencyRange, voltageRange);
        }
    }

    public ADLX_RESULT GetGPUTuningStates(IADLXManualTuningStateList** ppGFXStates)
    {
        fixed (IADLXManualGraphicsTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUTuningStates>((IntPtr)(lpVtbl[4]))(pThis, ppGFXStates);
        }
    }

    public ADLX_RESULT GetEmptyGPUTuningStates(IADLXManualTuningStateList** ppGFXStates)
    {
        fixed (IADLXManualGraphicsTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetEmptyGPUTuningStates>((IntPtr)(lpVtbl[5]))(pThis, ppGFXStates);
        }
    }

    public ADLX_RESULT IsValidGPUTuningStates([NativeTypeName("adlx::IADLXManualTuningStateList *")] IADLXManualTuningStateList* pGFXStates, [NativeTypeName("adlx_int *")] int* errorIndex)
    {
        fixed (IADLXManualGraphicsTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsValidGPUTuningStates>((IntPtr)(lpVtbl[6]))(pThis, pGFXStates, errorIndex);
        }
    }

    public ADLX_RESULT SetGPUTuningStates([NativeTypeName("adlx::IADLXManualTuningStateList *")] IADLXManualTuningStateList* pGFXStates)
    {
        fixed (IADLXManualGraphicsTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetGPUTuningStates>((IntPtr)(lpVtbl[7]))(pThis, pGFXStates);
        }
    }
}
