namespace ADLXWrapper;

/// <include file='ADLX_IntRange.xml' path='doc/member[@name="ADLX_IntRange"]/*' />
public partial struct ADLX_IntRange
{
    /// <include file='ADLX_IntRange.xml' path='doc/member[@name="ADLX_IntRange.minValue"]/*' />
    [NativeTypeName("adlx_int")]
    public int minValue;

    /// <include file='ADLX_IntRange.xml' path='doc/member[@name="ADLX_IntRange.maxValue"]/*' />
    [NativeTypeName("adlx_int")]
    public int maxValue;

    /// <include file='ADLX_IntRange.xml' path='doc/member[@name="ADLX_IntRange.step"]/*' />
    [NativeTypeName("adlx_int")]
    public int step;
}
