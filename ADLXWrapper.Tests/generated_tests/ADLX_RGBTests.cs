using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="ADLX_RGB" /> struct.</summary>
public static unsafe partial class ADLX_RGBTests
{
    /// <summary>Validates that the <see cref="ADLX_RGB" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(ADLX_RGB), Marshal.SizeOf<ADLX_RGB>());
    }

    /// <summary>Validates that the <see cref="ADLX_RGB" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(ADLX_RGB).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="ADLX_RGB" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        Assert.Equal(24, sizeof(ADLX_RGB));
    }
}
