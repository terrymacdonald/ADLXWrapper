using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXGPU1" /> struct.</summary>
public static unsafe partial class IADLXGPU1Tests
{
    /// <summary>Validates that the <see cref="IADLXGPU1" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXGPU1), Marshal.SizeOf<IADLXGPU1>());
    }

    /// <summary>Validates that the <see cref="IADLXGPU1" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXGPU1).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXGPU1" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXGPU1));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXGPU1));
        }
    }
}

