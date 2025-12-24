using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXSystem1.xml' path='doc/member[@name="IADLXSystem1"]/*' />
[NativeTypeName("struct IADLXSystem1 : adlx::IADLXInterface")]
public unsafe partial struct IADLXSystem1
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem1*, int>)(lpVtbl[0]))((IADLXSystem1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem1*, int>)(lpVtbl[1]))((IADLXSystem1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXSystem1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXSystem1.xml' path='doc/member[@name="IADLXSystem1.GetPowerTuningServices"]/*' />
    public ADLX_RESULT GetPowerTuningServices(IADLXPowerTuningServices** ppPowerTuningServices)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem1*, IADLXPowerTuningServices**, ADLX_RESULT>)(lpVtbl[3]))((IADLXSystem1*)Unsafe.AsPointer(ref this), ppPowerTuningServices);
    }
}
