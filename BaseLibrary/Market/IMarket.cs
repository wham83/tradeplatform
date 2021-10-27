using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseLibrary.MarketData;

namespace BaseLibrary.Market
{
    public interface IMarket
    {
        void Initialize(DataType dataType, List<string> codeList);

        event Action<Trade> OnTradeDataReceived;
        event Action<Ticker> OnTickerDataReceived;
        event Action<OrderBook> OnOrderBookDataReceived;
    }
}
