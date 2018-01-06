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
        public string CoinName { get; set; }
        public string Quantity { get; set; }
        public string NetCost { get; set; }
        public string APILink { get; set; }

        public AddNewCoin()
        {
            InitializeComponent();
            addButton.DialogResult = DialogResult.OK;
            cancelButton.DialogResult = DialogResult.Cancel;
        }


        private void addButton_Click(object sender, EventArgs e)
        {
            CoinName = coinName_TB.Text;
            Quantity = quantity_TB.Text;
            NetCost = netCost_TB.Text;
            APILink = apiLink_TB.Text;

            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
