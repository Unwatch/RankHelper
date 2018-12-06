namespace RankHelper
{
    partial class WebForm_bak
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebForm_bak));
            this.textBox_url = new System.Windows.Forms.TextBox();
            this.panel_webbrowser = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // textBox_url
            // 
            this.textBox_url.Location = new System.Drawing.Point(3, 3);
            this.textBox_url.Name = "textBox_url";
            this.textBox_url.Size = new System.Drawing.Size(1066, 25);
            this.textBox_url.TabIndex = 0;
            // 
            // panel_webbrowser
            // 
            this.panel_webbrowser.Location = new System.Drawing.Point(1, 32);
            this.panel_webbrowser.Name = "panel_webbrowser";
            this.panel_webbrowser.Size = new System.Drawing.Size(1068, 530);
            this.panel_webbrowser.TabIndex = 1;
            // 
            // WebForm_bak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(228)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(1073, 613);
            this.Controls.Add(this.textBox_url);
            this.Controls.Add(this.panel_webbrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WebForm_bak";
            this.Text = "WebForm_bak";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WebForm_bak_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_url;
        private System.Windows.Forms.Panel panel_webbrowser;
    }
}