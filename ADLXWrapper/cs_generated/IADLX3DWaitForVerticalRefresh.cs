using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLX3DWaitForVerticalRefresh.xml' path='doc/member[@name="IADLX3DWaitForVerticalRefresh"]/*' />
[NativeTypeName("struct IADLX3DWaitForVerticalRefresh : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DWaitForVerticalRefresh
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DWaitForVerticalRefresh*, int>)(lpVtbl[0]))((IADLX3DWaitForVerticalRefresh*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DWaitForVerticalRefresh*, int>)(lpVtbl[1]))((IADLX3DWaitForVerticalRefresh*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DWaitForVerticalRefresh*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DWaitForVerticalRefresh*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLX3DWaitForVerticalRefresh.xml' path='doc/member[@name="IADLX3DWaitForVerticalRefresh.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DWaitForVerticalRefresh*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DWaitForVerticalRefresh*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLX3DWaitForVerticalRefresh.xml' path='doc/member[@name="IADLX3DWaitForVerticalRefresh.IsEnabled"]/*' />
    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* isEnabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DWaitForVerticalRefresh*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DWaitForVerticalRefresh*)Unsafe.AsPointer(ref this), isEnabled);
    }

    /// <include file='IADLX3DWaitForVerticalRefresh.xml' path='doc/member[@name="IADLX3DWaitForVerticalRefresh.GetMode"]/*' />
    public ADLX_RESULT GetMode(ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE* currentMode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DWaitForVerticalRefresh*, ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE*, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DWaitForVerticalRefresh*)Unsafe.AsPointer(ref this), currentMode);
    }

    /// <include file='IADLX3DWaitForVerticalRefresh.xml' path='doc/member[@name="IADLX3DWaitForVerticalRefresh.SetMode"]/*' />
    public ADLX_RESULT SetMode(ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE mode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DWaitForVerticalRefresh*, ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE, ADLX_RESULT>)(lpVtbl[6]))((IADLX3DWaitForVerticalRefresh*)Unsafe.AsPointer(ref this), mode);
    }
}
