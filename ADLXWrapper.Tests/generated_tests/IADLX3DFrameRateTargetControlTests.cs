using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLX3DFrameRateTargetControl" /> struct.</summary>
public static unsafe partial class IADLX3DFrameRateTargetControlTests
{
    /// <summary>Validates that the <see cref="IADLX3DFrameRateTargetControl" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLX3DFrameRateTargetControl), Marshal.SizeOf<IADLX3DFrameRateTargetControl>());
    }

    /// <summary>Validates that the <see cref="IADLX3DFrameRateTargetControl" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLX3DFrameRateTargetControl).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLX3DFrameRateTargetControl" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLX3DFrameRateTargetControl));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLX3DFrameRateTargetControl));
        }
    }
}

