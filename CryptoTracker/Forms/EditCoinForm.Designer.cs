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
            this.label1 = new System.Windows.Forms.Label();
            this.selectCoin_CB = new System.Windows.Forms.ComboBox();
            this.saveAfterEditCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.quantity_TB = new System.Windows.Forms.TextBox();
            this.netCost_TB = new System.Windows.Forms.TextBox();
            this.okay_Button = new System.Windows.Forms.Button();
            this.cancel_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Coin";
            // 
            // selectCoin_CB
            // 
            this.selectCoin_CB.FormattingEnabled = true;
            this.selectCoin_CB.Location = new System.Drawing.Point(79, 18);
            this.selectCoin_CB.Name = "selectCoin_CB";
            this.selectCoin_CB.Size = new System.Drawing.Size(121, 21);
            this.selectCoin_CB.TabIndex = 1;
            // 
            // saveAfterEditCheckBox
            // 
            this.saveAfterEditCheckBox.AutoSize = true;
            this.saveAfterEditCheckBox.Location = new System.Drawing.Point(206, 20);
            this.saveAfterEditCheckBox.Name = "saveAfterEditCheckBox";
            this.saveAfterEditCheckBox.Size = new System.Drawing.Size(95, 17);
            this.saveAfterEditCheckBox.TabIndex = 2;
            this.saveAfterEditCheckBox.Text = "Save after edit";
            this.saveAfterEditCheckBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Quantity:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Net Cost:";
            // 
            // quantity_TB
            // 
            this.quantity_TB.Location = new System.Drawing.Point(79, 51);
            this.quantity_TB.Name = "quantity_TB";
            this.quantity_TB.Size = new System.Drawing.Size(121, 20);
            this.quantity_TB.TabIndex = 5;
            // 
            // netCost_TB
            // 
            this.netCost_TB.Location = new System.Drawing.Point(79, 85);
            this.netCost_TB.Name = "netCost_TB";
            this.netCost_TB.Size = new System.Drawing.Size(121, 20);
            this.netCost_TB.TabIndex = 6;
            // 
            // okay_Button
            // 
            this.okay_Button.Location = new System.Drawing.Point(226, 129);
            this.okay_Button.Name = "okay_Button";
            this.okay_Button.Size = new System.Drawing.Size(75, 23);
            this.okay_Button.TabIndex = 7;
            this.okay_Button.Text = "Ok";
            this.okay_Button.UseVisualStyleBackColor = true;
            this.okay_Button.Click += new System.EventHandler(this.okay_Button_Click);
            // 
            // cancel_Button
            // 
            this.cancel_Button.Location = new System.Drawing.Point(145, 129);
            this.cancel_Button.Name = "cancel_Button";
            this.cancel_Button.Size = new System.Drawing.Size(75, 23);
            this.cancel_Button.TabIndex = 8;
            this.cancel_Button.Text = "Cancel";
            this.cancel_Button.UseVisualStyleBackColor = true;
            this.cancel_Button.Click += new System.EventHandler(this.cancel_Button_Click);
            // 
            // EditCoinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 164);
            this.Controls.Add(this.cancel_Button);
            this.Controls.Add(this.okay_Button);
            this.Controls.Add(this.netCost_TB);
            this.Controls.Add(this.quantity_TB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.saveAfterEditCheckBox);
            this.Controls.Add(this.selectCoin_CB);
            this.Controls.Add(this.label1);
            this.Name = "EditCoinForm";
            this.Text = "EditCoinForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox selectCoin_CB;
        private System.Windows.Forms.CheckBox saveAfterEditCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox quantity_TB;
        private System.Windows.Forms.TextBox netCost_TB;
        private System.Windows.Forms.Button okay_Button;
        private System.Windows.Forms.Button cancel_Button;
    }
}