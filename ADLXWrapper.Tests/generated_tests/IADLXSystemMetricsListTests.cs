using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXSystemMetricsList" /> struct.</summary>
public static unsafe partial class IADLXSystemMetricsListTests
{
    /// <summary>Validates that the <see cref="IADLXSystemMetricsList" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXSystemMetricsList), Marshal.SizeOf<IADLXSystemMetricsList>());
    }

    /// <summary>Validates that the <see cref="IADLXSystemMetricsList" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXSystemMetricsList).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXSystemMetricsList" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXSystemMetricsList));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXSystemMetricsList));
        }
    }
}
