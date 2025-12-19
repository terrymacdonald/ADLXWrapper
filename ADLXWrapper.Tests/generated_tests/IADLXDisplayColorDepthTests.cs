using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplayColorDepth" /> struct.</summary>
public static unsafe partial class IADLXDisplayColorDepthTests
{
    /// <summary>Validates that the <see cref="IADLXDisplayColorDepth" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplayColorDepth), Marshal.SizeOf<IADLXDisplayColorDepth>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplayColorDepth" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplayColorDepth).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplayColorDepth" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplayColorDepth));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplayColorDepth));
        }
    }
}

