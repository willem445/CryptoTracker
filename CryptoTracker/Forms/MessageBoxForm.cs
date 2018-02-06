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
        public MessageBoxForm(string message, bool cancelButtonEnabled = true)
        {
            InitializeComponent();


            if (cancelButtonEnabled)
            {
                cancelButton.DialogResult = DialogResult.Cancel;
            }
            else
            {
                cancelButton.Enabled = false;
                cancelButton.Visible = false;
            }

       
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
