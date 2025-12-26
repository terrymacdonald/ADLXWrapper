using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplayDynamicRefreshRateControl.xml' path='doc/member[@name="IADLXDisplayDynamicRefreshRateControl"]/*' />
[NativeTypeName("struct IADLXDisplayDynamicRefreshRateControl : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayDynamicRefreshRateControl
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayDynamicRefreshRateControl*, int>)(lpVtbl[0]))((IADLXDisplayDynamicRefreshRateControl*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayDynamicRefreshRateControl*, int>)(lpVtbl[1]))((IADLXDisplayDynamicRefreshRateControl*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayDynamicRefreshRateControl*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayDynamicRefreshRateControl*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXDisplayDynamicRefreshRateControl.xml' path='doc/member[@name="IADLXDisplayDynamicRefreshRateControl.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayDynamicRefreshRateControl*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayDynamicRefreshRateControl*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXDisplayDynamicRefreshRateControl.xml' path='doc/member[@name="IADLXDisplayDynamicRefreshRateControl.IsEnabled"]/*' />
    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayDynamicRefreshRateControl*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayDynamicRefreshRateControl*)Unsafe.AsPointer(ref this), enabled);
    }

    /// <include file='IADLXDisplayDynamicRefreshRateControl.xml' path='doc/member[@name="IADLXDisplayDynamicRefreshRateControl.SetEnabled"]/*' />
    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayDynamicRefreshRateControl*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayDynamicRefreshRateControl*)Unsafe.AsPointer(ref this), enabled);
    }
}
