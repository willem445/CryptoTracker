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
    public partial class ExportTradesForm : MetroFramework.Forms.MetroForm
    {
        public int Year { get; set; }

        public ExportTradesForm()
        {
            InitializeComponent();
            year_CB.Items.Add("2012");
            year_CB.Items.Add("2013");
            year_CB.Items.Add("2014");
            year_CB.Items.Add("2015");
            year_CB.Items.Add("2016");
            year_CB.Items.Add("2017");
            year_CB.Items.Add("2018");
        }

        private void year_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Year = Convert.ToInt16(year_CB.Text);
        }

        private void okayButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
