using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXGPU2" /> struct.</summary>
public static unsafe partial class IADLXGPU2Tests
{
    /// <summary>Validates that the <see cref="IADLXGPU2" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXGPU2), Marshal.SizeOf<IADLXGPU2>());
    }

    /// <summary>Validates that the <see cref="IADLXGPU2" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXGPU2).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXGPU2" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXGPU2));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXGPU2));
        }
    }
}

