using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLX3DSettingsChangedHandling" /> struct.</summary>
public static unsafe partial class IADLX3DSettingsChangedHandlingTests
{
    /// <summary>Validates that the <see cref="IADLX3DSettingsChangedHandling" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLX3DSettingsChangedHandling), Marshal.SizeOf<IADLX3DSettingsChangedHandling>());
    }

    /// <summary>Validates that the <see cref="IADLX3DSettingsChangedHandling" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLX3DSettingsChangedHandling).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLX3DSettingsChangedHandling" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLX3DSettingsChangedHandling));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLX3DSettingsChangedHandling));
        }
    }
}

