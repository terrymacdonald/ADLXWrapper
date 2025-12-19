using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXGPUMetricsSupport" /> struct.</summary>
public static unsafe partial class IADLXGPUMetricsSupportTests
{
    /// <summary>Validates that the <see cref="IADLXGPUMetricsSupport" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXGPUMetricsSupport), Marshal.SizeOf<IADLXGPUMetricsSupport>());
    }

    /// <summary>Validates that the <see cref="IADLXGPUMetricsSupport" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXGPUMetricsSupport).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXGPUMetricsSupport" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXGPUMetricsSupport));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXGPUMetricsSupport));
        }
    }
}

