using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXManualFanTuningStateList.xml' path='doc/member[@name="IADLXManualFanTuningStateList"]/*' />
[NativeTypeName("struct IADLXManualFanTuningStateList : adlx::IADLXList")]
public unsafe partial struct IADLXManualFanTuningStateList
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, int>)(lpVtbl[0]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, int>)(lpVtbl[1]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXList.Size" />
    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, uint>)(lpVtbl[3]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Empty" />
    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, byte>)(lpVtbl[4]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXList.Begin" />
    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, uint>)(lpVtbl[5]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.End" />
    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, uint>)(lpVtbl[6]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.At" />
    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, uint, IADLXInterface**, ADLX_RESULT>)(lpVtbl[7]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    /// <inheritdoc cref="IADLXList.Clear" />
    public ADLX_RESULT Clear()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, ADLX_RESULT>)(lpVtbl[8]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Remove_Back" />
    public ADLX_RESULT Remove_Back()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, ADLX_RESULT>)(lpVtbl[9]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Add_Back" />
    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, IADLXInterface*, ADLX_RESULT>)(lpVtbl[10]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this), pItem);
    }

    /// <include file='IADLXManualFanTuningStateList.xml' path='doc/member[@name="IADLXManualFanTuningStateList.At"]/*' />
    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXManualFanTuningState** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, uint, IADLXManualFanTuningState**, ADLX_RESULT>)(lpVtbl[11]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    /// <include file='IADLXManualFanTuningStateList.xml' path='doc/member[@name="IADLXManualFanTuningStateList.Add_Back"]/*' />
    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXManualFanTuningState *")] IADLXManualFanTuningState* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, IADLXManualFanTuningState*, ADLX_RESULT>)(lpVtbl[12]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this), pItem);
    }
}
