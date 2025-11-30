using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUAppsListChangedHandling : adlx::IADLXInterface")]
public unsafe partial struct IADLXGPUAppsListChangedHandling
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXGPUAppsListChangedHandling* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXGPUAppsListChangedHandling* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXGPUAppsListChangedHandling* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _AddGPUAppsListEventListener(IADLXGPUAppsListChangedHandling* pThis, [NativeTypeName("adlx::IADLXGPUAppsListEventListener *")] IADLXGPUAppsListEventListener* pGPUAppsListEventListener);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _RemoveGPUAppsListEventListener(IADLXGPUAppsListChangedHandling* pThis, [NativeTypeName("adlx::IADLXGPUAppsListEventListener *")] IADLXGPUAppsListEventListener* pGPUAppsListEventListener);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXGPUAppsListChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXGPUAppsListChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXGPUAppsListChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT AddGPUAppsListEventListener([NativeTypeName("adlx::IADLXGPUAppsListEventListener *")] IADLXGPUAppsListEventListener* pGPUAppsListEventListener)
    {
        fixed (IADLXGPUAppsListChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_AddGPUAppsListEventListener>((IntPtr)(lpVtbl[3]))(pThis, pGPUAppsListEventListener);
        }
    }

    public ADLX_RESULT RemoveGPUAppsListEventListener([NativeTypeName("adlx::IADLXGPUAppsListEventListener *")] IADLXGPUAppsListEventListener* pGPUAppsListEventListener)
    {
        fixed (IADLXGPUAppsListChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_RemoveGPUAppsListEventListener>((IntPtr)(lpVtbl[4]))(pThis, pGPUAppsListEventListener);
        }
    }
}

public partial struct IADLXGPUAppsListChangedHandling
{
}
