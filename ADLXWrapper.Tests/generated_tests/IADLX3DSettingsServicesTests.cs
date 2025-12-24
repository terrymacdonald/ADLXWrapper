using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLX3DSettingsServices" /> struct.</summary>
public static unsafe partial class IADLX3DSettingsServicesTests
{
    /// <summary>Validates that the <see cref="IADLX3DSettingsServices" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLX3DSettingsServices), Marshal.SizeOf<IADLX3DSettingsServices>());
    }

    /// <summary>Validates that the <see cref="IADLX3DSettingsServices" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLX3DSettingsServices).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLX3DSettingsServices" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLX3DSettingsServices));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLX3DSettingsServices));
        }
    }
}
