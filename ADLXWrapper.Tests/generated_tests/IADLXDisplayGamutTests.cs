using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplayGamut" /> struct.</summary>
public static unsafe partial class IADLXDisplayGamutTests
{
    /// <summary>Validates that the <see cref="IADLXDisplayGamut" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplayGamut), Marshal.SizeOf<IADLXDisplayGamut>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplayGamut" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplayGamut).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplayGamut" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplayGamut));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplayGamut));
        }
    }
}
