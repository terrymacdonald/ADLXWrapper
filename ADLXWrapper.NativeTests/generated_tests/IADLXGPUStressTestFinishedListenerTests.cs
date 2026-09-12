using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXGPUStressTestFinishedListener" /> struct.</summary>
public static unsafe partial class IADLXGPUStressTestFinishedListenerTests
{
    /// <summary>Validates that the <see cref="IADLXGPUStressTestFinishedListener" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXGPUStressTestFinishedListener), Marshal.SizeOf<IADLXGPUStressTestFinishedListener>());
    }

    /// <summary>Validates that the <see cref="IADLXGPUStressTestFinishedListener" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXGPUStressTestFinishedListener).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXGPUStressTestFinishedListener" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXGPUStressTestFinishedListener));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXGPUStressTestFinishedListener));
        }
    }
}
