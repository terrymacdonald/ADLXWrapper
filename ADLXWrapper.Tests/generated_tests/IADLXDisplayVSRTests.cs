using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplayVSR" /> struct.</summary>
public static unsafe partial class IADLXDisplayVSRTests
{
    /// <summary>Validates that the <see cref="IADLXDisplayVSR" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplayVSR), Marshal.SizeOf<IADLXDisplayVSR>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplayVSR" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplayVSR).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplayVSR" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplayVSR));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplayVSR));
        }
    }
}
