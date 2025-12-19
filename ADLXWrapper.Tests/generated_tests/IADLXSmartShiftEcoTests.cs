using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXSmartShiftEco" /> struct.</summary>
public static unsafe partial class IADLXSmartShiftEcoTests
{
    /// <summary>Validates that the <see cref="IADLXSmartShiftEco" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXSmartShiftEco), Marshal.SizeOf<IADLXSmartShiftEco>());
    }

    /// <summary>Validates that the <see cref="IADLXSmartShiftEco" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXSmartShiftEco).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXSmartShiftEco" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXSmartShiftEco));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXSmartShiftEco));
        }
    }
}

