using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLX3DMorphologicalAntiAliasing" /> struct.</summary>
public static unsafe partial class IADLX3DMorphologicalAntiAliasingTests
{
    /// <summary>Validates that the <see cref="IADLX3DMorphologicalAntiAliasing" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLX3DMorphologicalAntiAliasing), Marshal.SizeOf<IADLX3DMorphologicalAntiAliasing>());
    }

    /// <summary>Validates that the <see cref="IADLX3DMorphologicalAntiAliasing" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLX3DMorphologicalAntiAliasing).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLX3DMorphologicalAntiAliasing" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLX3DMorphologicalAntiAliasing));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLX3DMorphologicalAntiAliasing));
        }
    }
}

