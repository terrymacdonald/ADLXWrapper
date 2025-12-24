using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLX3DSettingsChangedEvent2" /> struct.</summary>
public static unsafe partial class IADLX3DSettingsChangedEvent2Tests
{
    /// <summary>Validates that the <see cref="IADLX3DSettingsChangedEvent2" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLX3DSettingsChangedEvent2), Marshal.SizeOf<IADLX3DSettingsChangedEvent2>());
    }

    /// <summary>Validates that the <see cref="IADLX3DSettingsChangedEvent2" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLX3DSettingsChangedEvent2).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLX3DSettingsChangedEvent2" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLX3DSettingsChangedEvent2));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLX3DSettingsChangedEvent2));
        }
    }
}
