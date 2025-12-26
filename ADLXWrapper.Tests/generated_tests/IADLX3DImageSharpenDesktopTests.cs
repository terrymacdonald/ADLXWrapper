using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLX3DImageSharpenDesktop" /> struct.</summary>
public static unsafe partial class IADLX3DImageSharpenDesktopTests
{
    /// <summary>Validates that the <see cref="IADLX3DImageSharpenDesktop" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLX3DImageSharpenDesktop), Marshal.SizeOf<IADLX3DImageSharpenDesktop>());
    }

    /// <summary>Validates that the <see cref="IADLX3DImageSharpenDesktop" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLX3DImageSharpenDesktop).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLX3DImageSharpenDesktop" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLX3DImageSharpenDesktop));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLX3DImageSharpenDesktop));
        }
    }
}
