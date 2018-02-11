namespace CryptoTracker
{
    partial class AddNewPriceMonitor
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
            this.selectCoin_CB = new MetroFramework.Controls.MetroComboBox();
            this.cancelButton = new MetroFramework.Controls.MetroButton();
            this.addButton = new MetroFramework.Controls.MetroButton();
            this.messageLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // selectCoin_CB
            // 
            this.selectCoin_CB.FontSize = MetroFramework.MetroLinkSize.Small;
            this.selectCoin_CB.FormattingEnabled = true;
            this.selectCoin_CB.ItemHeight = 19;
            this.selectCoin_CB.Location = new System.Drawing.Point(26, 92);
            this.selectCoin_CB.Name = "selectCoin_CB";
            this.selectCoin_CB.Size = new System.Drawing.Size(223, 25);
            this.selectCoin_CB.TabIndex = 21;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(92, 134);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 23;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(173, 134);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 22;
            this.addButton.Text = "Add";
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // messageLabel
            // 
            this.messageLabel.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageLabel.Location = new System.Drawing.Point(23, 60);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(323, 29);
            this.messageLabel.TabIndex = 24;
            this.messageLabel.Text = "Select coin to add to price monitoring.";
            // 
            // AddNewPriceMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(272, 182);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.selectCoin_CB);
            this.Name = "AddNewPriceMonitor";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "AddNewPriceMonitor";
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroComboBox selectCoin_CB;
        private MetroFramework.Controls.MetroButton cancelButton;
        private MetroFramework.Controls.MetroButton addButton;
        private System.Windows.Forms.Label messageLabel;
    }
}