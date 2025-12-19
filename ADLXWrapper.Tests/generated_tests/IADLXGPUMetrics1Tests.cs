using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXGPUMetrics1" /> struct.</summary>
public static unsafe partial class IADLXGPUMetrics1Tests
{
    /// <summary>Validates that the <see cref="IADLXGPUMetrics1" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXGPUMetrics1), Marshal.SizeOf<IADLXGPUMetrics1>());
    }

    /// <summary>Validates that the <see cref="IADLXGPUMetrics1" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXGPUMetrics1).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXGPUMetrics1" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXGPUMetrics1));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXGPUMetrics1));
        }
    }
}

