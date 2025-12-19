using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplayConnectivityExperience" /> struct.</summary>
public static unsafe partial class IADLXDisplayConnectivityExperienceTests
{
    /// <summary>Validates that the <see cref="IADLXDisplayConnectivityExperience" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplayConnectivityExperience), Marshal.SizeOf<IADLXDisplayConnectivityExperience>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplayConnectivityExperience" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplayConnectivityExperience).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplayConnectivityExperience" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplayConnectivityExperience));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplayConnectivityExperience));
        }
    }
}

