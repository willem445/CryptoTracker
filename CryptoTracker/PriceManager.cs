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

        public List<string> coinApiUrlList = new List<string>();
        public List<float[]> valueArrayList = new List<float[]>();
        public List<float> coinPrice = new List<float>();

        public float totalProfit = 0.0F;
        public float totalValue = 0.0F;
        public float totalInvestment = 0.0F;

        public List<string[]> toolTipValues = new List<string[]>();


        public void UpdatePriceData()
        {
            APIUpdate();
            CalculateValue();
            CalculateProfit();
            CalculateTotalProfit();
        }


        private void APIUpdate()
        {
            coinPrice.Clear();
            toolTipValues.Clear();

            //Read data from API
            for (int i = 0; i < coinApiUrlList.Count; i++)
            {
                string input = coinApiUrlList[i];
                string[] values = new string[5];

                try
                {
                    var cli = new System.Net.WebClient();
                    string prices = cli.DownloadString(input);

                    dynamic results = JsonConvert.DeserializeObject<dynamic>(prices);

                    coinPrice.Add((float)(Convert.ToDouble(results[0].price_usd)));

                    values[0] = results[0].rank;
                    values[1] = results[0].market_cap_usd;
                    values[2] = results[0].percent_change_1h;
                    values[3] = results[0].percent_change_24h;
                    values[4] = results[0].percent_change_7d;
                    toolTipValues.Add(values);
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
            for (int i = 0; i < coinApiUrlList.Count; i++)
            {
                valueArrayList[i][(int)rowNames.Value] = valueArrayList[i][(int)rowNames.Quantity] * coinPrice[i];
                totalInvestment += valueArrayList[i][(int)rowNames.TotalInvested];
                totalValue += valueArrayList[i][(int)rowNames.Value];
            }
        }

        private void CalculateProfit()
        {
            for (int i = 0; i < coinApiUrlList.Count; i++)
            {
                valueArrayList[i][(int)rowNames.Profit] = valueArrayList[i][(int)rowNames.Value] - valueArrayList[i][(int)rowNames.TotalInvested];
                valueArrayList[i][(int)rowNames.ProfitPercent] = (valueArrayList[i][(int)rowNames.Profit] / valueArrayList[i][(int)rowNames.Value]) * 100;
            }
        }

        private void CalculateTotalProfit()
        {
            totalProfit = 0.0F;
            for (int i = 0; i < coinApiUrlList.Count; i++)
            {
                totalProfit += valueArrayList[i][(int)rowNames.Profit];
            }
        }




    }
}
