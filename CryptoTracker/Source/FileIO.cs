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

        public DataTable ParseTradesFile()
        {
            DataSet temp = new DataSet();

            string path = System.IO.Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.MyDoc‌​uments), "CrytoTracker");

            if (Directory.Exists(path))
            {
                temp.ReadXml(Path.Combine(path, "TradeData.xml"));
            }

            return temp.Tables[0];
        }

        public void SaveToXML(DataGridView table)
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.MyDoc‌​uments), "CrytoTracker");

            DataTable dT = GetDataTableFromDGV(table);
            DataSet dS = new DataSet();
            dS.Tables.Add(dT);
            dS.WriteXml(File.OpenWrite(Path.Combine(path, "TradeData.xml")));
        }

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
