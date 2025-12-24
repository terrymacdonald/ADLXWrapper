using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXGPU2.xml' path='doc/member[@name="IADLXGPU2"]/*' />
[NativeTypeName("struct IADLXGPU2 : adlx::IADLXGPU1")]
public unsafe partial struct IADLXGPU2
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, int>)(lpVtbl[0]))((IADLXGPU2*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, int>)(lpVtbl[1]))((IADLXGPU2*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPU2*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXGPU.VendorId" />
    public ADLX_RESULT VendorId([NativeTypeName("const char **")] sbyte** vendorId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPU2*)Unsafe.AsPointer(ref this), vendorId);
    }

    /// <inheritdoc cref="IADLXGPU.ASICFamilyType" />
    public readonly ADLX_RESULT ASICFamilyType(ADLX_ASIC_FAMILY_TYPE* asicFamilyType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, ADLX_ASIC_FAMILY_TYPE*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPU2*)Unsafe.AsPointer(in this), asicFamilyType);
    }

    /// <inheritdoc cref="IADLXGPU.Type" />
    public readonly ADLX_RESULT Type(ADLX_GPU_TYPE* gpuType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, ADLX_GPU_TYPE*, ADLX_RESULT>)(lpVtbl[5]))((IADLXGPU2*)Unsafe.AsPointer(in this), gpuType);
    }

    /// <inheritdoc cref="IADLXGPU.IsExternal" />
    public readonly ADLX_RESULT IsExternal([NativeTypeName("adlx_bool *")] bool* isExternal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXGPU2*)Unsafe.AsPointer(in this), isExternal);
    }

    /// <inheritdoc cref="IADLXGPU.Name" />
    public readonly ADLX_RESULT Name([NativeTypeName("const char **")] sbyte** name)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPU2*)Unsafe.AsPointer(in this), name);
    }

    /// <inheritdoc cref="IADLXGPU.DriverPath" />
    public readonly ADLX_RESULT DriverPath([NativeTypeName("const char **")] sbyte** driverPath)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPU2*)Unsafe.AsPointer(in this), driverPath);
    }

    /// <inheritdoc cref="IADLXGPU.PNPString" />
    public readonly ADLX_RESULT PNPString([NativeTypeName("const char **")] sbyte** pnpString)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPU2*)Unsafe.AsPointer(in this), pnpString);
    }

    /// <inheritdoc cref="IADLXGPU.HasDesktops" />
    public readonly ADLX_RESULT HasDesktops([NativeTypeName("adlx_bool *")] bool* hasDesktops)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, bool*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPU2*)Unsafe.AsPointer(in this), hasDesktops);
    }

    /// <inheritdoc cref="IADLXGPU.TotalVRAM" />
    public ADLX_RESULT TotalVRAM([NativeTypeName("adlx_uint *")] uint* vramMB)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, uint*, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPU2*)Unsafe.AsPointer(ref this), vramMB);
    }

    /// <inheritdoc cref="IADLXGPU.VRAMType" />
    public ADLX_RESULT VRAMType([NativeTypeName("const char **")] sbyte** type)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[12]))((IADLXGPU2*)Unsafe.AsPointer(ref this), type);
    }

    /// <inheritdoc cref="IADLXGPU.BIOSInfo" />
    public ADLX_RESULT BIOSInfo([NativeTypeName("const char **")] sbyte** partNumber, [NativeTypeName("const char **")] sbyte** version, [NativeTypeName("const char **")] sbyte** date)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, sbyte**, sbyte**, ADLX_RESULT>)(lpVtbl[13]))((IADLXGPU2*)Unsafe.AsPointer(ref this), partNumber, version, date);
    }

    /// <inheritdoc cref="IADLXGPU.DeviceId" />
    public ADLX_RESULT DeviceId([NativeTypeName("const char **")] sbyte** deviceId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[14]))((IADLXGPU2*)Unsafe.AsPointer(ref this), deviceId);
    }

    /// <inheritdoc cref="IADLXGPU.RevisionId" />
    public ADLX_RESULT RevisionId([NativeTypeName("const char **")] sbyte** revisionId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[15]))((IADLXGPU2*)Unsafe.AsPointer(ref this), revisionId);
    }

    /// <inheritdoc cref="IADLXGPU.SubSystemId" />
    public ADLX_RESULT SubSystemId([NativeTypeName("const char **")] sbyte** subSystemId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[16]))((IADLXGPU2*)Unsafe.AsPointer(ref this), subSystemId);
    }

    /// <inheritdoc cref="IADLXGPU.SubSystemVendorId" />
    public ADLX_RESULT SubSystemVendorId([NativeTypeName("const char **")] sbyte** subSystemVendorId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[17]))((IADLXGPU2*)Unsafe.AsPointer(ref this), subSystemVendorId);
    }

    /// <inheritdoc cref="IADLXGPU.UniqueId" />
    public ADLX_RESULT UniqueId([NativeTypeName("adlx_int *")] int* uniqueId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, int*, ADLX_RESULT>)(lpVtbl[18]))((IADLXGPU2*)Unsafe.AsPointer(ref this), uniqueId);
    }

    /// <inheritdoc cref="IADLXGPU1.PCIBusType" />
    public readonly ADLX_RESULT PCIBusType(ADLX_PCI_BUS_TYPE* busType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, ADLX_PCI_BUS_TYPE*, ADLX_RESULT>)(lpVtbl[19]))((IADLXGPU2*)Unsafe.AsPointer(in this), busType);
    }

    /// <inheritdoc cref="IADLXGPU1.PCIBusLaneWidth" />
    public readonly ADLX_RESULT PCIBusLaneWidth([NativeTypeName("adlx_uint *")] uint* laneWidth)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, uint*, ADLX_RESULT>)(lpVtbl[20]))((IADLXGPU2*)Unsafe.AsPointer(in this), laneWidth);
    }

    /// <inheritdoc cref="IADLXGPU1.MultiGPUMode" />
    public ADLX_RESULT MultiGPUMode(ADLX_MGPU_MODE* mode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, ADLX_MGPU_MODE*, ADLX_RESULT>)(lpVtbl[21]))((IADLXGPU2*)Unsafe.AsPointer(ref this), mode);
    }

    /// <inheritdoc cref="IADLXGPU1.ProductName" />
    public readonly ADLX_RESULT ProductName([NativeTypeName("const char **")] sbyte** productName)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[22]))((IADLXGPU2*)Unsafe.AsPointer(in this), productName);
    }

    /// <include file='IADLXGPU2.xml' path='doc/member[@name="IADLXGPU2.IsPowerOff"]/*' />
    public ADLX_RESULT IsPowerOff([NativeTypeName("adlx_bool *")] bool* state)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, bool*, ADLX_RESULT>)(lpVtbl[23]))((IADLXGPU2*)Unsafe.AsPointer(ref this), state);
    }

    /// <include file='IADLXGPU2.xml' path='doc/member[@name="IADLXGPU2.PowerOn"]/*' />
    public ADLX_RESULT PowerOn()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, ADLX_RESULT>)(lpVtbl[24]))((IADLXGPU2*)Unsafe.AsPointer(ref this));
    }

    /// <include file='IADLXGPU2.xml' path='doc/member[@name="IADLXGPU2.StartPowerOff"]/*' />
    public ADLX_RESULT StartPowerOff([NativeTypeName("adlx::IADLXGPUConnectChangedListener *")] IADLXGPUConnectChangedListener* pGPUConnectChangedListener, [NativeTypeName("adlx_int")] int timeout)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, IADLXGPUConnectChangedListener*, int, ADLX_RESULT>)(lpVtbl[25]))((IADLXGPU2*)Unsafe.AsPointer(ref this), pGPUConnectChangedListener, timeout);
    }

    /// <include file='IADLXGPU2.xml' path='doc/member[@name="IADLXGPU2.AbortPowerOff"]/*' />
    public ADLX_RESULT AbortPowerOff()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, ADLX_RESULT>)(lpVtbl[26]))((IADLXGPU2*)Unsafe.AsPointer(ref this));
    }

    /// <include file='IADLXGPU2.xml' path='doc/member[@name="IADLXGPU2.IsSupportedApplicationList"]/*' />
    public ADLX_RESULT IsSupportedApplicationList([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, bool*, ADLX_RESULT>)(lpVtbl[27]))((IADLXGPU2*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXGPU2.xml' path='doc/member[@name="IADLXGPU2.GetApplications"]/*' />
    public ADLX_RESULT GetApplications(IADLXApplicationList** ppApplications)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, IADLXApplicationList**, ADLX_RESULT>)(lpVtbl[28]))((IADLXGPU2*)Unsafe.AsPointer(ref this), ppApplications);
    }

    /// <include file='IADLXGPU2.xml' path='doc/member[@name="IADLXGPU2.AMDSoftwareReleaseDate"]/*' />
    public ADLX_RESULT AMDSoftwareReleaseDate([NativeTypeName("adlx_uint *")] uint* year, [NativeTypeName("adlx_uint *")] uint* month, [NativeTypeName("adlx_uint *")] uint* day)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, uint*, uint*, uint*, ADLX_RESULT>)(lpVtbl[29]))((IADLXGPU2*)Unsafe.AsPointer(ref this), year, month, day);
    }

    /// <include file='IADLXGPU2.xml' path='doc/member[@name="IADLXGPU2.AMDSoftwareEdition"]/*' />
    public ADLX_RESULT AMDSoftwareEdition([NativeTypeName("const char **")] sbyte** edition)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[30]))((IADLXGPU2*)Unsafe.AsPointer(ref this), edition);
    }

    /// <include file='IADLXGPU2.xml' path='doc/member[@name="IADLXGPU2.AMDSoftwareVersion"]/*' />
    public ADLX_RESULT AMDSoftwareVersion([NativeTypeName("const char **")] sbyte** version)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[31]))((IADLXGPU2*)Unsafe.AsPointer(ref this), version);
    }

    /// <include file='IADLXGPU2.xml' path='doc/member[@name="IADLXGPU2.DriverVersion"]/*' />
    public ADLX_RESULT DriverVersion([NativeTypeName("const char **")] sbyte** version)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[32]))((IADLXGPU2*)Unsafe.AsPointer(ref this), version);
    }

    /// <include file='IADLXGPU2.xml' path='doc/member[@name="IADLXGPU2.AMDWindowsDriverVersion"]/*' />
    public ADLX_RESULT AMDWindowsDriverVersion([NativeTypeName("const char **")] sbyte** version)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[33]))((IADLXGPU2*)Unsafe.AsPointer(ref this), version);
    }

    /// <include file='IADLXGPU2.xml' path='doc/member[@name="IADLXGPU2.LUID"]/*' />
    public ADLX_RESULT LUID(ADLX_LUID* luid)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, ADLX_LUID*, ADLX_RESULT>)(lpVtbl[34]))((IADLXGPU2*)Unsafe.AsPointer(ref this), luid);
    }
}
