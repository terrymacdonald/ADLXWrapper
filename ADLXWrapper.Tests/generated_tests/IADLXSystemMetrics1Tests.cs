using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXSystemMetrics1" /> struct.</summary>
public static unsafe partial class IADLXSystemMetrics1Tests
{
    /// <summary>Validates that the <see cref="IADLXSystemMetrics1" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXSystemMetrics1), Marshal.SizeOf<IADLXSystemMetrics1>());
    }

    /// <summary>Validates that the <see cref="IADLXSystemMetrics1" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXSystemMetrics1).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXSystemMetrics1" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXSystemMetrics1));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXSystemMetrics1));
        }
    }
}

