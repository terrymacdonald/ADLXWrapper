using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXGPUConnectChangedEvent" /> struct.</summary>
public static unsafe partial class IADLXGPUConnectChangedEventTests
{
    /// <summary>Validates that the <see cref="IADLXGPUConnectChangedEvent" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXGPUConnectChangedEvent), Marshal.SizeOf<IADLXGPUConnectChangedEvent>());
    }

    /// <summary>Validates that the <see cref="IADLXGPUConnectChangedEvent" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXGPUConnectChangedEvent).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXGPUConnectChangedEvent" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXGPUConnectChangedEvent));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXGPUConnectChangedEvent));
        }
    }
}
