using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXSmartAccessMemory.xml' path='doc/member[@name="IADLXSmartAccessMemory"]/*' />
[NativeTypeName("struct IADLXSmartAccessMemory : adlx::IADLXInterface")]
public unsafe partial struct IADLXSmartAccessMemory
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartAccessMemory*, int>)(lpVtbl[0]))((IADLXSmartAccessMemory*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartAccessMemory*, int>)(lpVtbl[1]))((IADLXSmartAccessMemory*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartAccessMemory*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXSmartAccessMemory*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXSmartAccessMemory.xml' path='doc/member[@name="IADLXSmartAccessMemory.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartAccessMemory*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXSmartAccessMemory*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXSmartAccessMemory.xml' path='doc/member[@name="IADLXSmartAccessMemory.IsEnabled"]/*' />
    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartAccessMemory*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXSmartAccessMemory*)Unsafe.AsPointer(ref this), enabled);
    }

    /// <include file='IADLXSmartAccessMemory.xml' path='doc/member[@name="IADLXSmartAccessMemory.SetEnabled"]/*' />
    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartAccessMemory*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLXSmartAccessMemory*)Unsafe.AsPointer(ref this), enabled);
    }
}
