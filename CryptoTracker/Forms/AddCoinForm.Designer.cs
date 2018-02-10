namespace CryptoTracker
{
    partial class AddCoinForm
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
            this.addButton = new MetroFramework.Controls.MetroButton();
            this.cancelButton = new MetroFramework.Controls.MetroButton();
            this.netCost_TB = new MetroFramework.Controls.MetroTextBox();
            this.quantity_TB = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.selectCoin_CB = new MetroFramework.Controls.MetroComboBox();
            this.SuspendLayout();
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(147, 180);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 10;
            this.addButton.Text = "Add";
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(66, 180);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // netCost_TB
            // 
            this.netCost_TB.Location = new System.Drawing.Point(101, 118);
            this.netCost_TB.Name = "netCost_TB";
            this.netCost_TB.Size = new System.Drawing.Size(119, 23);
            this.netCost_TB.TabIndex = 13;
            // 
            // quantity_TB
            // 
            this.quantity_TB.BackColor = System.Drawing.Color.White;
            this.quantity_TB.Location = new System.Drawing.Point(101, 89);
            this.quantity_TB.Name = "quantity_TB";
            this.quantity_TB.Size = new System.Drawing.Size(119, 23);
            this.quantity_TB.TabIndex = 14;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(16, 64);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(79, 19);
            this.metroLabel1.TabIndex = 16;
            this.metroLabel1.Text = "Coin Name:";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(16, 93);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(61, 19);
            this.metroLabel2.TabIndex = 17;
            this.metroLabel2.Text = "Quantity:";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(16, 122);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(63, 19);
            this.metroLabel3.TabIndex = 18;
            this.metroLabel3.Text = "Net Cost:";
            // 
            // selectCoin_CB
            // 
            this.selectCoin_CB.FontSize = MetroFramework.MetroLinkSize.Small;
            this.selectCoin_CB.FormattingEnabled = true;
            this.selectCoin_CB.ItemHeight = 19;
            this.selectCoin_CB.Location = new System.Drawing.Point(101, 58);
            this.selectCoin_CB.Name = "selectCoin_CB";
            this.selectCoin_CB.Size = new System.Drawing.Size(121, 25);
            this.selectCoin_CB.TabIndex = 20;
            // 
            // AddCoinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(266, 226);
            this.Controls.Add(this.selectCoin_CB);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.quantity_TB);
            this.Controls.Add(this.netCost_TB);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addButton);
            this.Name = "AddCoinForm";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Add New Coin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton addButton;
        private MetroFramework.Controls.MetroButton cancelButton;
        private MetroFramework.Controls.MetroTextBox netCost_TB;
        private MetroFramework.Controls.MetroTextBox quantity_TB;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroComboBox selectCoin_CB;
    }
}