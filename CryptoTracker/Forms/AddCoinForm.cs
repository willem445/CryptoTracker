using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoTracker
{
    public partial class AddCoinForm : MetroFramework.Forms.MetroForm
    {
        List<CoinModel.CoinNameStruct> CoinNames = new List<CoinModel.CoinNameStruct>();

        //Properties****************************************************************************
        public CoinModel Coin
        {
            get
            {
                return coin;
            }
        }

        //Fields********************************************************************************
        private CoinModel coin;

        //Constructor***************************************************************************
        public AddCoinForm(List<CoinModel.CoinNameStruct> coinNames)
        {
            InitializeComponent();
            this.Text = "Add New Coin";
            cancelButton.DialogResult = DialogResult.Cancel;

            CoinNames = coinNames;

            foreach (var item in CoinNames)
            {
                selectCoin_CB.Items.Add(item.Name);
            }

            coin = new CoinModel();
        }

        //Methods*******************************************************************************
        /// <summary>
        /// Check if data is valid and add new coin to form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            bool error = false;

            //Check for valid coin name
            if ((selectCoin_CB.Text == null) || (selectCoin_CB.Text == ""))
            {
                MessageBox.Show("Enter coin name!");
                error = true;
            }

            //Check for valid quantity
            if ((quantity_TB.Text == null) || (quantity_TB.Text == ""))
            {
                MessageBox.Show("Enter coin quantity!");
                error = true;
            }
            else if (!Extensions.IsNumeric(quantity_TB.Text))
            {
                MessageBox.Show("Enter valid quantity!");
                error = true;
            }

            //Check for valid coin cost
            if ((netCost_TB.Text == null) || (netCost_TB.Text == ""))
            {
                MessageBox.Show("Enter netcost!");
                error = true;
            }
            else
            {
                if (netCost_TB.Text.Contains('$'))
                {
                    netCost_TB.Text = netCost_TB.Text.Split('$')[1];
                }

                if (!Extensions.IsNumeric(netCost_TB.Text))
                {
                    MessageBox.Show("Enter valid net cost!");
                    error = true;
                }
            }

            //If no errors, continue
            if(!error)
            {
                coin.Name = selectCoin_CB.Text;
                coin.Quantity = (float)Convert.ToDouble(quantity_TB.Text);
                coin.NetCost = (float)Convert.ToDouble(netCost_TB.Text);
                coin.APILink = "https://api.coinmarketcap.com/v1/ticker/" + CoinNames[selectCoin_CB.SelectedIndex].Id + "/";

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// Cancel and close form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
