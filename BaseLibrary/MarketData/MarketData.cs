using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.MarketData
{
    public enum DataType
    {
        ticker,
        trade,
        orderbook

    }

    public class Common
    {
        public string type;
        public string code;


    }

    public class Trade : Common
    {
        public double trade_price;
        public double trade_volume;
        public string ask_bid;
        public double prev_closing_price;
        public string change;
        public double change_price;
        public string trade_date;
        public string trade_time;
        public long trade_timestamp;
        public long timestamp;
        public long sequential_id;
        public string stream_type;
    }

    public class Ticker : Common
    {
        public double opening_price;
        public double high_price;
        public double low_price;
        public double trade_price;
        public double prev_closing_price;
        public string change;
        public double change_price;
        public double signed_change_price;
        public double change_rate;
        public double signed_change_rate;
        public double trade_volume;
        public double acc_trade_volume;
        public double acc_trade_volume_24h;
        public double acc_trade_price;
        public double acc_trade_price_24h;
        public string trade_date;
        public string trade_time;
        public long trade_timestamp;
        public string ask_bid;
        public double acc_ask_volume;
        public double acc_bid_volume;
        public double highest_52_week_price;
        public string highest_52_week_date;
        public double lowest_52_week_price;
        public string lowest_52_week_date;
        public string trade_status;
        public string market_state;
        public string market_state_for_ios;
        public Boolean is_trading_suspended;
        //이것도 확인 필요.
        public string delisting_date;
        public string market_warning;
        public long timestamp;
        public string stream_type;
    }

    public class OrderBook : Common
    {
        public double total_ask_size;
        public double total_bid_size;

        //이거 확인해봐야함
        //public List of Objects orderbook_units;
        public List<OrderBookUnit> orderbook_units;
    }

    public class OrderBookUnit
    {
        public double ask_price;
        public double bid_price;
        public double ask_size;
        public double bid_size;
        public long timestamp;
    }
}
