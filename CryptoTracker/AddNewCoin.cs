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
    public partial class AddNewCoin : Form
    {
        public CoinModel Coin
        {
            get
            {
                return coin;
            }
        }

        private CoinModel coin;

        public AddNewCoin()
        {
            InitializeComponent();
            addButton.DialogResult = DialogResult.OK;
            cancelButton.DialogResult = DialogResult.Cancel;

            coin = new CoinModel();
        }


        private void addButton_Click(object sender, EventArgs e)
        {
            coin.CoinName = coinName_TB.Text;
            coin.Quantity = quantity_TB.Text;
            coin.NetCost = netCost_TB.Text;
            coin.APILink = apiLink_TB.Text;

            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
