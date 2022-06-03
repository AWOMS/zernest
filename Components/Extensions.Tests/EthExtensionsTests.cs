using Xunit;
using FluentAssertions;
using AWOMS.Zernest.Components.Extensions;

namespace AWOMS.Zernest.Components.Extensions.Tests;

public class EthExtensionsTests
{
    [Theory]
    [InlineData("1000000000000000000", "1.000")]
    [InlineData("500000000000000000", "0.500")]
    [InlineData("100000000000000000", "0.100")]
    [InlineData("49000000000000000", "0.049")]
    [InlineData("15600000000000000", "0.016")] // rounds up
    [InlineData("20000000000000000", "0.020")]
    [InlineData("12300000000000000", "0.012")]
    public void WeiToEth_Converts(string wei, string expected)
    {
        var eth = wei.WeiToEth();
        eth.Should().Be(expected);
    }

    // [Theory]
    // [InlineData("1000000000000000000", "1.000")]
    // [InlineData("500000000000000000", "0.500")]
    // [InlineData("100000000000000000", "0.100")]
    // [InlineData("49000000000000000", "0.049")]
    // [InlineData("15600000000000000", "0.016")] // rounds up
    // [InlineData("20000000000000000", "0.020")]
    // [InlineData("12300000000000000", "0.012")]
    // public void EthToWei_Converts(string expected, string eth)
    // {
    //     var wei = eth.EthToWei();
    //     wei.Should().Be(expected);
    // }
}