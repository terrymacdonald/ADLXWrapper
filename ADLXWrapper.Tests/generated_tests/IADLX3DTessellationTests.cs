using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLX3DTessellation" /> struct.</summary>
public static unsafe partial class IADLX3DTessellationTests
{
    /// <summary>Validates that the <see cref="IADLX3DTessellation" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLX3DTessellation), Marshal.SizeOf<IADLX3DTessellation>());
    }

    /// <summary>Validates that the <see cref="IADLX3DTessellation" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLX3DTessellation).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLX3DTessellation" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLX3DTessellation));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLX3DTessellation));
        }
    }
}

