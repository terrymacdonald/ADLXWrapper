using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplay.xml' path='doc/member[@name="IADLXDisplay"]/*' />
[NativeTypeName("struct IADLXDisplay : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplay
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, int>)(lpVtbl[0]))((IADLXDisplay*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, int>)(lpVtbl[1]))((IADLXDisplay*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplay*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXDisplay.xml' path='doc/member[@name="IADLXDisplay.ManufacturerID"]/*' />
    public readonly ADLX_RESULT ManufacturerID([NativeTypeName("adlx_uint *")] uint* manufacturerID)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, uint*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplay*)Unsafe.AsPointer(in this), manufacturerID);
    }

    /// <include file='IADLXDisplay.xml' path='doc/member[@name="IADLXDisplay.DisplayType"]/*' />
    public readonly ADLX_RESULT DisplayType(ADLX_DISPLAY_TYPE* displayType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, ADLX_DISPLAY_TYPE*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplay*)Unsafe.AsPointer(in this), displayType);
    }

    /// <include file='IADLXDisplay.xml' path='doc/member[@name="IADLXDisplay.ConnectorType"]/*' />
    public readonly ADLX_RESULT ConnectorType(ADLX_DISPLAY_CONNECTOR_TYPE* connectType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, ADLX_DISPLAY_CONNECTOR_TYPE*, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplay*)Unsafe.AsPointer(in this), connectType);
    }

    /// <include file='IADLXDisplay.xml' path='doc/member[@name="IADLXDisplay.Name"]/*' />
    public readonly ADLX_RESULT Name([NativeTypeName("const char **")] sbyte** displayName)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, sbyte**, ADLX_RESULT>)(lpVtbl[6]))((IADLXDisplay*)Unsafe.AsPointer(in this), displayName);
    }

    /// <include file='IADLXDisplay.xml' path='doc/member[@name="IADLXDisplay.EDID"]/*' />
    public readonly ADLX_RESULT EDID([NativeTypeName("const char **")] sbyte** edid)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, sbyte**, ADLX_RESULT>)(lpVtbl[7]))((IADLXDisplay*)Unsafe.AsPointer(in this), edid);
    }

    /// <include file='IADLXDisplay.xml' path='doc/member[@name="IADLXDisplay.NativeResolution"]/*' />
    public readonly ADLX_RESULT NativeResolution([NativeTypeName("adlx_int *")] int* maxHResolution, [NativeTypeName("adlx_int *")] int* maxVResolution)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, int*, int*, ADLX_RESULT>)(lpVtbl[8]))((IADLXDisplay*)Unsafe.AsPointer(in this), maxHResolution, maxVResolution);
    }

    /// <include file='IADLXDisplay.xml' path='doc/member[@name="IADLXDisplay.RefreshRate"]/*' />
    public readonly ADLX_RESULT RefreshRate([NativeTypeName("adlx_double *")] double* refreshRate)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, double*, ADLX_RESULT>)(lpVtbl[9]))((IADLXDisplay*)Unsafe.AsPointer(in this), refreshRate);
    }

    /// <include file='IADLXDisplay.xml' path='doc/member[@name="IADLXDisplay.PixelClock"]/*' />
    public readonly ADLX_RESULT PixelClock([NativeTypeName("adlx_uint *")] uint* pixelClock)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, uint*, ADLX_RESULT>)(lpVtbl[10]))((IADLXDisplay*)Unsafe.AsPointer(in this), pixelClock);
    }

    /// <include file='IADLXDisplay.xml' path='doc/member[@name="IADLXDisplay.ScanType"]/*' />
    public readonly ADLX_RESULT ScanType(ADLX_DISPLAY_SCAN_TYPE* scanType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, ADLX_DISPLAY_SCAN_TYPE*, ADLX_RESULT>)(lpVtbl[11]))((IADLXDisplay*)Unsafe.AsPointer(in this), scanType);
    }

    /// <include file='IADLXDisplay.xml' path='doc/member[@name="IADLXDisplay.GetGPU"]/*' />
    public ADLX_RESULT GetGPU(IADLXGPU** ppGPU)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, IADLXGPU**, ADLX_RESULT>)(lpVtbl[12]))((IADLXDisplay*)Unsafe.AsPointer(ref this), ppGPU);
    }

    /// <include file='IADLXDisplay.xml' path='doc/member[@name="IADLXDisplay.UniqueId"]/*' />
    public ADLX_RESULT UniqueId([NativeTypeName("adlx_size *")] nuint* uniqueId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, nuint*, ADLX_RESULT>)(lpVtbl[13]))((IADLXDisplay*)Unsafe.AsPointer(ref this), uniqueId);
    }
}
