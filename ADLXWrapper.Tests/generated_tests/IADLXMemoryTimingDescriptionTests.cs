using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXMemoryTimingDescription" /> struct.</summary>
public static unsafe partial class IADLXMemoryTimingDescriptionTests
{
    /// <summary>Validates that the <see cref="IADLXMemoryTimingDescription" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXMemoryTimingDescription), Marshal.SizeOf<IADLXMemoryTimingDescription>());
    }

    /// <summary>Validates that the <see cref="IADLXMemoryTimingDescription" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXMemoryTimingDescription).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXMemoryTimingDescription" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXMemoryTimingDescription));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXMemoryTimingDescription));
        }
    }
}

