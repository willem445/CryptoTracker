namespace CryptoTracker
{
    partial class ExportTradesForm
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
            this.year_CB = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.okayButton = new MetroFramework.Controls.MetroButton();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.selectFormat_CB = new MetroFramework.Controls.MetroComboBox();
            this.SuspendLayout();
            // 
            // year_CB
            // 
            this.year_CB.FontSize = MetroFramework.MetroLinkSize.Small;
            this.year_CB.FormattingEnabled = true;
            this.year_CB.ItemHeight = 19;
            this.year_CB.Location = new System.Drawing.Point(122, 82);
            this.year_CB.Name = "year_CB";
            this.year_CB.Size = new System.Drawing.Size(167, 25);
            this.year_CB.TabIndex = 0;
            this.year_CB.SelectedIndexChanged += new System.EventHandler(this.year_CB_SelectedIndexChanged);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(23, 82);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(77, 19);
            this.metroLabel1.TabIndex = 1;
            this.metroLabel1.Text = "Select Year:";
            // 
            // okayButton
            // 
            this.okayButton.Location = new System.Drawing.Point(214, 174);
            this.okayButton.Name = "okayButton";
            this.okayButton.Size = new System.Drawing.Size(75, 23);
            this.okayButton.TabIndex = 2;
            this.okayButton.Text = "Okay";
            this.okayButton.Click += new System.EventHandler(this.okayButton_Click);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(23, 126);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(93, 19);
            this.metroLabel2.TabIndex = 4;
            this.metroLabel2.Text = "Select Format:";
            // 
            // selectFormat_CB
            // 
            this.selectFormat_CB.FontSize = MetroFramework.MetroLinkSize.Small;
            this.selectFormat_CB.FormattingEnabled = true;
            this.selectFormat_CB.ItemHeight = 19;
            this.selectFormat_CB.Location = new System.Drawing.Point(122, 126);
            this.selectFormat_CB.Name = "selectFormat_CB";
            this.selectFormat_CB.Size = new System.Drawing.Size(167, 25);
            this.selectFormat_CB.TabIndex = 3;
            // 
            // ExportTradesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(322, 229);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.selectFormat_CB);
            this.Controls.Add(this.okayButton);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.year_CB);
            this.Name = "ExportTradesForm";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Export Trades";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroComboBox year_CB;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton okayButton;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroComboBox selectFormat_CB;
    }
}