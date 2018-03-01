using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace CryptoTracker
{
    public class PriceManager
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
        private List<CoinModel> priceMonitorCoinsList = new List<CoinModel>();
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
        public List<CoinModel> MonitorCoinList
        {
            get
            {
                return priceMonitorCoinsList;
            }
        }

        //Constructor***************************************************************************
        /// <summary>
        /// Create new price manager with progress reporting (start from thread)
        /// </summary>
        /// <param name="progress"></param>
        public PriceManager(IProgress<int> progress)
        {
            Thread getCoinNames = new Thread(new ThreadStart(GetAllCoinNames));
            getCoinNames.Start();

            progress.Report(20);

            //Parse data in documents folder
            FileIO file = new FileIO();
            trackedCoinList = file.ParseSavedData();
            priceMonitorCoinsList = file.ParseSavedCoinMonitoring();

            progress.Report(30);

            //Update prices based on parsed data
            UpdateMarketData();
            UpdatePriceData();
            UpdateMonitorData();
            

            progress.Report(60);
        }

        /// <summary>
        /// Create new price manager 
        /// </summary>
        public PriceManager()
        {
            Thread getCoinNames = new Thread(new ThreadStart(GetAllCoinNames));
            getCoinNames.Start();

            //Parse data in documents folder
            FileIO file = new FileIO();
            trackedCoinList = file.ParseSavedData();
            priceMonitorCoinsList = file.ParseSavedCoinMonitoring();

            //Update prices based on parsed data
            UpdateMarketData();
            UpdatePriceData();
            UpdateMonitorData();
        }

        //Methods*******************************************************************************
        //Public API----------------------------------
        /// <summary>
        /// Gets data from API and calculates profits, value, etc.
        /// </summary>
        public void UpdatePriceData()
        {
            APIPriceUpdate();
            UpdateValues();
        }

        /// <summary>
        /// Gets data from API related to the market, percent, rank, marketcap, etc
        /// </summary>
        public void UpdateMarketData()
        {
            APIMarketUpdate();
        }

        /// <summary>
        /// Gets data from API used for price monitor tab
        /// </summary>
        public void UpdateMonitorData()
        {
            APIUpdateMonitorList();
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

        //Public Methods--------------------------------
        /// <summary>
        /// Retrieves the names of all current cryptocurrencies listed on coinmarketcap
        /// </summary>
        private void GetAllCoinNames()
        {
            CoinMarketCapAPI getNames = new CoinMarketCapAPI();
            List<CoinMarketCapAPI.CoinMarketCapCoinResponse> data = getNames.GetAllData(CoinMarketCapAPI.CMCValidCurrency.USD, 0, CoinMarketCapAPI.ALLCOINS);

            foreach (var item in data)
            {
                CoinModel.CoinNameStruct newCoin = new CoinModel.CoinNameStruct();
                newCoin.Id = item.id;
                newCoin.Name = item.name;
                newCoin.Symbol = item.symbol;
                allCoinNames.Add(newCoin);
            }

            AllCoinNames.Sort((x, y) => x.Name.CompareTo(y.Name));
        }

        /// <summary>
        /// Connect to coinmarketcap API and retrieve data for each coin in tracked coin list
        /// </summary>
        private void APIPriceUpdate()
        {
            CryptoCompareAPI api = new CryptoCompareAPI();
            List<float?> results = api.GetCryptoComparePriceData(trackedCoinList);

            int i = 0;
            foreach (var item in results)
            {
                trackedCoinList[i].Price = item;
                i++;
            }
        }

        /// <summary>
        /// Update market data from coinmarketcap api
        /// </summary>
        private void APIMarketUpdate()
        {
            //Read data from API
            for (int i = 0; i < trackedCoinList.Count; i++)
            {
                string input = trackedCoinList[i].APILink;

                CoinMarketCapAPI getData = new CoinMarketCapAPI();
                CoinMarketCapAPI.CoinMarketCapCoinResponse data = getData.GetCoinData(CoinMarketCapAPI.CMCValidCurrency.USD, input, true);

                if (data != null)
                {
                    //Update tool tip array and add array to tool tip list
                    trackedCoinList[i].Rank = data.rank;
                    trackedCoinList[i].MarketCap = data.market_cap_usd;
                    trackedCoinList[i].Percent_Change_1h = data.percent_change_1h;
                    trackedCoinList[i].Percent_Change_24h = data.percent_change_24h;
                    trackedCoinList[i].Percent_Change_7d = data.percent_change_7d;
                    trackedCoinList[i].Symbol = data.symbol;
                }
            }
        }

        /// <summary>
        /// Updates data used in price monitor tab
        /// </summary>
        private void APIUpdateMonitorList()
        {
            //Read data from API
            for (int i = 0; i < priceMonitorCoinsList.Count; i++)
            {
                string input = priceMonitorCoinsList[i].APILink;

                CoinMarketCapAPI getData = new CoinMarketCapAPI();
                CoinMarketCapAPI.CoinMarketCapCoinResponse data = getData.GetCoinData(CoinMarketCapAPI.CMCValidCurrency.USD, input, true);

                if (data != null)
                {
                    priceMonitorCoinsList[i].Name = data.name;
                    priceMonitorCoinsList[i].Price = (float)Convert.ToDouble(data.price_usd);
                    priceMonitorCoinsList[i].Percent_Change_1h = data.percent_change_1h;
                    priceMonitorCoinsList[i].Percent_Change_24h = data.percent_change_24h;
                    priceMonitorCoinsList[i].Percent_Change_7d = data.percent_change_7d;
                    priceMonitorCoinsList[i].Symbol = data.symbol;
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
                if (trackedCoinList[i].Price != 0.0 && trackedCoinList[i].Price != null)
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
                    trackedCoinList[i].Value = 0;
                    trackedCoinList[i].Profit = 0;
                    trackedCoinList[i].ProfitPercent = 0;
                }
            }
        }
    }

    public class Item
    {
        [JsonProperty("USD")]
        public float? USD { get; set; }
    }
}
