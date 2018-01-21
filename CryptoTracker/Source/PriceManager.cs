using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CryptoTracker
{
    class PriceManager
    {
        //Enums*********************************************************************************


        //Fields********************************************************************************
        public List<CoinModel> coinModelList = new List<CoinModel>();

        //Fields to hold total investement data
        public float totalProfit = 0.0F;
        public float totalValue = 0.0F;
        public float totalInvestment = 0.0F;

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
            //Read data from API
            for (int i = 0; i < coinModelList.Count; i++)
            {
                string input = coinModelList[i].APILink;

                try
                {
                    //Connect to API
                    var cli = new System.Net.WebClient();
                    string prices = cli.DownloadString(input);
                    dynamic results = JsonConvert.DeserializeObject<dynamic>(prices);

                    //Add price to temp list
                    coinModelList[i].Price = results[0].price_usd;

                    //Update tool tip array and add array to tool tip list
                    coinModelList[i].Rank = results[0].rank;
                    coinModelList[i].MarketCap = results[0].market_cap_usd;
                    coinModelList[i].Percent_Change_1h = results[0].percent_change_1h;
                    coinModelList[i].Percent_Change_24hr = results[0].percent_change_24h;
                    coinModelList[i].Percent_Change_7d = results[0].percent_change_7d;
                    coinModelList[i].Symbol = results[0].symbol;
                }
                catch (System.Net.WebException e)
                {
                    //If there is an error connecting to the API, fill list with null data to avoid index out of bounds later
                    coinModelList[i].Price = "";
                }
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

            for (int i = 0; i < coinModelList.Count; i++)
            {              
                    coinModelList[i].Value = coinModelList[i].Quantity * (float)Convert.ToDouble(coinModelList[i].Price);
                    coinModelList[i].Profit = coinModelList[i].Value - coinModelList[i].NetCost;
                    coinModelList[i].ProfitPercent = (coinModelList[i].Profit / coinModelList[i].Value) * 100;

                    totalInvestment += coinModelList[i].NetCost;
                    totalValue += coinModelList[i].Value;
                    totalProfit += coinModelList[i].Profit;
            }
        }

        /// <summary>
        /// Creates new array in valueArrayList for new coin and populates quantity and net cost
        /// </summary>
        /// <param name="addCoin">CoinModel class holding coin related data</param>
        public void AddNewCoin(CoinModel addCoin)
        {
            //coinApiUrlList.Add(addCoin.APILink); //Add api url to apiurllist

            //float?[] coinValues = new float?[5]; //Create array to be added to valueArrayList
            //coinValues[(int)PriceManager.rowNames.Quantity] = (float)Convert.ToDouble(addCoin.Quantity);
            //coinValues[(int)PriceManager.rowNames.TotalInvested] = (float)Convert.ToDouble(addCoin.NetCost);
            //valueArrayList.Add(coinValues);

            //coinCount++;
        }
    }
}
