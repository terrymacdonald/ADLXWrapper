using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXGPUMetrics2" /> struct.</summary>
public static unsafe partial class IADLXGPUMetrics2Tests
{
    /// <summary>Validates that the <see cref="IADLXGPUMetrics2" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXGPUMetrics2), Marshal.SizeOf<IADLXGPUMetrics2>());
    }

    /// <summary>Validates that the <see cref="IADLXGPUMetrics2" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXGPUMetrics2).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXGPUMetrics2" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXGPUMetrics2));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXGPUMetrics2));
        }
    }
}

