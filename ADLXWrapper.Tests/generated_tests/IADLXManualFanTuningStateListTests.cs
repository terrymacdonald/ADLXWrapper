using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXManualFanTuningStateList" /> struct.</summary>
public static unsafe partial class IADLXManualFanTuningStateListTests
{
    /// <summary>Validates that the <see cref="IADLXManualFanTuningStateList" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXManualFanTuningStateList), Marshal.SizeOf<IADLXManualFanTuningStateList>());
    }

    /// <summary>Validates that the <see cref="IADLXManualFanTuningStateList" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXManualFanTuningStateList).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXManualFanTuningStateList" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXManualFanTuningStateList));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXManualFanTuningStateList));
        }
    }
}
