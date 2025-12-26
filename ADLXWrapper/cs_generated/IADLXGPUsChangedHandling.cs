using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXGPUsChangedHandling.xml' path='doc/member[@name="IADLXGPUsChangedHandling"]/*' />
[NativeTypeName("struct IADLXGPUsChangedHandling : adlx::IADLXInterface")]
public unsafe partial struct IADLXGPUsChangedHandling
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUsChangedHandling*, int>)(lpVtbl[0]))((IADLXGPUsChangedHandling*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUsChangedHandling*, int>)(lpVtbl[1]))((IADLXGPUsChangedHandling*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUsChangedHandling*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUsChangedHandling*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXGPUsChangedHandling.xml' path='doc/member[@name="IADLXGPUsChangedHandling.AddGPUsListEventListener"]/*' />
    public ADLX_RESULT AddGPUsListEventListener([NativeTypeName("adlx::IADLXGPUsEventListener *")] IADLXGPUsEventListener* pListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUsChangedHandling*, IADLXGPUsEventListener*, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPUsChangedHandling*)Unsafe.AsPointer(ref this), pListener);
    }

    /// <include file='IADLXGPUsChangedHandling.xml' path='doc/member[@name="IADLXGPUsChangedHandling.RemoveGPUsListEventListener"]/*' />
    public ADLX_RESULT RemoveGPUsListEventListener([NativeTypeName("adlx::IADLXGPUsEventListener *")] IADLXGPUsEventListener* pListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUsChangedHandling*, IADLXGPUsEventListener*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPUsChangedHandling*)Unsafe.AsPointer(ref this), pListener);
    }
}
