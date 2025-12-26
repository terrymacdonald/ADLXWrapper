using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplayList.xml' path='doc/member[@name="IADLXDisplayList"]/*' />
[NativeTypeName("struct IADLXDisplayList : adlx::IADLXList")]
public unsafe partial struct IADLXDisplayList
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayList*, int>)(lpVtbl[0]))((IADLXDisplayList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayList*, int>)(lpVtbl[1]))((IADLXDisplayList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayList*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayList*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXList.Size" />
    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayList*, uint>)(lpVtbl[3]))((IADLXDisplayList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Empty" />
    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayList*, byte>)(lpVtbl[4]))((IADLXDisplayList*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXList.Begin" />
    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayList*, uint>)(lpVtbl[5]))((IADLXDisplayList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.End" />
    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayList*, uint>)(lpVtbl[6]))((IADLXDisplayList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.At" />
    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayList*, uint, IADLXInterface**, ADLX_RESULT>)(lpVtbl[7]))((IADLXDisplayList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    /// <inheritdoc cref="IADLXList.Clear" />
    public ADLX_RESULT Clear()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayList*, ADLX_RESULT>)(lpVtbl[8]))((IADLXDisplayList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Remove_Back" />
    public ADLX_RESULT Remove_Back()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayList*, ADLX_RESULT>)(lpVtbl[9]))((IADLXDisplayList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Add_Back" />
    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayList*, IADLXInterface*, ADLX_RESULT>)(lpVtbl[10]))((IADLXDisplayList*)Unsafe.AsPointer(ref this), pItem);
    }

    /// <include file='IADLXDisplayList.xml' path='doc/member[@name="IADLXDisplayList.At"]/*' />
    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXDisplay** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayList*, uint, IADLXDisplay**, ADLX_RESULT>)(lpVtbl[11]))((IADLXDisplayList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    /// <include file='IADLXDisplayList.xml' path='doc/member[@name="IADLXDisplayList.Add_Back"]/*' />
    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayList*, IADLXDisplay*, ADLX_RESULT>)(lpVtbl[12]))((IADLXDisplayList*)Unsafe.AsPointer(ref this), pItem);
    }
}
