using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplayCustomColor" /> struct.</summary>
public static unsafe partial class IADLXDisplayCustomColorTests
{
    /// <summary>Validates that the <see cref="IADLXDisplayCustomColor" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplayCustomColor), Marshal.SizeOf<IADLXDisplayCustomColor>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplayCustomColor" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplayCustomColor).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplayCustomColor" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplayCustomColor));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplayCustomColor));
        }
    }
}

