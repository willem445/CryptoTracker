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

        public List<string> coinApiUrlList = new List<string>();
        public List<float[]> valueArrayList = new List<float[]>();
        public List<float> coinPrice = new List<float>();

        public float totalProfit = 0.0F;
        public float totalValue = 0.0F;
        public float totalInvestment = 0.0F;




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

            //Read data from API
            for (int i = 0; i < coinApiUrlList.Count; i++)
            {
                string input = coinApiUrlList[i];

                try
                {
                    var cli = new System.Net.WebClient();
                    string prices = cli.DownloadString(input);

                    dynamic results = JsonConvert.DeserializeObject<dynamic>(prices);

                    coinPrice.Add((float)(Convert.ToDouble(results[0].price_usd)));
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
