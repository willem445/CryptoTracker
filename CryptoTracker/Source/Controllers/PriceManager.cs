using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace CryptoTracker
{
    class PriceManager
    {
        //Constants****************************************************************************
        private const string ALL_COIN_LIMIT = "0"; 
        private const string BUY = "BUY";
        private const string SELL = "SELL";
        private const int TRADE_COIN = 0;
        private const int TRADE_BASE = 1;

        //Enums*********************************************************************************
        /// <summary>
        /// Enumeration to describe each index in the data table
        /// </summary>
        enum DataTableRows
        {
            Date,
            Exchange,
            TradePair,
            Type,
            OrderQuantity,
            TradePrice,
            OrderCost,
            NetCostUSD
        }

        //Fields********************************************************************************
        private List<CoinModel> trackedCoinList = new List<CoinModel>();
        private List<CoinModel.CoinNameStruct> allCoinNames = new List<CoinModel.CoinNameStruct>();

        //Fields to hold total investement data
        private float totalProfit = 0.0F;
        private float totalValue = 0.0F;
        private float totalInvestment = 0.0F;
        private float totalFiatCost = 0.0F; //Total of USD bought and sold

        //Properties****************************************************************************
        public float TotalProfit
        {
            get
            {
                return totalProfit;
            }
        }
        public float TotalValue
        {
            get
            {
                return totalValue;
            }
        }
        public float TotalInvestment
        {
            get
            {
                return totalInvestment;
            }
        }
        public float TotalFiatCost
        {
            get
            {
                return totalFiatCost;
            }
        }
        public List<CoinModel.CoinNameStruct> AllCoinNames
        {
            get
            {
                return allCoinNames;
            }
        }
        public List<CoinModel> TrackedCoinList
        {
            get
            {
                return trackedCoinList;
            }
        }

        //Constructor***************************************************************************
        public PriceManager(IProgress<int> progress)
        {
            Thread getCoinNames = new Thread(new ThreadStart(GetAllCoinNames));
            getCoinNames.Start();

            progress.Report(20);

            //Parse data in documents folder
            FileIO file = new FileIO();
            trackedCoinList = file.ParseSavedData();

            progress.Report(30);

            //Update prices based on parsed data
            UpdatePriceData();

            progress.Report(60);
        }

        //Methods*******************************************************************************
        /// <summary>
        /// Gets data from API and calculates profits, value, etc.
        /// </summary>
        public void UpdatePriceData()
        {
            APIUpdate();
            UpdateValues();
        }

        /// <summary>
        /// Retrieves the names of all current cryptocurrencies listed on coinmarketcap
        /// </summary>
        private void GetAllCoinNames()
        {
            string input = "https://api.coinmarketcap.com/v1/ticker/?limit=" + ALL_COIN_LIMIT;

            //Connect to API
            var cli = new System.Net.WebClient();
            string prices = cli.DownloadString(input);
            dynamic results = JsonConvert.DeserializeObject<dynamic>(prices);

            int i = 0;
            foreach (var item in results)
            {
                CoinModel.CoinNameStruct newCoin = new CoinModel.CoinNameStruct();
                newCoin.Id = item.id;
                newCoin.Name = item.name;
                newCoin.Symbol = item.symbol;
                allCoinNames.Add(newCoin);

                i++;
            }

            AllCoinNames.Sort((x, y) => x.Name.CompareTo(y.Name));
        }

        /// <summary>
        /// Connect to coinmarketcap API and retrieve data for each coin in tracked coin list
        /// </summary>
        private void APIUpdate()
        {
            //Read data from API
            for (int i = 0; i < trackedCoinList.Count; i++)
            {
                string input = trackedCoinList[i].APILink;

                try
                {
                    //Connect to API
                    var cli = new System.Net.WebClient();
                    string prices = cli.DownloadString(input);
                    dynamic results = JsonConvert.DeserializeObject<dynamic>(prices);

                    //Add price to temp list
                    trackedCoinList[i].Price = (float)(Convert.ToDouble(results[0].price_usd));

                    //Update tool tip array and add array to tool tip list
                    trackedCoinList[i].Rank = results[0].rank;
                    trackedCoinList[i].MarketCap = results[0].market_cap_usd;
                    trackedCoinList[i].Percent_Change_1h = results[0].percent_change_1h;
                    trackedCoinList[i].Percent_Change_24hr = results[0].percent_change_24h;
                    trackedCoinList[i].Percent_Change_7d = results[0].percent_change_7d;
                    trackedCoinList[i].Symbol = results[0].symbol;
                }
                catch (System.Net.WebException e)
                {
                    Console.WriteLine(e.Message);

                    //If there is an error connecting to the API, fill list with null data to avoid index out of bounds later
                    trackedCoinList[i].Price = 0.0F;
                }
            }
        }

        /// <summary>
        /// Updates values in trackedCoinList for each coin. Also calculates total investement data.
        /// </summary>
        private void UpdateValues()
        {
            totalProfit = 0.0F;
            totalValue = 0.0F;
            totalInvestment = 0.0F;

            for (int i = 0; i < trackedCoinList.Count; i++)
            {              
                if (trackedCoinList[i].Price != 0.0)
                {
                    trackedCoinList[i].Value = trackedCoinList[i].Quantity * trackedCoinList[i].Price;
                    trackedCoinList[i].Profit = trackedCoinList[i].Value - trackedCoinList[i].NetCost;
                    trackedCoinList[i].ProfitPercent = (trackedCoinList[i].Profit / trackedCoinList[i].Value) * 100;

                    totalInvestment += trackedCoinList[i].NetCost;
                    totalValue += trackedCoinList[i].Value.Value;
                    totalProfit += trackedCoinList[i].Profit.Value;
                }
                else
                {
                    trackedCoinList[i].Value = null;
                    trackedCoinList[i].Profit = null;
                    trackedCoinList[i].ProfitPercent = null;
                }
            }
        }

        /// <summary>
        /// Reads data from a datatable and updates total netcost and quantity based on trades imported
        /// </summary>
        /// <param name="table"></param>
        public void UpdatePriceDataFromTrades(DataTable table)
        {
            //Update quantity and net cost for each coin currently being tracked
            for (int i = 0; i < table.Rows.Count; i++)
            {
                int buyindex = trackedCoinList.FindIndex(a => a.Symbol == table.Rows[i][(int)DataTableRows.TradePair].ToString().Split('/')[TRADE_COIN]);
                int tradeindex = trackedCoinList.FindIndex(a => a.Symbol == table.Rows[i][(int)DataTableRows.TradePair].ToString().Split('/')[TRADE_BASE]);

                if (buyindex > -1)
                {
                    if (table.Rows[i][(int)DataTableRows.Type].ToString() == BUY)
                    {
                        //Update coin being traded for
                        trackedCoinList[buyindex].Quantity += (float)Convert.ToDouble(table.Rows[i][(int)DataTableRows.OrderQuantity]);
                        trackedCoinList[buyindex].NetCost += (float)Convert.ToDouble(table.Rows[i][(int)DataTableRows.NetCostUSD]);

                        if (tradeindex > -1)
                        {
                            //Update coin being traded with
                            trackedCoinList[tradeindex].NetCost -= (float)Convert.ToDouble(table.Rows[i][(int)DataTableRows.NetCostUSD]);
                            trackedCoinList[tradeindex].Quantity -= (float)Convert.ToDouble(table.Rows[i][(int)DataTableRows.OrderCost]);
                        }

                        //Update FIAT USD total
                        if (table.Rows[i][(int)DataTableRows.TradePair].ToString().Split('/')[TRADE_BASE] == "USD")
                        {
                            totalFiatCost -= (float)Convert.ToDouble(table.Rows[i][(int)DataTableRows.NetCostUSD]);
                        }
                    }
                    else 
                    {
                        //Update coin being traded for
                        trackedCoinList[buyindex].Quantity -= (float)Convert.ToDouble(table.Rows[i][(int)DataTableRows.OrderQuantity]);
                        trackedCoinList[buyindex].NetCost -= (float)Convert.ToDouble(table.Rows[i][(int)DataTableRows.NetCostUSD]);

                        if (tradeindex > -1)
                        {
                            //Update coin being traded with
                            trackedCoinList[tradeindex].NetCost += (float)Convert.ToDouble(table.Rows[i][(int)DataTableRows.NetCostUSD]);
                            trackedCoinList[tradeindex].Quantity += (float)Convert.ToDouble(table.Rows[i][(int)DataTableRows.OrderCost]);
                        }

                        //Update FIAT USD total
                        if (table.Rows[i][(int)DataTableRows.TradePair].ToString().Split('/')[TRADE_BASE] == "USD")
                        {
                            totalFiatCost += (float)Convert.ToDouble(table.Rows[i][(int)DataTableRows.NetCostUSD]);
                        }
                    }
                }
            }
            #if DEBUG
            Console.WriteLine(TotalFiatCost);
            #endif
        }

        /// <summary>
        /// Add newly added coin to tracked coin list
        /// </summary>
        /// <param name="addCoin">CoinModel class holding coin related data</param>
        public void AddNewCoin(CoinModel addCoin)
        {
            trackedCoinList.Add(addCoin);
        }
    }
}
