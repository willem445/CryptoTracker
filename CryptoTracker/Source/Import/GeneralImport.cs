using ExcelDataReader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoTracker
{
    public class GeneralImport
    {
        public struct TradePair
        {
            public string trade;
            public string baseTrade;
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

        protected DataTable table;

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

        public DataTable ImportFromExchange(string exchange, string fileName)
        {
            DataTable table = new DataTable();

            if (exchange == "Binance")
            {
                BinanceImport importBinance = new BinanceImport();
                table = importBinance.ImportBinanceTradeData(fileName);
            }
            else if (exchange == "Coinbase")
            {
                CoinbaseImport importCoinbase = new CoinbaseImport();
                table = importCoinbase.ImportCoinbaseTradeData(fileName);
            }

            return table;
        }

        protected float GetHistoricalUsdValue(DateTime date, string currency)
        {
            string input = "https://min-api.cryptocompare.com/data/pricehistorical?fsym=" + currency + "&tsyms=USD&ts=" + date.DateTimeToUNIX().ToString();
            float price = 0.0F;

            //Connect to API
            var cli = new System.Net.WebClient();
            string prices = cli.DownloadString(input);
            dynamic results = JsonConvert.DeserializeObject<dynamic>(prices);

            //TODO - Add other trade pair and default cases
            switch (currency)
            {
                case "ETH":
                    price = (float)Convert.ToDouble(results.ETH.USD);
                    break;
            }

            return price;
        }

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
