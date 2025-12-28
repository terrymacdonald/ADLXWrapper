using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXGPUMetricsList" /> struct.</summary>
public static unsafe partial class IADLXGPUMetricsListTests
{
    /// <summary>Validates that the <see cref="IADLXGPUMetricsList" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXGPUMetricsList), Marshal.SizeOf<IADLXGPUMetricsList>());
    }

    /// <summary>Validates that the <see cref="IADLXGPUMetricsList" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXGPUMetricsList).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXGPUMetricsList" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXGPUMetricsList));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXGPUMetricsList));
        }
    }
}
