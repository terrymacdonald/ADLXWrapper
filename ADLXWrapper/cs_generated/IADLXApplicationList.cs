using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXApplicationList.xml' path='doc/member[@name="IADLXApplicationList"]/*' />
[NativeTypeName("struct IADLXApplicationList : adlx::IADLXList")]
public unsafe partial struct IADLXApplicationList
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, int>)(lpVtbl[0]))((IADLXApplicationList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, int>)(lpVtbl[1]))((IADLXApplicationList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXApplicationList*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXList.Size" />
    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, uint>)(lpVtbl[3]))((IADLXApplicationList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Empty" />
    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, byte>)(lpVtbl[4]))((IADLXApplicationList*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXList.Begin" />
    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, uint>)(lpVtbl[5]))((IADLXApplicationList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.End" />
    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, uint>)(lpVtbl[6]))((IADLXApplicationList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.At" />
    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, uint, IADLXInterface**, ADLX_RESULT>)(lpVtbl[7]))((IADLXApplicationList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    /// <inheritdoc cref="IADLXList.Clear" />
    public ADLX_RESULT Clear()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, ADLX_RESULT>)(lpVtbl[8]))((IADLXApplicationList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Remove_Back" />
    public ADLX_RESULT Remove_Back()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, ADLX_RESULT>)(lpVtbl[9]))((IADLXApplicationList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Add_Back" />
    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, IADLXInterface*, ADLX_RESULT>)(lpVtbl[10]))((IADLXApplicationList*)Unsafe.AsPointer(ref this), pItem);
    }

    /// <include file='IADLXApplicationList.xml' path='doc/member[@name="IADLXApplicationList.At"]/*' />
    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXApplication** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, uint, IADLXApplication**, ADLX_RESULT>)(lpVtbl[11]))((IADLXApplicationList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    /// <include file='IADLXApplicationList.xml' path='doc/member[@name="IADLXApplicationList.Add_Back"]/*' />
    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXApplication *")] IADLXApplication* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, IADLXApplication*, ADLX_RESULT>)(lpVtbl[12]))((IADLXApplicationList*)Unsafe.AsPointer(ref this), pItem);
    }
}
