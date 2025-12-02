using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXSmartAccessMemory" /> struct.</summary>
public static unsafe partial class IADLXSmartAccessMemoryTests
{
    /// <summary>Validates that the <see cref="IADLXSmartAccessMemory" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXSmartAccessMemory), Marshal.SizeOf<IADLXSmartAccessMemory>());
    }

    /// <summary>Validates that the <see cref="IADLXSmartAccessMemory" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXSmartAccessMemory).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXSmartAccessMemory" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXSmartAccessMemory));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXSmartAccessMemory));
        }
    }
}
