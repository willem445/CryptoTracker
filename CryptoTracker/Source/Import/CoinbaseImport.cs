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

        public DataTable ImportCoinbaseTradeData(string filePath)
        {
            var excelData = ExcelToDataSet(filePath).Tables[0];

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
                                    (float)Convert.ToDouble(excelData.Rows[i][(int)CoinbaseColumns.AMOUNT]),
                                    GetHistoricalUsdValue(dateValue, excelData.Rows[i][(int)CoinbaseColumns.TRADE_CURRENCY].ToString()),
                                    (float)Convert.ToDouble(excelData.Rows[i][(int)CoinbaseColumns.TRANSFER_TOTAL]),
                                    (float)Convert.ToDouble(excelData.Rows[i][(int)CoinbaseColumns.TRANSFER_TOTAL])
                                  );
                }
            }

            return table;
        }

    }
}
