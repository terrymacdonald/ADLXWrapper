using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplayGamma" /> struct.</summary>
public static unsafe partial class IADLXDisplayGammaTests
{
    /// <summary>Validates that the <see cref="IADLXDisplayGamma" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplayGamma), Marshal.SizeOf<IADLXDisplayGamma>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplayGamma" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplayGamma).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplayGamma" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplayGamma));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplayGamma));
        }
    }
}
