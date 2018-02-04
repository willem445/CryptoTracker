using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker
{
    class KucoinImport : GeneralImport
    {
        const int TRADE_BASE = 1;
        const int TRADE = 0;

        //Enumerations***********************************************************************************
        enum KucoinColumns
        {
            TRADEPAIR,
            DATE,
            TYPE,
            PRICE,
            QUANTITY,
            TOTALCOST
        };

        //Fields*****************************************************************************************
        public string[] kucoinColumnNamesValidation = new string[] { "Assets", "Time", "Buy／Sell", "Dealt Price ", "Amount ", "Volume " };

        //Methods****************************************************************************************
        /// <summary>
        /// Parse data from binance generated report
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public DataTable ImportKucoinTradeData(string filePath, IProgress<int> progress)
        {
#if DEBUG
            float percentComplete = 0.0F;
#endif

            var excelData = ExcelToDataSet(filePath).Tables[0];

            if (ValidateDataFormat(kucoinColumnNamesValidation, excelData))
            {
                for (int i = 0; i < excelData.Rows.Count; i++)
                {
                    DateTime dateValue;

                    if (DateTime.TryParse(excelData.Rows[i][(int)KucoinColumns.DATE].ToString(), out dateValue))
                    {
                        TradePair tradePair = new TradePair();

                        tradePair.trade = excelData.Rows[i][(int)KucoinColumns.TRADEPAIR].ToString().Split('/')[TRADE].Replace(" ", "");
                        tradePair.baseTrade = excelData.Rows[i][(int)KucoinColumns.TRADEPAIR].ToString().Split('/')[TRADE_BASE].Replace(" ", "");

                        table.Rows.Add(
                            dateValue,
                            "Kucoin",
                            tradePair.trade + "/" + tradePair.baseTrade,
                            excelData.Rows[i][(int)KucoinColumns.TYPE].ToString() == "Buy" ? "BUY" : "SELL",
                            excelData.Rows[i][(int)KucoinColumns.QUANTITY].ToString().Replace(tradePair.trade, "").ParseFloatFromString(),
                            excelData.Rows[i][(int)KucoinColumns.PRICE].ToString().Replace(tradePair.baseTrade, "").ParseFloatFromString(),
                            excelData.Rows[i][(int)KucoinColumns.TOTALCOST].ToString().Replace(tradePair.baseTrade, "").ParseFloatFromString(),
                            GetHistoricalUsdValue(dateValue, tradePair.baseTrade) * excelData.Rows[i][(int)KucoinColumns.TOTALCOST].ToString().Replace(tradePair.baseTrade, "").ParseFloatFromString()
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
            return null;
        }

        public DataTable ImportKucoinTradeData(string filePath)
        {
#if DEBUG
            float percentComplete = 0.0F;
#endif

            var excelData = ExcelToDataSet(filePath).Tables[0];

            if (ValidateDataFormat(kucoinColumnNamesValidation, excelData))
            {
                for (int i = 0; i < excelData.Rows.Count; i++)
                {
                    DateTime dateValue;

                    if (DateTime.TryParse(excelData.Rows[i][(int)KucoinColumns.DATE].ToString(), out dateValue))
                    {
                        TradePair tradePair = new TradePair();

                        tradePair.trade = excelData.Rows[i][(int)KucoinColumns.TRADEPAIR].ToString().Split('/')[TRADE].Replace(" ", "");
                        tradePair.baseTrade = excelData.Rows[i][(int)KucoinColumns.TRADEPAIR].ToString().Split('/')[TRADE_BASE].Replace(" ", "");

                        table.Rows.Add(
                            dateValue,
                            "Kucoin",
                            tradePair.trade + "/" + tradePair.baseTrade,
                            excelData.Rows[i][(int)KucoinColumns.TYPE].ToString() == "Buy" ? "BUY" : "SELL",
                            excelData.Rows[i][(int)KucoinColumns.QUANTITY].ToString().Replace(tradePair.trade, "").ParseFloatFromString(),
                            excelData.Rows[i][(int)KucoinColumns.PRICE].ToString().Replace(tradePair.baseTrade, "").ParseFloatFromString(),
                            excelData.Rows[i][(int)KucoinColumns.TOTALCOST].ToString().Replace(tradePair.baseTrade, "").ParseFloatFromString(),
                            GetHistoricalUsdValue(dateValue, tradePair.baseTrade) * excelData.Rows[i][(int)KucoinColumns.TOTALCOST].ToString().Replace(tradePair.baseTrade, "").ParseFloatFromString()
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
