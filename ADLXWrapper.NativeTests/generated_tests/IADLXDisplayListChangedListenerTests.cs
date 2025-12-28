using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplayListChangedListener" /> struct.</summary>
public static unsafe partial class IADLXDisplayListChangedListenerTests
{
    /// <summary>Validates that the <see cref="IADLXDisplayListChangedListener" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplayListChangedListener), Marshal.SizeOf<IADLXDisplayListChangedListener>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplayListChangedListener" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplayListChangedListener).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplayListChangedListener" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplayListChangedListener));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplayListChangedListener));
        }
    }
}
