using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLX3DBoost.xml' path='doc/member[@name="IADLX3DBoost"]/*' />
[NativeTypeName("struct IADLX3DBoost : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DBoost
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DBoost*, int>)(lpVtbl[0]))((IADLX3DBoost*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DBoost*, int>)(lpVtbl[1]))((IADLX3DBoost*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DBoost*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DBoost*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLX3DBoost.xml' path='doc/member[@name="IADLX3DBoost.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DBoost*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DBoost*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLX3DBoost.xml' path='doc/member[@name="IADLX3DBoost.IsEnabled"]/*' />
    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* isEnabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DBoost*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DBoost*)Unsafe.AsPointer(ref this), isEnabled);
    }

    /// <include file='IADLX3DBoost.xml' path='doc/member[@name="IADLX3DBoost.GetResolutionRange"]/*' />
    public ADLX_RESULT GetResolutionRange(ADLX_IntRange* range)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DBoost*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DBoost*)Unsafe.AsPointer(ref this), range);
    }

    /// <include file='IADLX3DBoost.xml' path='doc/member[@name="IADLX3DBoost.GetResolution"]/*' />
    public ADLX_RESULT GetResolution([NativeTypeName("adlx_int *")] int* currentMinRes)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DBoost*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLX3DBoost*)Unsafe.AsPointer(ref this), currentMinRes);
    }

    /// <include file='IADLX3DBoost.xml' path='doc/member[@name="IADLX3DBoost.SetEnabled"]/*' />
    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DBoost*, byte, ADLX_RESULT>)(lpVtbl[7]))((IADLX3DBoost*)Unsafe.AsPointer(ref this), enable);
    }

    /// <include file='IADLX3DBoost.xml' path='doc/member[@name="IADLX3DBoost.SetResolution"]/*' />
    public ADLX_RESULT SetResolution([NativeTypeName("adlx_int")] int minRes)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DBoost*, int, ADLX_RESULT>)(lpVtbl[8]))((IADLX3DBoost*)Unsafe.AsPointer(ref this), minRes);
    }
}
