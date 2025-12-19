using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLX3DImageSharpening" /> struct.</summary>
public static unsafe partial class IADLX3DImageSharpeningTests
{
    /// <summary>Validates that the <see cref="IADLX3DImageSharpening" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLX3DImageSharpening), Marshal.SizeOf<IADLX3DImageSharpening>());
    }

    /// <summary>Validates that the <see cref="IADLX3DImageSharpening" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLX3DImageSharpening).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLX3DImageSharpening" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLX3DImageSharpening));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLX3DImageSharpening));
        }
    }
}

