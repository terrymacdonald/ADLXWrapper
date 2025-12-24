using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="ADLX_RegammaCoeff" /> struct.</summary>
public static unsafe partial class ADLX_RegammaCoeffTests
{
    /// <summary>Validates that the <see cref="ADLX_RegammaCoeff" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(ADLX_RegammaCoeff), Marshal.SizeOf<ADLX_RegammaCoeff>());
    }

    /// <summary>Validates that the <see cref="ADLX_RegammaCoeff" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(ADLX_RegammaCoeff).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="ADLX_RegammaCoeff" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        Assert.Equal(20, sizeof(ADLX_RegammaCoeff));
    }
}
