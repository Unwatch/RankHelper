namespace RankHelper
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControlTop = new System.Windows.Forms.TabControl();
            this.tabPage_Statistics = new System.Windows.Forms.TabPage();
            this.tabPage_Setting = new System.Windows.Forms.TabPage();
            this.tabPage_Adsl = new System.Windows.Forms.TabPage();
            this.statusStrip_main = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_IP = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel = new System.Windows.Forms.Panel();
            this.tabControlTop.SuspendLayout();
            this.statusStrip_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlTop
            // 
            this.tabControlTop.Controls.Add(this.tabPage_Statistics);
            this.tabControlTop.Controls.Add(this.tabPage_Setting);
            this.tabControlTop.Controls.Add(this.tabPage_Adsl);
            this.tabControlTop.Location = new System.Drawing.Point(-1, 3);
            this.tabControlTop.Margin = new System.Windows.Forms.Padding(2);
            this.tabControlTop.Name = "tabControlTop";
            this.tabControlTop.SelectedIndex = 0;
            this.tabControlTop.Size = new System.Drawing.Size(811, 481);
            this.tabControlTop.TabIndex = 1;
            this.tabControlTop.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControlTop_Selected);
            // 
            // tabPage_Statistics
            // 
            this.tabPage_Statistics.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Statistics.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage_Statistics.Name = "tabPage_Statistics";
            this.tabPage_Statistics.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage_Statistics.Size = new System.Drawing.Size(803, 455);
            this.tabPage_Statistics.TabIndex = 1;
            this.tabPage_Statistics.Text = "统计";
            this.tabPage_Statistics.UseVisualStyleBackColor = true;
            // 
            // tabPage_Setting
            // 
            this.tabPage_Setting.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Setting.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage_Setting.Name = "tabPage_Setting";
            this.tabPage_Setting.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage_Setting.Size = new System.Drawing.Size(803, 455);
            this.tabPage_Setting.TabIndex = 2;
            this.tabPage_Setting.Text = "任务设置";
            this.tabPage_Setting.UseVisualStyleBackColor = true;
            // 
            // tabPage_Adsl
            // 
            this.tabPage_Adsl.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Adsl.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage_Adsl.Name = "tabPage_Adsl";
            this.tabPage_Adsl.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage_Adsl.Size = new System.Drawing.Size(803, 455);
            this.tabPage_Adsl.TabIndex = 3;
            this.tabPage_Adsl.Text = "ADSL设置";
            this.tabPage_Adsl.UseVisualStyleBackColor = true;
            // 
            // statusStrip_main
            // 
            this.statusStrip_main.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_Status,
            this.toolStripStatusLabel_IP});
            this.statusStrip_main.Location = new System.Drawing.Point(0, 486);
            this.statusStrip_main.Name = "statusStrip_main";
            this.statusStrip_main.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip_main.Size = new System.Drawing.Size(810, 24);
            this.statusStrip_main.TabIndex = 2;
            // 
            // toolStripStatusLabel_Status
            // 
            this.toolStripStatusLabel_Status.AutoSize = false;
            this.toolStripStatusLabel_Status.BackColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel_Status.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel_Status.Name = "toolStripStatusLabel_Status";
            this.toolStripStatusLabel_Status.Size = new System.Drawing.Size(700, 19);
            this.toolStripStatusLabel_Status.Text = "未开始挂机";
            // 
            // toolStripStatusLabel_IP
            // 
            this.toolStripStatusLabel_IP.AutoSize = false;
            this.toolStripStatusLabel_IP.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripStatusLabel_IP.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel_IP.Name = "toolStripStatusLabel_IP";
            this.toolStripStatusLabel_IP.Size = new System.Drawing.Size(360, 19);
            this.toolStripStatusLabel_IP.Text = "正在获取IP...";
            // 
            // panel
            // 
            this.panel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel.Location = new System.Drawing.Point(0, 3);
            this.panel.Margin = new System.Windows.Forms.Padding(2);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(807, 455);
            this.panel.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(228)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(810, 510);
            this.Controls.Add(this.statusStrip_main);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.tabControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Main";
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.tabControlTop.ResumeLayout(false);
            this.statusStrip_main.ResumeLayout(false);
            this.statusStrip_main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }




        #endregion
        private System.Windows.Forms.TabControl tabControlTop;
        private System.Windows.Forms.TabPage tabPage_Statistics;
        private System.Windows.Forms.TabPage tabPage_Setting;
        private System.Windows.Forms.TabPage tabPage_Adsl;
        private System.Windows.Forms.StatusStrip statusStrip_main;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Status;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_IP;
        private System.Windows.Forms.Panel panel;
    }
}