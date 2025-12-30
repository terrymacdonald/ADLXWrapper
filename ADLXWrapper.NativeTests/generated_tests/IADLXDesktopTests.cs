using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDesktop" /> struct.</summary>
public static unsafe partial class IADLXDesktopTests
{
    /// <summary>Validates that the <see cref="IADLXDesktop" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDesktop), Marshal.SizeOf<IADLXDesktop>());
    }

    /// <summary>Validates that the <see cref="IADLXDesktop" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDesktop).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDesktop" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDesktop));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDesktop));
        }
    }
}
