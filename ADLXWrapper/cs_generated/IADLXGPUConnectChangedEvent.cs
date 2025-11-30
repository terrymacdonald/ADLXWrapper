using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUConnectChangedEvent : adlx::IADLXChangedEvent")]
public unsafe partial struct IADLXGPUConnectChangedEvent
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXGPUConnectChangedEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXGPUConnectChangedEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXGPUConnectChangedEvent* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_SYNC_ORIGIN _GetOrigin(IADLXGPUConnectChangedEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPU(IADLXGPUConnectChangedEvent* pThis, IADLXGPU2** ppGPU);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsGPUAppsListChanged(IADLXGPUConnectChangedEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsGPUPowerChanged(IADLXGPUConnectChangedEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsGPUPowerChangeError(IADLXGPUConnectChangedEvent* pThis, ADLX_RESULT* pPowerChangeError);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXGPUConnectChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXGPUConnectChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXGPUConnectChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        fixed (IADLXGPUConnectChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetOrigin>((IntPtr)(lpVtbl[3]))(pThis);
        }
    }

    public ADLX_RESULT GetGPU(IADLXGPU2** ppGPU)
    {
        fixed (IADLXGPUConnectChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPU>((IntPtr)(lpVtbl[4]))(pThis, ppGPU);
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsGPUAppsListChanged()
    {
        fixed (IADLXGPUConnectChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsGPUAppsListChanged>((IntPtr)(lpVtbl[5]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsGPUPowerChanged()
    {
        fixed (IADLXGPUConnectChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsGPUPowerChanged>((IntPtr)(lpVtbl[6]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsGPUPowerChangeError(ADLX_RESULT* pPowerChangeError)
    {
        fixed (IADLXGPUConnectChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsGPUPowerChangeError>((IntPtr)(lpVtbl[7]))(pThis, pPowerChangeError) != 0;
        }
    }
}
