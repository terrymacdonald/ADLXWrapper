using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLX3DEnhancedSync" /> struct.</summary>
public static unsafe partial class IADLX3DEnhancedSyncTests
{
    /// <summary>Validates that the <see cref="IADLX3DEnhancedSync" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLX3DEnhancedSync), Marshal.SizeOf<IADLX3DEnhancedSync>());
    }

    /// <summary>Validates that the <see cref="IADLX3DEnhancedSync" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLX3DEnhancedSync).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLX3DEnhancedSync" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLX3DEnhancedSync));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLX3DEnhancedSync));
        }
    }
}
