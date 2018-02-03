using ExcelDataReader;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoTracker
{
    public class GeneralImport
    {
        //Structures*************************************************************************************
        public struct TradePair
        {
            public string trade;
            public string baseTrade;
        };

        //Enumerations***********************************************************************************
        enum BinanceColumns
        {
            DATE,
            PAIR,
            TYPE,
            ORDER_PRICE,
            ORDER_AMOUNT,
            TOTAL,
            FEE,
            FEECOIN
        };

        public enum Type
        {
            BUY,
            SELL
        };

        public enum Status
        {
            FILLED,
            CANCELED
        };

        //Fields*****************************************************************************************
        protected DataTable table;
        public string Percent = "";

        private string[] binanceTradeBases = new string[] { "BTC", "ETH", "BNB", "USDT" };

        //Constructor************************************************************************************
        public GeneralImport()
        {
            table = new DataTable();
            table.Columns.Add("Date", typeof(DateTime));
            table.Columns.Add("Exchange", typeof(string));
            table.Columns.Add("Trade Pair", typeof(string));
            table.Columns.Add("Type", typeof(string));
            table.Columns.Add("Order Quantity", typeof(float));
            table.Columns.Add("Trade Price", typeof(float));
            table.Columns.Add("Order Cost", typeof(float));
            table.Columns.Add("Net Cost (USD)", typeof(float));
        }

        //Methods*****************************************************************************************
        /// <summary>
        /// Parse data from binance generated report
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public DataTable ImportBinanceTradeData(string filePath, IProgress<int> progress)
        {
#if DEBUG
            float percentComplete = 0.0F;
#endif


            var excelData = ExcelToDataSet(filePath).Tables[0];

            for (int i = 0; i < excelData.Rows.Count; i++)
            {
                DateTime dateValue;

                if (DateTime.TryParseExact(excelData.Rows[i][(int)BinanceColumns.DATE].ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                {
                    TradePair tradePair = new TradePair();

                    foreach (var item in binanceTradeBases)
                    {
                        if (excelData.Rows[i][(int)BinanceColumns.PAIR].ToString().Contains(item))
                        {
                            tradePair.baseTrade = item;
                            break;
                        }
                    }

                    tradePair.trade = excelData.Rows[i][(int)BinanceColumns.PAIR].ToString().Replace(tradePair.baseTrade, "");

                    //TODO - Develop more elegant solution for biance using different trade name
                    if (tradePair.trade == "IOTA")
                    {
                        tradePair.trade = "MIOTA";
                    }

                    table.Rows.Add(
                        dateValue,
                        "Binance",
                        tradePair.trade + "/" + tradePair.baseTrade,
                        excelData.Rows[i][(int)BinanceColumns.TYPE].ToString() == "BUY" ? "BUY" : "SELL",
                        (float)Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns.ORDER_AMOUNT]),
                        (float)Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns.ORDER_PRICE]),
                        (float)Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns.TOTAL]),
                        GetHistoricalUsdValue(dateValue, tradePair.baseTrade) * (float)Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns.TOTAL])
                        );

#if DEBUG

                    progress.Report((int)(((float)i / (float)excelData.Rows.Count) * 100.0F));


                    percentComplete = ((float)i / (float)excelData.Rows.Count) * 100.0F;
                    Console.WriteLine(percentComplete.FloatToPercent());
#endif

                }
            }
            return table;
        }

        /// <summary>
        /// Calls correct import function based on exchange name, parses data, and returns a datatable
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public DataTable ImportFromExchange(string exchange, string fileName, IProgress<int> progress)
        {
            //TODO - TradesTabIntegration - Check that imported file is in correct format before attempting to parse data
            DataTable table = new DataTable();

            if (exchange == "Binance")
            {
                //BinanceImport importBinance = new BinanceImport();
                table = ImportBinanceTradeData(fileName, progress);
            }
            else if (exchange == "Coinbase")
            {
                CoinbaseImport importCoinbase = new CoinbaseImport();
                table = importCoinbase.ImportCoinbaseTradeData(fileName);
            }

            return table;
        }

        /// <summary>
        /// Gets the historical price of a coin based on timestamp
        /// </summary>
        /// <param name="date"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public float GetHistoricalUsdValue(DateTime date, string currency)
        {
            //TODO - return null float if trying to access api for non existent coin
            string input = "https://min-api.cryptocompare.com/data/pricehistorical?fsym=" + currency + "&tsyms=USD&ts=" + date.DateTimeToUNIX().ToString();
            float price = 0.0F;

            //Connect to API
            var cli = new System.Net.WebClient();
            string prices = cli.DownloadString(input);
            dynamic results = (JObject)JsonConvert.DeserializeObject<dynamic>(prices);
            price = (float)Convert.ToDouble((JValue)results[currency]["USD"]);

            return price;
        }

        /// <summary>
        /// Converts an excel document worksheet to a DataSet
        /// </summary>
        /// <param name="_filePath"></param>
        /// <returns></returns>
        protected DataSet ExcelToDataSet(string _filePath)
        {
            DataSet result;

            using (var stream = File.Open(_filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    result = reader.AsDataSet();
                }
            }
            return result;
        }

    }
}
