using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXGPUsChangedHandling" /> struct.</summary>
public static unsafe partial class IADLXGPUsChangedHandlingTests
{
    /// <summary>Validates that the <see cref="IADLXGPUsChangedHandling" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXGPUsChangedHandling), Marshal.SizeOf<IADLXGPUsChangedHandling>());
    }

    /// <summary>Validates that the <see cref="IADLXGPUsChangedHandling" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXGPUsChangedHandling).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXGPUsChangedHandling" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXGPUsChangedHandling));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXGPUsChangedHandling));
        }
    }
}
