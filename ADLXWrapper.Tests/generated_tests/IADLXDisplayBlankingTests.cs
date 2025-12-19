using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplayBlanking" /> struct.</summary>
public static unsafe partial class IADLXDisplayBlankingTests
{
    /// <summary>Validates that the <see cref="IADLXDisplayBlanking" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplayBlanking), Marshal.SizeOf<IADLXDisplayBlanking>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplayBlanking" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplayBlanking).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplayBlanking" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplayBlanking));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplayBlanking));
        }
    }
}

