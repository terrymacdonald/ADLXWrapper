using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXGPU.xml' path='doc/member[@name="IADLXGPU"]/*' />
[NativeTypeName("struct IADLXGPU : adlx::IADLXInterface")]
public unsafe partial struct IADLXGPU
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, int>)(lpVtbl[0]))((IADLXGPU*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, int>)(lpVtbl[1]))((IADLXGPU*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPU*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXGPU.xml' path='doc/member[@name="IADLXGPU.VendorId"]/*' />
    public ADLX_RESULT VendorId([NativeTypeName("const char **")] sbyte** vendorId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, sbyte**, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPU*)Unsafe.AsPointer(ref this), vendorId);
    }

    /// <include file='IADLXGPU.xml' path='doc/member[@name="IADLXGPU.ASICFamilyType"]/*' />
    public readonly ADLX_RESULT ASICFamilyType(ADLX_ASIC_FAMILY_TYPE* asicFamilyType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, ADLX_ASIC_FAMILY_TYPE*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPU*)Unsafe.AsPointer(in this), asicFamilyType);
    }

    /// <include file='IADLXGPU.xml' path='doc/member[@name="IADLXGPU.Type"]/*' />
    public readonly ADLX_RESULT Type(ADLX_GPU_TYPE* gpuType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, ADLX_GPU_TYPE*, ADLX_RESULT>)(lpVtbl[5]))((IADLXGPU*)Unsafe.AsPointer(in this), gpuType);
    }

    /// <include file='IADLXGPU.xml' path='doc/member[@name="IADLXGPU.IsExternal"]/*' />
    public readonly ADLX_RESULT IsExternal([NativeTypeName("adlx_bool *")] bool* isExternal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXGPU*)Unsafe.AsPointer(in this), isExternal);
    }

    /// <include file='IADLXGPU.xml' path='doc/member[@name="IADLXGPU.Name"]/*' />
    public readonly ADLX_RESULT Name([NativeTypeName("const char **")] sbyte** name)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, sbyte**, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPU*)Unsafe.AsPointer(in this), name);
    }

    /// <include file='IADLXGPU.xml' path='doc/member[@name="IADLXGPU.DriverPath"]/*' />
    public readonly ADLX_RESULT DriverPath([NativeTypeName("const char **")] sbyte** driverPath)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, sbyte**, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPU*)Unsafe.AsPointer(in this), driverPath);
    }

    /// <include file='IADLXGPU.xml' path='doc/member[@name="IADLXGPU.PNPString"]/*' />
    public readonly ADLX_RESULT PNPString([NativeTypeName("const char **")] sbyte** pnpString)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, sbyte**, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPU*)Unsafe.AsPointer(in this), pnpString);
    }

    /// <include file='IADLXGPU.xml' path='doc/member[@name="IADLXGPU.HasDesktops"]/*' />
    public readonly ADLX_RESULT HasDesktops([NativeTypeName("adlx_bool *")] bool* hasDesktops)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPU*)Unsafe.AsPointer(in this), hasDesktops);
    }

    /// <include file='IADLXGPU.xml' path='doc/member[@name="IADLXGPU.TotalVRAM"]/*' />
    public ADLX_RESULT TotalVRAM([NativeTypeName("adlx_uint *")] uint* vramMB)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, uint*, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPU*)Unsafe.AsPointer(ref this), vramMB);
    }

    /// <include file='IADLXGPU.xml' path='doc/member[@name="IADLXGPU.VRAMType"]/*' />
    public ADLX_RESULT VRAMType([NativeTypeName("const char **")] sbyte** type)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, sbyte**, ADLX_RESULT>)(lpVtbl[12]))((IADLXGPU*)Unsafe.AsPointer(ref this), type);
    }

    /// <include file='IADLXGPU.xml' path='doc/member[@name="IADLXGPU.BIOSInfo"]/*' />
    public ADLX_RESULT BIOSInfo([NativeTypeName("const char **")] sbyte** partNumber, [NativeTypeName("const char **")] sbyte** version, [NativeTypeName("const char **")] sbyte** date)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, sbyte**, sbyte**, sbyte**, ADLX_RESULT>)(lpVtbl[13]))((IADLXGPU*)Unsafe.AsPointer(ref this), partNumber, version, date);
    }

    /// <include file='IADLXGPU.xml' path='doc/member[@name="IADLXGPU.DeviceId"]/*' />
    public ADLX_RESULT DeviceId([NativeTypeName("const char **")] sbyte** deviceId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, sbyte**, ADLX_RESULT>)(lpVtbl[14]))((IADLXGPU*)Unsafe.AsPointer(ref this), deviceId);
    }

    /// <include file='IADLXGPU.xml' path='doc/member[@name="IADLXGPU.RevisionId"]/*' />
    public ADLX_RESULT RevisionId([NativeTypeName("const char **")] sbyte** revisionId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, sbyte**, ADLX_RESULT>)(lpVtbl[15]))((IADLXGPU*)Unsafe.AsPointer(ref this), revisionId);
    }

    /// <include file='IADLXGPU.xml' path='doc/member[@name="IADLXGPU.SubSystemId"]/*' />
    public ADLX_RESULT SubSystemId([NativeTypeName("const char **")] sbyte** subSystemId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, sbyte**, ADLX_RESULT>)(lpVtbl[16]))((IADLXGPU*)Unsafe.AsPointer(ref this), subSystemId);
    }

    /// <include file='IADLXGPU.xml' path='doc/member[@name="IADLXGPU.SubSystemVendorId"]/*' />
    public ADLX_RESULT SubSystemVendorId([NativeTypeName("const char **")] sbyte** subSystemVendorId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, sbyte**, ADLX_RESULT>)(lpVtbl[17]))((IADLXGPU*)Unsafe.AsPointer(ref this), subSystemVendorId);
    }

    /// <include file='IADLXGPU.xml' path='doc/member[@name="IADLXGPU.UniqueId"]/*' />
    public ADLX_RESULT UniqueId([NativeTypeName("adlx_int *")] int* uniqueId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, int*, ADLX_RESULT>)(lpVtbl[18]))((IADLXGPU*)Unsafe.AsPointer(ref this), uniqueId);
    }
}
