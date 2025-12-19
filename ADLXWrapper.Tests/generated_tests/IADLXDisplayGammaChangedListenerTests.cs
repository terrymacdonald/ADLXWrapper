using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplayGammaChangedListener" /> struct.</summary>
public static unsafe partial class IADLXDisplayGammaChangedListenerTests
{
    /// <summary>Validates that the <see cref="IADLXDisplayGammaChangedListener" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplayGammaChangedListener), Marshal.SizeOf<IADLXDisplayGammaChangedListener>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplayGammaChangedListener" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplayGammaChangedListener).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplayGammaChangedListener" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplayGammaChangedListener));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplayGammaChangedListener));
        }
    }
}

