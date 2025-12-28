using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXMemoryTimingDescriptionList" /> struct.</summary>
public static unsafe partial class IADLXMemoryTimingDescriptionListTests
{
    /// <summary>Validates that the <see cref="IADLXMemoryTimingDescriptionList" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXMemoryTimingDescriptionList), Marshal.SizeOf<IADLXMemoryTimingDescriptionList>());
    }

    /// <summary>Validates that the <see cref="IADLXMemoryTimingDescriptionList" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXMemoryTimingDescriptionList).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXMemoryTimingDescriptionList" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXMemoryTimingDescriptionList));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXMemoryTimingDescriptionList));
        }
    }
}
