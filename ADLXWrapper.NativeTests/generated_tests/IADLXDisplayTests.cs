using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplay" /> struct.</summary>
public static unsafe partial class IADLXDisplayTests
{
    /// <summary>Validates that the <see cref="IADLXDisplay" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplay), Marshal.SizeOf<IADLXDisplay>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplay" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplay).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplay" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplay));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplay));
        }
    }
}
