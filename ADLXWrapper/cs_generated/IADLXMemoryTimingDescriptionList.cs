using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXMemoryTimingDescriptionList.xml' path='doc/member[@name="IADLXMemoryTimingDescriptionList"]/*' />
[NativeTypeName("struct IADLXMemoryTimingDescriptionList : adlx::IADLXList")]
public unsafe partial struct IADLXMemoryTimingDescriptionList
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMemoryTimingDescriptionList*, int>)(lpVtbl[0]))((IADLXMemoryTimingDescriptionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMemoryTimingDescriptionList*, int>)(lpVtbl[1]))((IADLXMemoryTimingDescriptionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMemoryTimingDescriptionList*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXMemoryTimingDescriptionList*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXList.Size" />
    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMemoryTimingDescriptionList*, uint>)(lpVtbl[3]))((IADLXMemoryTimingDescriptionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Empty" />
    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMemoryTimingDescriptionList*, byte>)(lpVtbl[4]))((IADLXMemoryTimingDescriptionList*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXList.Begin" />
    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMemoryTimingDescriptionList*, uint>)(lpVtbl[5]))((IADLXMemoryTimingDescriptionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.End" />
    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMemoryTimingDescriptionList*, uint>)(lpVtbl[6]))((IADLXMemoryTimingDescriptionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.At" />
    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMemoryTimingDescriptionList*, uint, IADLXInterface**, ADLX_RESULT>)(lpVtbl[7]))((IADLXMemoryTimingDescriptionList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    /// <inheritdoc cref="IADLXList.Clear" />
    public ADLX_RESULT Clear()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMemoryTimingDescriptionList*, ADLX_RESULT>)(lpVtbl[8]))((IADLXMemoryTimingDescriptionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Remove_Back" />
    public ADLX_RESULT Remove_Back()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMemoryTimingDescriptionList*, ADLX_RESULT>)(lpVtbl[9]))((IADLXMemoryTimingDescriptionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Add_Back" />
    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMemoryTimingDescriptionList*, IADLXInterface*, ADLX_RESULT>)(lpVtbl[10]))((IADLXMemoryTimingDescriptionList*)Unsafe.AsPointer(ref this), pItem);
    }

    /// <include file='IADLXMemoryTimingDescriptionList.xml' path='doc/member[@name="IADLXMemoryTimingDescriptionList.At"]/*' />
    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXMemoryTimingDescription** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMemoryTimingDescriptionList*, uint, IADLXMemoryTimingDescription**, ADLX_RESULT>)(lpVtbl[11]))((IADLXMemoryTimingDescriptionList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    /// <include file='IADLXMemoryTimingDescriptionList.xml' path='doc/member[@name="IADLXMemoryTimingDescriptionList.Add_Back"]/*' />
    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXMemoryTimingDescription *")] IADLXMemoryTimingDescription* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMemoryTimingDescriptionList*, IADLXMemoryTimingDescription*, ADLX_RESULT>)(lpVtbl[12]))((IADLXMemoryTimingDescriptionList*)Unsafe.AsPointer(ref this), pItem);
    }
}
