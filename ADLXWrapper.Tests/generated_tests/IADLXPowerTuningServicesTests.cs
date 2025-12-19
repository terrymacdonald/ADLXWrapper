using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXPowerTuningServices" /> struct.</summary>
public static unsafe partial class IADLXPowerTuningServicesTests
{
    /// <summary>Validates that the <see cref="IADLXPowerTuningServices" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXPowerTuningServices), Marshal.SizeOf<IADLXPowerTuningServices>());
    }

    /// <summary>Validates that the <see cref="IADLXPowerTuningServices" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXPowerTuningServices).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXPowerTuningServices" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXPowerTuningServices));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXPowerTuningServices));
        }
    }
}

