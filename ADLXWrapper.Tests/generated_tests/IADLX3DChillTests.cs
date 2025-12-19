using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLX3DChill" /> struct.</summary>
public static unsafe partial class IADLX3DChillTests
{
    /// <summary>Validates that the <see cref="IADLX3DChill" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLX3DChill), Marshal.SizeOf<IADLX3DChill>());
    }

    /// <summary>Validates that the <see cref="IADLX3DChill" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLX3DChill).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLX3DChill" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLX3DChill));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLX3DChill));
        }
    }
}

