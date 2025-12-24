using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='ADLX_GammaRamp.xml' path='doc/member[@name="ADLX_GammaRamp"]/*' />
public partial struct ADLX_GammaRamp
{
    /// <include file='ADLX_GammaRamp.xml' path='doc/member[@name="ADLX_GammaRamp.gamma"]/*' />
    [NativeTypeName("adlx_uint16[768]")]
    public _gamma_e__FixedBuffer gamma;

    /// <include file='_gamma_e__FixedBuffer.xml' path='doc/member[@name="_gamma_e__FixedBuffer"]/*' />
    [InlineArray(768)]
    public partial struct _gamma_e__FixedBuffer
    {
        public ushort e0;
    }
}
