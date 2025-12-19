using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="ADLX_Point" /> struct.</summary>
public static unsafe partial class ADLX_PointTests
{
    /// <summary>Validates that the <see cref="ADLX_Point" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(ADLX_Point), Marshal.SizeOf<ADLX_Point>());
    }

    /// <summary>Validates that the <see cref="ADLX_Point" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(ADLX_Point).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="ADLX_Point" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        Assert.Equal(8, sizeof(ADLX_Point));
    }
}

