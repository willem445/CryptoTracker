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

            //Tooltip
            ToolTip tool = new ToolTip();
            tool.AutoPopDelay = 5000;
            tool.InitialDelay = 1000;
            tool.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            tool.ShowAlways = true;

            tool.SetToolTip(this.exchangeLabel, "Select the exchange where the trade took place.");
            tool.SetToolTip(this.typeLabel, "Select the trade type.");
            tool.SetToolTip(this.tradePairLabel, "Select the base trade currency. Select USD if trading for fiat.");
            tool.SetToolTip(this.quantityLabel, "Select the quantity of the coin being bought or sold. (ie. for XLM/ETH - 500 XLM)");
            tool.SetToolTip(this.priceLabel, "Select the price at which the trade occured. (ie for XLM/ETH - 0.000568 ETH");

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
            addTradeCalender.MaxDate = DateTime.Now;

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
            tradeBase_CB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            tradeBase_CB.AutoCompleteSource = AutoCompleteSource.ListItems;
            tradeBase_CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

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
            string exchange = "";
            string tradeBase = "";
            string trade = "";
            string type = "";
            float quantity = 0.0F;
            float tradePrice = 0.0F;
            float orderCost = 0.0F;
            float netCost = 0.0F;

            //Datetime validation
            if (date > DateTime.Now)
            {
                MessageBoxForm messageBox = new MessageBoxForm("Can't select a future date.");
                messageBox.ShowDialog();
                error = true;
            }

            //Combobox data validation
            if (exchange_CB.SelectedIndex < 0)
            {
                MessageBoxForm messageBox = new MessageBoxForm("Select an exchange.");
                messageBox.ShowDialog();
                error = true;
            }
            else
            {
                exchange = exchange_CB.Text;
            }

            //Trade pair data validation
            if (tradeBase_CB.SelectedIndex < 0)
            {
                MessageBoxForm messageBox = new MessageBoxForm("Select trade pair.");
                messageBox.ShowDialog();
                error = true;
            }
            else
            {
                tradeBase = allCoinNames[tradeBase_CB.SelectedIndex].Symbol;
               trade = allCoinNames[tradeIndex].Symbol;
            }

            //Type data validation
            if (type_CB.SelectedIndex < 0)
            {
                MessageBoxForm messageBox = new MessageBoxForm("Select a trade type.");
                messageBox.ShowDialog();
                error = true;
            }
            else
            {
                type = type_CB.Text;
            }

            //Quantity data validation
            importQuantity_TB.Text = importQuantity_TB.Text.StripDollarSign();
            if (!importQuantity_TB.Text.IsNumeric() || importQuantity_TB.Text == "")
            {
                MessageBoxForm messageBox = new MessageBoxForm("Enter valid quantity.");
                messageBox.ShowDialog();
                error = true;
            }
            else
            {
                quantity = (float)Convert.ToDouble(importQuantity_TB.Text);
            }

            //Price data validation
            importPrice_TB.Text = importPrice_TB.Text.StripDollarSign();
            if (!importPrice_TB.Text.IsNumeric() || importPrice_TB.Text == "")
            {
                MessageBoxForm messageBox = new MessageBoxForm("Enter valid price.");
                messageBox.ShowDialog();
                error = true;
            }
            else
            {
                tradePrice = (float)Convert.ToDouble(importPrice_TB.Text);
                orderCost = tradePrice * quantity;
                netCost = import.GetHistoricalUsdValue(date, tradeBase) * (quantity * tradePrice);
            }

            if (!error)
            {
                //Confirm trade
                ConfirmAddTradeForm confirmTrade = new ConfirmAddTradeForm
                (
                    date, 
                    quantity + " " + trade, 
                    tradePrice + " " + tradeBase + " (" + (netCost/quantity).FloatToMonetary() + ")", 
                    orderCost.ToString() + " " + tradeBase, 
                    netCost.FloatToMonetary()
                );
                
                if (confirmTrade.ShowDialog() == DialogResult.OK)
                {
                    table.Rows.Add
                    (
                        date,
                        exchange,
                        trade + "/" + tradeBase,
                        type,
                        quantity,
                        tradePrice,
                        orderCost,
                        netCost
                    );

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }
    }
}
