using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="ADLX_3DLUT_Data" /> struct.</summary>
public static unsafe partial class ADLX_3DLUT_DataTests
{
    /// <summary>Validates that the <see cref="ADLX_3DLUT_Data" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(ADLX_3DLUT_Data), Marshal.SizeOf<ADLX_3DLUT_Data>());
    }

    /// <summary>Validates that the <see cref="ADLX_3DLUT_Data" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(ADLX_3DLUT_Data).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="ADLX_3DLUT_Data" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        Assert.Equal(29478, sizeof(ADLX_3DLUT_Data));
    }
}
