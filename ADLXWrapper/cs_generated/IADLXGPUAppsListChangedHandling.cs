using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXGPUAppsListChangedHandling.xml' path='doc/member[@name="IADLXGPUAppsListChangedHandling"]/*' />
[NativeTypeName("struct IADLXGPUAppsListChangedHandling : adlx::IADLXInterface")]
public unsafe partial struct IADLXGPUAppsListChangedHandling
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAppsListChangedHandling*, int>)(lpVtbl[0]))((IADLXGPUAppsListChangedHandling*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAppsListChangedHandling*, int>)(lpVtbl[1]))((IADLXGPUAppsListChangedHandling*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAppsListChangedHandling*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUAppsListChangedHandling*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXGPUAppsListChangedHandling.xml' path='doc/member[@name="IADLXGPUAppsListChangedHandling.AddGPUAppsListEventListener"]/*' />
    public ADLX_RESULT AddGPUAppsListEventListener([NativeTypeName("adlx::IADLXGPUAppsListEventListener *")] IADLXGPUAppsListEventListener* pGPUAppsListEventListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAppsListChangedHandling*, IADLXGPUAppsListEventListener*, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPUAppsListChangedHandling*)Unsafe.AsPointer(ref this), pGPUAppsListEventListener);
    }

    /// <include file='IADLXGPUAppsListChangedHandling.xml' path='doc/member[@name="IADLXGPUAppsListChangedHandling.RemoveGPUAppsListEventListener"]/*' />
    public ADLX_RESULT RemoveGPUAppsListEventListener([NativeTypeName("adlx::IADLXGPUAppsListEventListener *")] IADLXGPUAppsListEventListener* pGPUAppsListEventListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAppsListChangedHandling*, IADLXGPUAppsListEventListener*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPUAppsListChangedHandling*)Unsafe.AsPointer(ref this), pGPUAppsListEventListener);
    }
}
