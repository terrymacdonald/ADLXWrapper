using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXGPUMetricsList.xml' path='doc/member[@name="IADLXGPUMetricsList"]/*' />
[NativeTypeName("struct IADLXGPUMetricsList : adlx::IADLXList")]
public unsafe partial struct IADLXGPUMetricsList
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsList*, int>)(lpVtbl[0]))((IADLXGPUMetricsList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsList*, int>)(lpVtbl[1]))((IADLXGPUMetricsList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsList*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUMetricsList*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXList.Size" />
    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsList*, uint>)(lpVtbl[3]))((IADLXGPUMetricsList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Empty" />
    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsList*, byte>)(lpVtbl[4]))((IADLXGPUMetricsList*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXList.Begin" />
    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsList*, uint>)(lpVtbl[5]))((IADLXGPUMetricsList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.End" />
    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsList*, uint>)(lpVtbl[6]))((IADLXGPUMetricsList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.At" />
    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsList*, uint, IADLXInterface**, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPUMetricsList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    /// <inheritdoc cref="IADLXList.Clear" />
    public ADLX_RESULT Clear()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsList*, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPUMetricsList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Remove_Back" />
    public ADLX_RESULT Remove_Back()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsList*, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPUMetricsList*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXList.Add_Back" />
    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsList*, IADLXInterface*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPUMetricsList*)Unsafe.AsPointer(ref this), pItem);
    }

    /// <include file='IADLXGPUMetricsList.xml' path='doc/member[@name="IADLXGPUMetricsList.At"]/*' />
    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXGPUMetrics** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsList*, uint, IADLXGPUMetrics**, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPUMetricsList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    /// <include file='IADLXGPUMetricsList.xml' path='doc/member[@name="IADLXGPUMetricsList.Add_Back"]/*' />
    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXGPUMetrics *")] IADLXGPUMetrics* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsList*, IADLXGPUMetrics*, ADLX_RESULT>)(lpVtbl[12]))((IADLXGPUMetricsList*)Unsafe.AsPointer(ref this), pItem);
    }
}
