namespace ADLXWrapper;

/// <include file='ADLX_CustomResolution.xml' path='doc/member[@name="ADLX_CustomResolution"]/*' />
public partial struct ADLX_CustomResolution
{
    /// <include file='ADLX_CustomResolution.xml' path='doc/member[@name="ADLX_CustomResolution.resWidth"]/*' />
    [NativeTypeName("adlx_int")]
    public int resWidth;

    /// <include file='ADLX_CustomResolution.xml' path='doc/member[@name="ADLX_CustomResolution.resHeight"]/*' />
    [NativeTypeName("adlx_int")]
    public int resHeight;

    /// <include file='ADLX_CustomResolution.xml' path='doc/member[@name="ADLX_CustomResolution.refreshRate"]/*' />
    [NativeTypeName("adlx_int")]
    public int refreshRate;

    /// <include file='ADLX_CustomResolution.xml' path='doc/member[@name="ADLX_CustomResolution.presentation"]/*' />
    public ADLX_DISPLAY_SCAN_TYPE presentation;

    /// <include file='ADLX_CustomResolution.xml' path='doc/member[@name="ADLX_CustomResolution.timingStandard"]/*' />
    public ADLX_TIMING_STANDARD timingStandard;

    /// <include file='ADLX_CustomResolution.xml' path='doc/member[@name="ADLX_CustomResolution.GPixelClock"]/*' />
    [NativeTypeName("adlx_long")]
    public int GPixelClock;

    /// <include file='ADLX_CustomResolution.xml' path='doc/member[@name="ADLX_CustomResolution.detailedTiming"]/*' />
    public ADLX_TimingInfo detailedTiming;
}
