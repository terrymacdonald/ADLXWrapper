using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXManualPowerTuning1" /> struct.</summary>
public static unsafe partial class IADLXManualPowerTuning1Tests
{
    /// <summary>Validates that the <see cref="IADLXManualPowerTuning1" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXManualPowerTuning1), Marshal.SizeOf<IADLXManualPowerTuning1>());
    }

    /// <summary>Validates that the <see cref="IADLXManualPowerTuning1" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXManualPowerTuning1).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXManualPowerTuning1" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXManualPowerTuning1));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXManualPowerTuning1));
        }
    }
}

