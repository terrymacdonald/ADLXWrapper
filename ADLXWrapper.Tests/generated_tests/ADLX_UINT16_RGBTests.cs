using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="ADLX_UINT16_RGB" /> struct.</summary>
public static unsafe partial class ADLX_UINT16_RGBTests
{
    /// <summary>Validates that the <see cref="ADLX_UINT16_RGB" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(ADLX_UINT16_RGB), Marshal.SizeOf<ADLX_UINT16_RGB>());
    }

    /// <summary>Validates that the <see cref="ADLX_UINT16_RGB" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(ADLX_UINT16_RGB).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="ADLX_UINT16_RGB" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        Assert.Equal(6, sizeof(ADLX_UINT16_RGB));
    }
}

