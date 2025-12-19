using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXManualTuningStateList" /> struct.</summary>
public static unsafe partial class IADLXManualTuningStateListTests
{
    /// <summary>Validates that the <see cref="IADLXManualTuningStateList" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXManualTuningStateList), Marshal.SizeOf<IADLXManualTuningStateList>());
    }

    /// <summary>Validates that the <see cref="IADLXManualTuningStateList" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXManualTuningStateList).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXManualTuningStateList" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXManualTuningStateList));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXManualTuningStateList));
        }
    }
}

