using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplayChangedHandling" /> struct.</summary>
public static unsafe partial class IADLXDisplayChangedHandlingTests
{
    /// <summary>Validates that the <see cref="IADLXDisplayChangedHandling" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplayChangedHandling), Marshal.SizeOf<IADLXDisplayChangedHandling>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplayChangedHandling" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplayChangedHandling).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplayChangedHandling" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplayChangedHandling));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplayChangedHandling));
        }
    }
}

