using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXSystem" /> struct.</summary>
public static unsafe partial class IADLXSystemTests
{
    /// <summary>Validates that the <see cref="IADLXSystem" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXSystem), Marshal.SizeOf<IADLXSystem>());
    }

    /// <summary>Validates that the <see cref="IADLXSystem" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXSystem).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXSystem" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXSystem));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXSystem));
        }
    }
}

