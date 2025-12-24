using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXGPUList.xml' path='doc/member[@name="IADLXGPUList"]/*' />
[NativeTypeName("struct IADLXGPUList : adlx::IADLXList")]
public unsafe partial struct IADLXGPUList
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, int>)(lpVtbl[0]))((IADLXGPUList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, int>)(lpVtbl[1]))((IADLXGPUList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUList*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXList.Size" />
    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, uint>)(lpVtbl[3]))((IADLXGPUList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Empty" />
    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, byte>)(lpVtbl[4]))((IADLXGPUList*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXList.Begin" />
    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, uint>)(lpVtbl[5]))((IADLXGPUList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.End" />
    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, uint>)(lpVtbl[6]))((IADLXGPUList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.At" />
    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, uint, IADLXInterface**, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPUList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    /// <inheritdoc cref="IADLXList.Clear" />
    public ADLX_RESULT Clear()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPUList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Remove_Back" />
    public ADLX_RESULT Remove_Back()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPUList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Add_Back" />
    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, IADLXInterface*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPUList*)Unsafe.AsPointer(ref this), pItem);
    }

    /// <include file='IADLXGPUList.xml' path='doc/member[@name="IADLXGPUList.At"]/*' />
    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXGPU** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, uint, IADLXGPU**, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPUList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    /// <include file='IADLXGPUList.xml' path='doc/member[@name="IADLXGPUList.Add_Back"]/*' />
    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, IADLXGPU*, ADLX_RESULT>)(lpVtbl[12]))((IADLXGPUList*)Unsafe.AsPointer(ref this), pItem);
    }
}
