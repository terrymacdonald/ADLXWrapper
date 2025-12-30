using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXGPU2List" /> struct.</summary>
public static unsafe partial class IADLXGPU2ListTests
{
    /// <summary>Validates that the <see cref="IADLXGPU2List" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXGPU2List), Marshal.SizeOf<IADLXGPU2List>());
    }

    /// <summary>Validates that the <see cref="IADLXGPU2List" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXGPU2List).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXGPU2List" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXGPU2List));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXGPU2List));
        }
    }
}
