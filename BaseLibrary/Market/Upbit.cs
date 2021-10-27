using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using BaseLibrary.MarketData;
namespace BaseLibrary.Market
{
   
    
    public class Upbit : IMarket
    {
        public event Action<Trade> OnTradeDataReceived;
        public event Action<Ticker> OnTickerDataReceived;
        public event Action<OrderBook> OnOrderBookDataReceived;

        private WebSocket webSocket = new WebSocket("wss://api.upbit.com/websocket/v1");

        public void Initialize(DataType dataType, List<string> coinList)
        {
            connect();
            AttachData(dataType, coinList);
        }

        public void AttachData(DataType dataType, List<string> codeList)
        {
            JArray array = new JArray();
            foreach ( var current in codeList)
                array.Add(current);
            //array.Add("BTC-XRP");

            JObject obj1 = new JObject();
            obj1["ticket"] = Guid.NewGuid();//UUID

            JObject obj2 = new JObject();
            obj2["type"] = DataType.trade.ToString();
            obj2["codes"] = array;

            JObject obj3 = new JObject();
            obj3["type"] = DataType.orderbook.ToString();
            obj3["codes"] = array;

            JObject obj4 = new JObject();
            obj4["type"] = DataType.ticker.ToString();
            obj4["codes"] = array;

            string sendMsg = string.Format("[{0},{1},{2},{3}]", obj1.ToString(), obj2.ToString(), obj3.ToString(), obj4.ToString());
            webSocket.Send(sendMsg);
        }

        internal void AttachData(object trade, string v)
        {
            throw new NotImplementedException();
        }

        public void connect()
        {
            JArray array = new JArray();
            array.Add("KRW-BTC");
            array.Add("BTC-XRP");

            JObject obj1 = new JObject();
            obj1["ticket"] = Guid.NewGuid();//UUID

            JObject obj2 = new JObject();
            obj2["type"] = DataType.trade.ToString();
            obj2["codes"] = array;

            JObject obj3 = new JObject();
            obj3["type"] = DataType.orderbook.ToString();
            obj3["codes"] = array;

            string sendMsg = string.Format("[{0},{1},{2}]", obj1.ToString(), obj2.ToString(), obj3.ToString());

            try
            {
                
                webSocket.OnMessage += WebSocket_OnMessage;
                webSocket.Connect();
                if (webSocket.ReadyState == WebSocketState.Open)
                {
                    webSocket.Send(sendMsg);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }


            //try
            //{
            //    string url = "wss://api.upbit.com/websocket/v1";
            //    var uri = new Uri(url);
            //    using (var client = new Websocket.Client.WebsocketClient(uri))
            //    {
            //        client.ReconnectTimeout = TimeSpan.FromSeconds(30);
            //        //client.ReconnectionHappened.Subscribe(info =>
            //        //    Log.Information($"Reconnection happened, type: {info.Type}"));

            //        client.MessageReceived.Subscribe(msg => Console.WriteLine($"Message received: {msg}"));
            //        client.Start();

            //        Task.Run(() => client.Send(sendMsg));

            //        //exitEvent.WaitOne();
            //    }

            //    //Websocket.Client.WebsocketClient(uri);
            //    //WebSocket webSocket = new WebSocket("wss://api.upbit.com/websocket/v1");
            //    //webSocket.OnMessage += WebSocket_OnMessage;
            //    //webSocket.Connect();
            //    //if (webSocket.ReadyState == WebSocketState.Open)
            //    //{
            //    //    webSocket.Send(sendMsg);
            //    //}
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.ToString());
            //}


        }
        //https://docs.upbit.com/docs/upbit-quotation-websocket
        //맨 앞에 type을 읽어서 type에 해당되는 녀석으로 deserializeObject해주면 된다.
        private void WebSocket_OnMessage(object sender, MessageEventArgs e)
        {
            string requestMsg = Encoding.UTF8.GetString(e.RawData);
            var commonData = JsonConvert.DeserializeObject<Common>(requestMsg);
            //message타입에 따라 
            if (commonData.type == DataType.trade.ToString())
            {
                var tradeData = JsonConvert.DeserializeObject<Trade>(requestMsg);
                //Console.WriteLine(tradeData);
                OnTradeDataReceived?.Invoke(tradeData);
            }
            else if ( commonData.type == DataType.ticker.ToString())
            {
                var tickerData = JsonConvert.DeserializeObject<Ticker>(requestMsg);
                OnTickerDataReceived?.Invoke(tickerData);
                //Console.WriteLine(tickerData);
            }
            else if (commonData.type == DataType.orderbook.ToString())
            {
                var orderBookData = JsonConvert.DeserializeObject<OrderBook>(requestMsg);
                OnOrderBookDataReceived?.Invoke(orderBookData);
                //Console.WriteLine(orderBookData);
            }
            //Console.WriteLine(requestMsg);
        }
    }
}
