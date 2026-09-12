using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLX3DFidelityFXFrameGenUpgrade" /> struct.</summary>
public static unsafe partial class IADLX3DFidelityFXFrameGenUpgradeTests
{
    /// <summary>Validates that the <see cref="IADLX3DFidelityFXFrameGenUpgrade" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLX3DFidelityFXFrameGenUpgrade), Marshal.SizeOf<IADLX3DFidelityFXFrameGenUpgrade>());
    }

    /// <summary>Validates that the <see cref="IADLX3DFidelityFXFrameGenUpgrade" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLX3DFidelityFXFrameGenUpgrade).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLX3DFidelityFXFrameGenUpgrade" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLX3DFidelityFXFrameGenUpgrade));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLX3DFidelityFXFrameGenUpgrade));
        }
    }
}
