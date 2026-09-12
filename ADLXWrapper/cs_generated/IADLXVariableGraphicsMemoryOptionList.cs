using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXVariableGraphicsMemoryOptionList.xml' path='doc/member[@name="IADLXVariableGraphicsMemoryOptionList"]/*' />
[NativeTypeName("struct IADLXVariableGraphicsMemoryOptionList : adlx::IADLXList")]
public unsafe partial struct IADLXVariableGraphicsMemoryOptionList
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemoryOptionList*, int>)(lpVtbl[0]))((IADLXVariableGraphicsMemoryOptionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemoryOptionList*, int>)(lpVtbl[1]))((IADLXVariableGraphicsMemoryOptionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemoryOptionList*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXVariableGraphicsMemoryOptionList*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXList.Size" />
    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemoryOptionList*, uint>)(lpVtbl[3]))((IADLXVariableGraphicsMemoryOptionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Empty" />
    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemoryOptionList*, byte>)(lpVtbl[4]))((IADLXVariableGraphicsMemoryOptionList*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXList.Begin" />
    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemoryOptionList*, uint>)(lpVtbl[5]))((IADLXVariableGraphicsMemoryOptionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.End" />
    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemoryOptionList*, uint>)(lpVtbl[6]))((IADLXVariableGraphicsMemoryOptionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.At" />
    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemoryOptionList*, uint, IADLXInterface**, ADLX_RESULT>)(lpVtbl[7]))((IADLXVariableGraphicsMemoryOptionList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    /// <inheritdoc cref="IADLXList.Clear" />
    public ADLX_RESULT Clear()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemoryOptionList*, ADLX_RESULT>)(lpVtbl[8]))((IADLXVariableGraphicsMemoryOptionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Remove_Back" />
    public ADLX_RESULT Remove_Back()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemoryOptionList*, ADLX_RESULT>)(lpVtbl[9]))((IADLXVariableGraphicsMemoryOptionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Add_Back" />
    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemoryOptionList*, IADLXInterface*, ADLX_RESULT>)(lpVtbl[10]))((IADLXVariableGraphicsMemoryOptionList*)Unsafe.AsPointer(ref this), pItem);
    }

    /// <include file='IADLXVariableGraphicsMemoryOptionList.xml' path='doc/member[@name="IADLXVariableGraphicsMemoryOptionList.At"]/*' />
    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXVariableGraphicsMemoryOption** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemoryOptionList*, uint, IADLXVariableGraphicsMemoryOption**, ADLX_RESULT>)(lpVtbl[11]))((IADLXVariableGraphicsMemoryOptionList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    /// <include file='IADLXVariableGraphicsMemoryOptionList.xml' path='doc/member[@name="IADLXVariableGraphicsMemoryOptionList.Add_Back"]/*' />
    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXVariableGraphicsMemoryOption *")] IADLXVariableGraphicsMemoryOption* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemoryOptionList*, IADLXVariableGraphicsMemoryOption*, ADLX_RESULT>)(lpVtbl[12]))((IADLXVariableGraphicsMemoryOptionList*)Unsafe.AsPointer(ref this), pItem);
    }
}
