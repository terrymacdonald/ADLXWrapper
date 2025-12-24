using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLX3DSettingsChangedListener" /> struct.</summary>
public static unsafe partial class IADLX3DSettingsChangedListenerTests
{
    /// <summary>Validates that the <see cref="IADLX3DSettingsChangedListener" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLX3DSettingsChangedListener), Marshal.SizeOf<IADLX3DSettingsChangedListener>());
    }

    /// <summary>Validates that the <see cref="IADLX3DSettingsChangedListener" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLX3DSettingsChangedListener).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLX3DSettingsChangedListener" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLX3DSettingsChangedListener));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLX3DSettingsChangedListener));
        }
    }
}
