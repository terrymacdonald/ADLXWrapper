using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXGPUAppsListEventListener.xml' path='doc/member[@name="IADLXGPUAppsListEventListener"]/*' />
public unsafe partial struct IADLXGPUAppsListEventListener
{
    public void** lpVtbl;

    /// <include file='IADLXGPUAppsListEventListener.xml' path='doc/member[@name="IADLXGPUAppsListEventListener.OnGPUAppsListChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool OnGPUAppsListChanged([NativeTypeName("adlx::IADLXGPU2 *")] IADLXGPU2* pGPU, [NativeTypeName("adlx::IADLXApplicationList *")] IADLXApplicationList* pApplications)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAppsListEventListener*, IADLXGPU2*, IADLXApplicationList*, byte>)(lpVtbl[0]))((IADLXGPUAppsListEventListener*)Unsafe.AsPointer(ref this), pGPU, pApplications) != 0;
    }
}
