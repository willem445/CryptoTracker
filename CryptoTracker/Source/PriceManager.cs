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
        public enum rowNames
        {
            Quantity,
            TotalInvested,
            Value,
            Profit,
            ProfitPercent
        }

        public List<string> coinApiUrlList = new List<string>(); //List of api URLs
        public List<float?[]> valueArrayList = new List<float?[]>(); //Mirrors textbox array list but holds float values
        public List<float?> coinPriceList = new List<float?>(); //Contains current price for each coin updated by APIUpdate()

        public float totalProfit = 0.0F;
        public float totalValue = 0.0F;
        public float totalInvestment = 0.0F;

        public List<string[]> toolTipValues = new List<string[]>();

        int coinCount;

        public void UpdatePriceData()
        {
            APIUpdate();
            UpdateValues();
        }


        private void APIUpdate()
        {
            coinPriceList.Clear();
            toolTipValues.Clear();

            List<float?> tempCoinPrice = new List<float?>();

            //Read data from API
            for (int i = 0; i < coinCount; i++)
            {
                string input = coinApiUrlList[i];
                string[] values = new string[5];

                try
                {
                    var cli = new System.Net.WebClient();
                    string prices = cli.DownloadString(input);

                    dynamic results = JsonConvert.DeserializeObject<dynamic>(prices);

                    tempCoinPrice.Add((float)(Convert.ToDouble(results[0].price_usd)));

                    values[0] = results[0].rank;
                    values[1] = results[0].market_cap_usd;
                    values[2] = results[0].percent_change_1h;
                    values[3] = results[0].percent_change_24h;
                    values[4] = results[0].percent_change_7d;
                    toolTipValues.Add(values);
                }
                catch (System.Net.WebException e)
                {
                    tempCoinPrice.Add(null);
                }
            }

            if (tempCoinPrice.Count == coinCount)
            {
                coinPriceList = tempCoinPrice;
            }
        }

        private void UpdateValues()
        {
            for (int i = 0; i < coinCount; i++)
            {
                totalProfit = 0.0F;
                totalValue = 0.0F;
                totalInvestment = 0.0F;

                if (coinPriceList[i].HasValue)
                {
                    valueArrayList[i][(int)rowNames.Value] = valueArrayList[i][(int)rowNames.Quantity] * coinPriceList[i];
                    valueArrayList[i][(int)rowNames.Profit] = valueArrayList[i][(int)rowNames.Value] - valueArrayList[i][(int)rowNames.TotalInvested];
                    valueArrayList[i][(int)rowNames.ProfitPercent] = (valueArrayList[i][(int)rowNames.Profit] / valueArrayList[i][(int)rowNames.Value]) * 100;

                    totalInvestment += valueArrayList[i][(int)rowNames.TotalInvested].Value;
                    totalValue += valueArrayList[i][(int)rowNames.Value].Value;
                    totalProfit += valueArrayList[i][(int)rowNames.Profit].Value;
                }
                else
                {
                    valueArrayList[i][(int)rowNames.Value] = null;
                    valueArrayList[i][(int)rowNames.Profit] = null;
                    valueArrayList[i][(int)rowNames.ProfitPercent] = null;
                }
            }
        }

        public void AddNewCoin(CoinModel addCoin)
        {
            coinApiUrlList.Add(addCoin.APILink);

            float?[] coinValues = new float?[5]; //Create array to be added to valueArrayList
            coinValues[(int)PriceManager.rowNames.Quantity] = (float)Convert.ToDouble(addCoin.Quantity);
            coinValues[(int)PriceManager.rowNames.TotalInvested] = (float)Convert.ToDouble(addCoin.NetCost);
            valueArrayList.Add(coinValues);

            coinCount++;
        }

    }
}
