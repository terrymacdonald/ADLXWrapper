using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXGPUTuningChangedHandling.xml' path='doc/member[@name="IADLXGPUTuningChangedHandling"]/*' />
[NativeTypeName("struct IADLXGPUTuningChangedHandling : adlx::IADLXInterface")]
public unsafe partial struct IADLXGPUTuningChangedHandling
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedHandling*, int>)(lpVtbl[0]))((IADLXGPUTuningChangedHandling*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedHandling*, int>)(lpVtbl[1]))((IADLXGPUTuningChangedHandling*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedHandling*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUTuningChangedHandling*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXGPUTuningChangedHandling.xml' path='doc/member[@name="IADLXGPUTuningChangedHandling.AddGPUTuningEventListener"]/*' />
    public ADLX_RESULT AddGPUTuningEventListener([NativeTypeName("adlx::IADLXGPUTuningChangedListener *")] IADLXGPUTuningChangedListener* pGPUTuningChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedHandling*, IADLXGPUTuningChangedListener*, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPUTuningChangedHandling*)Unsafe.AsPointer(ref this), pGPUTuningChangedListener);
    }

    /// <include file='IADLXGPUTuningChangedHandling.xml' path='doc/member[@name="IADLXGPUTuningChangedHandling.RemoveGPUTuningEventListener"]/*' />
    public ADLX_RESULT RemoveGPUTuningEventListener([NativeTypeName("adlx::IADLXGPUTuningChangedListener *")] IADLXGPUTuningChangedListener* pGPUTuningChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedHandling*, IADLXGPUTuningChangedListener*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPUTuningChangedHandling*)Unsafe.AsPointer(ref this), pGPUTuningChangedListener);
    }
}
