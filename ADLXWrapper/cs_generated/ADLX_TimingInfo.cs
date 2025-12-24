namespace ADLXWrapper;

/// <include file='ADLX_TimingInfo.xml' path='doc/member[@name="ADLX_TimingInfo"]/*' />
public partial struct ADLX_TimingInfo
{
    /// <include file='ADLX_TimingInfo.xml' path='doc/member[@name="ADLX_TimingInfo.timingFlags"]/*' />
    [NativeTypeName("adlx_int")]
    public int timingFlags;

    /// <include file='ADLX_TimingInfo.xml' path='doc/member[@name="ADLX_TimingInfo.hTotal"]/*' />
    [NativeTypeName("adlx_int")]
    public int hTotal;

    /// <include file='ADLX_TimingInfo.xml' path='doc/member[@name="ADLX_TimingInfo.vTotal"]/*' />
    [NativeTypeName("adlx_int")]
    public int vTotal;

    /// <include file='ADLX_TimingInfo.xml' path='doc/member[@name="ADLX_TimingInfo.hDisplay"]/*' />
    [NativeTypeName("adlx_int")]
    public int hDisplay;

    /// <include file='ADLX_TimingInfo.xml' path='doc/member[@name="ADLX_TimingInfo.vDisplay"]/*' />
    [NativeTypeName("adlx_int")]
    public int vDisplay;

    /// <include file='ADLX_TimingInfo.xml' path='doc/member[@name="ADLX_TimingInfo.hFrontPorch"]/*' />
    [NativeTypeName("adlx_int")]
    public int hFrontPorch;

    /// <include file='ADLX_TimingInfo.xml' path='doc/member[@name="ADLX_TimingInfo.vFrontPorch"]/*' />
    [NativeTypeName("adlx_int")]
    public int vFrontPorch;

    /// <include file='ADLX_TimingInfo.xml' path='doc/member[@name="ADLX_TimingInfo.hSyncWidth"]/*' />
    [NativeTypeName("adlx_int")]
    public int hSyncWidth;

    /// <include file='ADLX_TimingInfo.xml' path='doc/member[@name="ADLX_TimingInfo.vSyncWidth"]/*' />
    [NativeTypeName("adlx_int")]
    public int vSyncWidth;

    /// <include file='ADLX_TimingInfo.xml' path='doc/member[@name="ADLX_TimingInfo.hPolarity"]/*' />
    [NativeTypeName("adlx_int")]
    public int hPolarity;

    /// <include file='ADLX_TimingInfo.xml' path='doc/member[@name="ADLX_TimingInfo.vPolarity"]/*' />
    [NativeTypeName("adlx_int")]
    public int vPolarity;
}
