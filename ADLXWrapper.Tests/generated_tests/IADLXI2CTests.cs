using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXI2C" /> struct.</summary>
public static unsafe partial class IADLXI2CTests
{
    /// <summary>Validates that the <see cref="IADLXI2C" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXI2C), Marshal.SizeOf<IADLXI2C>());
    }

    /// <summary>Validates that the <see cref="IADLXI2C" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXI2C).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXI2C" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXI2C));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXI2C));
        }
    }
}

