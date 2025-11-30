using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUTuningChangedHandling : adlx::IADLXInterface")]
public unsafe partial struct IADLXGPUTuningChangedHandling
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXGPUTuningChangedHandling* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXGPUTuningChangedHandling* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXGPUTuningChangedHandling* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _AddGPUTuningEventListener(IADLXGPUTuningChangedHandling* pThis, [NativeTypeName("adlx::IADLXGPUTuningChangedListener *")] IADLXGPUTuningChangedListener* pGPUTuningChangedListener);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _RemoveGPUTuningEventListener(IADLXGPUTuningChangedHandling* pThis, [NativeTypeName("adlx::IADLXGPUTuningChangedListener *")] IADLXGPUTuningChangedListener* pGPUTuningChangedListener);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXGPUTuningChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXGPUTuningChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXGPUTuningChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT AddGPUTuningEventListener([NativeTypeName("adlx::IADLXGPUTuningChangedListener *")] IADLXGPUTuningChangedListener* pGPUTuningChangedListener)
    {
        fixed (IADLXGPUTuningChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_AddGPUTuningEventListener>((IntPtr)(lpVtbl[3]))(pThis, pGPUTuningChangedListener);
        }
    }

    public ADLX_RESULT RemoveGPUTuningEventListener([NativeTypeName("adlx::IADLXGPUTuningChangedListener *")] IADLXGPUTuningChangedListener* pGPUTuningChangedListener)
    {
        fixed (IADLXGPUTuningChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_RemoveGPUTuningEventListener>((IntPtr)(lpVtbl[4]))(pThis, pGPUTuningChangedListener);
        }
    }
}
