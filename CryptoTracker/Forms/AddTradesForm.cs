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
    public partial class AddTradeForm : MetroFramework.Forms.MetroForm
    {
        public DataTable table = new DataTable();

        public AddTradeForm()
        {
            InitializeComponent();
            this.dateTimePicker1.CustomFormat = "hh:mm tt";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.ShowUpDown = true;

            addTradeCalender.MaxSelectionCount = 1;
            addTradeDate_TB.Text = addTradeCalender.SelectionStart.ToShortDateString();

            exchange_CB.Items.Add("Binance");
            exchange_CB.Items.Add("Coinbase");
            exchange_CB.SelectedIndex = 0;

            type_CB.Items.Add("Buy");
            type_CB.Items.Add("Sell");
            type_CB.SelectedIndex = 0;

            tradePair_TB.Text = "BTC/USD";
            tradePair_TB.Clear();

            table = new DataTable();
            table.Columns.Add("Date", typeof(DateTime));
            table.Columns.Add("Exchange", typeof(string));
            table.Columns.Add("Trade Pair", typeof(string));
            table.Columns.Add("Type", typeof(string));
            table.Columns.Add("Order Quantity", typeof(float));
            table.Columns.Add("Trade Price", typeof(float));
            table.Columns.Add("Order Cost", typeof(float));
            table.Columns.Add("Net Cost (USD)", typeof(float));
        }

        private void addTradeCalender_DateSelected(object sender, DateRangeEventArgs e)
        {
            addTradeDate_TB.Text = addTradeCalender.SelectionStart.ToShortDateString();
        }

        private void addTradeCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addTradeOkayButton_Click(object sender, EventArgs e)
        {
            //TODO - TradesTabIntegration - Verify data is valid before returning ok

            GeneralImport import = new GeneralImport();

            DateTime date = addTradeCalender.SelectionStart.Date + dateTimePicker1.Value.TimeOfDay;

            table.Rows.Add(date,
                exchange_CB.Text, tradePair_TB.Text, type_CB.Text, (float)Convert.ToDouble(importQuantity_TB.Text),
                (float)Convert.ToDouble(importPrice_TB.Text), (float)Convert.ToDouble(importTotalCost_TB.Text), import.GetHistoricalUsdValue(date, tradePair_TB.Text.Split('/')[1]) * (float)Convert.ToDouble(importTotalCost_TB.Text));

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
