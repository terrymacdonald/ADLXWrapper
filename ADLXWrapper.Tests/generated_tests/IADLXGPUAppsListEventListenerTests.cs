using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXGPUAppsListEventListener" /> struct.</summary>
public static unsafe partial class IADLXGPUAppsListEventListenerTests
{
    /// <summary>Validates that the <see cref="IADLXGPUAppsListEventListener" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXGPUAppsListEventListener), Marshal.SizeOf<IADLXGPUAppsListEventListener>());
    }

    /// <summary>Validates that the <see cref="IADLXGPUAppsListEventListener" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXGPUAppsListEventListener).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXGPUAppsListEventListener" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXGPUAppsListEventListener));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXGPUAppsListEventListener));
        }
    }
}

