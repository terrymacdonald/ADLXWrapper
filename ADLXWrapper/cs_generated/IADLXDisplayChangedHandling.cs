using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplayChangedHandling.xml' path='doc/member[@name="IADLXDisplayChangedHandling"]/*' />
[NativeTypeName("struct IADLXDisplayChangedHandling : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayChangedHandling
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, int>)(lpVtbl[0]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, int>)(lpVtbl[1]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXDisplayChangedHandling.xml' path='doc/member[@name="IADLXDisplayChangedHandling.AddDisplayListEventListener"]/*' />
    public ADLX_RESULT AddDisplayListEventListener([NativeTypeName("adlx::IADLXDisplayListChangedListener *")] IADLXDisplayListChangedListener* pDisplayListChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, IADLXDisplayListChangedListener*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this), pDisplayListChangedListener);
    }

    /// <include file='IADLXDisplayChangedHandling.xml' path='doc/member[@name="IADLXDisplayChangedHandling.RemoveDisplayListEventListener"]/*' />
    public ADLX_RESULT RemoveDisplayListEventListener([NativeTypeName("adlx::IADLXDisplayListChangedListener *")] IADLXDisplayListChangedListener* pDisplayListChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, IADLXDisplayListChangedListener*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this), pDisplayListChangedListener);
    }

    /// <include file='IADLXDisplayChangedHandling.xml' path='doc/member[@name="IADLXDisplayChangedHandling.AddDisplayGamutEventListener"]/*' />
    public ADLX_RESULT AddDisplayGamutEventListener([NativeTypeName("adlx::IADLXDisplayGamutChangedListener *")] IADLXDisplayGamutChangedListener* pDisplayGamutChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, IADLXDisplayGamutChangedListener*, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this), pDisplayGamutChangedListener);
    }

    /// <include file='IADLXDisplayChangedHandling.xml' path='doc/member[@name="IADLXDisplayChangedHandling.RemoveDisplayGamutEventListener"]/*' />
    public ADLX_RESULT RemoveDisplayGamutEventListener([NativeTypeName("adlx::IADLXDisplayGamutChangedListener *")] IADLXDisplayGamutChangedListener* pDisplayGamutChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, IADLXDisplayGamutChangedListener*, ADLX_RESULT>)(lpVtbl[6]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this), pDisplayGamutChangedListener);
    }

    /// <include file='IADLXDisplayChangedHandling.xml' path='doc/member[@name="IADLXDisplayChangedHandling.AddDisplayGammaEventListener"]/*' />
    public ADLX_RESULT AddDisplayGammaEventListener([NativeTypeName("adlx::IADLXDisplayGammaChangedListener *")] IADLXDisplayGammaChangedListener* pDisplayGammaChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, IADLXDisplayGammaChangedListener*, ADLX_RESULT>)(lpVtbl[7]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this), pDisplayGammaChangedListener);
    }

    /// <include file='IADLXDisplayChangedHandling.xml' path='doc/member[@name="IADLXDisplayChangedHandling.RemoveDisplayGammaEventListener"]/*' />
    public ADLX_RESULT RemoveDisplayGammaEventListener([NativeTypeName("adlx::IADLXDisplayGammaChangedListener *")] IADLXDisplayGammaChangedListener* pDisplayGammaChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, IADLXDisplayGammaChangedListener*, ADLX_RESULT>)(lpVtbl[8]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this), pDisplayGammaChangedListener);
    }

    /// <include file='IADLXDisplayChangedHandling.xml' path='doc/member[@name="IADLXDisplayChangedHandling.AddDisplay3DLUTEventListener"]/*' />
    public ADLX_RESULT AddDisplay3DLUTEventListener([NativeTypeName("adlx::IADLXDisplay3DLUTChangedListener *")] IADLXDisplay3DLUTChangedListener* pDisplay3DLUTChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, IADLXDisplay3DLUTChangedListener*, ADLX_RESULT>)(lpVtbl[9]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this), pDisplay3DLUTChangedListener);
    }

    /// <include file='IADLXDisplayChangedHandling.xml' path='doc/member[@name="IADLXDisplayChangedHandling.RemoveDisplay3DLUTEventListener"]/*' />
    public ADLX_RESULT RemoveDisplay3DLUTEventListener([NativeTypeName("adlx::IADLXDisplay3DLUTChangedListener *")] IADLXDisplay3DLUTChangedListener* pDisplay3DLUTChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, IADLXDisplay3DLUTChangedListener*, ADLX_RESULT>)(lpVtbl[10]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this), pDisplay3DLUTChangedListener);
    }

    /// <include file='IADLXDisplayChangedHandling.xml' path='doc/member[@name="IADLXDisplayChangedHandling.AddDisplaySettingsEventListener"]/*' />
    public ADLX_RESULT AddDisplaySettingsEventListener([NativeTypeName("adlx::IADLXDisplaySettingsChangedListener *")] IADLXDisplaySettingsChangedListener* pDisplaySettingsChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, IADLXDisplaySettingsChangedListener*, ADLX_RESULT>)(lpVtbl[11]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this), pDisplaySettingsChangedListener);
    }

    /// <include file='IADLXDisplayChangedHandling.xml' path='doc/member[@name="IADLXDisplayChangedHandling.RemoveDisplaySettingsEventListener"]/*' />
    public ADLX_RESULT RemoveDisplaySettingsEventListener([NativeTypeName("adlx::IADLXDisplaySettingsChangedListener *")] IADLXDisplaySettingsChangedListener* pDisplaySettingsChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, IADLXDisplaySettingsChangedListener*, ADLX_RESULT>)(lpVtbl[12]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this), pDisplaySettingsChangedListener);
    }
}
