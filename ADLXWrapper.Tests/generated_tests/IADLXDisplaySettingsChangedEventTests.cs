using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplaySettingsChangedEvent" /> struct.</summary>
public static unsafe partial class IADLXDisplaySettingsChangedEventTests
{
    /// <summary>Validates that the <see cref="IADLXDisplaySettingsChangedEvent" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplaySettingsChangedEvent), Marshal.SizeOf<IADLXDisplaySettingsChangedEvent>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplaySettingsChangedEvent" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplaySettingsChangedEvent).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplaySettingsChangedEvent" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplaySettingsChangedEvent));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplaySettingsChangedEvent));
        }
    }
}

