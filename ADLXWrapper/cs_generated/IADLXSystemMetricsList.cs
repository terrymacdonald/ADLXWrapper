using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXSystemMetricsList.xml' path='doc/member[@name="IADLXSystemMetricsList"]/*' />
[NativeTypeName("struct IADLXSystemMetricsList : adlx::IADLXList")]
public unsafe partial struct IADLXSystemMetricsList
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, int>)(lpVtbl[0]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, int>)(lpVtbl[1]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXList.Size" />
    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, uint>)(lpVtbl[3]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Empty" />
    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, byte>)(lpVtbl[4]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXList.Begin" />
    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, uint>)(lpVtbl[5]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.End" />
    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, uint>)(lpVtbl[6]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.At" />
    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, uint, IADLXInterface**, ADLX_RESULT>)(lpVtbl[7]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    /// <inheritdoc cref="IADLXList.Clear" />
    public ADLX_RESULT Clear()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, ADLX_RESULT>)(lpVtbl[8]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Remove_Back" />
    public ADLX_RESULT Remove_Back()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, ADLX_RESULT>)(lpVtbl[9]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Add_Back" />
    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, IADLXInterface*, ADLX_RESULT>)(lpVtbl[10]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this), pItem);
    }

    /// <include file='IADLXSystemMetricsList.xml' path='doc/member[@name="IADLXSystemMetricsList.At"]/*' />
    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXSystemMetrics** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, uint, IADLXSystemMetrics**, ADLX_RESULT>)(lpVtbl[11]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    /// <include file='IADLXSystemMetricsList.xml' path='doc/member[@name="IADLXSystemMetricsList.Add_Back"]/*' />
    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXSystemMetrics *")] IADLXSystemMetrics* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, IADLXSystemMetrics*, ADLX_RESULT>)(lpVtbl[12]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this), pItem);
    }
}
