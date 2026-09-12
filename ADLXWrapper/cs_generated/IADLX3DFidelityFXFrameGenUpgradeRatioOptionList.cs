using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLX3DFidelityFXFrameGenUpgradeRatioOptionList.xml' path='doc/member[@name="IADLX3DFidelityFXFrameGenUpgradeRatioOptionList"]/*' />
[NativeTypeName("struct IADLX3DFidelityFXFrameGenUpgradeRatioOptionList : adlx::IADLXList")]
public unsafe partial struct IADLX3DFidelityFXFrameGenUpgradeRatioOptionList
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*, int>)(lpVtbl[0]))((IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*, int>)(lpVtbl[1]))((IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXList.Size" />
    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*, uint>)(lpVtbl[3]))((IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Empty" />
    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*, byte>)(lpVtbl[4]))((IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXList.Begin" />
    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*, uint>)(lpVtbl[5]))((IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.End" />
    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*, uint>)(lpVtbl[6]))((IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.At" />
    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*, uint, IADLXInterface**, ADLX_RESULT>)(lpVtbl[7]))((IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    /// <inheritdoc cref="IADLXList.Clear" />
    public ADLX_RESULT Clear()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*, ADLX_RESULT>)(lpVtbl[8]))((IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Remove_Back" />
    public ADLX_RESULT Remove_Back()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*, ADLX_RESULT>)(lpVtbl[9]))((IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Add_Back" />
    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*, IADLXInterface*, ADLX_RESULT>)(lpVtbl[10]))((IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*)Unsafe.AsPointer(ref this), pItem);
    }

    /// <include file='IADLX3DFidelityFXFrameGenUpgradeRatioOptionList.xml' path='doc/member[@name="IADLX3DFidelityFXFrameGenUpgradeRatioOptionList.At"]/*' />
    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLX3DFidelityFXFrameGenUpgradeRatioOption** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*, uint, IADLX3DFidelityFXFrameGenUpgradeRatioOption**, ADLX_RESULT>)(lpVtbl[11]))((IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    /// <include file='IADLX3DFidelityFXFrameGenUpgradeRatioOptionList.xml' path='doc/member[@name="IADLX3DFidelityFXFrameGenUpgradeRatioOptionList.Add_Back"]/*' />
    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLX3DFidelityFXFrameGenUpgradeRatioOption *")] IADLX3DFidelityFXFrameGenUpgradeRatioOption* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*, IADLX3DFidelityFXFrameGenUpgradeRatioOption*, ADLX_RESULT>)(lpVtbl[12]))((IADLX3DFidelityFXFrameGenUpgradeRatioOptionList*)Unsafe.AsPointer(ref this), pItem);
    }
}
