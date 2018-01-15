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
    public partial class EditCoinForm : MetroFramework.Forms.MetroForm
    {
        //Properties****************************************************************************
        public bool SaveEnabled { get; set; }

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
        public EditCoinForm(List<string> coinList)
        {
            InitializeComponent();
            this.Text = "Edit Coin";

            coin = new CoinModel();

            foreach (var item in coinList)
            {
                selectCoin_CB.Items.Add(item);
            }
        }

        //Methods*******************************************************************************
        /// <summary>
        /// Check if data is valid and return updated coin data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okay_Button_Click(object sender, EventArgs e)
        {
            bool error = false;

            if (saveAfterEditCheckBox.Checked)
            {
                SaveEnabled = true;
            }

            if (selectCoin_CB.Text == "")
            {
                MessageBox.Show("Select coin!");
                error = true;
            }

            //Quantity
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

            //Net cost
            if ((netCost_TB.Text == null) || (netCost_TB.Text == ""))
            {
                MessageBox.Show("Enter valid netcost!");
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

            if (!error)
            {
                coin.Quantity = (float)Convert.ToDouble(quantity_TB.Text);
                coin.TotalInvested = (float)Convert.ToDouble(netCost_TB.Text);
                coin.CoinName = selectCoin_CB.Text;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// Cancel and close form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
