using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ADLXWrapper.UnitTests;

/// <summary>Provides validation of the <see cref="IADLXLog" /> struct.</summary>
public static unsafe partial class IADLXLogTests
{
    /// <summary>Validates that the <see cref="IADLXLog" /> struct is blittable.</summary>
    [Fact]
    public static void IsBlittableTest()
    {
        Assert.Equal(sizeof(IADLXLog), Marshal.SizeOf<IADLXLog>());
    }

    /// <summary>Validates that the <see cref="IADLXLog" /> struct has the right <see cref="LayoutKind" />.</summary>
    [Fact]
    public static void IsLayoutSequentialTest()
    {
        Assert.True(typeof(IADLXLog).IsLayoutSequential);
    }

    /// <summary>Validates that the <see cref="IADLXLog" /> struct has the correct size.</summary>
    [Fact]
    public static void SizeOfTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(8, sizeof(IADLXLog));
        }
        else
        {
            Assert.Equal(4, sizeof(IADLXLog));
        }
    }
}
