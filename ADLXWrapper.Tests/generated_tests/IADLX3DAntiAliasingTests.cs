using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLX3DAntiAliasing" /> struct.</summary>
public static unsafe partial class IADLX3DAntiAliasingTests
{
    /// <summary>Validates that the <see cref="IADLX3DAntiAliasing" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLX3DAntiAliasing), Marshal.SizeOf<IADLX3DAntiAliasing>());
    }

    /// <summary>Validates that the <see cref="IADLX3DAntiAliasing" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLX3DAntiAliasing).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLX3DAntiAliasing" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLX3DAntiAliasing));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLX3DAntiAliasing));
        }
    }
}
