using System;
using Xunit;
using System.Collections.Generic;
using System.Text;
using MarketGuru.BusinessLogic;

namespace MarketGuru.Tests.BusinessLogic
{
    public class UtilitiesTests
    {
        [Fact]
        public void CalculateColor_returnGreen()
        {
        Assert.Equal("green", Utilities.CalculateColor(100, 90));
        }

        [Fact]
        public void CalculateColor_returnRed()
        {
            Assert.Equal("red", Utilities.CalculateColor(100, 110));
        }

        [Fact]
        public void BuyOrSell_returnBuy()
        {
            Assert.True(Utilities.BuyOrSell(1000, 900, 100, 90));
        }

        [Fact]
        public void BuyOrSell_returnSell()
        {
            Assert.False(Utilities.BuyOrSell(1000, 900, 100, 110));
        }

        [Fact]
        public void HumanReadableTime_returnTime()
        {
            Assert.Equal(new DateTime(2015, 12, 25), Utilities.HumanReadableTime(1451001600));
        }
    }
}
