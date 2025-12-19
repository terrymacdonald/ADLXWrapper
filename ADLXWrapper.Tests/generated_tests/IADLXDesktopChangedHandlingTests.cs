using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDesktopChangedHandling" /> struct.</summary>
public static unsafe partial class IADLXDesktopChangedHandlingTests
{
    /// <summary>Validates that the <see cref="IADLXDesktopChangedHandling" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDesktopChangedHandling), Marshal.SizeOf<IADLXDesktopChangedHandling>());
    }

    /// <summary>Validates that the <see cref="IADLXDesktopChangedHandling" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDesktopChangedHandling).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDesktopChangedHandling" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDesktopChangedHandling));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDesktopChangedHandling));
        }
    }
}

