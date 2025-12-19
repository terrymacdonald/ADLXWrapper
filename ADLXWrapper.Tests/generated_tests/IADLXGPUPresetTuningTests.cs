using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXGPUPresetTuning" /> struct.</summary>
public static unsafe partial class IADLXGPUPresetTuningTests
{
    /// <summary>Validates that the <see cref="IADLXGPUPresetTuning" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXGPUPresetTuning), Marshal.SizeOf<IADLXGPUPresetTuning>());
    }

    /// <summary>Validates that the <see cref="IADLXGPUPresetTuning" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXGPUPresetTuning).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXGPUPresetTuning" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXGPUPresetTuning));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXGPUPresetTuning));
        }
    }
}

