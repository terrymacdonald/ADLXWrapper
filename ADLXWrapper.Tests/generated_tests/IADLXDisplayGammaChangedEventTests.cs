using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplayGammaChangedEvent" /> struct.</summary>
public static unsafe partial class IADLXDisplayGammaChangedEventTests
{
    /// <summary>Validates that the <see cref="IADLXDisplayGammaChangedEvent" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplayGammaChangedEvent), Marshal.SizeOf<IADLXDisplayGammaChangedEvent>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplayGammaChangedEvent" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplayGammaChangedEvent).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplayGammaChangedEvent" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplayGammaChangedEvent));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplayGammaChangedEvent));
        }
    }
}
