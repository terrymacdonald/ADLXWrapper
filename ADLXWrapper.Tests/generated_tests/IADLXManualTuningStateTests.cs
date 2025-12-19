using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXManualTuningState" /> struct.</summary>
public static unsafe partial class IADLXManualTuningStateTests
{
    /// <summary>Validates that the <see cref="IADLXManualTuningState" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXManualTuningState), Marshal.SizeOf<IADLXManualTuningState>());
    }

    /// <summary>Validates that the <see cref="IADLXManualTuningState" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXManualTuningState).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXManualTuningState" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXManualTuningState));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXManualTuningState));
        }
    }
}

