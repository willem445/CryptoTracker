using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker
{
    class CryptoCompareAPI
    {
        //Constants**********************************************************


        //Enumerations*******************************************************


        //Data Model Class****************************************************


        //Fields**************************************************************
        

        //Methods*************************************************************
        public List<float?> GetCryptoComparePriceData(List<CoinModel> coins)
        {
            List<float?> result = new List<float?>();

            //Use this api, 20-60ms reponse time
            //https://min-api.cryptocompare.com/data/pricemulti?fsyms=ETH,DASH&tsyms=USD

            string input = "";

            if (coins.Count != 0)
            {
                for (int j = 0; j < coins.Count; j++)
                {
                    if (coins[j].Symbol == "MIOTA")
                    {
                        input += "IOTA" + ",";
                    }
                    else if (coins[j].Symbol == "NANO")
                    {
                        input += "XRB" + ",";
                    }
                    else
                    {
                        input += coins[j].Symbol + ",";
                    }

                }
                input = input.TrimEnd(',');
                input = string.Format("https://min-api.cryptocompare.com/data/pricemulti?fsyms={0}&tsyms=USD", input);

                //TODOHP - If CMC returns null when it updates market data, the symbol is never updated in coin model resulting in the symbol being omitted from input string
                //Causes returned data to be off when displayed

                try
                {
                    //Connect to API
                    var cli = new System.Net.WebClient();
                    string prices = cli.DownloadString(input);
                    var results = JsonConvert.DeserializeObject<Dictionary<string, Item>>(prices);

                    int k = 0;
                    foreach (var item in results)
                    {
                        result.Add(item.Value.USD);
                        k++;
                    }
                }
                catch (System.Net.WebException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return result;
        }

        //Private Methods******************************************************
    }
}
