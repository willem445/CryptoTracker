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
            TOTAL,
            FEE,
            FEECOIN
        };

        //Fields*****************************************************************************************
        private string[] binanceTradeBases = new string[] { "BTC", "ETH", "BNB", "USDT" };

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

                    progress.Report((int)(((float)i / (float)excelData.Rows.Count) * 100.0F));

#if DEBUG
                    percentComplete = ((float)i / (float)excelData.Rows.Count) * 100.0F;
                    Console.WriteLine(percentComplete.FloatToPercent());
#endif
                }
            }
            progress.Report(100);
            return table;
        }

        /// <summary>
        /// Parse data from binance generated report
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public DataTable ImportBinanceTradeData(string filePath)
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
                    percentComplete = ((float)i / (float)excelData.Rows.Count) * 100.0F;
                    Console.WriteLine(percentComplete.FloatToPercent());
#endif

                }
            }
            return table;
        }

    }
}
