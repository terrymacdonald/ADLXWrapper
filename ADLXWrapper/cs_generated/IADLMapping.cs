using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLMapping.xml' path='doc/member[@name="IADLMapping"]/*' />
public unsafe partial struct IADLMapping
{
    public void** lpVtbl;

    /// <include file='IADLMapping.xml' path='doc/member[@name="IADLMapping.GetADLXGPUFromBdf"]/*' />
    public ADLX_RESULT GetADLXGPUFromBdf([NativeTypeName("adlx_int")] int bus, [NativeTypeName("adlx_int")] int device, [NativeTypeName("adlx_int")] int function, IADLXGPU** ppGPU)
    {
        return ((delegate* unmanaged[Stdcall]<IADLMapping*, int, int, int, IADLXGPU**, ADLX_RESULT>)(lpVtbl[0]))((IADLMapping*)Unsafe.AsPointer(ref this), bus, device, function, ppGPU);
    }

    /// <include file='IADLMapping.xml' path='doc/member[@name="IADLMapping.GetADLXGPUFromAdlAdapterIndex"]/*' />
    public ADLX_RESULT GetADLXGPUFromAdlAdapterIndex([NativeTypeName("adlx_int")] int adlAdapterIndex, IADLXGPU** ppGPU)
    {
        return ((delegate* unmanaged[Stdcall]<IADLMapping*, int, IADLXGPU**, ADLX_RESULT>)(lpVtbl[1]))((IADLMapping*)Unsafe.AsPointer(ref this), adlAdapterIndex, ppGPU);
    }

    /// <include file='IADLMapping.xml' path='doc/member[@name="IADLMapping.BdfFromADLXGPU"]/*' />
    public ADLX_RESULT BdfFromADLXGPU([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_int *")] int* bus, [NativeTypeName("adlx_int *")] int* device, [NativeTypeName("adlx_int *")] int* function)
    {
        return ((delegate* unmanaged[Stdcall]<IADLMapping*, IADLXGPU*, int*, int*, int*, ADLX_RESULT>)(lpVtbl[2]))((IADLMapping*)Unsafe.AsPointer(ref this), pGPU, bus, device, function);
    }

    /// <include file='IADLMapping.xml' path='doc/member[@name="IADLMapping.AdlAdapterIndexFromADLXGPU"]/*' />
    public ADLX_RESULT AdlAdapterIndexFromADLXGPU([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_int *")] int* adlAdapterIndex)
    {
        return ((delegate* unmanaged[Stdcall]<IADLMapping*, IADLXGPU*, int*, ADLX_RESULT>)(lpVtbl[3]))((IADLMapping*)Unsafe.AsPointer(ref this), pGPU, adlAdapterIndex);
    }

    /// <include file='IADLMapping.xml' path='doc/member[@name="IADLMapping.GetADLXDisplayFromADLIds"]/*' />
    public ADLX_RESULT GetADLXDisplayFromADLIds([NativeTypeName("adlx_int")] int adapterIndex, [NativeTypeName("adlx_int")] int displayIndex, [NativeTypeName("adlx_int")] int bus, [NativeTypeName("adlx_int")] int device, [NativeTypeName("adlx_int")] int function, IADLXDisplay** ppDisplay)
    {
        return ((delegate* unmanaged[Stdcall]<IADLMapping*, int, int, int, int, int, IADLXDisplay**, ADLX_RESULT>)(lpVtbl[4]))((IADLMapping*)Unsafe.AsPointer(ref this), adapterIndex, displayIndex, bus, device, function, ppDisplay);
    }

    /// <include file='IADLMapping.xml' path='doc/member[@name="IADLMapping.ADLIdsFromADLXDisplay"]/*' />
    public ADLX_RESULT ADLIdsFromADLXDisplay([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, [NativeTypeName("adlx_int *")] int* adapterIndex, [NativeTypeName("adlx_int *")] int* displayIndex, [NativeTypeName("adlx_int *")] int* bus, [NativeTypeName("adlx_int *")] int* device, [NativeTypeName("adlx_int *")] int* function)
    {
        return ((delegate* unmanaged[Stdcall]<IADLMapping*, IADLXDisplay*, int*, int*, int*, int*, int*, ADLX_RESULT>)(lpVtbl[5]))((IADLMapping*)Unsafe.AsPointer(ref this), pDisplay, adapterIndex, displayIndex, bus, device, function);
    }

    /// <include file='IADLMapping.xml' path='doc/member[@name="IADLMapping.GetADLXDesktopFromADLIds"]/*' />
    public ADLX_RESULT GetADLXDesktopFromADLIds([NativeTypeName("adlx_int")] int adapterIndex, [NativeTypeName("adlx_int")] int VidPnSourceId, [NativeTypeName("adlx_int")] int bus, [NativeTypeName("adlx_int")] int device, [NativeTypeName("adlx_int")] int function, IADLXDesktop** ppDesktop)
    {
        return ((delegate* unmanaged[Stdcall]<IADLMapping*, int, int, int, int, int, IADLXDesktop**, ADLX_RESULT>)(lpVtbl[6]))((IADLMapping*)Unsafe.AsPointer(ref this), adapterIndex, VidPnSourceId, bus, device, function, ppDesktop);
    }

    /// <include file='IADLMapping.xml' path='doc/member[@name="IADLMapping.ADLIdsFromADLXDesktop"]/*' />
    public ADLX_RESULT ADLIdsFromADLXDesktop([NativeTypeName("adlx::IADLXDesktop *")] IADLXDesktop* pDesktop, [NativeTypeName("adlx_int *")] int* adapterIndex, [NativeTypeName("adlx_int *")] int* VidPnSourceId, [NativeTypeName("adlx_int *")] int* bus, [NativeTypeName("adlx_int *")] int* device, [NativeTypeName("adlx_int *")] int* function)
    {
        return ((delegate* unmanaged[Stdcall]<IADLMapping*, IADLXDesktop*, int*, int*, int*, int*, int*, ADLX_RESULT>)(lpVtbl[7]))((IADLMapping*)Unsafe.AsPointer(ref this), pDesktop, adapterIndex, VidPnSourceId, bus, device, function);
    }
}
