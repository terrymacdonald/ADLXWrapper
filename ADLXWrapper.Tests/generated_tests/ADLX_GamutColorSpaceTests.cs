using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="ADLX_GamutColorSpace" /> struct.</summary>
public static unsafe partial class ADLX_GamutColorSpaceTests
{
    /// <summary>Validates that the <see cref="ADLX_GamutColorSpace" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(ADLX_GamutColorSpace), Marshal.SizeOf<ADLX_GamutColorSpace>());
    }

    /// <summary>Validates that the <see cref="ADLX_GamutColorSpace" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(ADLX_GamutColorSpace).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="ADLX_GamutColorSpace" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        Assert.Equal(24, sizeof(ADLX_GamutColorSpace));
    }
}

