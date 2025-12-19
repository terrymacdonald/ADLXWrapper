using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="ADLX_TimingInfo" /> struct.</summary>
public static unsafe partial class ADLX_TimingInfoTests
{
    /// <summary>Validates that the <see cref="ADLX_TimingInfo" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(ADLX_TimingInfo), Marshal.SizeOf<ADLX_TimingInfo>());
    }

    /// <summary>Validates that the <see cref="ADLX_TimingInfo" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(ADLX_TimingInfo).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="ADLX_TimingInfo" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        Assert.Equal(44, sizeof(ADLX_TimingInfo));
    }
}

