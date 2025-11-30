using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXPowerTuningServices1 : adlx::IADLXPowerTuningServices")]
public unsafe partial struct IADLXPowerTuningServices1
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXPowerTuningServices1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXPowerTuningServices1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXPowerTuningServices1* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetPowerTuningChangedHandling(IADLXPowerTuningServices1* pThis, IADLXPowerTuningChangedHandling** ppPowerTuningChangedHandling);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSmartShiftMax(IADLXPowerTuningServices1* pThis, IADLXSmartShiftMax** ppSmartShiftMax);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSmartShiftEco(IADLXPowerTuningServices1* pThis, IADLXSmartShiftEco** ppSmartShiftEco);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsGPUConnectSupported(IADLXPowerTuningServices1* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUConnectGPUs(IADLXPowerTuningServices1* pThis, IADLXGPU2List** ppGPUs);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXPowerTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXPowerTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXPowerTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT GetPowerTuningChangedHandling(IADLXPowerTuningChangedHandling** ppPowerTuningChangedHandling)
    {
        fixed (IADLXPowerTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetPowerTuningChangedHandling>((IntPtr)(lpVtbl[3]))(pThis, ppPowerTuningChangedHandling);
        }
    }

    public ADLX_RESULT GetSmartShiftMax(IADLXSmartShiftMax** ppSmartShiftMax)
    {
        fixed (IADLXPowerTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSmartShiftMax>((IntPtr)(lpVtbl[4]))(pThis, ppSmartShiftMax);
        }
    }

    public ADLX_RESULT GetSmartShiftEco(IADLXSmartShiftEco** ppSmartShiftEco)
    {
        fixed (IADLXPowerTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSmartShiftEco>((IntPtr)(lpVtbl[5]))(pThis, ppSmartShiftEco);
        }
    }

    public ADLX_RESULT IsGPUConnectSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXPowerTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsGPUConnectSupported>((IntPtr)(lpVtbl[6]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetGPUConnectGPUs(IADLXGPU2List** ppGPUs)
    {
        fixed (IADLXPowerTuningServices1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUConnectGPUs>((IntPtr)(lpVtbl[7]))(pThis, ppGPUs);
        }
    }
}
