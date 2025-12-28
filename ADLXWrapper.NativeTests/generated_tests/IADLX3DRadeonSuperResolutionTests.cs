using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLX3DRadeonSuperResolution" /> struct.</summary>
public static unsafe partial class IADLX3DRadeonSuperResolutionTests
{
    /// <summary>Validates that the <see cref="IADLX3DRadeonSuperResolution" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLX3DRadeonSuperResolution), Marshal.SizeOf<IADLX3DRadeonSuperResolution>());
    }

    /// <summary>Validates that the <see cref="IADLX3DRadeonSuperResolution" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLX3DRadeonSuperResolution).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLX3DRadeonSuperResolution" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLX3DRadeonSuperResolution));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLX3DRadeonSuperResolution));
        }
    }
}
