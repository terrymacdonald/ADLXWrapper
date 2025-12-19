using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXAllMetrics" /> struct.</summary>
public static unsafe partial class IADLXAllMetricsTests
{
    /// <summary>Validates that the <see cref="IADLXAllMetrics" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXAllMetrics), Marshal.SizeOf<IADLXAllMetrics>());
    }

    /// <summary>Validates that the <see cref="IADLXAllMetrics" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXAllMetrics).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXAllMetrics" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXAllMetrics));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXAllMetrics));
        }
    }
}

