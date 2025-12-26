using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLX3DAntiLag" /> struct.</summary>
public static unsafe partial class IADLX3DAntiLagTests
{
    /// <summary>Validates that the <see cref="IADLX3DAntiLag" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLX3DAntiLag), Marshal.SizeOf<IADLX3DAntiLag>());
    }

    /// <summary>Validates that the <see cref="IADLX3DAntiLag" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLX3DAntiLag).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLX3DAntiLag" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLX3DAntiLag));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLX3DAntiLag));
        }
    }
}
