using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXMultimediaChangedEvent" /> struct.</summary>
public static unsafe partial class IADLXMultimediaChangedEventTests
{
    /// <summary>Validates that the <see cref="IADLXMultimediaChangedEvent" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXMultimediaChangedEvent), Marshal.SizeOf<IADLXMultimediaChangedEvent>());
    }

    /// <summary>Validates that the <see cref="IADLXMultimediaChangedEvent" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXMultimediaChangedEvent).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXMultimediaChangedEvent" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXMultimediaChangedEvent));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXMultimediaChangedEvent));
        }
    }
}

