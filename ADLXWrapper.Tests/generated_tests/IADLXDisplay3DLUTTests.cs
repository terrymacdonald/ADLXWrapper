using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplay3DLUT" /> struct.</summary>
public static unsafe partial class IADLXDisplay3DLUTTests
{
    /// <summary>Validates that the <see cref="IADLXDisplay3DLUT" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplay3DLUT), Marshal.SizeOf<IADLXDisplay3DLUT>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplay3DLUT" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplay3DLUT).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplay3DLUT" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplay3DLUT));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplay3DLUT));
        }
    }
}
