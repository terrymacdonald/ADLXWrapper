using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXApplication" /> struct.</summary>
public static unsafe partial class IADLXApplicationTests
{
    /// <summary>Validates that the <see cref="IADLXApplication" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXApplication), Marshal.SizeOf<IADLXApplication>());
    }

    /// <summary>Validates that the <see cref="IADLXApplication" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXApplication).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXApplication" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXApplication));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXApplication));
        }
    }
}
