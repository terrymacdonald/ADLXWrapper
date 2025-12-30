using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLX3DSettingsChangedEvent" /> struct.</summary>
public static unsafe partial class IADLX3DSettingsChangedEventTests
{
    /// <summary>Validates that the <see cref="IADLX3DSettingsChangedEvent" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLX3DSettingsChangedEvent), Marshal.SizeOf<IADLX3DSettingsChangedEvent>());
    }

    /// <summary>Validates that the <see cref="IADLX3DSettingsChangedEvent" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLX3DSettingsChangedEvent).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLX3DSettingsChangedEvent" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLX3DSettingsChangedEvent));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLX3DSettingsChangedEvent));
        }
    }
}
