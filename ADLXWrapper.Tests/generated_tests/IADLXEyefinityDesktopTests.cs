using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXEyefinityDesktop" /> struct.</summary>
public static unsafe partial class IADLXEyefinityDesktopTests
{
    /// <summary>Validates that the <see cref="IADLXEyefinityDesktop" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXEyefinityDesktop), Marshal.SizeOf<IADLXEyefinityDesktop>());
    }

    /// <summary>Validates that the <see cref="IADLXEyefinityDesktop" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXEyefinityDesktop).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXEyefinityDesktop" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXEyefinityDesktop));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXEyefinityDesktop));
        }
    }
}

