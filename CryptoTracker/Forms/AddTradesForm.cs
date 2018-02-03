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
    //TODO - Add tooltips to each field to aid in data entry
    //TODO - Highlight incorrect data if data validation fails
    public partial class AddTradeForm : MetroFramework.Forms.MetroForm
    {
        private DataTable table = new DataTable();
        private List<CoinModel.CoinNameStruct> allCoinNames = new List<CoinModel.CoinNameStruct>();
        private int tradeIndex;

        public DataTable AddTradeTable
        {
            get
            {
                return table;
            }
        }

        public AddTradeForm(List<CoinModel.CoinNameStruct> allCoins, int selectCoinIndex)
        {
            InitializeComponent();

            allCoinNames = allCoins;
            tradeIndex = selectCoinIndex;

            //TODO - Add fiat currencies
            CoinModel.CoinNameStruct usd = new CoinModel.CoinNameStruct();
            usd.Symbol = "USD";
            usd.Name = "USD";
            usd.Id = "usd";
            allCoinNames.Add(usd);

            this.dateTimePicker1.CustomFormat = "hh:mm tt";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.ShowUpDown = true;

            addTradeCalender.MaxSelectionCount = 1;
            addTradeDate_TB.Text = addTradeCalender.SelectionStart.ToShortDateString();

            exchange_CB.Items.Add("Binance");
            exchange_CB.Items.Add("Coinbase");
            exchange_CB.SelectedIndex = 0;

            type_CB.Items.Add("BUY");
            type_CB.Items.Add("SELL");
            type_CB.SelectedIndex = 0;

            foreach(var item in allCoins)
            {
                tradeBase_CB.Items.Add(item.Name);
            }

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
            bool error = false;
            GeneralImport import = new GeneralImport();

            DateTime date = addTradeCalender.SelectionStart.Date + dateTimePicker1.Value.TimeOfDay;

            //Combobox data validation
            if (exchange_CB.SelectedIndex < 0)
            {
                MessageBoxForm messageBox = new MessageBoxForm("Select an exchange.");
                messageBox.ShowDialog();
                error = true;
            }

            //Trade pair data validation
            if (tradeBase_CB.SelectedIndex < 0)
            {
                MessageBoxForm messageBox = new MessageBoxForm("Select trade pair.");
                messageBox.ShowDialog();
                error = true;
            }

            //Type data validation
            if (type_CB.SelectedIndex < 0)
            {
                MessageBoxForm messageBox = new MessageBoxForm("Select a trade type.");
                messageBox.ShowDialog();
                error = true;
            }

            //Quantity data validation
            importQuantity_TB.Text = importQuantity_TB.Text.StripDollarSign();
            if (!importQuantity_TB.Text.IsNumeric() || importQuantity_TB.Text == "")
            {
                MessageBoxForm messageBox = new MessageBoxForm("Enter valid quantity.");
                messageBox.ShowDialog();
                error = true;
            }

            //Price data validation
            importPrice_TB.Text = importPrice_TB.Text.StripDollarSign();
            if (!importPrice_TB.Text.IsNumeric() || importPrice_TB.Text == "")
            {
                MessageBoxForm messageBox = new MessageBoxForm("Enter valid price.");
                messageBox.ShowDialog();
                error = true;
            }

            if (!error)
            {
                table.Rows.Add(date,
                               exchange_CB.Text, 
                               allCoinNames[tradeIndex].Symbol + "/" + allCoinNames[tradeBase_CB.SelectedIndex].Symbol, 
                               type_CB.Text, 
                               (float)Convert.ToDouble(importQuantity_TB.Text),
                               (float)Convert.ToDouble(importPrice_TB.Text),
                               (float)Convert.ToDouble(importQuantity_TB.Text) * (float)Convert.ToDouble(importPrice_TB.Text), 
                               import.GetHistoricalUsdValue(date, allCoinNames[tradeBase_CB.SelectedIndex].Symbol) * ((float)Convert.ToDouble(importQuantity_TB.Text) * (float)Convert.ToDouble(importPrice_TB.Text)));

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
