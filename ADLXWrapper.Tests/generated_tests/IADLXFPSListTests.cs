using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXFPSList" /> struct.</summary>
public static unsafe partial class IADLXFPSListTests
{
    /// <summary>Validates that the <see cref="IADLXFPSList" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXFPSList), Marshal.SizeOf<IADLXFPSList>());
    }

    /// <summary>Validates that the <see cref="IADLXFPSList" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXFPSList).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXFPSList" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXFPSList));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXFPSList));
        }
    }
}

