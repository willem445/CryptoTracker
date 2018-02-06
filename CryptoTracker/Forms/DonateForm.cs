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
    public partial class DonateForm : MetroFramework.Forms.MetroForm
    {
        ToolTip message;

        public DonateForm()
        {
            InitializeComponent();
            message = new ToolTip();
        }

        private void etherLink_MouseClick(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText("0xDC286C7B9aD248C8474803aFDdF9abeA89031D06");
            message.Show("Copied", etherLink, 800);
        }

        private void nanoLink_MouseClick(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText("xrb_1jpy9x6cij7xhmtz1i6j7cxdrdkg391zc3airzb1aq64qd5z54cfhjggh9ut"); 
            message.Show("Copied", nanoLink, 800);
        }

        private void stellarLink_MouseClick(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText("GAD5K3AQB6MOGG47MB5ZOBY5YMFMHFSWMNBLMWWKESNWS67REWUVZHZJ");
            message.Show("Copied", stellarLink, 800);
        }
    }
}
