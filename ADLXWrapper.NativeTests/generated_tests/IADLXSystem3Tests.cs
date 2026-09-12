using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXSystem3" /> struct.</summary>
public static unsafe partial class IADLXSystem3Tests
{
    /// <summary>Validates that the <see cref="IADLXSystem3" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXSystem3), Marshal.SizeOf<IADLXSystem3>());
    }

    /// <summary>Validates that the <see cref="IADLXSystem3" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXSystem3).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXSystem3" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXSystem3));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXSystem3));
        }
    }
}
