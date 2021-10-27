using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary
{
    public interface IStrategy
    {
        void Start();

        void Initialize(ITradeMainSystem tradeSystem);

        /// <summary>
        /// 시스템에서 전체적으로 청산을 원할때 처리하는걸 고려해보겠다라는 개념.
        /// </summary>
        void SellAll();


        string ToString();
    }
}
