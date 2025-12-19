using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLX3DAMDFluidMotionFrames" /> struct.</summary>
public static unsafe partial class IADLX3DAMDFluidMotionFramesTests
{
    /// <summary>Validates that the <see cref="IADLX3DAMDFluidMotionFrames" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLX3DAMDFluidMotionFrames), Marshal.SizeOf<IADLX3DAMDFluidMotionFrames>());
    }

    /// <summary>Validates that the <see cref="IADLX3DAMDFluidMotionFrames" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLX3DAMDFluidMotionFrames).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLX3DAMDFluidMotionFrames" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLX3DAMDFluidMotionFrames));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLX3DAMDFluidMotionFrames));
        }
    }
}

