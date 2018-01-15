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
        public CoinModel Coin
        {
            get
            {
                return coin;
            }
        }

        private CoinModel coin;
        
        public AddCoinForm()
        {
            InitializeComponent();
            this.Text = "Add New Coin";
            cancelButton.DialogResult = DialogResult.Cancel;
            //addButton.DialogResult = DialogResult.OK;

            apiLink_TB.Text = "https://api.coinmarketcap.com/v1/ticker/";

            coin = new CoinModel();
        }


        private void addButton_Click(object sender, EventArgs e)
        {
            bool error = false;

            //Check for valid coin name
            if ((coinName_TB.Text == null) || (coinName_TB.Text == ""))
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

            //Check for valid api link
            if ((apiLink_TB.Text == null) || (apiLink_TB.Text == ""))
            {
                MessageBox.Show("Enter api link!");
                error = true;
            }
            else
            {
                try
                {
                    var cli = new System.Net.WebClient();
                    string prices = cli.DownloadString(apiLink_TB.Text);
                }
                catch
                {
                    MessageBox.Show("Enter valid api url!");
                    error = true;
                }
            }

            //If no errors, continue
            if(!error)
            {
                coin.CoinName = coinName_TB.Text;
                coin.Quantity = (float)Convert.ToDouble(quantity_TB.Text);
                coin.NetCost = (float)Convert.ToDouble(netCost_TB.Text);
                coin.APILink = apiLink_TB.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
