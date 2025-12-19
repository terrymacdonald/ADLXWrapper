using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplayIntegerScaling" /> struct.</summary>
public static unsafe partial class IADLXDisplayIntegerScalingTests
{
    /// <summary>Validates that the <see cref="IADLXDisplayIntegerScaling" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplayIntegerScaling), Marshal.SizeOf<IADLXDisplayIntegerScaling>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplayIntegerScaling" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplayIntegerScaling).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplayIntegerScaling" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplayIntegerScaling));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplayIntegerScaling));
        }
    }
}

