using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLX3DResetShaderCache" /> struct.</summary>
public static unsafe partial class IADLX3DResetShaderCacheTests
{
    /// <summary>Validates that the <see cref="IADLX3DResetShaderCache" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLX3DResetShaderCache), Marshal.SizeOf<IADLX3DResetShaderCache>());
    }

    /// <summary>Validates that the <see cref="IADLX3DResetShaderCache" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLX3DResetShaderCache).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLX3DResetShaderCache" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLX3DResetShaderCache));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLX3DResetShaderCache));
        }
    }
}

