namespace CryptoTracker
{
    partial class EditCoinForm
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
            this.okay_Button = new MetroFramework.Controls.MetroButton();
            this.cancel_Button = new MetroFramework.Controls.MetroButton();
            this.saveAfterEditCheckBox = new MetroFramework.Controls.MetroCheckBox();
            this.quantity_TB = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.netCost_TB = new MetroFramework.Controls.MetroTextBox();
            this.selectCoin_CB = new MetroFramework.Controls.MetroComboBox();
            this.SuspendLayout();
            // 
            // okay_Button
            // 
            this.okay_Button.Location = new System.Drawing.Point(172, 201);
            this.okay_Button.Name = "okay_Button";
            this.okay_Button.Size = new System.Drawing.Size(75, 23);
            this.okay_Button.TabIndex = 9;
            this.okay_Button.Text = "Okay";
            this.okay_Button.Click += new System.EventHandler(this.okay_Button_Click);
            // 
            // cancel_Button
            // 
            this.cancel_Button.Location = new System.Drawing.Point(91, 201);
            this.cancel_Button.Name = "cancel_Button";
            this.cancel_Button.Size = new System.Drawing.Size(75, 23);
            this.cancel_Button.TabIndex = 10;
            this.cancel_Button.Text = "Cancel";
            this.cancel_Button.Click += new System.EventHandler(this.cancel_Button_Click);
            // 
            // saveAfterEditCheckBox
            // 
            this.saveAfterEditCheckBox.AutoSize = true;
            this.saveAfterEditCheckBox.Location = new System.Drawing.Point(75, 159);
            this.saveAfterEditCheckBox.Name = "saveAfterEditCheckBox";
            this.saveAfterEditCheckBox.Size = new System.Drawing.Size(136, 15);
            this.saveAfterEditCheckBox.TabIndex = 11;
            this.saveAfterEditCheckBox.Text = "Save When Complete";
            this.saveAfterEditCheckBox.UseVisualStyleBackColor = true;
            // 
            // quantity_TB
            // 
            this.quantity_TB.Location = new System.Drawing.Point(136, 95);
            this.quantity_TB.Name = "quantity_TB";
            this.quantity_TB.Size = new System.Drawing.Size(111, 23);
            this.quantity_TB.TabIndex = 12;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(23, 68);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(77, 19);
            this.metroLabel1.TabIndex = 13;
            this.metroLabel1.Text = "Select Coin:";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(23, 99);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(61, 19);
            this.metroLabel2.TabIndex = 14;
            this.metroLabel2.Text = "Quantity:";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(23, 131);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(63, 19);
            this.metroLabel3.TabIndex = 15;
            this.metroLabel3.Text = "Net Cost:";
            // 
            // netCost_TB
            // 
            this.netCost_TB.Location = new System.Drawing.Point(136, 127);
            this.netCost_TB.Name = "netCost_TB";
            this.netCost_TB.Size = new System.Drawing.Size(111, 23);
            this.netCost_TB.TabIndex = 16;
            // 
            // selectCoin_CB
            // 
            this.selectCoin_CB.FontSize = MetroFramework.MetroLinkSize.Small;
            this.selectCoin_CB.FormattingEnabled = true;
            this.selectCoin_CB.ItemHeight = 19;
            this.selectCoin_CB.Location = new System.Drawing.Point(136, 63);
            this.selectCoin_CB.Name = "selectCoin_CB";
            this.selectCoin_CB.Size = new System.Drawing.Size(111, 25);
            this.selectCoin_CB.TabIndex = 17;
            this.selectCoin_CB.SelectedIndexChanged += new System.EventHandler(this.selectCoin_CB_SelectedIndexChanged);
            // 
            // EditCoinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(267, 247);
            this.Controls.Add(this.selectCoin_CB);
            this.Controls.Add(this.netCost_TB);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.quantity_TB);
            this.Controls.Add(this.saveAfterEditCheckBox);
            this.Controls.Add(this.cancel_Button);
            this.Controls.Add(this.okay_Button);
            this.Name = "EditCoinForm";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Edit Coin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroButton okay_Button;
        private MetroFramework.Controls.MetroButton cancel_Button;
        private MetroFramework.Controls.MetroCheckBox saveAfterEditCheckBox;
        private MetroFramework.Controls.MetroTextBox quantity_TB;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroTextBox netCost_TB;
        private MetroFramework.Controls.MetroComboBox selectCoin_CB;
    }
}