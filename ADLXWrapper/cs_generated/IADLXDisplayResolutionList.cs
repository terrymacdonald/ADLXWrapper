using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplayResolutionList.xml' path='doc/member[@name="IADLXDisplayResolutionList"]/*' />
[NativeTypeName("struct IADLXDisplayResolutionList : adlx::IADLXList")]
public unsafe partial struct IADLXDisplayResolutionList
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, int>)(lpVtbl[0]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, int>)(lpVtbl[1]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXList.Size" />
    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, uint>)(lpVtbl[3]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Empty" />
    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, byte>)(lpVtbl[4]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXList.Begin" />
    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, uint>)(lpVtbl[5]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.End" />
    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, uint>)(lpVtbl[6]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.At" />
    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, uint, IADLXInterface**, ADLX_RESULT>)(lpVtbl[7]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    /// <inheritdoc cref="IADLXList.Clear" />
    public ADLX_RESULT Clear()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, ADLX_RESULT>)(lpVtbl[8]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Remove_Back" />
    public ADLX_RESULT Remove_Back()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, ADLX_RESULT>)(lpVtbl[9]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Add_Back" />
    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, IADLXInterface*, ADLX_RESULT>)(lpVtbl[10]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this), pItem);
    }

    /// <include file='IADLXDisplayResolutionList.xml' path='doc/member[@name="IADLXDisplayResolutionList.At"]/*' />
    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXDisplayResolution** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, uint, IADLXDisplayResolution**, ADLX_RESULT>)(lpVtbl[11]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    /// <include file='IADLXDisplayResolutionList.xml' path='doc/member[@name="IADLXDisplayResolutionList.Add_Back"]/*' />
    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXDisplayResolution *")] IADLXDisplayResolution* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, IADLXDisplayResolution*, ADLX_RESULT>)(lpVtbl[12]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this), pItem);
    }
}
