using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="ADLX_CustomResolution" /> struct.</summary>
public static unsafe partial class ADLX_CustomResolutionTests
{
    /// <summary>Validates that the <see cref="ADLX_CustomResolution" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(ADLX_CustomResolution), Marshal.SizeOf<ADLX_CustomResolution>());
    }

    /// <summary>Validates that the <see cref="ADLX_CustomResolution" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(ADLX_CustomResolution).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="ADLX_CustomResolution" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        Assert.Equal(68, sizeof(ADLX_CustomResolution));
    }
}
