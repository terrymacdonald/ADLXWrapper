using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplayCustomResolution" /> struct.</summary>
public static unsafe partial class IADLXDisplayCustomResolutionTests
{
    /// <summary>Validates that the <see cref="IADLXDisplayCustomResolution" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplayCustomResolution), Marshal.SizeOf<IADLXDisplayCustomResolution>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplayCustomResolution" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplayCustomResolution).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplayCustomResolution" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplayCustomResolution));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplayCustomResolution));
        }
    }
}

