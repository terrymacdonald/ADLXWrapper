using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXList" /> struct.</summary>
public static unsafe partial class IADLXListTests
{
    /// <summary>Validates that the <see cref="IADLXList" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXList), Marshal.SizeOf<IADLXList>());
    }

    /// <summary>Validates that the <see cref="IADLXList" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXList).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXList" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXList));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXList));
        }
    }
}
