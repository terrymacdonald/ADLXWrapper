using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplay3DLUTChangedEvent" /> struct.</summary>
public static unsafe partial class IADLXDisplay3DLUTChangedEventTests
{
    /// <summary>Validates that the <see cref="IADLXDisplay3DLUTChangedEvent" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplay3DLUTChangedEvent), Marshal.SizeOf<IADLXDisplay3DLUTChangedEvent>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplay3DLUTChangedEvent" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplay3DLUTChangedEvent).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplay3DLUTChangedEvent" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplay3DLUTChangedEvent));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplay3DLUTChangedEvent));
        }
    }
}
