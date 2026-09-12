using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXVariableGraphicsMemoryOptionList" /> struct.</summary>
public static unsafe partial class IADLXVariableGraphicsMemoryOptionListTests
{
    /// <summary>Validates that the <see cref="IADLXVariableGraphicsMemoryOptionList" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXVariableGraphicsMemoryOptionList), Marshal.SizeOf<IADLXVariableGraphicsMemoryOptionList>());
    }

    /// <summary>Validates that the <see cref="IADLXVariableGraphicsMemoryOptionList" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXVariableGraphicsMemoryOptionList).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXVariableGraphicsMemoryOptionList" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXVariableGraphicsMemoryOptionList));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXVariableGraphicsMemoryOptionList));
        }
    }
}
