using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoTracker
{
    class PriceManager
    {
        enum rowNames
        {
            Quantity,
            TotalInvested,
            Value,
            Profit,
            ProfitPercent
        }

        string[] coins =
        {
            "https://api.coinmarketcap.com/v1/ticker/bitcoin/",
            "https://api.coinmarketcap.com/v1/ticker/ethereum/",
            "https://api.coinmarketcap.com/v1/ticker/litecoin/",
            "https://api.coinmarketcap.com/v1/ticker/stellar/",
            "https://api.coinmarketcap.com/v1/ticker/ripple/",
            "https://api.coinmarketcap.com/v1/ticker/iota/",
            "https://api.coinmarketcap.com/v1/ticker/walton/",
            "https://api.coinmarketcap.com/v1/ticker/request-network/",

            "https://api.coinmarketcap.com/v1/ticker/icon/",
            "https://api.coinmarketcap.com/v1/ticker/vechain/",
            "https://api.coinmarketcap.com/v1/ticker/binance-coin/",
            "https://api.coinmarketcap.com/v1/ticker/oyster-pearl/",
            "https://api.coinmarketcap.com/v1/ticker/bounty0x/",
            "https://api.coinmarketcap.com/v1/ticker/dent/",
            "https://api.coinmarketcap.com/v1/ticker/chainlink/"
        };

        public float[,] valueArray =
        {
            { 0.0F, 0.0F, 0.0F, 0.0F, 0.0F},
            { 0.0F, 0.0F, 0.0F, 0.0F, 0.0F},
            { 0.0F, 0.0F, 0.0F, 0.0F, 0.0F},
            { 0.0F, 0.0F, 0.0F, 0.0F, 0.0F},
            { 0.0F, 0.0F, 0.0F, 0.0F, 0.0F},
            { 0.0F, 0.0F, 0.0F, 0.0F, 0.0F},
            { 0.0F, 0.0F, 0.0F, 0.0F, 0.0F},
            { 0.0F, 0.0F, 0.0F, 0.0F, 0.0F},

            { 0.0F, 0.0F, 0.0F, 0.0F, 0.0F},
            { 0.0F, 0.0F, 0.0F, 0.0F, 0.0F},
            { 0.0F, 0.0F, 0.0F, 0.0F, 0.0F},
            { 0.0F, 0.0F, 0.0F, 0.0F, 0.0F},
            { 0.0F, 0.0F, 0.0F, 0.0F, 0.0F},
            { 0.0F, 0.0F, 0.0F, 0.0F, 0.0F},
            { 0.0F, 0.0F, 0.0F, 0.0F, 0.0F}
        };

        public float[] coinPrice = new float[15];

        public float totalProfit = 0.0F;
        public float totalValue = 0.0F;
        public float totalInvestment = 0.0F;

        public void ParseSavedData(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);

            int i = 0;
            foreach (string line in lines)
            {
                string[] data = line.Split(',');
                valueArray[i, (int)rowNames.Quantity] = (float)Convert.ToDouble(data[1]);
                valueArray[i, (int)rowNames.TotalInvested] = (float)Convert.ToDouble(data[2]);
                i++;
            }
        }

        public void UpdatePriceData()
        {
            APIUpdate();
            CalculateValue();
            CalculateProfit();
            CalculateTotalProfit();
        }


        private void APIUpdate()
        {
            //Read data from API
            for (int i = 0; i < coins.Length; i++)
            {
                string input = coins[i];

                try
                {
                    var cli = new System.Net.WebClient();
                    string prices = cli.DownloadString(input);

                    dynamic results = JsonConvert.DeserializeObject<dynamic>(prices);

                    coinPrice[i] = (float)(Convert.ToDouble(results[0].price_usd));
                }
                catch
                {
                    MessageBox.Show("Count not connect to API");
                }

            }
        }

        private void CalculateValue()
        {
            totalValue = 0.0F;
            totalInvestment = 0.0F;
            for (int i = 0; i < coins.Length; i++)
            {
                valueArray[i, (int)rowNames.Value] = valueArray[i, (int)rowNames.Quantity] * coinPrice[i];
                totalInvestment += valueArray[i, (int)rowNames.TotalInvested];
                totalValue += valueArray[i, (int)rowNames.Value];
            }
        }

        private void CalculateProfit()
        {
            for (int i = 0; i < coins.Length; i++)
            {
                valueArray[i, (int)rowNames.Profit] = valueArray[i, (int)rowNames.Value] - valueArray[i, (int)rowNames.TotalInvested];
                valueArray[i, (int)rowNames.ProfitPercent] = (valueArray[i, (int)rowNames.Profit] / valueArray[i, (int)rowNames.Value]) * 100;
            }
        }

        private void CalculateTotalProfit()
        {
            totalProfit = 0.0F;
            for (int i = 0; i < coins.Length; i++)
            {
                totalProfit += valueArray[i, (int)rowNames.Profit];
            }
        }




    }
}
