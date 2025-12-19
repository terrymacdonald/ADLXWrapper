using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXGPUTuningChangedHandling" /> struct.</summary>
public static unsafe partial class IADLXGPUTuningChangedHandlingTests
{
    /// <summary>Validates that the <see cref="IADLXGPUTuningChangedHandling" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXGPUTuningChangedHandling), Marshal.SizeOf<IADLXGPUTuningChangedHandling>());
    }

    /// <summary>Validates that the <see cref="IADLXGPUTuningChangedHandling" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXGPUTuningChangedHandling).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXGPUTuningChangedHandling" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXGPUTuningChangedHandling));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXGPUTuningChangedHandling));
        }
    }
}

