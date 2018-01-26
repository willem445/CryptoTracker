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

        private string[] binanceTradeBases = new string[] { "BTC", "ETH", "BNB", "USDT" };

        public BinanceImport()
        {

        }

        public DataTable ImportBinanceTradeData(string filePath)
        {
            var excelData = ExcelToDataSet(filePath).Tables[0];
            
            for (int i = 0; i < excelData.Rows.Count; i++)
            {
                DateTime dateValue;

                if (DateTime.TryParseExact(excelData.Rows[i][(int)BinanceColumns.DATE].ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                {
                    //TradeData newData;
                    //newData.date = dateValue;

                    //TODO - Possible exception here if can't find base trade
                    TradePair tradePair = new TradePair();
                    //newData.tradePair.baseTrade = "BTC";
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

                    //newData.tradePair.trade = excelData.Rows[i][(int)BinanceColumns.PAIR].ToString().Replace(newData.tradePair.baseTrade, "");           
                    //newData.type = excelData.Rows[i][(int)BinanceColumns.TYPE].ToString() == "BUY" ? Type.BUY : Type.SELL;
                    //newData.orderPrice = Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns.ORDER_PRICE]);
                    //newData.orderAmount = (float)Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns.ORDER_AMOUNT]);
                    //newData.avgTradePrice = Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns.AVG_TRADE_PRICE]);
                    //newData.filled = (float)Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns.FILLED]);
                    //newData.total = Convert.ToDouble(excelData.Rows[i][(int)BinanceColumns.TOTAL]);
                    //newData.status = excelData.Rows[i][(int)BinanceColumns.STATUS].ToString() == "Filled" ? Status.FILLED : Status.CANCELED;
                    //newData.fee = 0.0F;
                    //newData.usdValue = GetHistoricalUsdValue(newData.date, newData.tradePair.baseTrade) * (float)newData.total;

                    //binanceTradeList.Add(newData);
                }
            }

            //return binanceTradeList;
            return table;
        }

    }
}
