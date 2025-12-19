using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLX3DBoost" /> struct.</summary>
public static unsafe partial class IADLX3DBoostTests
{
    /// <summary>Validates that the <see cref="IADLX3DBoost" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLX3DBoost), Marshal.SizeOf<IADLX3DBoost>());
    }

    /// <summary>Validates that the <see cref="IADLX3DBoost" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLX3DBoost).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLX3DBoost" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLX3DBoost));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLX3DBoost));
        }
    }
}

