using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXMultimediaChangedEventListener" /> struct.</summary>
public static unsafe partial class IADLXMultimediaChangedEventListenerTests
{
    /// <summary>Validates that the <see cref="IADLXMultimediaChangedEventListener" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXMultimediaChangedEventListener), Marshal.SizeOf<IADLXMultimediaChangedEventListener>());
    }

    /// <summary>Validates that the <see cref="IADLXMultimediaChangedEventListener" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXMultimediaChangedEventListener).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXMultimediaChangedEventListener" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXMultimediaChangedEventListener));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXMultimediaChangedEventListener));
        }
    }
}

