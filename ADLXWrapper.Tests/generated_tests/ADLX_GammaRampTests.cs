using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="ADLX_GammaRamp" /> struct.</summary>
public static unsafe partial class ADLX_GammaRampTests
{
    /// <summary>Validates that the <see cref="ADLX_GammaRamp" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(ADLX_GammaRamp), Marshal.SizeOf<ADLX_GammaRamp>());
    }

    /// <summary>Validates that the <see cref="ADLX_GammaRamp" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(ADLX_GammaRamp).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="ADLX_GammaRamp" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        Assert.Equal(1536, sizeof(ADLX_GammaRamp));
    }
}

