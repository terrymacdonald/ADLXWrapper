using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='ADLX_3DLUT_Data.xml' path='doc/member[@name="ADLX_3DLUT_Data"]/*' />
public partial struct ADLX_3DLUT_Data
{
    /// <include file='ADLX_3DLUT_Data.xml' path='doc/member[@name="ADLX_3DLUT_Data.data"]/*' />
    [NativeTypeName("ADLX_UINT16_RGB[4913]")]
    public _data_e__FixedBuffer data;

    /// <include file='_data_e__FixedBuffer.xml' path='doc/member[@name="_data_e__FixedBuffer"]/*' />
    [InlineArray(4913)]
    public partial struct _data_e__FixedBuffer
    {
        public ADLX_UINT16_RGB e0;
    }
}
