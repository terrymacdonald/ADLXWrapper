using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXPowerTuningChangedHandling" /> struct.</summary>
public static unsafe partial class IADLXPowerTuningChangedHandlingTests
{
    /// <summary>Validates that the <see cref="IADLXPowerTuningChangedHandling" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXPowerTuningChangedHandling), Marshal.SizeOf<IADLXPowerTuningChangedHandling>());
    }

    /// <summary>Validates that the <see cref="IADLXPowerTuningChangedHandling" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXPowerTuningChangedHandling).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXPowerTuningChangedHandling" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXPowerTuningChangedHandling));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXPowerTuningChangedHandling));
        }
    }
}
