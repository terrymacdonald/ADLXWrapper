using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXGPUTuningChangedEvent" /> struct.</summary>
public static unsafe partial class IADLXGPUTuningChangedEventTests
{
    /// <summary>Validates that the <see cref="IADLXGPUTuningChangedEvent" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXGPUTuningChangedEvent), Marshal.SizeOf<IADLXGPUTuningChangedEvent>());
    }

    /// <summary>Validates that the <see cref="IADLXGPUTuningChangedEvent" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXGPUTuningChangedEvent).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXGPUTuningChangedEvent" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXGPUTuningChangedEvent));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXGPUTuningChangedEvent));
        }
    }
}
