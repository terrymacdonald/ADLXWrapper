using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDesktopListChangedListener" /> struct.</summary>
public static unsafe partial class IADLXDesktopListChangedListenerTests
{
    /// <summary>Validates that the <see cref="IADLXDesktopListChangedListener" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDesktopListChangedListener), Marshal.SizeOf<IADLXDesktopListChangedListener>());
    }

    /// <summary>Validates that the <see cref="IADLXDesktopListChangedListener" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDesktopListChangedListener).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDesktopListChangedListener" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDesktopListChangedListener));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDesktopListChangedListener));
        }
    }
}

