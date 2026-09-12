using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXVariableGraphicsMemory" /> struct.</summary>
public static unsafe partial class IADLXVariableGraphicsMemoryTests
{
    /// <summary>Validates that the <see cref="IADLXVariableGraphicsMemory" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXVariableGraphicsMemory), Marshal.SizeOf<IADLXVariableGraphicsMemory>());
    }

    /// <summary>Validates that the <see cref="IADLXVariableGraphicsMemory" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXVariableGraphicsMemory).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXVariableGraphicsMemory" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXVariableGraphicsMemory));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXVariableGraphicsMemory));
        }
    }
}
