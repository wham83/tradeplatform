using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

using BaseLibrary.MarketData;

namespace BaseLibrary
{
    public class DistributionData
    {
        public DateTime startTime;
        protected long startTick;

        public double last_price;
        public double total_volume;
        public double average_price;
        
        public void Start()
        {
            startTick = Environment.TickCount64;
        }

        public bool IsFull()
        {
            double elaspedSeconds = (Environment.TickCount64 - startTick) / 1000;

            if (elaspedSeconds >= 30)
                return false;

            return true;
        }


        public void ApplyTradeData(Trade tradeData)
        {
            last_price = tradeData.trade_price;

            double beforeTotal = total_volume * average_price;
            
            double total = 0;

            if ( total_volume != 0 )
                total = beforeTotal + tradeData.trade_volume * tradeData.trade_price;

            total_volume += tradeData.trade_volume;
            average_price = total / total_volume;
        }
    }

    //최소 단위의 봉을 만들어본다.
    //원하는 세팅을 넣으면 해당 세팅에 결과값의 봉을 돌려준다.
    public class TradeVolume
    {
        private List<Trade> tradeDataList = new List<Trade>();

        public List<DistributionData> distributionDatas30Sec;

        private DistributionData currentDistributionData = new DistributionData();

        public TradeVolume(int capacity = 0)
        {
            if (capacity == 0)
                distributionDatas30Sec = new List<DistributionData>();
            else
                distributionDatas30Sec = new List<DistributionData>(capacity);
        }

        public void Add(Trade trade)
        {
            if ( true == currentDistributionData.IsFull())
            {
                distributionDatas30Sec.Add(currentDistributionData);
                currentDistributionData = new DistributionData();
                currentDistributionData.Start();
            }

            currentDistributionData.ApplyTradeData(trade);
            
            tradeDataList.Add(trade);
        }

        public List<DistributionData> GetDistributionData(int distributionByMin, int count)
        {
            List<DistributionData> distributions = new List<DistributionData>();

            return distributions;
        }

        public void FixedData()
        {

            //시간 단위로 짤라서 
            //OldData deleted
            
            //tradeDataList.RemoveAll(trade=>trade.timestamp )

        }

    }

    public class TimeUtil
    {
        public static DateTime JavaTimeStampToDateTime(double javaTimeStamp)
        {
            // Java timestamp is milliseconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(javaTimeStamp).ToLocalTime();
            return dateTime;
        }

        public static long ToTimeStamp(DateTime date)
        {
            DateTime javaDate = new DateTime(1970, 1, 1);
            TimeSpan javaDiff = date - javaDate;
            // 1370509200000 
            long longTime = (long)javaDiff.TotalMilliseconds;

            return longTime;
        }

    }
}
