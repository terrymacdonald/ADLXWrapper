using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXGPUAppsListEventListener
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _OnGPUAppsListChanged(IADLXGPUAppsListEventListener* pThis, [NativeTypeName("adlx::IADLXGPU2 *")] IADLXGPU2* pGPU, [NativeTypeName("adlx::IADLXApplicationList *")] IADLXApplicationList* pApplications);

    [return: NativeTypeName("adlx_bool")]
    public bool OnGPUAppsListChanged([NativeTypeName("adlx::IADLXGPU2 *")] IADLXGPU2* pGPU, [NativeTypeName("adlx::IADLXApplicationList *")] IADLXApplicationList* pApplications)
    {
        fixed (IADLXGPUAppsListEventListener* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_OnGPUAppsListChanged>((IntPtr)(lpVtbl[0]))(pThis, pGPU, pApplications) != 0;
        }
    }
}
