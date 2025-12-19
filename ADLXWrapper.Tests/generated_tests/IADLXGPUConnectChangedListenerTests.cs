using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXGPUConnectChangedListener" /> struct.</summary>
public static unsafe partial class IADLXGPUConnectChangedListenerTests
{
    /// <summary>Validates that the <see cref="IADLXGPUConnectChangedListener" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXGPUConnectChangedListener), Marshal.SizeOf<IADLXGPUConnectChangedListener>());
    }

    /// <summary>Validates that the <see cref="IADLXGPUConnectChangedListener" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXGPUConnectChangedListener).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXGPUConnectChangedListener" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXGPUConnectChangedListener));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXGPUConnectChangedListener));
        }
    }
}

