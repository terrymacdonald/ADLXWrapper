using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplayPixelFormat" /> struct.</summary>
public static unsafe partial class IADLXDisplayPixelFormatTests
{
    /// <summary>Validates that the <see cref="IADLXDisplayPixelFormat" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplayPixelFormat), Marshal.SizeOf<IADLXDisplayPixelFormat>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplayPixelFormat" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplayPixelFormat).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplayPixelFormat" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplayPixelFormat));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplayPixelFormat));
        }
    }
}

