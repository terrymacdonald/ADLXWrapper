using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXGPUConnectChangedListener
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _OnGPUConnectChanged(IADLXGPUConnectChangedListener* pThis, [NativeTypeName("adlx::IADLXGPUConnectChangedEvent *")] IADLXGPUConnectChangedEvent* pGPUConnectChangedEvent);

    [return: NativeTypeName("adlx_bool")]
    public bool OnGPUConnectChanged([NativeTypeName("adlx::IADLXGPUConnectChangedEvent *")] IADLXGPUConnectChangedEvent* pGPUConnectChangedEvent)
    {
        fixed (IADLXGPUConnectChangedListener* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_OnGPUConnectChanged>((IntPtr)(lpVtbl[0]))(pThis, pGPUConnectChangedEvent) != 0;
        }
    }
}

public partial struct IADLXGPUConnectChangedListener
{
}
