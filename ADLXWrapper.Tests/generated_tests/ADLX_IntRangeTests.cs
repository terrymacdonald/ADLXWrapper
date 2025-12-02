using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="ADLX_IntRange" /> struct.</summary>
public static unsafe partial class ADLX_IntRangeTests
{
    /// <summary>Validates that the <see cref="ADLX_IntRange" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(ADLX_IntRange), Marshal.SizeOf<ADLX_IntRange>());
    }

    /// <summary>Validates that the <see cref="ADLX_IntRange" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(ADLX_IntRange).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="ADLX_IntRange" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        Assert.Equal(12, sizeof(ADLX_IntRange));
    }
}
