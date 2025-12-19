using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplayList" /> struct.</summary>
public static unsafe partial class IADLXDisplayListTests
{
    /// <summary>Validates that the <see cref="IADLXDisplayList" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplayList), Marshal.SizeOf<IADLXDisplayList>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplayList" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplayList).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplayList" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplayList));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplayList));
        }
    }
}

