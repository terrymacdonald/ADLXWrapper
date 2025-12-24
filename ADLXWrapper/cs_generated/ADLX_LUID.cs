namespace ADLXWrapper;

/// <include file='ADLX_LUID.xml' path='doc/member[@name="ADLX_LUID"]/*' />
public partial struct ADLX_LUID
{
    /// <include file='ADLX_LUID.xml' path='doc/member[@name="ADLX_LUID.lowPart"]/*' />
    [NativeTypeName("adlx_ulong")]
    public uint lowPart;

    /// <include file='ADLX_LUID.xml' path='doc/member[@name="ADLX_LUID.highPart"]/*' />
    [NativeTypeName("adlx_long")]
    public int highPart;
}
