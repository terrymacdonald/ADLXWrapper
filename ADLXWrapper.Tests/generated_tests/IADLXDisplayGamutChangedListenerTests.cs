using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplayGamutChangedListener" /> struct.</summary>
public static unsafe partial class IADLXDisplayGamutChangedListenerTests
{
    /// <summary>Validates that the <see cref="IADLXDisplayGamutChangedListener" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplayGamutChangedListener), Marshal.SizeOf<IADLXDisplayGamutChangedListener>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplayGamutChangedListener" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplayGamutChangedListener).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplayGamutChangedListener" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplayGamutChangedListener));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplayGamutChangedListener));
        }
    }
}
