using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplayFreeSync.xml' path='doc/member[@name="IADLXDisplayFreeSync"]/*' />
[NativeTypeName("struct IADLXDisplayFreeSync : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayFreeSync
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayFreeSync*, int>)(lpVtbl[0]))((IADLXDisplayFreeSync*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayFreeSync*, int>)(lpVtbl[1]))((IADLXDisplayFreeSync*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayFreeSync*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayFreeSync*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXDisplayFreeSync.xml' path='doc/member[@name="IADLXDisplayFreeSync.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayFreeSync*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayFreeSync*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXDisplayFreeSync.xml' path='doc/member[@name="IADLXDisplayFreeSync.IsEnabled"]/*' />
    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayFreeSync*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayFreeSync*)Unsafe.AsPointer(ref this), enabled);
    }

    /// <include file='IADLXDisplayFreeSync.xml' path='doc/member[@name="IADLXDisplayFreeSync.SetEnabled"]/*' />
    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayFreeSync*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayFreeSync*)Unsafe.AsPointer(ref this), enabled);
    }
}
