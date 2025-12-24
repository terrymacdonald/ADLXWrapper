namespace ADLXWrapper;

/// <include file='ADLX_RGB.xml' path='doc/member[@name="ADLX_RGB"]/*' />
public partial struct ADLX_RGB
{
    /// <include file='ADLX_RGB.xml' path='doc/member[@name="ADLX_RGB.gamutR"]/*' />
    [NativeTypeName("adlx_double")]
    public double gamutR;

    /// <include file='ADLX_RGB.xml' path='doc/member[@name="ADLX_RGB.gamutG"]/*' />
    [NativeTypeName("adlx_double")]
    public double gamutG;

    /// <include file='ADLX_RGB.xml' path='doc/member[@name="ADLX_RGB.gamutB"]/*' />
    [NativeTypeName("adlx_double")]
    public double gamutB;
}
