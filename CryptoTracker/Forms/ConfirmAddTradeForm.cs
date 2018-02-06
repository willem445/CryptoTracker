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
    public partial class ConfirmAddTradeForm : MetroFramework.Forms.MetroForm
    {
        public ConfirmAddTradeForm(DateTime date, string buy, string tradePrice, string totalCost, string totalCostUSD)
        {
            InitializeComponent();

            cancelButton.DialogResult = DialogResult.Cancel;

            dateLabel.Text = date.ToString();
            buyLabel.Text = buy;
            tradePriceLabel.Text = tradePrice;
            totalCostLabel.Text = totalCost;
            totalCostUSDLabel.Text = totalCostUSD;
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
