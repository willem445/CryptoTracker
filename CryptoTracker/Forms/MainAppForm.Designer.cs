namespace CryptoTracker
{
    partial class MainAppForm
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
            this.components = new System.ComponentModel.Container();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addBuyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.donateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.totalValueLabel = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.totalInvestedLabel = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.totalProfitLabel = new MetroFramework.Controls.MetroLabel();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.refreshButton = new MetroFramework.Controls.MetroButton();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.metroComboBox1 = new MetroFramework.Controls.MetroComboBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.metroTabPage4 = new MetroFramework.Controls.MetroTabPage();
            this.infoFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.metroStyleExtender1 = new MetroFramework.Components.MetroStyleExtender(this.components);
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.pieChart1 = new LiveCharts.WinForms.PieChart();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.menuStrip1.SuspendLayout();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.metroTabPage3.SuspendLayout();
            this.metroTabPage4.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(789, 379);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(20, 60);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(787, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.settingsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addBuyToolStripMenuItem,
            this.editToolStripMenuItem});
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
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.editToolStripMenuItem.Text = "Edit Coin";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.donateToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // donateToolStripMenuItem
            // 
            this.donateToolStripMenuItem.Name = "donateToolStripMenuItem";
            this.donateToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.donateToolStripMenuItem.Text = "Donate";
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage3);
            this.metroTabControl1.Controls.Add(this.metroTabPage4);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Location = new System.Drawing.Point(23, 87);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 3;
            this.metroTabControl1.Size = new System.Drawing.Size(804, 460);
            this.metroTabControl1.TabIndex = 5;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.totalValueLabel);
            this.metroTabPage1.Controls.Add(this.metroLabel2);
            this.metroTabPage1.Controls.Add(this.totalInvestedLabel);
            this.metroTabPage1.Controls.Add(this.metroLabel4);
            this.metroTabPage1.Controls.Add(this.totalProfitLabel);
            this.metroTabPage1.Controls.Add(this.metroLabel6);
            this.metroTabPage1.Controls.Add(this.refreshButton);
            this.metroTabPage1.Controls.Add(this.flowLayoutPanel1);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(796, 421);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "Price Tracking";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            // 
            // totalValueLabel
            // 
            this.totalValueLabel.AutoSize = true;
            this.totalValueLabel.FontSize = MetroFramework.MetroLabelSize.Small;
            this.totalValueLabel.Location = new System.Drawing.Point(410, 392);
            this.totalValueLabel.Name = "totalValueLabel";
            this.totalValueLabel.Size = new System.Drawing.Size(46, 15);
            this.totalValueLabel.TabIndex = 6;
            this.totalValueLabel.Text = "$200.00";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel2.Location = new System.Drawing.Point(367, 392);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(37, 15);
            this.metroLabel2.TabIndex = 7;
            this.metroLabel2.Text = "Value:";
            // 
            // totalInvestedLabel
            // 
            this.totalInvestedLabel.AutoSize = true;
            this.totalInvestedLabel.FontSize = MetroFramework.MetroLabelSize.Small;
            this.totalInvestedLabel.Location = new System.Drawing.Point(293, 392);
            this.totalInvestedLabel.Name = "totalInvestedLabel";
            this.totalInvestedLabel.Size = new System.Drawing.Size(46, 15);
            this.totalInvestedLabel.TabIndex = 8;
            this.totalInvestedLabel.Text = "$200.00";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel4.Location = new System.Drawing.Point(235, 392);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(52, 15);
            this.metroLabel4.TabIndex = 9;
            this.metroLabel4.Text = "Invested:";
            // 
            // totalProfitLabel
            // 
            this.totalProfitLabel.AutoSize = true;
            this.totalProfitLabel.FontSize = MetroFramework.MetroLabelSize.Small;
            this.totalProfitLabel.Location = new System.Drawing.Point(157, 392);
            this.totalProfitLabel.Name = "totalProfitLabel";
            this.totalProfitLabel.Size = new System.Drawing.Size(46, 15);
            this.totalProfitLabel.TabIndex = 10;
            this.totalProfitLabel.Text = "$200.00";
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel6.Location = new System.Drawing.Point(114, 392);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(37, 15);
            this.metroLabel6.TabIndex = 11;
            this.metroLabel6.Text = "Profit:";
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(13, 388);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(77, 19);
            this.refreshButton.TabIndex = 10;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.refreshButton_Click);
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.Controls.Add(this.metroButton2);
            this.metroTabPage3.Controls.Add(this.metroComboBox1);
            this.metroTabPage3.Controls.Add(this.listBox1);
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(796, 421);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "Trades";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            // 
            // metroButton2
            // 
            this.metroButton2.Location = new System.Drawing.Point(702, 29);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(75, 23);
            this.metroButton2.TabIndex = 4;
            this.metroButton2.Text = "Import";
            // 
            // metroComboBox1
            // 
            this.metroComboBox1.FontSize = MetroFramework.MetroLinkSize.Small;
            this.metroComboBox1.FormattingEnabled = true;
            this.metroComboBox1.ItemHeight = 19;
            this.metroComboBox1.Location = new System.Drawing.Point(125, 27);
            this.metroComboBox1.Name = "metroComboBox1";
            this.metroComboBox1.Size = new System.Drawing.Size(121, 25);
            this.metroComboBox1.TabIndex = 3;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(3, 62);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(774, 342);
            this.listBox1.TabIndex = 2;
            // 
            // metroTabPage4
            // 
            this.metroTabPage4.Controls.Add(this.infoFlowPanel);
            this.metroTabPage4.HorizontalScrollbarBarColor = true;
            this.metroTabPage4.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage4.Name = "metroTabPage4";
            this.metroTabPage4.Size = new System.Drawing.Size(796, 421);
            this.metroTabPage4.TabIndex = 3;
            this.metroTabPage4.Text = "Coin Info";
            this.metroTabPage4.VerticalScrollbarBarColor = true;
            // 
            // infoFlowPanel
            // 
            this.infoFlowPanel.AutoScroll = true;
            this.infoFlowPanel.BackColor = System.Drawing.Color.White;
            this.infoFlowPanel.Location = new System.Drawing.Point(3, 3);
            this.infoFlowPanel.Name = "infoFlowPanel";
            this.infoFlowPanel.Size = new System.Drawing.Size(777, 415);
            this.infoFlowPanel.TabIndex = 2;
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.metroButton1);
            this.metroTabPage2.Controls.Add(this.pieChart1);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(796, 421);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "Portfolio";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            this.metroStyleManager1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // pieChart1
            // 
            this.pieChart1.BackColor = System.Drawing.Color.White;
            this.pieChart1.Location = new System.Drawing.Point(3, 12);
            this.pieChart1.Name = "pieChart1";
            this.pieChart1.Size = new System.Drawing.Size(477, 394);
            this.pieChart1.TabIndex = 2;
            this.pieChart1.Text = "pieChart1";
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(552, 107);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(75, 23);
            this.metroButton1.TabIndex = 3;
            this.metroButton1.Text = "metroButton1";
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // MainAppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(827, 551);
            this.Controls.Add(this.metroTabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainAppForm";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Crypto Tracker";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage1.PerformLayout();
            this.metroTabPage3.ResumeLayout(false);
            this.metroTabPage4.ResumeLayout(false);
            this.metroTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addBuyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private MetroFramework.Components.MetroStyleExtender metroStyleExtender1;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroLabel totalProfitLabel;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel totalInvestedLabel;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel totalValueLabel;
        private MetroFramework.Controls.MetroButton refreshButton;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private MetroFramework.Controls.MetroComboBox metroComboBox1;
        private System.Windows.Forms.ListBox listBox1;
        private MetroFramework.Controls.MetroTabPage metroTabPage4;
        private System.Windows.Forms.FlowLayoutPanel infoFlowPanel;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem donateToolStripMenuItem;
        private MetroFramework.Controls.MetroButton metroButton2;
        private LiveCharts.WinForms.PieChart pieChart1;
        private MetroFramework.Controls.MetroButton metroButton1;
    }
}

