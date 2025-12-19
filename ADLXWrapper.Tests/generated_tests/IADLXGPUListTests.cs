using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXGPUList" /> struct.</summary>
public static unsafe partial class IADLXGPUListTests
{
    /// <summary>Validates that the <see cref="IADLXGPUList" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXGPUList), Marshal.SizeOf<IADLXGPUList>());
    }

    /// <summary>Validates that the <see cref="IADLXGPUList" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXGPUList).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXGPUList" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXGPUList));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXGPUList));
        }
    }
}

