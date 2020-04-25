using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketGuru.BusinessLogic
{
    public static class Utilities
    {
        public static string CalculateColor(double price, double RegularMarketPreviousClose)
        {
            // if current market price above previous day close price, then green, Otherwise red
            var color = ((price - RegularMarketPreviousClose) > 0) ? "green" : "red";
            return color;
        }

        public static bool BuyOrSell(long RegularMarketVolume, long AverageDailyVolume10Day, double price, double FiftyDayAverage)
        {
            // if current volume greater than 10 day avg volume and current price greater than 50 day average, then buy. Otherwise sell
            bool recommend = (RegularMarketVolume > AverageDailyVolume10Day) && (price > FiftyDayAverage) ? true : false;
            return recommend;
        }

        public static DateTime HumanReadableTime(long unixTime)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);
            return epoch.AddSeconds(unixTime);
        }
    }
}
