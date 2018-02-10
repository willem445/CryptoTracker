using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker
{
    class CoinMarketCapAPI
    {
        public const int ALLCOINS = 0;

        public enum CMCValidCurrency
        {
            AUD, 
            BRL,
            CAD,
            CHF,
            CLP,
            CNY,
            CZK,
            DKK,
            EUR,
            GBP,
            HKD,
            HUF,
            IDR,
            ILS,
            INR,
            JPY,
            KRW,
            MXN,
            MYR,
            NOK,
            NZD,
            PHP,
            PKR,
            PLN,
            RUB,
            SEK,
            SGD,
            THB,
            TRY,
            TWD,
            USD,
            ZAR
        }

        private string[] validCurrencies = new string[] { "AUD", "BRL", "CAD", "CHF", "CLP", "CNY", "CZK", "DKK", "EUR", "GBP", "HKD", "HUF", "IDR", "ILS", "INR", "JPY", "KRW", "MXN", "MYR", "NOK", "NZD", "PHP", "PKR", "PLN", "RUB", "SEK", "SGD", "THB", "TRY", "TWD", "USD", "ZAR" };

        /// <summary>
        /// Gets data for all currencies listed on CMC
        /// </summary>
        /// <param name="currency">return price, 24h volume, and market cap in terms of another currency</param>
        /// <param name="start">Optional: return results from rank [start] and above</param>
        /// <param name="limit">Optional: return a maximum of [limit] results (default is 100, use ALLCOINS to return all results)</param>
        public void GetAllData(CMCValidCurrency currency, int start = 0, int limit = 100)
        {
            string input = "";

            if (start == 0)
            {
                input = string.Format("https://api.coinmarketcap.com/v1/ticker/?limit={0}/?convert={1}", limit, validCurrencies[(int)currency]);
            }
            else
            {
                input = string.Format("https://api.coinmarketcap.com/v1/ticker/?start={2}/?limit={0}/?convert={1}", limit, validCurrencies[(int)currency], start);
            }

            //Connect to API
            var cli = new System.Net.WebClient();
            string response = cli.DownloadString(input);
            dynamic results = JsonConvert.DeserializeObject<dynamic>(response);
        }
    }
}
