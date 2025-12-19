using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLX3DSettingsServices2" /> struct.</summary>
public static unsafe partial class IADLX3DSettingsServices2Tests
{
    /// <summary>Validates that the <see cref="IADLX3DSettingsServices2" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLX3DSettingsServices2), Marshal.SizeOf<IADLX3DSettingsServices2>());
    }

    /// <summary>Validates that the <see cref="IADLX3DSettingsServices2" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLX3DSettingsServices2).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLX3DSettingsServices2" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLX3DSettingsServices2));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLX3DSettingsServices2));
        }
    }
}

