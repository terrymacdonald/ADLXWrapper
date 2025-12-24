using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplayResolution.xml' path='doc/member[@name="IADLXDisplayResolution"]/*' />
[NativeTypeName("struct IADLXDisplayResolution : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayResolution
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolution*, int>)(lpVtbl[0]))((IADLXDisplayResolution*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolution*, int>)(lpVtbl[1]))((IADLXDisplayResolution*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolution*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayResolution*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXDisplayResolution.xml' path='doc/member[@name="IADLXDisplayResolution.GetValue"]/*' />
    public ADLX_RESULT GetValue(ADLX_CustomResolution* customResolution)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolution*, ADLX_CustomResolution*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayResolution*)Unsafe.AsPointer(ref this), customResolution);
    }

    /// <include file='IADLXDisplayResolution.xml' path='doc/member[@name="IADLXDisplayResolution.SetValue"]/*' />
    public ADLX_RESULT SetValue(ADLX_CustomResolution customResolution)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolution*, ADLX_CustomResolution, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayResolution*)Unsafe.AsPointer(ref this), customResolution);
    }
}
