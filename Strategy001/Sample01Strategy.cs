using System;
using BaseLibrary;
using BaseLibrary.Indicators;
using BaseLibrary.MarketData;

namespace Strategy001
{
    public class Sample01Strategy : IStrategy
    {
        private MarketDataBridge marketDataBridge;

        //거래 데이터 관리.
        private TradeVolume tradeData = new TradeVolume(200);

        private ITradeMainSystem tradeSystem;

        private MovingAverage tradeMovingAvg = new MovingAverage();

        /// <summary>
        /// 물량이 받쳐주면서 가격이 상승할때.
        /// 
        /// </summary>
        /// <param name="marketDataBridge"></param>
        public void Initialize(ITradeMainSystem tradeSystem)
        {
            this.tradeSystem = tradeSystem;
            this.marketDataBridge = tradeSystem.GetMarketDataBridge("Upbit", DataType.trade, new System.Collections.Generic.List<string>() { "KRW-XRP" });

            
            marketDataBridge.OnTradeDataReceived += MarketDataBridge_OnTradeDataReceived;
            tradeMovingAvg.SetTradeVolume(tradeData);
        }

        public void Start()
        {

        }

        private void MarketDataBridge_OnTradeDataReceived(Trade obj)
        {
            Console.WriteLine(obj.trade_price);
            tradeData.Add(obj);
            tradeMovingAvg.Update();
        }

        public void SellAll()
        {

        }
    }
}
