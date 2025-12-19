using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXGPU" /> struct.</summary>
public static unsafe partial class IADLXGPUTests
{
    /// <summary>Validates that the <see cref="IADLXGPU" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXGPU), Marshal.SizeOf<IADLXGPU>());
    }

    /// <summary>Validates that the <see cref="IADLXGPU" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXGPU).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXGPU" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXGPU));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXGPU));
        }
    }
}

