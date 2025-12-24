using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLX3DSettingsChangedHandling.xml' path='doc/member[@name="IADLX3DSettingsChangedHandling"]/*' />
[NativeTypeName("struct IADLX3DSettingsChangedHandling : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DSettingsChangedHandling
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedHandling*, int>)(lpVtbl[0]))((IADLX3DSettingsChangedHandling*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedHandling*, int>)(lpVtbl[1]))((IADLX3DSettingsChangedHandling*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedHandling*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DSettingsChangedHandling*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLX3DSettingsChangedHandling.xml' path='doc/member[@name="IADLX3DSettingsChangedHandling.Add3DSettingsEventListener"]/*' />
    public ADLX_RESULT Add3DSettingsEventListener([NativeTypeName("adlx::IADLX3DSettingsChangedListener *")] IADLX3DSettingsChangedListener* p3DSettingsChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedHandling*, IADLX3DSettingsChangedListener*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DSettingsChangedHandling*)Unsafe.AsPointer(ref this), p3DSettingsChangedListener);
    }

    /// <include file='IADLX3DSettingsChangedHandling.xml' path='doc/member[@name="IADLX3DSettingsChangedHandling.Remove3DSettingsEventListener"]/*' />
    public ADLX_RESULT Remove3DSettingsEventListener([NativeTypeName("adlx::IADLX3DSettingsChangedListener *")] IADLX3DSettingsChangedListener* p3DSettingsChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedHandling*, IADLX3DSettingsChangedListener*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DSettingsChangedHandling*)Unsafe.AsPointer(ref this), p3DSettingsChangedListener);
    }
}
