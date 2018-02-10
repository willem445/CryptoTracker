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
    public partial class AddNewPriceMonitor : MetroFramework.Forms.MetroForm
    {
        List<CoinModel.CoinNameStruct> CoinNames = new List<CoinModel.CoinNameStruct>();

        public AddNewPriceMonitor(List<CoinModel.CoinNameStruct> coinNames)
        {
            InitializeComponent();
            this.Text = "Price Monitor";
            cancelButton.DialogResult = DialogResult.Cancel;

            CoinNames = coinNames;

            foreach (var item in CoinNames)
            {
                selectCoin_CB.Items.Add(item.Name);
            }
            selectCoin_CB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            selectCoin_CB.AutoCompleteSource = AutoCompleteSource.ListItems;
            selectCoin_CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }

        private void addButton_Click(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {

        }
    }
}
