using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXGPUMetrics" /> struct.</summary>
public static unsafe partial class IADLXGPUMetricsTests
{
    /// <summary>Validates that the <see cref="IADLXGPUMetrics" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXGPUMetrics), Marshal.SizeOf<IADLXGPUMetrics>());
    }

    /// <summary>Validates that the <see cref="IADLXGPUMetrics" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXGPUMetrics).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXGPUMetrics" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXGPUMetrics));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXGPUMetrics));
        }
    }
}
