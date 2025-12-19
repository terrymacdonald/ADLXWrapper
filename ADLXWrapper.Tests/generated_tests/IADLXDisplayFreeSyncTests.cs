using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplayFreeSync" /> struct.</summary>
public static unsafe partial class IADLXDisplayFreeSyncTests
{
    /// <summary>Validates that the <see cref="IADLXDisplayFreeSync" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplayFreeSync), Marshal.SizeOf<IADLXDisplayFreeSync>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplayFreeSync" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplayFreeSync).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplayFreeSync" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplayFreeSync));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplayFreeSync));
        }
    }
}

