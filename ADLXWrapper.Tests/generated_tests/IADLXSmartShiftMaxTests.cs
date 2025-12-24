using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXSmartShiftMax" /> struct.</summary>
public static unsafe partial class IADLXSmartShiftMaxTests
{
    /// <summary>Validates that the <see cref="IADLXSmartShiftMax" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXSmartShiftMax), Marshal.SizeOf<IADLXSmartShiftMax>());
    }

    /// <summary>Validates that the <see cref="IADLXSmartShiftMax" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXSmartShiftMax).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXSmartShiftMax" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXSmartShiftMax));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXSmartShiftMax));
        }
    }
}
