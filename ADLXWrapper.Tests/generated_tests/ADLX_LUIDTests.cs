using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="ADLX_LUID" /> struct.</summary>
public static unsafe partial class ADLX_LUIDTests
{
    /// <summary>Validates that the <see cref="ADLX_LUID" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(ADLX_LUID), Marshal.SizeOf<ADLX_LUID>());
    }

    /// <summary>Validates that the <see cref="ADLX_LUID" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(ADLX_LUID).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="ADLX_LUID" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        Assert.Equal(8, sizeof(ADLX_LUID));
    }
}
