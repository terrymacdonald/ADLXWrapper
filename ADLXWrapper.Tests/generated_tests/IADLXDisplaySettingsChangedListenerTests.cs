using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplaySettingsChangedListener" /> struct.</summary>
public static unsafe partial class IADLXDisplaySettingsChangedListenerTests
{
    /// <summary>Validates that the <see cref="IADLXDisplaySettingsChangedListener" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplaySettingsChangedListener), Marshal.SizeOf<IADLXDisplaySettingsChangedListener>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplaySettingsChangedListener" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplaySettingsChangedListener).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplaySettingsChangedListener" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplaySettingsChangedListener));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplaySettingsChangedListener));
        }
    }
}

