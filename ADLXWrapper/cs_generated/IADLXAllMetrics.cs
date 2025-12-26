using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXAllMetrics.xml' path='doc/member[@name="IADLXAllMetrics"]/*' />
[NativeTypeName("struct IADLXAllMetrics : adlx::IADLXInterface")]
public unsafe partial struct IADLXAllMetrics
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetrics*, int>)(lpVtbl[0]))((IADLXAllMetrics*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetrics*, int>)(lpVtbl[1]))((IADLXAllMetrics*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetrics*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXAllMetrics*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXAllMetrics.xml' path='doc/member[@name="IADLXAllMetrics.TimeStamp"]/*' />
    public ADLX_RESULT TimeStamp([NativeTypeName("adlx_int64 *")] long* ms)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetrics*, long*, ADLX_RESULT>)(lpVtbl[3]))((IADLXAllMetrics*)Unsafe.AsPointer(ref this), ms);
    }

    /// <include file='IADLXAllMetrics.xml' path='doc/member[@name="IADLXAllMetrics.GetSystemMetrics"]/*' />
    public ADLX_RESULT GetSystemMetrics(IADLXSystemMetrics** ppSystemMetrics)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetrics*, IADLXSystemMetrics**, ADLX_RESULT>)(lpVtbl[4]))((IADLXAllMetrics*)Unsafe.AsPointer(ref this), ppSystemMetrics);
    }

    /// <include file='IADLXAllMetrics.xml' path='doc/member[@name="IADLXAllMetrics.GetFPS"]/*' />
    public ADLX_RESULT GetFPS(IADLXFPS** ppFPS)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetrics*, IADLXFPS**, ADLX_RESULT>)(lpVtbl[5]))((IADLXAllMetrics*)Unsafe.AsPointer(ref this), ppFPS);
    }

    /// <include file='IADLXAllMetrics.xml' path='doc/member[@name="IADLXAllMetrics.GetGPUMetrics"]/*' />
    public ADLX_RESULT GetGPUMetrics([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXGPUMetrics** ppGPUMetrics)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetrics*, IADLXGPU*, IADLXGPUMetrics**, ADLX_RESULT>)(lpVtbl[6]))((IADLXAllMetrics*)Unsafe.AsPointer(ref this), pGPU, ppGPUMetrics);
    }
}
