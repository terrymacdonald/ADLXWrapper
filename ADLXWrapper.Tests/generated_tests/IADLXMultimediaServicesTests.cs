using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXMultimediaServices" /> struct.</summary>
public static unsafe partial class IADLXMultimediaServicesTests
{
    /// <summary>Validates that the <see cref="IADLXMultimediaServices" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXMultimediaServices), Marshal.SizeOf<IADLXMultimediaServices>());
    }

    /// <summary>Validates that the <see cref="IADLXMultimediaServices" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXMultimediaServices).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXMultimediaServices" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXMultimediaServices));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXMultimediaServices));
        }
    }
}

