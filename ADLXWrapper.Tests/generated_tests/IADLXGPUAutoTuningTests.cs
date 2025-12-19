using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXGPUAutoTuning" /> struct.</summary>
public static unsafe partial class IADLXGPUAutoTuningTests
{
    /// <summary>Validates that the <see cref="IADLXGPUAutoTuning" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXGPUAutoTuning), Marshal.SizeOf<IADLXGPUAutoTuning>());
    }

    /// <summary>Validates that the <see cref="IADLXGPUAutoTuning" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXGPUAutoTuning).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXGPUAutoTuning" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXGPUAutoTuning));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXGPUAutoTuning));
        }
    }
}

