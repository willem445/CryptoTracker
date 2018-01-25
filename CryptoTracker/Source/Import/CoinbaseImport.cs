using System;
using System.Collections.Generic;
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

        List<TradeData> coinbaseTradeList = new List<TradeData>();


        public List<TradeData> ImportCoinbaseTradeData(string filePath)
        {
            var excelData = ExcelToDataSet(filePath).Tables[0];

            for (int i = 0; i < excelData.Rows.Count; i++)
            {
                DateTime dateValue;

                if (DateTime.TryParse(excelData.Rows[i][(int)CoinbaseColumns.DATE].ToString(), out dateValue) && excelData.Rows[i][(int)CoinbaseColumns.TYPE].ToString() != "")
                {
                    TradeData newData;
                    newData.date = dateValue;
                    newData.tradePair.trade = excelData.Rows[i][(int)CoinbaseColumns.TRADE_CURRENCY].ToString();
                    newData.tradePair.baseTrade = excelData.Rows[i][(int)CoinbaseColumns.BASE_CURRENCY].ToString();
                    newData.type = excelData.Rows[i][(int)CoinbaseColumns.TYPE].ToString().Split(' ')[0] == "Bought" ? Type.BUY : Type.SELL;
                    newData.orderPrice = GetHistoricalUsdValue(newData.date, newData.tradePair.trade);
                    newData.orderAmount = (float)Convert.ToDouble(excelData.Rows[i][(int)CoinbaseColumns.AMOUNT]);
                    newData.avgTradePrice = newData.orderPrice;
                    newData.filled = null;
                    newData.total = (float)Convert.ToDouble(excelData.Rows[i][(int)CoinbaseColumns.TRANSFER_TOTAL]);
                    newData.status = null;
                    newData.fee = (float)Convert.ToDouble(excelData.Rows[i][(int)CoinbaseColumns.TRANSFER_FEE]);
                    newData.usdValue = (float)Convert.ToDouble(excelData.Rows[i][(int)CoinbaseColumns.TRANSFER_TOTAL]);

                    coinbaseTradeList.Add(newData);
                }
            }
            return coinbaseTradeList;
        }

    }
}
