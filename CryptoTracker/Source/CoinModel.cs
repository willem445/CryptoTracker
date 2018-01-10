using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker
{
    public class CoinModel
    {
        public struct Coin
        {
            public string name;
            public float percent_change_1h;
            public float percent_change_24hr;
            public float percent_change_7d;
            public float quantity;
            public float totalInvested;
            public float value;
            public float profit;
            public float profitPercent;
        }

        public string CoinName { get; set; }

        public float Percent_Change_1h { get; set; }
        public float Percent_Change_24hr { get; set; }
        public float Percent_Change_7d { get; set; }

        public float Quantity { get; set; }
        public float NetCost { get; set; }
        public float TotalInvested { get; set; }
        public float Profit { get; set; }
        public float ProfitPercent { get; set; }
        public string APILink { get; set; }
    }
}
