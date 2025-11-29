using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper
{
    public unsafe partial struct IADLMapping
    {
        public void** lpVtbl;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _GetADLXGPUFromBdf(IADLMapping* pThis, [NativeTypeName("adlx_int")] int bus, [NativeTypeName("adlx_int")] int device, [NativeTypeName("adlx_int")] int function, IADLXGPU** ppGPU);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _GetADLXGPUFromAdlAdapterIndex(IADLMapping* pThis, [NativeTypeName("adlx_int")] int adlAdapterIndex, IADLXGPU** ppGPU);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _BdfFromADLXGPU(IADLMapping* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_int *")] int* bus, [NativeTypeName("adlx_int *")] int* device, [NativeTypeName("adlx_int *")] int* function);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _AdlAdapterIndexFromADLXGPU(IADLMapping* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_int *")] int* adlAdapterIndex);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _GetADLXDisplayFromADLIds(IADLMapping* pThis, [NativeTypeName("adlx_int")] int adapterIndex, [NativeTypeName("adlx_int")] int displayIndex, [NativeTypeName("adlx_int")] int bus, [NativeTypeName("adlx_int")] int device, [NativeTypeName("adlx_int")] int function, IADLXDisplay** ppDisplay);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _ADLIdsFromADLXDisplay(IADLMapping* pThis, [NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, [NativeTypeName("adlx_int *")] int* adapterIndex, [NativeTypeName("adlx_int *")] int* displayIndex, [NativeTypeName("adlx_int *")] int* bus, [NativeTypeName("adlx_int *")] int* device, [NativeTypeName("adlx_int *")] int* function);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _GetADLXDesktopFromADLIds(IADLMapping* pThis, [NativeTypeName("adlx_int")] int adapterIndex, [NativeTypeName("adlx_int")] int VidPnSourceId, [NativeTypeName("adlx_int")] int bus, [NativeTypeName("adlx_int")] int device, [NativeTypeName("adlx_int")] int function, IADLXDesktop** ppDesktop);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _ADLIdsFromADLXDesktop(IADLMapping* pThis, [NativeTypeName("adlx::IADLXDesktop *")] IADLXDesktop* pDesktop, [NativeTypeName("adlx_int *")] int* adapterIndex, [NativeTypeName("adlx_int *")] int* VidPnSourceId, [NativeTypeName("adlx_int *")] int* bus, [NativeTypeName("adlx_int *")] int* device, [NativeTypeName("adlx_int *")] int* function);

        public ADLX_RESULT GetADLXGPUFromBdf([NativeTypeName("adlx_int")] int bus, [NativeTypeName("adlx_int")] int device, [NativeTypeName("adlx_int")] int function, IADLXGPU** ppGPU)
        {
            fixed (IADLMapping* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_GetADLXGPUFromBdf>((IntPtr)(lpVtbl[0]))(pThis, bus, device, function, ppGPU);
            }
        }

        public ADLX_RESULT GetADLXGPUFromAdlAdapterIndex([NativeTypeName("adlx_int")] int adlAdapterIndex, IADLXGPU** ppGPU)
        {
            fixed (IADLMapping* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_GetADLXGPUFromAdlAdapterIndex>((IntPtr)(lpVtbl[1]))(pThis, adlAdapterIndex, ppGPU);
            }
        }

        public ADLX_RESULT BdfFromADLXGPU([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_int *")] int* bus, [NativeTypeName("adlx_int *")] int* device, [NativeTypeName("adlx_int *")] int* function)
        {
            fixed (IADLMapping* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_BdfFromADLXGPU>((IntPtr)(lpVtbl[2]))(pThis, pGPU, bus, device, function);
            }
        }

        public ADLX_RESULT AdlAdapterIndexFromADLXGPU([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_int *")] int* adlAdapterIndex)
        {
            fixed (IADLMapping* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_AdlAdapterIndexFromADLXGPU>((IntPtr)(lpVtbl[3]))(pThis, pGPU, adlAdapterIndex);
            }
        }

        public ADLX_RESULT GetADLXDisplayFromADLIds([NativeTypeName("adlx_int")] int adapterIndex, [NativeTypeName("adlx_int")] int displayIndex, [NativeTypeName("adlx_int")] int bus, [NativeTypeName("adlx_int")] int device, [NativeTypeName("adlx_int")] int function, IADLXDisplay** ppDisplay)
        {
            fixed (IADLMapping* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_GetADLXDisplayFromADLIds>((IntPtr)(lpVtbl[4]))(pThis, adapterIndex, displayIndex, bus, device, function, ppDisplay);
            }
        }

        public ADLX_RESULT ADLIdsFromADLXDisplay([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, [NativeTypeName("adlx_int *")] int* adapterIndex, [NativeTypeName("adlx_int *")] int* displayIndex, [NativeTypeName("adlx_int *")] int* bus, [NativeTypeName("adlx_int *")] int* device, [NativeTypeName("adlx_int *")] int* function)
        {
            fixed (IADLMapping* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_ADLIdsFromADLXDisplay>((IntPtr)(lpVtbl[5]))(pThis, pDisplay, adapterIndex, displayIndex, bus, device, function);
            }
        }

        public ADLX_RESULT GetADLXDesktopFromADLIds([NativeTypeName("adlx_int")] int adapterIndex, [NativeTypeName("adlx_int")] int VidPnSourceId, [NativeTypeName("adlx_int")] int bus, [NativeTypeName("adlx_int")] int device, [NativeTypeName("adlx_int")] int function, IADLXDesktop** ppDesktop)
        {
            fixed (IADLMapping* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_GetADLXDesktopFromADLIds>((IntPtr)(lpVtbl[6]))(pThis, adapterIndex, VidPnSourceId, bus, device, function, ppDesktop);
            }
        }

        public ADLX_RESULT ADLIdsFromADLXDesktop([NativeTypeName("adlx::IADLXDesktop *")] IADLXDesktop* pDesktop, [NativeTypeName("adlx_int *")] int* adapterIndex, [NativeTypeName("adlx_int *")] int* VidPnSourceId, [NativeTypeName("adlx_int *")] int* bus, [NativeTypeName("adlx_int *")] int* device, [NativeTypeName("adlx_int *")] int* function)
        {
            fixed (IADLMapping* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_ADLIdsFromADLXDesktop>((IntPtr)(lpVtbl[7]))(pThis, pDesktop, adapterIndex, VidPnSourceId, bus, device, function);
            }
        }
    }
}
