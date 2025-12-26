using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXManualVRAMTuning1" /> struct.</summary>
public static unsafe partial class IADLXManualVRAMTuning1Tests
{
    /// <summary>Validates that the <see cref="IADLXManualVRAMTuning1" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXManualVRAMTuning1), Marshal.SizeOf<IADLXManualVRAMTuning1>());
    }

    /// <summary>Validates that the <see cref="IADLXManualVRAMTuning1" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXManualVRAMTuning1).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXManualVRAMTuning1" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXManualVRAMTuning1));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXManualVRAMTuning1));
        }
    }
}
