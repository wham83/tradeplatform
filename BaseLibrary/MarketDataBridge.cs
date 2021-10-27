using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseLibrary.Market;
using BaseLibrary.MarketData;

namespace BaseLibrary
{
    public interface IMarketOrder
    {

    }

    public class MarketDataBridge
    {
        public event Action<Trade> OnTradeDataReceived;
        public event Action<Ticker> OnTickerDataReceived;
        public event Action<OrderBook> OnOrderBookDataReceived;

        public IMarket market;
        public IMarketOrder marketOrderBridge;

        public IMarket GetMarket(string marketName, DataType dataType, List<string> codeList)
        {
            switch(marketName)
            {
                case "Upbit":
                    market = new Upbit();
                    break;
            }
            market.Initialize(dataType, codeList);

            market.OnOrderBookDataReceived += Market_OnOrderBookDataReceived;
            market.OnTradeDataReceived += Market_OnTradeDataReceived;
            market.OnTickerDataReceived += Market_OnTickerDataReceived;
            return market;

        }

        private void Market_OnTickerDataReceived(Ticker obj)
        {
            OnTickerDataReceived?.Invoke(obj);
        }

        private void Market_OnTradeDataReceived(Trade obj)
        {
            OnTradeDataReceived?.Invoke(obj);
        }

        private void Market_OnOrderBookDataReceived(OrderBook obj)
        {
            OnOrderBookDataReceived?.Invoke(obj);
        }
    }


}
