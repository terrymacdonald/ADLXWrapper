using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXDisplayServices" /> struct.</summary>
public static unsafe partial class IADLXDisplayServicesTests
{
    /// <summary>Validates that the <see cref="IADLXDisplayServices" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXDisplayServices), Marshal.SizeOf<IADLXDisplayServices>());
    }

    /// <summary>Validates that the <see cref="IADLXDisplayServices" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXDisplayServices).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXDisplayServices" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXDisplayServices));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXDisplayServices));
        }
    }
}

