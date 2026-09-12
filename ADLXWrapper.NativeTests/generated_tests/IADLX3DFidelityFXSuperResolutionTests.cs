using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLX3DFidelityFXSuperResolution" /> struct.</summary>
public static unsafe partial class IADLX3DFidelityFXSuperResolutionTests
{
    /// <summary>Validates that the <see cref="IADLX3DFidelityFXSuperResolution" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLX3DFidelityFXSuperResolution), Marshal.SizeOf<IADLX3DFidelityFXSuperResolution>());
    }

    /// <summary>Validates that the <see cref="IADLX3DFidelityFXSuperResolution" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLX3DFidelityFXSuperResolution).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLX3DFidelityFXSuperResolution" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLX3DFidelityFXSuperResolution));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLX3DFidelityFXSuperResolution));
        }
    }
}
