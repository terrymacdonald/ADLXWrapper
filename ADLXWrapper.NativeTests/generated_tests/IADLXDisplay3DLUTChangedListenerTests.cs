using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplay3DLUTChangedListener" /> struct.</summary>
public static unsafe partial class IADLXDisplay3DLUTChangedListenerTests
{
    /// <summary>Validates that the <see cref="IADLXDisplay3DLUTChangedListener" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplay3DLUTChangedListener), Marshal.SizeOf<IADLXDisplay3DLUTChangedListener>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplay3DLUTChangedListener" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplay3DLUTChangedListener).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplay3DLUTChangedListener" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplay3DLUTChangedListener));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplay3DLUTChangedListener));
        }
    }
}
