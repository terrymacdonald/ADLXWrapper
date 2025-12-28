using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplaySettingsChangedEvent2" /> struct.</summary>
public static unsafe partial class IADLXDisplaySettingsChangedEvent2Tests
{
    /// <summary>Validates that the <see cref="IADLXDisplaySettingsChangedEvent2" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplaySettingsChangedEvent2), Marshal.SizeOf<IADLXDisplaySettingsChangedEvent2>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplaySettingsChangedEvent2" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplaySettingsChangedEvent2).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplaySettingsChangedEvent2" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplaySettingsChangedEvent2));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplaySettingsChangedEvent2));
        }
    }
}
