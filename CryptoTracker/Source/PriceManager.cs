using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CryptoTracker
{
    class PriceManager
    {
        //Enums*********************************************************************************
        public enum rowNames
        {
            Quantity,
            TotalInvested,
            Value,
            Profit,
            ProfitPercent
        }

        //Fields********************************************************************************
        public List<string> coinApiUrlList = new List<string>(); //List of api URLs
        public List<float?[]> valueArrayList = new List<float?[]>(); //Mirrors textbox array list but holds float values
        public List<float?> coinPriceList = new List<float?>(); //Contains current price for each coin updated by APIUpdate()

        //Fields to hold total investement data
        public float totalProfit = 0.0F;
        public float totalValue = 0.0F;
        public float totalInvestment = 0.0F;

        public List<string[]> toolTipValues = new List<string[]>(); //List of arrays holding tooltip data for each coin (marketcap, rank, % change, etc)

        int coinCount; //Number of coins currently added to the form

        //Constructor***************************************************************************
        public void UpdatePriceData()
        {
            APIUpdate();
            UpdateValues();
        }

        //Methods*******************************************************************************
        /// <summary>
        /// Connect to coinmarketcap API and retrieve data for each coin added to form
        /// </summary>
        private void APIUpdate()
        {
            //Clear lists before updating new data
            coinPriceList.Clear();
            toolTipValues.Clear();

            List<float?> tempCoinPrice = new List<float?>(); //Temp list to hold price data

            //Read data from API
            for (int i = 0; i < coinCount; i++)
            {
                string input = coinApiUrlList[i];
                string[] values = new string[5]; //Temp array to hold tool tip data

                try
                {
                    //Connect to API
                    var cli = new System.Net.WebClient();
                    string prices = cli.DownloadString(input);
                    dynamic results = JsonConvert.DeserializeObject<dynamic>(prices);

                    //Add price to temp list
                    tempCoinPrice.Add((float)(Convert.ToDouble(results[0].price_usd)));

                    //Update tool tip array and add array to tool tip list
                    values[0] = results[0].rank;
                    values[1] = results[0].market_cap_usd;
                    values[2] = results[0].percent_change_1h;
                    values[3] = results[0].percent_change_24h;
                    values[4] = results[0].percent_change_7d;
                    toolTipValues.Add(values);
                }
                catch (System.Net.WebException e)
                {
                    //If there is an error connecting to the API, fill list with null data to avoid index out of bounds later
                    tempCoinPrice.Add(null);
                    values[0] = "0";
                    values[1] = "0";
                    values[2] = "0";
                    values[3] = "0";
                    values[4] = "0";
                    toolTipValues.Add(values);
                }
            }

            //If there wasn't any errors reading data, update the coinPriceList
            if (tempCoinPrice.Count == coinCount)
            {
                coinPriceList = tempCoinPrice;
            }
        }

        /// <summary>
        /// Updates values in valueArrayList for each coin. Also calculates total investement data.
        /// </summary>
        private void UpdateValues()
        {
            totalProfit = 0.0F;
            totalValue = 0.0F;
            totalInvestment = 0.0F;

            for (int i = 0; i < coinCount; i++)
            {
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

        /// <summary>
        /// Creates new array in valueArrayList for new coin and populates quantity and net cost
        /// </summary>
        /// <param name="addCoin">CoinModel class holding coin related data</param>
        public void AddNewCoin(CoinModel addCoin)
        {
            coinApiUrlList.Add(addCoin.APILink); //Add api url to apiurllist

            float?[] coinValues = new float?[5]; //Create array to be added to valueArrayList
            coinValues[(int)PriceManager.rowNames.Quantity] = (float)Convert.ToDouble(addCoin.Quantity);
            coinValues[(int)PriceManager.rowNames.TotalInvested] = (float)Convert.ToDouble(addCoin.NetCost);
            valueArrayList.Add(coinValues);

            coinCount++;
        }
    }
}
