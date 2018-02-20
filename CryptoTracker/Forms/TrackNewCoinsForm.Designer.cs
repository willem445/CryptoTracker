namespace CryptoTracker
{
    partial class TrackNewCoinsForm
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
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.messageLabel = new System.Windows.Forms.Label();
            this.okayButton = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(23, 106);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(240, 174);
            this.checkedListBox1.TabIndex = 0;
            // 
            // messageLabel
            // 
            this.messageLabel.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageLabel.Location = new System.Drawing.Point(13, 60);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(264, 43);
            this.messageLabel.TabIndex = 3;
            this.messageLabel.Text = "The following coins are not being tracked. Check the coins you wish to add to tra" +
    "cking.";
            // 
            // okayButton
            // 
            this.okayButton.Location = new System.Drawing.Point(188, 288);
            this.okayButton.Name = "okayButton";
            this.okayButton.Size = new System.Drawing.Size(75, 23);
            this.okayButton.TabIndex = 4;
            this.okayButton.Text = "Okay";
            this.okayButton.Click += new System.EventHandler(this.okayButton_Click);
            // 
            // TrackNewCoinsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(290, 323);
            this.Controls.Add(this.okayButton);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.checkedListBox1);
            this.Name = "TrackNewCoinsForm";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Track Coins";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label messageLabel;
        private MetroFramework.Controls.MetroButton okayButton;
    }
}