using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CryptoTracker
{
    public class CoinMarketCapAPI
    {
        //TODO - JSON object does not have fields for other currencies

        //Constants**********************************************************
        public const int ALLCOINS = 0;

        //Enumerations*******************************************************
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

        //Data Model Class****************************************************
        public class CoinMarketCapCoinResponse
        {
            public string id { get; set; }
            public string name { get; set; }
            public string symbol { get; set; }
            public string rank { get; set; }
            public string price_usd { get; set; }
            public string price_btc { get; set; }
            public string volume_24h_usd { get; set; }
            public string market_cap_usd { get; set; }
            public string available_supply { get; set; }
            public string total_supply { get; set; }
            public string max_supply { get; set; }
            public string percent_change_1h { get; set; }
            public string percent_change_24h { get; set; }
            public string percent_change_7d { get; set; }
            public string last_updated { get; set; }
        }

        public class CoinMarketCapGlobalResponse
        {
            public long total_market_cap_usd { get; set; }
            public long total_24h_volume_usd { get; set; }
            public double bitcoin_percentage_of_market_cap { get; set; }
            public int active_currencies { get; set; }
            public int active_assets { get; set; }
            public int active_markets { get; set; }
            public int last_updated { get; set; }
        }

        //Fields**************************************************************
        private string[] validCurrencies = new string[] { "AUD", "BRL", "CAD", "CHF", "CLP", "CNY", "CZK", "DKK", "EUR", "GBP", "HKD", "HUF", "IDR", "ILS", "INR", "JPY", "KRW", "MXN", "MYR", "NOK", "NZD", "PHP", "PKR", "PLN", "RUB", "SEK", "SGD", "THB", "TRY", "TWD", "USD", "ZAR" };

        //Methods*************************************************************
        /// <summary>
        /// Gets data for all currencies listed on CMC
        /// </summary>
        /// <param name="currency">return price, 24h volume, and market cap in terms of another currency</param>
        /// <param name="start">Optional: return results from rank [start] and above</param>
        /// <param name="limit">Optional: return a maximum of [limit] results (default is 100, use ALLCOINS to return all results)</param>
        /// <returns>Returns null if not found</returns>
        public List<CoinMarketCapCoinResponse> GetAllData(CMCValidCurrency currency, int start = 0, int limit = 100)
        {
            string input = string.Format("https://api.coinmarketcap.com/v1/ticker/?convert={1}&limit={0}&start={2}", limit, validCurrencies[(int)currency], start);

            return CMC_CoinRequest(input);
        }

        /// <summary>
        /// Returns data for a single cryptocurrency listed on CMC
        /// </summary>
        /// <param name="currency">return price, 24h volume, and market cap in terms of another currency</param>
        /// <param name="cmc_id">ID of currency on CMC (ie. bitcoin, ethereum)</param>
        /// <param name="useURL">Set to true if cmc_id is the url to CMC, not just the id</param>
        /// <returns>Returns null if not found</returns>
        public CoinMarketCapCoinResponse GetCoinData(CMCValidCurrency currency, string cmc_id, bool useURL = false)
        {
            string input = "";

            if (useURL == true)
            {
                input = cmc_id;
            }
            else
            {
                input = string.Format("https://api.coinmarketcap.com/v1/ticker/{0}/?convert={1}", cmc_id, currency);
            }

            return CMC_CoinRequest(input)[0];
        }

        /// <summary>
        /// Gets global data from CMC
        /// </summary>
        /// <param name="currency">return price, 24h volume, and market cap in terms of another currency</param>
        /// <returns>Returns null if data is invalid</returns>
        public CoinMarketCapGlobalResponse GetGlobalData(CMCValidCurrency currency)
        {
            string input = string.Format("https://api.coinmarketcap.com/v1/global/?convert={0}", currency);

            return CMC_GlobalRequest(input);
        }

        //Private Methods******************************************************
        /// <summary>
        /// Connects to CMC API and parse JSON reponse
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private CoinMarketCapGlobalResponse CMC_GlobalRequest(string input)
        {
            CoinMarketCapGlobalResponse data = new CoinMarketCapGlobalResponse();

            try
            {
                //Connect to API
                var cli = new System.Net.WebClient();
                string response = cli.DownloadString(input);
                data = JsonConvert.DeserializeObject<CoinMarketCapGlobalResponse>(response);

                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine("CMC Error: " + e.Message);
            }

            return null;
        }

        /// <summary>
        /// Method to connect to coinmarketcap and parse JSON reponse
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Returns null if data not found</returns>
        private List<CoinMarketCapCoinResponse> CMC_CoinRequest(string input)
        {
            List<CoinMarketCapCoinResponse> data;

            try
            {
                //Connect to API
                var cli = new System.Net.WebClient();
                string response = cli.DownloadString(input);
                response = response.Replace("24h_volume_usd", "volume_24h_usd");
                data = JsonConvert.DeserializeObject<List<CoinMarketCapCoinResponse>>(response);

                if (data.Count != 0)
                {
                    return data;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("CMC Error: " + e.Message);
            }

            return null;
        }
    }
}
