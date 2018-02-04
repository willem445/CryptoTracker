namespace CryptoTracker
{
    partial class DonateForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.messageLabel = new System.Windows.Forms.Label();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.etherLink = new MetroFramework.Controls.MetroLink();
            this.stellarLink = new MetroFramework.Controls.MetroLink();
            this.nanoLink = new MetroFramework.Controls.MetroLink();
            this.SuspendLayout();
            // 
            // messageLabel
            // 
            this.messageLabel.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageLabel.Location = new System.Drawing.Point(23, 76);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(488, 52);
            this.messageLabel.TabIndex = 3;
            this.messageLabel.Text = "Thank you for using Crypto Tracker! If you like this product, consider donating! " +
    "Donations help the continued support and development of Crypto Tracker.";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(21, 132);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(42, 19);
            this.metroLabel1.TabIndex = 4;
            this.metroLabel1.Text = "Ether:";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(21, 166);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(52, 19);
            this.metroLabel2.TabIndex = 5;
            this.metroLabel2.Text = "Stellar: ";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(21, 200);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(44, 19);
            this.metroLabel3.TabIndex = 6;
            this.metroLabel3.Text = "Nano:";
            // 
            // etherLink
            // 
            this.etherLink.Location = new System.Drawing.Point(69, 131);
            this.etherLink.Name = "etherLink";
            this.etherLink.Size = new System.Drawing.Size(401, 23);
            this.etherLink.TabIndex = 7;
            this.etherLink.Text = "0xDC286C7B9aD248C8474803aFDdF9abeA89031D06";
            this.etherLink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.etherLink.MouseClick += new System.Windows.Forms.MouseEventHandler(this.etherLink_MouseClick);
            // 
            // stellarLink
            // 
            this.stellarLink.Location = new System.Drawing.Point(69, 165);
            this.stellarLink.Name = "stellarLink";
            this.stellarLink.Size = new System.Drawing.Size(478, 23);
            this.stellarLink.TabIndex = 8;
            this.stellarLink.Text = "GAD5K3AQB6MOGG47MB5ZOBY5YMFMHFSWMNBLMWWKESNWS67REWUVZHZJ";
            this.stellarLink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.stellarLink.MouseClick += new System.Windows.Forms.MouseEventHandler(this.stellarLink_MouseClick);
            // 
            // nanoLink
            // 
            this.nanoLink.Location = new System.Drawing.Point(69, 198);
            this.nanoLink.Name = "nanoLink";
            this.nanoLink.Size = new System.Drawing.Size(471, 23);
            this.nanoLink.TabIndex = 9;
            this.nanoLink.Text = "xrb_1jpy9x6cij7xhmtz1i6j7cxdrdkg391zc3airzb1aq64qd5z54cfhjggh9ut";
            this.nanoLink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.nanoLink.MouseClick += new System.Windows.Forms.MouseEventHandler(this.nanoLink_MouseClick);
            // 
            // DonateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(570, 244);
            this.Controls.Add(this.nanoLink);
            this.Controls.Add(this.stellarLink);
            this.Controls.Add(this.etherLink);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.messageLabel);
            this.Name = "DonateForm";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Help Support CryptoTracker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label messageLabel;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLink etherLink;
        private MetroFramework.Controls.MetroLink stellarLink;
        private MetroFramework.Controls.MetroLink nanoLink;
    }
}