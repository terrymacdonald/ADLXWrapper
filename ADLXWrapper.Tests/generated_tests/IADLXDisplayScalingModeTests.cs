using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplayScalingMode" /> struct.</summary>
public static unsafe partial class IADLXDisplayScalingModeTests
{
    /// <summary>Validates that the <see cref="IADLXDisplayScalingMode" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplayScalingMode), Marshal.SizeOf<IADLXDisplayScalingMode>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplayScalingMode" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplayScalingMode).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplayScalingMode" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplayScalingMode));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplayScalingMode));
        }
    }
}

