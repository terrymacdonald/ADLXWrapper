using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXPowerTuningChangedHandling.xml' path='doc/member[@name="IADLXPowerTuningChangedHandling"]/*' />
[NativeTypeName("struct IADLXPowerTuningChangedHandling : adlx::IADLXInterface")]
public unsafe partial struct IADLXPowerTuningChangedHandling
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedHandling*, int>)(lpVtbl[0]))((IADLXPowerTuningChangedHandling*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedHandling*, int>)(lpVtbl[1]))((IADLXPowerTuningChangedHandling*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedHandling*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXPowerTuningChangedHandling*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXPowerTuningChangedHandling.xml' path='doc/member[@name="IADLXPowerTuningChangedHandling.AddPowerTuningEventListener"]/*' />
    public ADLX_RESULT AddPowerTuningEventListener([NativeTypeName("adlx::IADLXPowerTuningChangedListener *")] IADLXPowerTuningChangedListener* pPowerTuningChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedHandling*, IADLXPowerTuningChangedListener*, ADLX_RESULT>)(lpVtbl[3]))((IADLXPowerTuningChangedHandling*)Unsafe.AsPointer(ref this), pPowerTuningChangedListener);
    }

    /// <include file='IADLXPowerTuningChangedHandling.xml' path='doc/member[@name="IADLXPowerTuningChangedHandling.RemovePowerTuningEventListener"]/*' />
    public ADLX_RESULT RemovePowerTuningEventListener([NativeTypeName("adlx::IADLXPowerTuningChangedListener *")] IADLXPowerTuningChangedListener* pPowerTuningChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedHandling*, IADLXPowerTuningChangedListener*, ADLX_RESULT>)(lpVtbl[4]))((IADLXPowerTuningChangedHandling*)Unsafe.AsPointer(ref this), pPowerTuningChangedListener);
    }
}
