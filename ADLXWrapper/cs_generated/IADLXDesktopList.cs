using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDesktopList.xml' path='doc/member[@name="IADLXDesktopList"]/*' />
[NativeTypeName("struct IADLXDesktopList : adlx::IADLXList")]
public unsafe partial struct IADLXDesktopList
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopList*, int>)(lpVtbl[0]))((IADLXDesktopList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopList*, int>)(lpVtbl[1]))((IADLXDesktopList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopList*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDesktopList*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXList.Size" />
    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopList*, uint>)(lpVtbl[3]))((IADLXDesktopList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Empty" />
    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopList*, byte>)(lpVtbl[4]))((IADLXDesktopList*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXList.Begin" />
    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopList*, uint>)(lpVtbl[5]))((IADLXDesktopList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.End" />
    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopList*, uint>)(lpVtbl[6]))((IADLXDesktopList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.At" />
    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopList*, uint, IADLXInterface**, ADLX_RESULT>)(lpVtbl[7]))((IADLXDesktopList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    /// <inheritdoc cref="IADLXList.Clear" />
    public ADLX_RESULT Clear()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopList*, ADLX_RESULT>)(lpVtbl[8]))((IADLXDesktopList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Remove_Back" />
    public ADLX_RESULT Remove_Back()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopList*, ADLX_RESULT>)(lpVtbl[9]))((IADLXDesktopList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Add_Back" />
    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopList*, IADLXInterface*, ADLX_RESULT>)(lpVtbl[10]))((IADLXDesktopList*)Unsafe.AsPointer(ref this), pItem);
    }

    /// <include file='IADLXDesktopList.xml' path='doc/member[@name="IADLXDesktopList.At"]/*' />
    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXDesktop** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopList*, uint, IADLXDesktop**, ADLX_RESULT>)(lpVtbl[11]))((IADLXDesktopList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    /// <include file='IADLXDesktopList.xml' path='doc/member[@name="IADLXDesktopList.Add_Back"]/*' />
    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXDesktop *")] IADLXDesktop* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopList*, IADLXDesktop*, ADLX_RESULT>)(lpVtbl[12]))((IADLXDesktopList*)Unsafe.AsPointer(ref this), pItem);
    }
}
