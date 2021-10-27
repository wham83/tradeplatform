using BaseLibrary.Market;
using BaseLibrary.MarketData;
using System;
using System.Collections.Generic;

namespace BaseLibrary
{
    class Program
    {
        public static TradeSystem tradeSystem = new TradeSystem();
        
        static void Main(string[] args)
        {
            Upbit upbit = new Upbit();
            upbit.connect();
            upbit.OnOrderBookDataReceived += Upbit_OnOrderBookDataReceived;
            upbit.OnTradeDataReceived += Upbit_OnTradeDataReceived;
            upbit.OnTickerDataReceived += Upbit_OnTickerDataReceived;
            upbit.AttachData(DataType.trade, new List<string>() { "KRW-XRP", "KRW-ETH" });

            LoadMarkets();

            while (true)
            {
                System.Threading.Thread.Sleep(10);
            }
        }

        private static async void LoadMarkets()
        {
            var publicAPI = new CCXT.NET.Upbit.Public.PublicApi();
            var markets = await publicAPI.LoadMarketsAsync();
            //var tradeAPI = new CCXT.NET.Upbit.Trade.TradeApi();
            //tradeAPI.
            var coins = new Dictionary<string, object>();
            coins.Add("KRW-BTC", "KRW-BTC");
            var tickers  = await publicAPI.FetchTickersAsync(coins);
            Console.WriteLine(markets);
        }

        private static void Upbit_OnTickerDataReceived(Ticker obj)
        {
            Console.Write(obj.code);
            Console.Write("   " + obj.type);
        }

        //분봉 만들기.
        private static void Upbit_OnTradeDataReceived(Trade obj)
        {
            //결정해야 하는 거시...
            //candle데이터를 분봉으로 계속 요청. 종목별로 다른 종목에 대한 요청.
            //long milSec = DateTime.Today.ToString("fff");
            long milSec = DateTime.Now.Ticks;
            Console.WriteLine(ToTimeStamp());
            Console.WriteLine(obj.timestamp);
            var dt = JavaTimeStampToDateTime(obj.timestamp);

            Console.Write(obj.code);
            Console.Write("   " + obj.type);
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        public static DateTime JavaTimeStampToDateTime(double javaTimeStamp)
        {
            // Java timestamp is milliseconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(javaTimeStamp).ToLocalTime();
            return dateTime;

        }

        public static long ToTimeStamp()
        {
            DateTime date = DateTime.Now;
            DateTime javaDate = new DateTime(1970, 1, 1);
            TimeSpan javaDiff = date - javaDate;
            // 1370509200000 
            long longTime = (long)javaDiff.TotalMilliseconds;

            //Console.WriteLine("Date: {0} => {1}", date, longTime);
            return longTime;
        }

        List<Trade> tradeVolumes = new List<Trade>();

        private static void Upbit_OnOrderBookDataReceived(OrderBook obj)
        {
            Console.Write(obj.code);
            Console.Write("   " + obj.type);
        }
    }
}