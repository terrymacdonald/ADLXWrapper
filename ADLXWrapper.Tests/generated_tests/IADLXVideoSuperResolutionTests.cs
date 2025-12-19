using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXVideoSuperResolution" /> struct.</summary>
public static unsafe partial class IADLXVideoSuperResolutionTests
{
    /// <summary>Validates that the <see cref="IADLXVideoSuperResolution" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXVideoSuperResolution), Marshal.SizeOf<IADLXVideoSuperResolution>());
    }

    /// <summary>Validates that the <see cref="IADLXVideoSuperResolution" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXVideoSuperResolution).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXVideoSuperResolution" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXVideoSuperResolution));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXVideoSuperResolution));
        }
    }
}

