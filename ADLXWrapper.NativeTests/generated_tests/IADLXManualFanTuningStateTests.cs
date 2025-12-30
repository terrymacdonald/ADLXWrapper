using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXManualFanTuningState" /> struct.</summary>
public static unsafe partial class IADLXManualFanTuningStateTests
{
    /// <summary>Validates that the <see cref="IADLXManualFanTuningState" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXManualFanTuningState), Marshal.SizeOf<IADLXManualFanTuningState>());
    }

    /// <summary>Validates that the <see cref="IADLXManualFanTuningState" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXManualFanTuningState).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXManualFanTuningState" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXManualFanTuningState));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXManualFanTuningState));
        }
    }
}
