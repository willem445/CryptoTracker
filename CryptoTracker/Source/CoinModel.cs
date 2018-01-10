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
        public string Quantity { get; set; }
        public string NetCost { get; set; }
        public string APILink { get; set; }
    }
}
