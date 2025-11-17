using Xunit;

namespace ADLXWrapper.Tests;

public class SimpleTest
{
    [Fact]
    public void Test_XUnit_Discovery()
    {
        Assert.True(true, "If you see this, xUnit discovery is working!");
    }

    [Fact]
    public void Test_Basic_Fact()
    {
        Assert.True(true, "Basic Fact attribute works!");
    }
}