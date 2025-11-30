using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXSystem2 : adlx::IADLXSystem1")]
public unsafe partial struct IADLXSystem2
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXSystem2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXSystem2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXSystem2* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetPowerTuningServices(IADLXSystem2* pThis, IADLXPowerTuningServices** ppPowerTuningServices);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMultimediaServices(IADLXSystem2* pThis, IADLXMultimediaServices** ppMultiMediaServices);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUAppsListChangedHandling(IADLXSystem2* pThis, IADLXGPUAppsListChangedHandling** ppGPUAppsListChangedHandling);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXSystem2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXSystem2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXSystem2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT GetPowerTuningServices(IADLXPowerTuningServices** ppPowerTuningServices)
    {
        fixed (IADLXSystem2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetPowerTuningServices>((IntPtr)(lpVtbl[3]))(pThis, ppPowerTuningServices);
        }
    }

    public ADLX_RESULT GetMultimediaServices(IADLXMultimediaServices** ppMultiMediaServices)
    {
        fixed (IADLXSystem2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMultimediaServices>((IntPtr)(lpVtbl[4]))(pThis, ppMultiMediaServices);
        }
    }

    public ADLX_RESULT GetGPUAppsListChangedHandling(IADLXGPUAppsListChangedHandling** ppGPUAppsListChangedHandling)
    {
        fixed (IADLXSystem2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUAppsListChangedHandling>((IntPtr)(lpVtbl[5]))(pThis, ppGPUAppsListChangedHandling);
        }
    }
}
