namespace RankHelper
{
    partial class WebForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebForm));
            this.textBox_url = new System.Windows.Forms.TextBox();
            this.webBrowser_new = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // textBox_url
            // 
            this.textBox_url.Location = new System.Drawing.Point(3, 3);
            this.textBox_url.Name = "textBox_url";
            this.textBox_url.Size = new System.Drawing.Size(1066, 25);
            this.textBox_url.TabIndex = 0;
            // 
            // webBrowser_new
            // 
            this.webBrowser_new.Location = new System.Drawing.Point(3, 32);
            this.webBrowser_new.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser_new.Name = "webBrowser_new";
            this.webBrowser_new.Size = new System.Drawing.Size(1066, 548);
            this.webBrowser_new.TabIndex = 1;
            // 
            // WebForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(228)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(1073, 613);
            this.Controls.Add(this.webBrowser_new);
            this.Controls.Add(this.textBox_url);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WebForm";
            this.Text = "WebForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WebForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBox_url;
        public System.Windows.Forms.WebBrowser webBrowser_new;
    }
}