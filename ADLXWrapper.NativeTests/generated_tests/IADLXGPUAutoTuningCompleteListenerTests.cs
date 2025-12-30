using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXGPUAutoTuningCompleteListener" /> struct.</summary>
public static unsafe partial class IADLXGPUAutoTuningCompleteListenerTests
{
    /// <summary>Validates that the <see cref="IADLXGPUAutoTuningCompleteListener" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXGPUAutoTuningCompleteListener), Marshal.SizeOf<IADLXGPUAutoTuningCompleteListener>());
    }

    /// <summary>Validates that the <see cref="IADLXGPUAutoTuningCompleteListener" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXGPUAutoTuningCompleteListener).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXGPUAutoTuningCompleteListener" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXGPUAutoTuningCompleteListener));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXGPUAutoTuningCompleteListener));
        }
    }
}
