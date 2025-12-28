using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDesktopServices" /> struct.</summary>
public static unsafe partial class IADLXDesktopServicesTests
{
    /// <summary>Validates that the <see cref="IADLXDesktopServices" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDesktopServices), Marshal.SizeOf<IADLXDesktopServices>());
    }

    /// <summary>Validates that the <see cref="IADLXDesktopServices" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDesktopServices).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDesktopServices" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDesktopServices));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDesktopServices));
        }
    }
}
