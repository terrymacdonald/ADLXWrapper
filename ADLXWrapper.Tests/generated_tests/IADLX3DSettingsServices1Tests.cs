using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLX3DSettingsServices1" /> struct.</summary>
public static unsafe partial class IADLX3DSettingsServices1Tests
{
    /// <summary>Validates that the <see cref="IADLX3DSettingsServices1" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLX3DSettingsServices1), Marshal.SizeOf<IADLX3DSettingsServices1>());
    }

    /// <summary>Validates that the <see cref="IADLX3DSettingsServices1" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLX3DSettingsServices1).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLX3DSettingsServices1" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLX3DSettingsServices1));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLX3DSettingsServices1));
        }
    }
}
