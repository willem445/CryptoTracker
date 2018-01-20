using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker
{
    class CoinbaseImport
    {
        public enum CoinbaseColumns
        {
            DATE = 0,
            AMOUNT = 2,
            CURRENCY = 3,
            TYPE = 5,
            TRANSFER_TOTAL = 7,
            TRANSFER_TOTAL_CURRENCY = 8,
            TRANSFER_FEE = 9
        }

        public enum Type
        {
            BUY,
            SELL
        };

        public struct CoinbaseData
        {
            public DateTime date;
            public float amount;
            public string currency;
            public Type type;
            public float transferTotal;
            public string transferCurrency;
            public float transferFee;
        };

        List<CoinbaseData> coinbaseTradeList = new List<CoinbaseData>();


        public List<CoinbaseData> ImportCoinbaseTradeData(string filePath)
        {
            var excelData = GeneralImport.ExcelToDataSet(filePath).Tables[0];

            for (int i = 0; i < excelData.Rows.Count; i++)
            {
                DateTime dateValue;

                if (DateTime.TryParse(excelData.Rows[i][(int)CoinbaseColumns.DATE].ToString(), out dateValue) && excelData.Rows[i][(int)CoinbaseColumns.TYPE].ToString() != "")
                {
                    CoinbaseData newData;
                    newData.date = dateValue;
                    newData.amount = (float)Convert.ToDouble(excelData.Rows[i][(int)CoinbaseColumns.AMOUNT]);
                    newData.currency = excelData.Rows[i][(int)CoinbaseColumns.CURRENCY].ToString();
                    newData.type = excelData.Rows[i][(int)CoinbaseColumns.TYPE].ToString().Split(' ')[0] == "Bought" ? Type.BUY : Type.SELL;
                    newData.transferTotal = (float)Convert.ToDouble(excelData.Rows[i][(int)CoinbaseColumns.TRANSFER_TOTAL]);
                    newData.transferCurrency = excelData.Rows[i][(int)CoinbaseColumns.TRANSFER_TOTAL_CURRENCY].ToString();
                    newData.transferFee = (float)Convert.ToDouble(excelData.Rows[i][(int)CoinbaseColumns.TRANSFER_FEE]);

                    coinbaseTradeList.Add(newData);
                }
            }
            return coinbaseTradeList;
        }

    }
}
