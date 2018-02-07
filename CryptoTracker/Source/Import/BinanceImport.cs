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
    public class BinanceImport : GeneralImport
    {
        //Enumerations***********************************************************************************
        enum BinanceColumns1
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

        enum BinanceColumns2
        {
            DATE = 0,
            PAIR = 1,
            TYPE = 2,
            ORDER_PRICE = 3,         
            AVG_TRADE_PRICE = 5,
            ORDER_AMOUNT = 6,
            TOTAL = 7,
            STAUS = 8

        }

        //Fields*****************************************************************************************
        private string[] binanceTradeBases = new string[] { "BTC", "ETH", "BNB", "USDT" };
        private string[] binanceColumnNamesValidation1 = new string[] { "Date", "Pair", "Type", "Order Price", "Order Amount", "Avg Trading Price", "Filled", "Total", "status" };
        private string[] binanceColumnNamesValidation2 = new string[] { "Date", "Market", "Type", "Price", "Amount", "Total", "Fee", "Fee Coin" };

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

            if (ValidateDataFormat(binanceColumnNamesValidation1, excelData))
            {
                for (int i = 0; i < excelData.Rows.Count; i++)
                {
                    DateTime dateValue;

                    if (DateTime.TryParseExact(excelData.Rows[i][(int)BinanceColumns2.DATE].ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                    {
                        if (excelData.Rows[i][(int)BinanceColumns2.STAUS].ToString() == "Filled")
                        {
                            TradePair tradePair = new TradePair();
                            double tradePrice = 0.0;

                            if (Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns2.AVG_TRADE_PRICE]) != 0.0)
                            {
                                tradePrice = Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns2.AVG_TRADE_PRICE]);
                            }
                            else
                            {
                                tradePrice = Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns2.ORDER_PRICE]);
                            }

                            foreach (var item in binanceTradeBases)
                            {
                                if (excelData.Rows[i][(int)BinanceColumns2.PAIR].ToString().Contains(item))
                                {
                                    tradePair.baseTrade = item;
                                    break;
                                }
                            }

                            tradePair.trade = excelData.Rows[i][(int)BinanceColumns2.PAIR].ToString().Replace(tradePair.baseTrade, "");

                            //TODO - Develop more elegant solution for biance using different trade name
                            if (tradePair.trade == "IOTA")
                            {
                                tradePair.trade = "MIOTA";
                            }

                            if (tradePair.trade == "NANO")
                            {
                                tradePair.trade = "XRB";
                            }

                            table.Rows.Add(
                                dateValue,
                                "Binance",
                                tradePair.trade + "/" + tradePair.baseTrade,
                                excelData.Rows[i][(int)BinanceColumns2.TYPE].ToString() == "BUY" ? "BUY" : "SELL",
                                (float)Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns2.ORDER_AMOUNT]),
                                (float)tradePrice,
                                (float)Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns2.TOTAL]),
                                GetHistoricalUsdValue(dateValue, tradePair.baseTrade) * (float)Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns2.TOTAL])
                                );

                            progress.Report((int)(((float)i / (float)excelData.Rows.Count) * 100.0F));

#if DEBUG
                            percentComplete = ((float)i / (float)excelData.Rows.Count) * 100.0F;
                            Console.WriteLine(percentComplete.FloatToPercent());
#endif
                        }
                    }
                }
                progress.Report(100);
                return table;
            }

            return null;
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

            if (ValidateDataFormat(binanceColumnNamesValidation2, excelData))
            {
                for (int i = 0; i < excelData.Rows.Count; i++)
                {
                    DateTime dateValue;

                    if (DateTime.TryParseExact(excelData.Rows[i][(int)BinanceColumns2.DATE].ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                    {
                        TradePair tradePair = new TradePair();

                        foreach (var item in binanceTradeBases)
                        {
                            if (excelData.Rows[i][(int)BinanceColumns2.PAIR].ToString().Contains(item))
                            {
                                tradePair.baseTrade = item;
                                break;
                            }
                        }

                        tradePair.trade = excelData.Rows[i][(int)BinanceColumns2.PAIR].ToString().Replace(tradePair.baseTrade, "");

                        //TODO - Develop more elegant solution for biance using different trade name
                        if (tradePair.trade == "IOTA")
                        {
                            tradePair.trade = "MIOTA";
                        }

                        table.Rows.Add(
                            dateValue,
                            "Binance",
                            tradePair.trade + "/" + tradePair.baseTrade,
                            excelData.Rows[i][(int)BinanceColumns2.TYPE].ToString() == "BUY" ? "BUY" : "SELL",
                            (float)Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns2.ORDER_AMOUNT]),
                            (float)Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns2.ORDER_PRICE]),
                            (float)Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns2.TOTAL]),
                            GetHistoricalUsdValue(dateValue, tradePair.baseTrade) * (float)Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns2.TOTAL])
                            );

#if DEBUG
                        percentComplete = ((float)i / (float)excelData.Rows.Count) * 100.0F;
                        Console.WriteLine(percentComplete.FloatToPercent());
#endif
                    }
                }
                return table;
            }

            return null;

        }
    }
}
