using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplayResolution" /> struct.</summary>
public static unsafe partial class IADLXDisplayResolutionTests
{
    /// <summary>Validates that the <see cref="IADLXDisplayResolution" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplayResolution), Marshal.SizeOf<IADLXDisplayResolution>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplayResolution" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplayResolution).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplayResolution" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplayResolution));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplayResolution));
        }
    }
}

