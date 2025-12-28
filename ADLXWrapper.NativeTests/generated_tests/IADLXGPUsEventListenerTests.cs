using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXGPUsEventListener" /> struct.</summary>
public static unsafe partial class IADLXGPUsEventListenerTests
{
    /// <summary>Validates that the <see cref="IADLXGPUsEventListener" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXGPUsEventListener), Marshal.SizeOf<IADLXGPUsEventListener>());
    }

    /// <summary>Validates that the <see cref="IADLXGPUsEventListener" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXGPUsEventListener).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXGPUsEventListener" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXGPUsEventListener));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXGPUsEventListener));
        }
    }
}
