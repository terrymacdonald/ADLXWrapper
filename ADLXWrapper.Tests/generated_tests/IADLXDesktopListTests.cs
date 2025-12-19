using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDesktopList" /> struct.</summary>
public static unsafe partial class IADLXDesktopListTests
{
    /// <summary>Validates that the <see cref="IADLXDesktopList" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDesktopList), Marshal.SizeOf<IADLXDesktopList>());
    }

    /// <summary>Validates that the <see cref="IADLXDesktopList" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDesktopList).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDesktopList" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDesktopList));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDesktopList));
        }
    }
}

