using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXGPU1.xml' path='doc/member[@name="IADLXGPU1"]/*' />
[NativeTypeName("struct IADLXGPU1 : adlx::IADLXGPU")]
public unsafe partial struct IADLXGPU1
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, int>)(lpVtbl[0]))((IADLXGPU1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, int>)(lpVtbl[1]))((IADLXGPU1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPU1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXGPU.VendorId" />
    public ADLX_RESULT VendorId([NativeTypeName("const char **")] sbyte** vendorId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, sbyte**, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPU1*)Unsafe.AsPointer(ref this), vendorId);
    }

    /// <inheritdoc cref="IADLXGPU.ASICFamilyType" />
    public readonly ADLX_RESULT ASICFamilyType(ADLX_ASIC_FAMILY_TYPE* asicFamilyType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, ADLX_ASIC_FAMILY_TYPE*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPU1*)Unsafe.AsPointer(in this), asicFamilyType);
    }

    /// <inheritdoc cref="IADLXGPU.Type" />
    public readonly ADLX_RESULT Type(ADLX_GPU_TYPE* gpuType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, ADLX_GPU_TYPE*, ADLX_RESULT>)(lpVtbl[5]))((IADLXGPU1*)Unsafe.AsPointer(in this), gpuType);
    }

    /// <inheritdoc cref="IADLXGPU.IsExternal" />
    public readonly ADLX_RESULT IsExternal([NativeTypeName("adlx_bool *")] bool* isExternal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXGPU1*)Unsafe.AsPointer(in this), isExternal);
    }

    /// <inheritdoc cref="IADLXGPU.Name" />
    public readonly ADLX_RESULT Name([NativeTypeName("const char **")] sbyte** name)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, sbyte**, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPU1*)Unsafe.AsPointer(in this), name);
    }

    /// <inheritdoc cref="IADLXGPU.DriverPath" />
    public readonly ADLX_RESULT DriverPath([NativeTypeName("const char **")] sbyte** driverPath)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, sbyte**, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPU1*)Unsafe.AsPointer(in this), driverPath);
    }

    /// <inheritdoc cref="IADLXGPU.PNPString" />
    public readonly ADLX_RESULT PNPString([NativeTypeName("const char **")] sbyte** pnpString)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, sbyte**, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPU1*)Unsafe.AsPointer(in this), pnpString);
    }

    /// <inheritdoc cref="IADLXGPU.HasDesktops" />
    public readonly ADLX_RESULT HasDesktops([NativeTypeName("adlx_bool *")] bool* hasDesktops)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, bool*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPU1*)Unsafe.AsPointer(in this), hasDesktops);
    }

    /// <inheritdoc cref="IADLXGPU.TotalVRAM" />
    public ADLX_RESULT TotalVRAM([NativeTypeName("adlx_uint *")] uint* vramMB)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, uint*, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPU1*)Unsafe.AsPointer(ref this), vramMB);
    }

    /// <inheritdoc cref="IADLXGPU.VRAMType" />
    public ADLX_RESULT VRAMType([NativeTypeName("const char **")] sbyte** type)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, sbyte**, ADLX_RESULT>)(lpVtbl[12]))((IADLXGPU1*)Unsafe.AsPointer(ref this), type);
    }

    /// <inheritdoc cref="IADLXGPU.BIOSInfo" />
    public ADLX_RESULT BIOSInfo([NativeTypeName("const char **")] sbyte** partNumber, [NativeTypeName("const char **")] sbyte** version, [NativeTypeName("const char **")] sbyte** date)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, sbyte**, sbyte**, sbyte**, ADLX_RESULT>)(lpVtbl[13]))((IADLXGPU1*)Unsafe.AsPointer(ref this), partNumber, version, date);
    }

    /// <inheritdoc cref="IADLXGPU.DeviceId" />
    public ADLX_RESULT DeviceId([NativeTypeName("const char **")] sbyte** deviceId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, sbyte**, ADLX_RESULT>)(lpVtbl[14]))((IADLXGPU1*)Unsafe.AsPointer(ref this), deviceId);
    }

    /// <inheritdoc cref="IADLXGPU.RevisionId" />
    public ADLX_RESULT RevisionId([NativeTypeName("const char **")] sbyte** revisionId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, sbyte**, ADLX_RESULT>)(lpVtbl[15]))((IADLXGPU1*)Unsafe.AsPointer(ref this), revisionId);
    }

    /// <inheritdoc cref="IADLXGPU.SubSystemId" />
    public ADLX_RESULT SubSystemId([NativeTypeName("const char **")] sbyte** subSystemId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, sbyte**, ADLX_RESULT>)(lpVtbl[16]))((IADLXGPU1*)Unsafe.AsPointer(ref this), subSystemId);
    }

    /// <inheritdoc cref="IADLXGPU.SubSystemVendorId" />
    public ADLX_RESULT SubSystemVendorId([NativeTypeName("const char **")] sbyte** subSystemVendorId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, sbyte**, ADLX_RESULT>)(lpVtbl[17]))((IADLXGPU1*)Unsafe.AsPointer(ref this), subSystemVendorId);
    }

    /// <inheritdoc cref="IADLXGPU.UniqueId" />
    public ADLX_RESULT UniqueId([NativeTypeName("adlx_int *")] int* uniqueId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, int*, ADLX_RESULT>)(lpVtbl[18]))((IADLXGPU1*)Unsafe.AsPointer(ref this), uniqueId);
    }

    /// <include file='IADLXGPU1.xml' path='doc/member[@name="IADLXGPU1.PCIBusType"]/*' />
    public readonly ADLX_RESULT PCIBusType(ADLX_PCI_BUS_TYPE* busType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, ADLX_PCI_BUS_TYPE*, ADLX_RESULT>)(lpVtbl[19]))((IADLXGPU1*)Unsafe.AsPointer(in this), busType);
    }

    /// <include file='IADLXGPU1.xml' path='doc/member[@name="IADLXGPU1.PCIBusLaneWidth"]/*' />
    public readonly ADLX_RESULT PCIBusLaneWidth([NativeTypeName("adlx_uint *")] uint* laneWidth)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, uint*, ADLX_RESULT>)(lpVtbl[20]))((IADLXGPU1*)Unsafe.AsPointer(in this), laneWidth);
    }

    /// <include file='IADLXGPU1.xml' path='doc/member[@name="IADLXGPU1.MultiGPUMode"]/*' />
    public ADLX_RESULT MultiGPUMode(ADLX_MGPU_MODE* mode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, ADLX_MGPU_MODE*, ADLX_RESULT>)(lpVtbl[21]))((IADLXGPU1*)Unsafe.AsPointer(ref this), mode);
    }

    /// <include file='IADLXGPU1.xml' path='doc/member[@name="IADLXGPU1.ProductName"]/*' />
    public readonly ADLX_RESULT ProductName([NativeTypeName("const char **")] sbyte** productName)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, sbyte**, ADLX_RESULT>)(lpVtbl[22]))((IADLXGPU1*)Unsafe.AsPointer(in this), productName);
    }
}
