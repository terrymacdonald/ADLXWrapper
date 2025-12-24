using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXSimpleEyefinity.xml' path='doc/member[@name="IADLXSimpleEyefinity"]/*' />
[NativeTypeName("struct IADLXSimpleEyefinity : adlx::IADLXInterface")]
public unsafe partial struct IADLXSimpleEyefinity
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSimpleEyefinity*, int>)(lpVtbl[0]))((IADLXSimpleEyefinity*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSimpleEyefinity*, int>)(lpVtbl[1]))((IADLXSimpleEyefinity*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSimpleEyefinity*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXSimpleEyefinity*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXSimpleEyefinity.xml' path='doc/member[@name="IADLXSimpleEyefinity.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSimpleEyefinity*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXSimpleEyefinity*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXSimpleEyefinity.xml' path='doc/member[@name="IADLXSimpleEyefinity.Create"]/*' />
    public ADLX_RESULT Create(IADLXEyefinityDesktop** ppEyefinityDesktop)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSimpleEyefinity*, IADLXEyefinityDesktop**, ADLX_RESULT>)(lpVtbl[4]))((IADLXSimpleEyefinity*)Unsafe.AsPointer(ref this), ppEyefinityDesktop);
    }

    /// <include file='IADLXSimpleEyefinity.xml' path='doc/member[@name="IADLXSimpleEyefinity.DestroyAll"]/*' />
    public ADLX_RESULT DestroyAll()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSimpleEyefinity*, ADLX_RESULT>)(lpVtbl[5]))((IADLXSimpleEyefinity*)Unsafe.AsPointer(ref this));
    }

    /// <include file='IADLXSimpleEyefinity.xml' path='doc/member[@name="IADLXSimpleEyefinity.Destroy"]/*' />
    public ADLX_RESULT Destroy([NativeTypeName("adlx::IADLXEyefinityDesktop *")] IADLXEyefinityDesktop* pDesktop)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSimpleEyefinity*, IADLXEyefinityDesktop*, ADLX_RESULT>)(lpVtbl[6]))((IADLXSimpleEyefinity*)Unsafe.AsPointer(ref this), pDesktop);
    }
}
