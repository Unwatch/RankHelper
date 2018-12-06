namespace RankHelper
{
    partial class AdslForm
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
            this.checkBox_AutoStart = new System.Windows.Forms.CheckBox();
            this.textBox_Pwd = new System.Windows.Forms.TextBox();
            this.textBox_Username = new System.Windows.Forms.TextBox();
            this.comboBox_Connect = new System.Windows.Forms.ComboBox();
            this.frameLabel = new System.Windows.Forms.Label();
            this.label_Pwd = new System.Windows.Forms.Label();
            this.label_Username = new System.Windows.Forms.Label();
            this.label_Connect = new System.Windows.Forms.Label();
            this.frame = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_Count = new System.Windows.Forms.TextBox();
            this.comboBox_Invert = new System.Windows.Forms.ComboBox();
            this.label_Invert = new System.Windows.Forms.Label();
            this.label_Count = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkBox_AutoStart
            // 
            this.checkBox_AutoStart.AutoSize = true;
            this.checkBox_AutoStart.Location = new System.Drawing.Point(120, 41);
            this.checkBox_AutoStart.Name = "checkBox_AutoStart";
            this.checkBox_AutoStart.Size = new System.Drawing.Size(149, 19);
            this.checkBox_AutoStart.TabIndex = 57;
            this.checkBox_AutoStart.Text = "开机自动启动程序";
            this.checkBox_AutoStart.UseVisualStyleBackColor = true;
            this.checkBox_AutoStart.CheckedChanged += new System.EventHandler(this.checkBox_AutoStart_CheckedChanged);
            // 
            // textBox_Pwd
            // 
            this.textBox_Pwd.Location = new System.Drawing.Point(202, 210);
            this.textBox_Pwd.Name = "textBox_Pwd";
            this.textBox_Pwd.Size = new System.Drawing.Size(176, 25);
            this.textBox_Pwd.TabIndex = 56;
            this.textBox_Pwd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Username_KeyDown);
            this.textBox_Pwd.Leave += new System.EventHandler(this.textBox_Username_Leave);
            // 
            // textBox_Username
            // 
            this.textBox_Username.Location = new System.Drawing.Point(202, 172);
            this.textBox_Username.Name = "textBox_Username";
            this.textBox_Username.Size = new System.Drawing.Size(176, 25);
            this.textBox_Username.TabIndex = 55;
            this.textBox_Username.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Username_KeyDown);
            this.textBox_Username.Leave += new System.EventHandler(this.textBox_Username_Leave);
            // 
            // comboBox_Connect
            // 
            this.comboBox_Connect.FormattingEnabled = true;
            this.comboBox_Connect.Location = new System.Drawing.Point(202, 249);
            this.comboBox_Connect.Name = "comboBox_Connect";
            this.comboBox_Connect.Size = new System.Drawing.Size(176, 23);
            this.comboBox_Connect.TabIndex = 54;
            this.comboBox_Connect.SelectedIndexChanged += new System.EventHandler(this.textBox_Username_Leave);
            // 
            // frameLabel
            // 
            this.frameLabel.AutoSize = true;
            this.frameLabel.Location = new System.Drawing.Point(45, 5);
            this.frameLabel.Name = "frameLabel";
            this.frameLabel.Size = new System.Drawing.Size(67, 15);
            this.frameLabel.TabIndex = 52;
            this.frameLabel.Text = "基本设置";
            // 
            // label_Pwd
            // 
            this.label_Pwd.Location = new System.Drawing.Point(57, 213);
            this.label_Pwd.Name = "label_Pwd";
            this.label_Pwd.Size = new System.Drawing.Size(135, 15);
            this.label_Pwd.TabIndex = 51;
            this.label_Pwd.Text = "宽带密码:";
            this.label_Pwd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_Username
            // 
            this.label_Username.Location = new System.Drawing.Point(57, 178);
            this.label_Username.Name = "label_Username";
            this.label_Username.Size = new System.Drawing.Size(135, 15);
            this.label_Username.TabIndex = 50;
            this.label_Username.Text = "宽带账号:";
            this.label_Username.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_Connect
            // 
            this.label_Connect.Location = new System.Drawing.Point(57, 252);
            this.label_Connect.Name = "label_Connect";
            this.label_Connect.Size = new System.Drawing.Size(135, 15);
            this.label_Connect.TabIndex = 49;
            this.label_Connect.Text = "连接列表:";
            this.label_Connect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frame
            // 
            this.frame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.frame.Location = new System.Drawing.Point(12, 13);
            this.frame.Name = "frame";
            this.frame.Size = new System.Drawing.Size(1049, 112);
            this.frame.TabIndex = 53;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(228)))), ((int)(((byte)(246)))));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(136, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(298, 15);
            this.label1.TabIndex = 58;
            this.label1.Text = "(如安全软件提示，请选择允许)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 15);
            this.label2.TabIndex = 59;
            this.label2.Text = "自动更换IP设置";
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(12, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1049, 434);
            this.label3.TabIndex = 60;
            // 
            // textBox_Count
            // 
            this.textBox_Count.Location = new System.Drawing.Point(202, 330);
            this.textBox_Count.Name = "textBox_Count";
            this.textBox_Count.Size = new System.Drawing.Size(176, 25);
            this.textBox_Count.TabIndex = 64;
            this.textBox_Count.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Username_KeyDown);
            this.textBox_Count.Leave += new System.EventHandler(this.textBox_Count_Leave);
            // 
            // comboBox_Invert
            // 
            this.comboBox_Invert.FormattingEnabled = true;
            this.comboBox_Invert.Location = new System.Drawing.Point(202, 290);
            this.comboBox_Invert.Name = "comboBox_Invert";
            this.comboBox_Invert.Size = new System.Drawing.Size(176, 23);
            this.comboBox_Invert.TabIndex = 63;
            this.comboBox_Invert.SelectedIndexChanged += new System.EventHandler(this.textBox_Username_Leave);
            // 
            // label_Invert
            // 
            this.label_Invert.Location = new System.Drawing.Point(22, 294);
            this.label_Invert.Name = "label_Invert";
            this.label_Invert.Size = new System.Drawing.Size(170, 15);
            this.label_Invert.TabIndex = 62;
            this.label_Invert.Text = "更换IP失败重试间隔:";
            this.label_Invert.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_Count
            // 
            this.label_Count.Location = new System.Drawing.Point(57, 333);
            this.label_Count.Name = "label_Count";
            this.label_Count.Size = new System.Drawing.Size(135, 15);
            this.label_Count.TabIndex = 61;
            this.label_Count.Text = "重试次数:";
            this.label_Count.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // AdslForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(228)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(1073, 589);
            this.Controls.Add(this.textBox_Count);
            this.Controls.Add(this.comboBox_Invert);
            this.Controls.Add(this.label_Invert);
            this.Controls.Add(this.label_Count);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox_AutoStart);
            this.Controls.Add(this.textBox_Pwd);
            this.Controls.Add(this.textBox_Username);
            this.Controls.Add(this.comboBox_Connect);
            this.Controls.Add(this.frameLabel);
            this.Controls.Add(this.label_Pwd);
            this.Controls.Add(this.label_Username);
            this.Controls.Add(this.label_Connect);
            this.Controls.Add(this.frame);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdslForm";
            this.Text = "AdslForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_AutoStart;
        private System.Windows.Forms.TextBox textBox_Pwd;
        private System.Windows.Forms.TextBox textBox_Username;
        private System.Windows.Forms.ComboBox comboBox_Connect;
        private System.Windows.Forms.Label frameLabel;
        private System.Windows.Forms.Label label_Pwd;
        private System.Windows.Forms.Label label_Username;
        private System.Windows.Forms.Label label_Connect;
        private System.Windows.Forms.Label frame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_Count;
        private System.Windows.Forms.ComboBox comboBox_Invert;
        private System.Windows.Forms.Label label_Invert;
        private System.Windows.Forms.Label label_Count;
    }
}