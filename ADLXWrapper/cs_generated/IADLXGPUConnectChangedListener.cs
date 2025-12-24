using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXGPUConnectChangedListener.xml' path='doc/member[@name="IADLXGPUConnectChangedListener"]/*' />
public unsafe partial struct IADLXGPUConnectChangedListener
{
    public void** lpVtbl;

    /// <include file='IADLXGPUConnectChangedListener.xml' path='doc/member[@name="IADLXGPUConnectChangedListener.OnGPUConnectChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool OnGPUConnectChanged([NativeTypeName("adlx::IADLXGPUConnectChangedEvent *")] IADLXGPUConnectChangedEvent* pGPUConnectChangedEvent)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUConnectChangedListener*, IADLXGPUConnectChangedEvent*, byte>)(lpVtbl[0]))((IADLXGPUConnectChangedListener*)Unsafe.AsPointer(ref this), pGPUConnectChangedEvent) != 0;
    }
}
