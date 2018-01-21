using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker
{
    class FileIO
    {
        /// <summary>
        /// Parses data from text file and adds coin to form
        /// </summary>
        /// <param name="path"></param>
        public List<CoinModel> ParseSavedData()
        {
            List<CoinModel> parsedDataList = new List<CoinModel>();

            string path = System.IO.Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.MyDoc‌​uments), "CrytoTracker");

            string[] lines;

            if (Directory.Exists(path))
            {
                lines = System.IO.File.ReadAllLines(Path.Combine(path, "CoinData.txt"));

                foreach (string line in lines)
                {
                    CoinModel newCoin = new CoinModel();

                    string[] data = line.Split(',');
                    newCoin.Name = data[0];
                    newCoin.Quantity = (float)(Convert.ToDouble(data[1]));
                    newCoin.NetCost = (float)(Convert.ToDouble(data[2]));
                    newCoin.APILink = data[3];

                    parsedDataList.Add(newCoin);
                }

                //priceManager.UpdatePriceData();

            }

            return parsedDataList;
        }


    }
}
