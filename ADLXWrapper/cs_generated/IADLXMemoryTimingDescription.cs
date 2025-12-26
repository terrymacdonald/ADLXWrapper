using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXMemoryTimingDescription.xml' path='doc/member[@name="IADLXMemoryTimingDescription"]/*' />
[NativeTypeName("struct IADLXMemoryTimingDescription : adlx::IADLXInterface")]
public unsafe partial struct IADLXMemoryTimingDescription
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMemoryTimingDescription*, int>)(lpVtbl[0]))((IADLXMemoryTimingDescription*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMemoryTimingDescription*, int>)(lpVtbl[1]))((IADLXMemoryTimingDescription*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMemoryTimingDescription*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXMemoryTimingDescription*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXMemoryTimingDescription.xml' path='doc/member[@name="IADLXMemoryTimingDescription.GetDescription"]/*' />
    public ADLX_RESULT GetDescription(ADLX_MEMORYTIMING_DESCRIPTION* description)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMemoryTimingDescription*, ADLX_MEMORYTIMING_DESCRIPTION*, ADLX_RESULT>)(lpVtbl[3]))((IADLXMemoryTimingDescription*)Unsafe.AsPointer(ref this), description);
    }
}
