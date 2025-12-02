using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXGPUTuningServices" /> struct.</summary>
public static unsafe partial class IADLXGPUTuningServicesTests
{
    /// <summary>Validates that the <see cref="IADLXGPUTuningServices" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXGPUTuningServices), Marshal.SizeOf<IADLXGPUTuningServices>());
    }

    /// <summary>Validates that the <see cref="IADLXGPUTuningServices" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXGPUTuningServices).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXGPUTuningServices" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXGPUTuningServices));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXGPUTuningServices));
        }
    }
}
