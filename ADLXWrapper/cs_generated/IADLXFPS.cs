using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXFPS.xml' path='doc/member[@name="IADLXFPS"]/*' />
[NativeTypeName("struct IADLXFPS : adlx::IADLXInterface")]
public unsafe partial struct IADLXFPS
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXFPS*, int>)(lpVtbl[0]))((IADLXFPS*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXFPS*, int>)(lpVtbl[1]))((IADLXFPS*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXFPS*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXFPS*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXFPS.xml' path='doc/member[@name="IADLXFPS.TimeStamp"]/*' />
    public ADLX_RESULT TimeStamp([NativeTypeName("adlx_int64 *")] long* ms)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXFPS*, long*, ADLX_RESULT>)(lpVtbl[3]))((IADLXFPS*)Unsafe.AsPointer(ref this), ms);
    }

    /// <include file='IADLXFPS.xml' path='doc/member[@name="IADLXFPS.FPS"]/*' />
    public ADLX_RESULT FPS([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXFPS*, int*, ADLX_RESULT>)(lpVtbl[4]))((IADLXFPS*)Unsafe.AsPointer(ref this), data);
    }
}
