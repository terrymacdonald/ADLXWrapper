using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper
{
    public unsafe partial struct IADLXGPUsEventListener
    {
        public void** lpVtbl;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        [return: NativeTypeName("adlx_bool")]
        public delegate byte _OnGPUListChanged(IADLXGPUsEventListener* pThis, [NativeTypeName("adlx::IADLXGPUList *")] IADLXGPUList* pNewGPUs);

        [return: NativeTypeName("adlx_bool")]
        public bool OnGPUListChanged([NativeTypeName("adlx::IADLXGPUList *")] IADLXGPUList* pNewGPUs)
        {
            fixed (IADLXGPUsEventListener* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_OnGPUListChanged>((IntPtr)(lpVtbl[0]))(pThis, pNewGPUs) != 0;
            }
        }
    }
}
