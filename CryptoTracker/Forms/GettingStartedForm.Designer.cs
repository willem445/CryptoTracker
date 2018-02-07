namespace CryptoTracker
{
    partial class GettingStartedForm
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
            this.okayButton = new MetroFramework.Controls.MetroButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // messageLabel
            // 
            this.messageLabel.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.messageLabel.Location = new System.Drawing.Point(23, 69);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(565, 51);
            this.messageLabel.TabIndex = 5;
            this.messageLabel.Text = "Welcome to CryptoTracker! Get started by importing your trades from an exchange o" +
    "r manually entering your trades.";
            // 
            // okayButton
            // 
            this.okayButton.Location = new System.Drawing.Point(513, 480);
            this.okayButton.Name = "okayButton";
            this.okayButton.Size = new System.Drawing.Size(75, 23);
            this.okayButton.TabIndex = 3;
            this.okayButton.Text = "Okay";
            this.okayButton.Click += new System.EventHandler(this.okayButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ImageLocation = "../../Resources\\TradesTab.png";
            this.pictureBox1.Location = new System.Drawing.Point(26, 123);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(562, 351);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // GettingStartedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(611, 526);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.okayButton);
            this.Name = "GettingStartedForm";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Getting Started";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label messageLabel;
        private MetroFramework.Controls.MetroButton okayButton;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}