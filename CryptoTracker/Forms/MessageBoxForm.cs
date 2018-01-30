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
    public partial class MessageBoxForm : MetroFramework.Forms.MetroForm
    {
        public MessageBoxForm(string message)
        {
            InitializeComponent();
            cancelButton.DialogResult = DialogResult.Cancel;
       
            messageLabel.Text = message;
        }

        private void okayButton_Click(object sender, EventArgs e)
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
