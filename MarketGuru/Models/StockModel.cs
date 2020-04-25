using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YahooFinanceApi;
using Newtonsoft.Json;

namespace MarketGuru.Models
{
    public class Quote
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "Scrip")]
        public string Scrip { get; set; }

        [JsonProperty(PropertyName = "MktTime")]
        public long MktTime { get; set; }

        [JsonProperty(PropertyName = "ReadableMktTime")]
        public DateTime ReadableMktTime { get; set; }

        [JsonProperty(PropertyName = "Currentprice")]
        public double Currentprice { get; set; }

        [JsonProperty(PropertyName = "High")]
        public double High { get; set; }

        [JsonProperty(PropertyName = "Low")]
        public double Low { get; set; }

        [JsonProperty(PropertyName = "Volume")]
        public long Volume { get; set; }

        [JsonProperty(PropertyName = "Avg10DVolume")]
        public long Avg10DVolume { get; set;}

        [JsonProperty(PropertyName = "FiftyDayAverage")]
        public double FiftyDayAverage { get; set; }

        [JsonProperty(PropertyName = "Recommendation")]
        public bool Recommendation { get; set; }

        [JsonProperty(PropertyName = "Color")]
        public string Color { get; set; }

        [JsonProperty(PropertyName = "PreviousClose")]
        public double PreviousClose { get; set; }

        [JsonProperty(PropertyName = "History")]
        public object History { get; set; }

        public string ErrorMessage { get; set; }

    }
    public class StockModel
    {
  
        public async Task<Quote> GetQuote(string scrip)
        {

            // You could query multiple symbols with multiple fields through the following steps:
            var securities = await Yahoo.Symbols(scrip).Fields(Field.Symbol, Field.RegularMarketPrice, Field.FiftyTwoWeekHigh,
                Field.RegularMarketDayHigh, Field.RegularMarketDayLow, Field.RegularMarketPreviousClose, Field.AverageDailyVolume10Day,
                Field.RegularMarketVolume, Field.FiftyDayAverage, Field.FiftyDayAverageChange, Field.FiftyDayAverageChangePercent, Field.RegularMarketTime
                ).QueryAsync();
            var sec = securities[scrip];

            //trend over past years
            var history = await Yahoo.GetHistoricalAsync(scrip, new DateTime(2019, 1, 1), new DateTime(2020, 4, 1), Period.Monthly);

            Quote myQuote = new Quote();
            myQuote.Scrip = sec.Symbol;
            myQuote.MktTime = sec.RegularMarketTime;
            myQuote.Currentprice = sec.RegularMarketPrice;
            myQuote.High = sec.RegularMarketDayHigh;
            myQuote.Low = sec.RegularMarketDayLow;
            myQuote.Volume = sec.RegularMarketVolume;
            myQuote.Avg10DVolume = sec.AverageDailyVolume10Day;
            myQuote.FiftyDayAverage = sec.FiftyDayAverage;
            myQuote.PreviousClose = sec.RegularMarketPreviousClose;
            myQuote.History = history;

            return myQuote;
        }
    }
}
