using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXPowerTuningChangedListener" /> struct.</summary>
public static unsafe partial class IADLXPowerTuningChangedListenerTests
{
    /// <summary>Validates that the <see cref="IADLXPowerTuningChangedListener" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXPowerTuningChangedListener), Marshal.SizeOf<IADLXPowerTuningChangedListener>());
    }

    /// <summary>Validates that the <see cref="IADLXPowerTuningChangedListener" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXPowerTuningChangedListener).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXPowerTuningChangedListener" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXPowerTuningChangedListener));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXPowerTuningChangedListener));
        }
    }
}

