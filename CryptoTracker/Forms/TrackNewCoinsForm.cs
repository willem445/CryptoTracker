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
    public partial class TrackNewCoinsForm : MetroFramework.Forms.MetroForm
    {
        private List<string> trackNewCoin = new List<string>();
        private List<string> untrackedCoins = new List<string>();

        public List<string> TrackNewCoin
        {
            get
            {
                return trackNewCoin;
            }
        }


        public TrackNewCoinsForm(List<string> untrackedCoins)
        {
            InitializeComponent();

            this.untrackedCoins = untrackedCoins;
            foreach (var item in untrackedCoins)
            {
                checkedListBox1.Items.Add(item, true);
            }
        }

        private void okayButton_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    trackNewCoin.Add(untrackedCoins[i]);
                }
            }
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
