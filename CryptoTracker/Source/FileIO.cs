using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoTracker
{
    class FileIO
    {
        /// <summary>
        /// Parses data from text file and returns a list of coin models
        /// </summary>
        /// <param name="path"></param>
        public List<CoinModel> ParseSavedData()
        {
            List<CoinModel> parsedDataList = new List<CoinModel>();

            string path = System.IO.Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.MyDoc‌​uments), "CrytoTracker");

            string[] lines;

            if (File.Exists(Path.Combine(path, "CoinData.txt")))
            {
                lines = System.IO.File.ReadAllLines(Path.Combine(path, "CoinData.txt"));

                //TODO - Crashed if text file is empty
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

        /// <summary>
        /// Saves data being tracked by form to text file 
        /// </summary>
        public void SavePriceTrackingToFile(List<CoinModel> trackedCoins)
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(
    Environment.SpecialFolder.MyDoc‌​uments), "CrytoTracker");

            string[] textFileArray = new string[trackedCoins.Count];
            bool readError = false;

            //Loop through each coin and put data in string array, if there is an error, do not write
            for (int i = 0; i < trackedCoins.Count; i++)
            {
                try
                {
                    textFileArray[i] = trackedCoins[i].Name + ", " + trackedCoins[i].QuantityToString + ", " + trackedCoins[i].NetCost.ToString() + ", " + trackedCoins[i].APILink;
                }
                catch
                {
                    MessageBox.Show("Error reading data");
                    readError = true;
                }
            }

            if (!readError)
            {
                if (!Directory.Exists(Path.Combine(path, "CoinData.txt")))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(Path.Combine(path, "CoinData.txt")))
                {
                    for (int i = 0; i < trackedCoins.Count; i++)
                    {
                        //Print name, quantity, net cost, and api link to text file
                        file.WriteLine(textFileArray[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Parses an xml file into a data table
        /// </summary>
        /// <returns></returns>
        public DataTable XmlToDatatable()
        {
            //TODO - Refactor to add optional my documents and path
            DataSet temp = new DataSet();

            string path = System.IO.Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.MyDoc‌​uments), "CrytoTracker");

            if (File.Exists(Path.Combine(path, "TradeData.xml"))) //TODO - TradesTabIntegration - Change to file exists, fails when file does not exists but path does
            {
                temp.ReadXml(Path.Combine(path, "TradeData.xml"));
                temp.Tables[0].Rows[temp.Tables[0].Rows.Count-1].Delete();
                return temp.Tables[0];
            }

            return null;
        }

        /// <summary>
        /// Saves datagridview data to an xml file
        /// </summary>
        /// <param name="table"></param>
        public void DataGridViewToXML(DataGridView table)
        {
            //TODO - Refactor to add optional my documents and path
            string path = System.IO.Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.MyDoc‌​uments), "CrytoTracker");

            DataTable dT = GetDataTableFromDGV(table);
            DataSet dS = new DataSet();
            dS.Tables.Add(dT);
            dS.WriteXml(File.OpenWrite(Path.Combine(path, "TradeData.xml")));
        }

        /// <summary>
        /// Converts data grid view to a data table
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        private DataTable GetDataTableFromDGV(DataGridView dgv)
        {
            var dt = new DataTable();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column.Visible)
                {
                    // You could potentially name the column based on the DGV column name (beware of dupes)
                    // or assign a type based on the data type of the data bound to this DGV column.
                    dt.Columns.Add(column.Name);
                }
            }

            object[] cellValues = new object[dgv.Columns.Count];
            foreach (DataGridViewRow row in dgv.Rows)
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    cellValues[i] = row.Cells[i].Value;
                }
                dt.Rows.Add(cellValues);
            }

            return dt;
        }
    }
}
