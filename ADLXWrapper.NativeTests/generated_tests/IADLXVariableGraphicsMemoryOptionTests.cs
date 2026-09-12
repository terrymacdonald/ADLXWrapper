using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXVariableGraphicsMemoryOption" /> struct.</summary>
public static unsafe partial class IADLXVariableGraphicsMemoryOptionTests
{
    /// <summary>Validates that the <see cref="IADLXVariableGraphicsMemoryOption" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXVariableGraphicsMemoryOption), Marshal.SizeOf<IADLXVariableGraphicsMemoryOption>());
    }

    /// <summary>Validates that the <see cref="IADLXVariableGraphicsMemoryOption" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXVariableGraphicsMemoryOption).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXVariableGraphicsMemoryOption" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXVariableGraphicsMemoryOption));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXVariableGraphicsMemoryOption));
        }
    }
}
