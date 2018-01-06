namespace CryptoTracker
{
    partial class Form1
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.totalValueLabel = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.totalInvestedLabel = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.totalProfitLabel = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.refreshTextBox = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addBuyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flowLayoutPanel1);
            this.groupBox2.Controls.Add(this.totalValueLabel);
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.totalInvestedLabel);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.totalProfitLabel);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.refreshTextBox);
            this.groupBox2.Location = new System.Drawing.Point(11, 26);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(816, 438);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Profit Calculator";
            // 
            // totalValueLabel
            // 
            this.totalValueLabel.AutoSize = true;
            this.totalValueLabel.Location = new System.Drawing.Point(398, 411);
            this.totalValueLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.totalValueLabel.Name = "totalValueLabel";
            this.totalValueLabel.Size = new System.Drawing.Size(46, 13);
            this.totalValueLabel.TabIndex = 66;
            this.totalValueLabel.Text = "$200.00";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(331, 411);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(64, 13);
            this.label26.TabIndex = 65;
            this.label26.Text = "Total Value:";
            // 
            // totalInvestedLabel
            // 
            this.totalInvestedLabel.AutoSize = true;
            this.totalInvestedLabel.Location = new System.Drawing.Point(282, 411);
            this.totalInvestedLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.totalInvestedLabel.Name = "totalInvestedLabel";
            this.totalInvestedLabel.Size = new System.Drawing.Size(46, 13);
            this.totalInvestedLabel.TabIndex = 64;
            this.totalInvestedLabel.Text = "$200.00";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(201, 411);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(78, 13);
            this.label24.TabIndex = 63;
            this.label24.Text = "Total Invested:";
            // 
            // totalProfitLabel
            // 
            this.totalProfitLabel.AutoSize = true;
            this.totalProfitLabel.Location = new System.Drawing.Point(152, 411);
            this.totalProfitLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.totalProfitLabel.Name = "totalProfitLabel";
            this.totalProfitLabel.Size = new System.Drawing.Size(46, 13);
            this.totalProfitLabel.TabIndex = 56;
            this.totalProfitLabel.Text = "$200.00";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(90, 411);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(61, 13);
            this.label21.TabIndex = 55;
            this.label21.Text = "Total Profit:";
            // 
            // refreshTextBox
            // 
            this.refreshTextBox.Location = new System.Drawing.Point(8, 407);
            this.refreshTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.refreshTextBox.Name = "refreshTextBox";
            this.refreshTextBox.Size = new System.Drawing.Size(56, 19);
            this.refreshTextBox.TabIndex = 9;
            this.refreshTextBox.Text = "Refresh";
            this.refreshTextBox.UseVisualStyleBackColor = true;
            this.refreshTextBox.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(836, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addBuyToolStripMenuItem,
            this.addSellToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // addBuyToolStripMenuItem
            // 
            this.addBuyToolStripMenuItem.Name = "addBuyToolStripMenuItem";
            this.addBuyToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.addBuyToolStripMenuItem.Text = "Add Coin";
            this.addBuyToolStripMenuItem.Click += new System.EventHandler(this.addBuyToolStripMenuItem_Click);
            // 
            // addSellToolStripMenuItem
            // 
            this.addSellToolStripMenuItem.Name = "addSellToolStripMenuItem";
            this.addSellToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.addSellToolStripMenuItem.Text = "Edit Coin";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(5, 18);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(789, 379);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(836, 469);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button refreshTextBox;
        private System.Windows.Forms.Label totalProfitLabel;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addBuyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSellToolStripMenuItem;
        private System.Windows.Forms.Label totalValueLabel;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label totalInvestedLabel;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}

