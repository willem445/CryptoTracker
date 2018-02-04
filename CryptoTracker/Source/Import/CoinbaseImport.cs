using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker
{
    class CoinbaseImport : GeneralImport
    {
        const int COINBASE_ROW_START = 4;

        //Enumerations***********************************************************************************
        public enum CoinbaseColumns
        {
            DATE = 0,
            AMOUNT = 2,
            TRADE_CURRENCY = 3,
            TYPE = 5,
            TRANSFER_TOTAL = 7,
            BASE_CURRENCY = 8,
            TRANSFER_FEE = 9
        }

        string[] coinBaseColumnValidation = new string[] { "Timestamp", "Balance", "Amount", "Currency", "To", "Notes", "Instantly Exchanged", "Transfer Total", "Transfer Total Currency", "Transfer Fee", "Transfer Fee Currency", "Transfer Payment Method", "Transfer ID", "Order Price", "Order Currency", "Order BTC", "Order Tracking Code", "Order Custom Parameter", "Order Paid Out", "Recurring Payment ID", "Coinbase ID (visit https://www.coinbase.com/transactions/[ID] in your browser)", "Bitcoin Hash (visit https://www.coinbase.com/tx/[HASH] in your browser for more info)" };

        //Methods*****************************************************************************************
        /// <summary>
        /// Parse data from coinbase generated report
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public DataTable ImportCoinbaseTradeData(string filePath, IProgress<int> progress)
        {
#if DEBUG
            float percentComplete = 0.0F;
#endif

            var excelData = ExcelToDataSet(filePath).Tables[0];

            if (ValidateDataFormat(coinBaseColumnValidation, excelData, COINBASE_ROW_START))
            {
                for (int i = 0; i < excelData.Rows.Count; i++)
                {
                    DateTime dateValue;

                    if (DateTime.TryParse(excelData.Rows[i][(int)CoinbaseColumns.DATE].ToString(), out dateValue) && excelData.Rows[i][(int)CoinbaseColumns.TYPE].ToString() != "")
                    {
                        table.Rows.Add(
                                        dateValue,
                                        "Coinbase",
                                        excelData.Rows[i][(int)CoinbaseColumns.TRADE_CURRENCY].ToString() + "/" + excelData.Rows[i][(int)CoinbaseColumns.BASE_CURRENCY].ToString(),
                                        excelData.Rows[i][(int)CoinbaseColumns.TYPE].ToString().Split(' ')[0] == "Bought" ? "BUY" : "SELL",
                                        Math.Abs((float)Convert.ToDouble(excelData.Rows[i][(int)CoinbaseColumns.AMOUNT])),
                                        GetHistoricalUsdValue(dateValue, excelData.Rows[i][(int)CoinbaseColumns.TRADE_CURRENCY].ToString()),
                                        (float)Convert.ToDouble(excelData.Rows[i][(int)CoinbaseColumns.TRANSFER_TOTAL]),
                                        (float)Convert.ToDouble(excelData.Rows[i][(int)CoinbaseColumns.TRANSFER_TOTAL])
                                      );
                    }

                    progress.Report((int)(((float)i / (float)excelData.Rows.Count) * 100.0F));

#if DEBUG
                    percentComplete = ((float)i / (float)excelData.Rows.Count) * 100.0F;
                    Console.WriteLine(percentComplete.FloatToPercent());
#endif
                }
                progress.Report(100);
                return table;
            }
            return null;
        }

        /// <summary>
        /// Parse data from coinbase generated report
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public DataTable ImportCoinbaseTradeData(string filePath)
        {
#if DEBUG
            float percentComplete = 0.0F;
#endif

            var excelData = ExcelToDataSet(filePath).Tables[0];

            if (ValidateDataFormat(coinBaseColumnValidation, excelData))
            {
                for (int i = 0; i < excelData.Rows.Count; i++)
                {
                    DateTime dateValue;

                    if (DateTime.TryParse(excelData.Rows[i][(int)CoinbaseColumns.DATE].ToString(), out dateValue) && excelData.Rows[i][(int)CoinbaseColumns.TYPE].ToString() != "")
                    {
                        table.Rows.Add(
                                        dateValue,
                                        "Coinbase",
                                        excelData.Rows[i][(int)CoinbaseColumns.TRADE_CURRENCY].ToString() + "/" + excelData.Rows[i][(int)CoinbaseColumns.BASE_CURRENCY].ToString(),
                                        excelData.Rows[i][(int)CoinbaseColumns.TYPE].ToString().Split(' ')[0] == "Bought" ? "BUY" : "SELL",
                                        Math.Abs((float)Convert.ToDouble(excelData.Rows[i][(int)CoinbaseColumns.AMOUNT])),
                                        GetHistoricalUsdValue(dateValue, excelData.Rows[i][(int)CoinbaseColumns.TRADE_CURRENCY].ToString()),
                                        (float)Convert.ToDouble(excelData.Rows[i][(int)CoinbaseColumns.TRANSFER_TOTAL]),
                                        (float)Convert.ToDouble(excelData.Rows[i][(int)CoinbaseColumns.TRANSFER_TOTAL])
                                      );
                    }
#if DEBUG
                    percentComplete = ((float)i / (float)excelData.Rows.Count) * 100.0F;
                    Console.WriteLine(percentComplete.FloatToPercent());
#endif
                }
                return table;
            }
            return null;
        }
    }
}
