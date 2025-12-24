using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplayFreeSyncColorAccuracy" /> struct.</summary>
public static unsafe partial class IADLXDisplayFreeSyncColorAccuracyTests
{
    /// <summary>Validates that the <see cref="IADLXDisplayFreeSyncColorAccuracy" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplayFreeSyncColorAccuracy), Marshal.SizeOf<IADLXDisplayFreeSyncColorAccuracy>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplayFreeSyncColorAccuracy" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplayFreeSyncColorAccuracy).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplayFreeSyncColorAccuracy" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplayFreeSyncColorAccuracy));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplayFreeSyncColorAccuracy));
        }
    }
}
