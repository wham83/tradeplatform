using BaseLibrary.MarketData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary
{
    public interface ITradeMainSystem
    {
        MarketDataBridge GetMarketDataBridge(string marketName, DataType dataType, List<string> codeList);

    }

    public class TradeSystem : ITradeMainSystem
    {
        private IStrategy strategy = null;

        public MarketDataBridge GetMarketDataBridge(string marketName, DataType dataType, List<string> codeList)
        {
            MarketDataBridge marketDataBridge = new MarketDataBridge();
            marketDataBridge.GetMarket(marketName, dataType, codeList);
            return marketDataBridge;
        }

        public void Start(IStrategy strategy)
        {
            this.strategy = strategy;
            this.strategy.Initialize(this);
            this.strategy.Start();
        }



    }
}
