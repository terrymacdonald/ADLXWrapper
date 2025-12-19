using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXManualGraphicsTuning1" /> struct.</summary>
public static unsafe partial class IADLXManualGraphicsTuning1Tests
{
    /// <summary>Validates that the <see cref="IADLXManualGraphicsTuning1" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXManualGraphicsTuning1), Marshal.SizeOf<IADLXManualGraphicsTuning1>());
    }

    /// <summary>Validates that the <see cref="IADLXManualGraphicsTuning1" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXManualGraphicsTuning1).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXManualGraphicsTuning1" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXManualGraphicsTuning1));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXManualGraphicsTuning1));
        }
    }
}

