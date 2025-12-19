using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplayHDCP" /> struct.</summary>
public static unsafe partial class IADLXDisplayHDCPTests
{
    /// <summary>Validates that the <see cref="IADLXDisplayHDCP" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplayHDCP), Marshal.SizeOf<IADLXDisplayHDCP>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplayHDCP" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplayHDCP).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplayHDCP" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplayHDCP));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplayHDCP));
        }
    }
}

