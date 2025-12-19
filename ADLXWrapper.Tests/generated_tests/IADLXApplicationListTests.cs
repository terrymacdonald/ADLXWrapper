using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXApplicationList" /> struct.</summary>
public static unsafe partial class IADLXApplicationListTests
{
    /// <summary>Validates that the <see cref="IADLXApplicationList" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXApplicationList), Marshal.SizeOf<IADLXApplicationList>());
    }

    /// <summary>Validates that the <see cref="IADLXApplicationList" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXApplicationList).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXApplicationList" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXApplicationList));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXApplicationList));
        }
    }
}

