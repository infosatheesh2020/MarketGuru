using System;
using Xunit;
using System.Collections.Generic;
using System.Text;
using MarketGuru.Models;


namespace MarketGuru.Tests.Models
{
    public class StockModelTests
    {
        [Fact]
        public void GetQuote_returnQuote()
        {
            Quote quote = new Quote();
            StockModel model = new StockModel();
            quote = model.GetQuote("MSFT").Result;
            Assert.Equal("MSFT", quote.Scrip);
        }
    }
}
