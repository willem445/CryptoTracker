using ExcelDataReader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker
{
    class BinanceImport
    {
        enum BinanceColumns
        {
            DATE,
            PAIR,  
            TYPE,
            ORDER_PRICE,
            ORDER_AMOUNT,
            AVG_TRADE_PRICE,
            FILLED,
            TOTAL,
            STATUS
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

        public struct BinanceData
        {
            public DateTime date;
            public TradePair tradePair;
            public Type type;
            public double orderPrice;
            public float orderAmount;
            public double avgTradePrice;
            public float filled;
            public double total;
            public Status status;
            public float usdValue;    
        };

        public struct TradePair
        {
            public string trade;
            public string baseTrade;
        };

        private string[] binanceTradeBases = new string[] { "BTC", "ETH", "BNB", "USDT" };

        List<BinanceData> binanceTradeList = new List<BinanceData>();

        public List<BinanceData> ImportBinanceTradeData(string filePath)
        {
            var excelData = GeneralImport.ExcelToDataSet(filePath).Tables[0];
            
            for (int i = 0; i < excelData.Rows.Count; i++)
            {
                DateTime dateValue;

                if (DateTime.TryParseExact(excelData.Rows[i][(int)BinanceColumns.DATE].ToString(), "yyyy-MM-dd h:mm:ss", new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
                {
                    BinanceData newData;
                    newData.date = dateValue;

                    //TODO - Possible exception here if can't find base trade
                    newData.tradePair.baseTrade = "BTC";
                    foreach (var item in binanceTradeBases)
                    {
                        if (excelData.Rows[i][(int)BinanceColumns.PAIR].ToString().Contains(item))
                        {
                            newData.tradePair.baseTrade = item;
                            break;
                        }
                    }

                    newData.tradePair.trade = excelData.Rows[i][(int)BinanceColumns.PAIR].ToString().Replace(newData.tradePair.baseTrade, "");           
                    newData.type = excelData.Rows[i][(int)BinanceColumns.TYPE].ToString() == "BUY" ? Type.BUY : Type.SELL;
                    newData.orderPrice = Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns.ORDER_PRICE]);
                    newData.orderAmount = (float)Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns.ORDER_AMOUNT]);
                    newData.avgTradePrice = Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns.AVG_TRADE_PRICE]);
                    newData.filled = (float)Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns.FILLED]);
                    newData.total = Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns.TOTAL]);
                    newData.status = excelData.Rows[i][(int)BinanceColumns.STATUS].ToString() == "Filled" ? Status.FILLED : Status.CANCELED;
                    newData.usdValue = GeneralImport.GetHistoricalUsdValue(newData.date, newData.tradePair.baseTrade) * (float)newData.total;

                    binanceTradeList.Add(newData);
                }
            }
            return binanceTradeList;
        }

    }
}
