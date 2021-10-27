using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BaseLibrary.Indicators
{
    public interface IIndicator
    {
        void SetTradeVolume(TradeVolume volume);
        void Update();
    }

    /// <summary>
    /// 이동평균선인데...
    /// 
    /// </summary>
    public class MovingAverage : IIndicator
    {
        private TradeVolume tradeVolume;

        public void SetTradeVolume(TradeVolume volume)
        {
            tradeVolume = volume;
        }

        public void Update()
        {
            //
            //startTime을 비교해서 처리.
            //tradesystem으로부터 최신 볼륨들을 가져와서 정리.
            //tradeVolume.distributionDatas30Sec


        }

    }
}
