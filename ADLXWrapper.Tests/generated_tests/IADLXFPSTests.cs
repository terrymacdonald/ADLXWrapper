using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXFPS" /> struct.</summary>
public static unsafe partial class IADLXFPSTests
{
    /// <summary>Validates that the <see cref="IADLXFPS" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXFPS), Marshal.SizeOf<IADLXFPS>());
    }

    /// <summary>Validates that the <see cref="IADLXFPS" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXFPS).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXFPS" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXFPS));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXFPS));
        }
    }
}

