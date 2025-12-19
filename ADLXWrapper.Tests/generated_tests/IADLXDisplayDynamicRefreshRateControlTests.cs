using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplayDynamicRefreshRateControl" /> struct.</summary>
public static unsafe partial class IADLXDisplayDynamicRefreshRateControlTests
{
    /// <summary>Validates that the <see cref="IADLXDisplayDynamicRefreshRateControl" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplayDynamicRefreshRateControl), Marshal.SizeOf<IADLXDisplayDynamicRefreshRateControl>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplayDynamicRefreshRateControl" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplayDynamicRefreshRateControl).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplayDynamicRefreshRateControl" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplayDynamicRefreshRateControl));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplayDynamicRefreshRateControl));
        }
    }
}

