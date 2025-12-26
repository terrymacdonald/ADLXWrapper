using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXManualTuningStateList.xml' path='doc/member[@name="IADLXManualTuningStateList"]/*' />
[NativeTypeName("struct IADLXManualTuningStateList : adlx::IADLXList")]
public unsafe partial struct IADLXManualTuningStateList
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, int>)(lpVtbl[0]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, int>)(lpVtbl[1]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXList.Size" />
    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, uint>)(lpVtbl[3]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Empty" />
    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, byte>)(lpVtbl[4]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXList.Begin" />
    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, uint>)(lpVtbl[5]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.End" />
    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, uint>)(lpVtbl[6]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.At" />
    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, uint, IADLXInterface**, ADLX_RESULT>)(lpVtbl[7]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    /// <inheritdoc cref="IADLXList.Clear" />
    public ADLX_RESULT Clear()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, ADLX_RESULT>)(lpVtbl[8]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Remove_Back" />
    public ADLX_RESULT Remove_Back()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, ADLX_RESULT>)(lpVtbl[9]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Add_Back" />
    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, IADLXInterface*, ADLX_RESULT>)(lpVtbl[10]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this), pItem);
    }

    /// <include file='IADLXManualTuningStateList.xml' path='doc/member[@name="IADLXManualTuningStateList.At"]/*' />
    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXManualTuningState** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, uint, IADLXManualTuningState**, ADLX_RESULT>)(lpVtbl[11]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    /// <include file='IADLXManualTuningStateList.xml' path='doc/member[@name="IADLXManualTuningStateList.Add_Back"]/*' />
    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXManualTuningState *")] IADLXManualTuningState* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, IADLXManualTuningState*, ADLX_RESULT>)(lpVtbl[12]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this), pItem);
    }
}
