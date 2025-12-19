using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLMapping" /> struct.</summary>
public static unsafe partial class IADLMappingTests
{
    /// <summary>Validates that the <see cref="IADLMapping" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLMapping), Marshal.SizeOf<IADLMapping>());
    }

    /// <summary>Validates that the <see cref="IADLMapping" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLMapping).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLMapping" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLMapping));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLMapping));
        }
    }
}

