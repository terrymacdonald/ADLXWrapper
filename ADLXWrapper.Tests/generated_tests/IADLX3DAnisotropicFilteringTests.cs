using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLX3DAnisotropicFiltering" /> struct.</summary>
public static unsafe partial class IADLX3DAnisotropicFilteringTests
{
    /// <summary>Validates that the <see cref="IADLX3DAnisotropicFiltering" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLX3DAnisotropicFiltering), Marshal.SizeOf<IADLX3DAnisotropicFiltering>());
    }

    /// <summary>Validates that the <see cref="IADLX3DAnisotropicFiltering" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLX3DAnisotropicFiltering).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLX3DAnisotropicFiltering" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLX3DAnisotropicFiltering));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLX3DAnisotropicFiltering));
        }
    }
}
