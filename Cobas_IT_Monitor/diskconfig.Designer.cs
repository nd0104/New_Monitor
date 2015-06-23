namespace CobasITMonitor
{
    partial class diskconfig
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
            this.cwran = new System.Windows.Forms.TextBox();
            this.cerror = new System.Windows.Forms.TextBox();
            this.derror = new System.Windows.Forms.TextBox();
            this.dwran = new System.Windows.Forms.TextBox();
            this.eerror = new System.Windows.Forms.TextBox();
            this.ewran = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ferror = new System.Windows.Forms.TextBox();
            this.fwran = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.time = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cwran
            // 
            this.cwran.Location = new System.Drawing.Point(168, 129);
            this.cwran.Name = "cwran";
            this.cwran.Size = new System.Drawing.Size(100, 20);
            this.cwran.TabIndex = 0;
            // 
            // cerror
            // 
            this.cerror.Location = new System.Drawing.Point(392, 129);
            this.cerror.Name = "cerror";
            this.cerror.Size = new System.Drawing.Size(100, 20);
            this.cerror.TabIndex = 1;
            // 
            // derror
            // 
            this.derror.Location = new System.Drawing.Point(392, 161);
            this.derror.Name = "derror";
            this.derror.Size = new System.Drawing.Size(100, 20);
            this.derror.TabIndex = 3;
            // 
            // dwran
            // 
            this.dwran.Location = new System.Drawing.Point(168, 161);
            this.dwran.Name = "dwran";
            this.dwran.Size = new System.Drawing.Size(100, 20);
            this.dwran.TabIndex = 2;
            // 
            // eerror
            // 
            this.eerror.Location = new System.Drawing.Point(392, 192);
            this.eerror.Name = "eerror";
            this.eerror.Size = new System.Drawing.Size(100, 20);
            this.eerror.TabIndex = 5;
            // 
            // ewran
            // 
            this.ewran.Location = new System.Drawing.Point(168, 192);
            this.ewran.Name = "ewran";
            this.ewran.Size = new System.Drawing.Size(100, 20);
            this.ewran.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "C盘";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "D盘";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(78, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "E盘";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(78, 227);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "F盘";
            // 
            // ferror
            // 
            this.ferror.Location = new System.Drawing.Point(392, 223);
            this.ferror.Name = "ferror";
            this.ferror.Size = new System.Drawing.Size(100, 20);
            this.ferror.TabIndex = 10;
            // 
            // fwran
            // 
            this.fwran.Location = new System.Drawing.Point(168, 223);
            this.fwran.Name = "fwran";
            this.fwran.Size = new System.Drawing.Size(100, 20);
            this.fwran.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(137, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(183, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "可用空间低于下列值时发出“警告”";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(347, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(183, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "可用空间低于下列值时发出“错误”";
            // 
            // time
            // 
            this.time.Location = new System.Drawing.Point(168, 47);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(100, 20);
            this.time.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(123, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "每隔";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(274, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "分钟检查一次";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(231, 267);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(347, 267);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(379, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(137, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "（磁盘可用空间，单位：G）";
            // 
            // diskconfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 318);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.time);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ferror);
            this.Controls.Add(this.fwran);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.eerror);
            this.Controls.Add(this.ewran);
            this.Controls.Add(this.derror);
            this.Controls.Add(this.dwran);
            this.Controls.Add(this.cerror);
            this.Controls.Add(this.cwran);
            this.Name = "diskconfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "监控参数设置：磁盘空间";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.diskconfig_FormClosing);
            this.Load += new System.EventHandler(this.diskconfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox cwran;
        private System.Windows.Forms.TextBox cerror;
        private System.Windows.Forms.TextBox derror;
        private System.Windows.Forms.TextBox dwran;
        private System.Windows.Forms.TextBox eerror;
        private System.Windows.Forms.TextBox ewran;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ferror;
        private System.Windows.Forms.TextBox fwran;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox time;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label9;

    }
}