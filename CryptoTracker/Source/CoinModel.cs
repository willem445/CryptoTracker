using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker
{
    public class CoinModel
    {
        public string CoinName { get; set; }
        public string CoinTradeName { get; set; }

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
