using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXSystemMetricsSupport" /> struct.</summary>
public static unsafe partial class IADLXSystemMetricsSupportTests
{
    /// <summary>Validates that the <see cref="IADLXSystemMetricsSupport" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXSystemMetricsSupport), Marshal.SizeOf<IADLXSystemMetricsSupport>());
    }

    /// <summary>Validates that the <see cref="IADLXSystemMetricsSupport" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXSystemMetricsSupport).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXSystemMetricsSupport" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXSystemMetricsSupport));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXSystemMetricsSupport));
        }
    }
}

