using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXGPUAppsListChangedHandling" /> struct.</summary>
public static unsafe partial class IADLXGPUAppsListChangedHandlingTests
{
    /// <summary>Validates that the <see cref="IADLXGPUAppsListChangedHandling" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXGPUAppsListChangedHandling), Marshal.SizeOf<IADLXGPUAppsListChangedHandling>());
    }

    /// <summary>Validates that the <see cref="IADLXGPUAppsListChangedHandling" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXGPUAppsListChangedHandling).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXGPUAppsListChangedHandling" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXGPUAppsListChangedHandling));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXGPUAppsListChangedHandling));
        }
    }
}
