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
    class BinanceImport : GeneralImport
    {
        //Enumerations***********************************************************************************
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

        //Fields*****************************************************************************************
        private string[] binanceTradeBases = new string[] { "BTC", "ETH", "BNB", "USDT" };

        //Methods*****************************************************************************************
        /// <summary>
        /// Parse data from binance generated report
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public DataTable ImportBinanceTradeData(string filePath)
        {
            var excelData = ExcelToDataSet(filePath).Tables[0];
            
            for (int i = 0; i < excelData.Rows.Count; i++)
            {
                DateTime dateValue;

                if (DateTime.TryParseExact(excelData.Rows[i][(int)BinanceColumns.DATE].ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                {
                    //TODO - TradesTabIntegration - Possible exception here if can't find base trade
                    TradePair tradePair = new TradePair();

                    foreach (var item in binanceTradeBases)
                    {
                        if (excelData.Rows[i][(int)BinanceColumns.PAIR].ToString().Contains(item))
                        {
                            tradePair.baseTrade = item;
                            break;
                        }
                    }

                    table.Rows.Add(
                        dateValue,
                        "Binance",
                        excelData.Rows[i][(int)BinanceColumns.PAIR].ToString().Replace(tradePair.baseTrade, "") + "/" + tradePair.baseTrade,
                        excelData.Rows[i][(int)BinanceColumns.TYPE].ToString() == "BUY" ? "BUY" : "SELL",
                        (float)Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns.ORDER_AMOUNT]),
                        Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns.AVG_TRADE_PRICE]),
                        Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns.TOTAL]),
                        GetHistoricalUsdValue(dateValue, tradePair.baseTrade) * (float)Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns.TOTAL])
                        );
                }
            }
            return table;
        }

    }
}
