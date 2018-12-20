namespace RankHelper
{
    partial class StatisticsForm
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
            this.listView_statistics = new System.Windows.Forms.ListView();
            this.checkBox_work = new System.Windows.Forms.CheckBox();
            this.timer_checkbox = new System.Windows.Forms.Timer(this.components);
            this.button_wait = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView_statistics
            // 
            this.listView_statistics.Location = new System.Drawing.Point(2, 2);
            this.listView_statistics.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listView_statistics.Name = "listView_statistics";
            this.listView_statistics.Size = new System.Drawing.Size(800, 432);
            this.listView_statistics.TabIndex = 1;
            this.listView_statistics.UseCompatibleStateImageBehavior = false;
            this.listView_statistics.DoubleClick += new System.EventHandler(this.listView_statistics_DoubleClick);
            // 
            // checkBox_work
            // 
            this.checkBox_work.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_work.Location = new System.Drawing.Point(382, 436);
            this.checkBox_work.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox_work.Name = "checkBox_work";
            this.checkBox_work.Size = new System.Drawing.Size(64, 29);
            this.checkBox_work.TabIndex = 3;
            this.checkBox_work.Text = "开始挂机";
            this.checkBox_work.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_work.UseVisualStyleBackColor = true;
            this.checkBox_work.CheckedChanged += new System.EventHandler(this.checkBox_work_CheckedChanged);
            this.checkBox_work.MouseDown += new System.Windows.Forms.MouseEventHandler(this.checkBox_work_MouseDown);
            // 
            // button_wait
            // 
            this.button_wait.Enabled = false;
            this.button_wait.Location = new System.Drawing.Point(382, 436);
            this.button_wait.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button_wait.Name = "button_wait";
            this.button_wait.Size = new System.Drawing.Size(64, 29);
            this.button_wait.TabIndex = 6;
            this.button_wait.Text = "等待...";
            this.button_wait.UseVisualStyleBackColor = true;
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(228)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(805, 471);
            this.Controls.Add(this.checkBox_work);
            this.Controls.Add(this.listView_statistics);
            this.Controls.Add(this.button_wait);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "StatisticsForm";
            this.Text = "StatisticsForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView_statistics;
        private System.Windows.Forms.CheckBox checkBox_work;
        private System.Windows.Forms.Timer timer_checkbox;
        private System.Windows.Forms.Button button_wait;
    }
}