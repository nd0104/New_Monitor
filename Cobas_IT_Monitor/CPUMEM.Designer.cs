namespace CobasITMonitor
{
    partial class CPUMEM
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
            this.label1 = new System.Windows.Forms.Label();
            this.time = new System.Windows.Forms.TextBox();
            this.cpuwarntime = new System.Windows.Forms.TextBox();
            this.cpuerrortime = new System.Windows.Forms.TextBox();
            this.memwarntime = new System.Windows.Forms.TextBox();
            this.memerrortime = new System.Windows.Forms.TextBox();
            this.memerrorvalue = new System.Windows.Forms.TextBox();
            this.memwarnvalue = new System.Windows.Forms.TextBox();
            this.cpuerrorvalue = new System.Windows.Forms.TextBox();
            this.cpuwarnvalue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "每隔";
            // 
            // time
            // 
            this.time.Location = new System.Drawing.Point(66, 49);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(100, 20);
            this.time.TabIndex = 1;
            // 
            // cpuwarntime
            // 
            this.cpuwarntime.Location = new System.Drawing.Point(66, 90);
            this.cpuwarntime.Name = "cpuwarntime";
            this.cpuwarntime.Size = new System.Drawing.Size(100, 20);
            this.cpuwarntime.TabIndex = 2;
            // 
            // cpuerrortime
            // 
            this.cpuerrortime.Location = new System.Drawing.Point(66, 128);
            this.cpuerrortime.Name = "cpuerrortime";
            this.cpuerrortime.Size = new System.Drawing.Size(100, 20);
            this.cpuerrortime.TabIndex = 3;
            // 
            // memwarntime
            // 
            this.memwarntime.Location = new System.Drawing.Point(66, 165);
            this.memwarntime.Name = "memwarntime";
            this.memwarntime.Size = new System.Drawing.Size(100, 20);
            this.memwarntime.TabIndex = 4;
            // 
            // memerrortime
            // 
            this.memerrortime.Location = new System.Drawing.Point(66, 202);
            this.memerrortime.Name = "memerrortime";
            this.memerrortime.Size = new System.Drawing.Size(100, 20);
            this.memerrortime.TabIndex = 5;
            // 
            // memerrorvalue
            // 
            this.memerrorvalue.Location = new System.Drawing.Point(308, 200);
            this.memerrorvalue.Name = "memerrorvalue";
            this.memerrorvalue.Size = new System.Drawing.Size(100, 20);
            this.memerrorvalue.TabIndex = 9;
            // 
            // memwarnvalue
            // 
            this.memwarnvalue.Location = new System.Drawing.Point(308, 163);
            this.memwarnvalue.Name = "memwarnvalue";
            this.memwarnvalue.Size = new System.Drawing.Size(100, 20);
            this.memwarnvalue.TabIndex = 8;
            // 
            // cpuerrorvalue
            // 
            this.cpuerrorvalue.Location = new System.Drawing.Point(308, 126);
            this.cpuerrorvalue.Name = "cpuerrorvalue";
            this.cpuerrorvalue.Size = new System.Drawing.Size(100, 20);
            this.cpuerrorvalue.TabIndex = 7;
            // 
            // cpuwarnvalue
            // 
            this.cpuwarnvalue.Location = new System.Drawing.Point(308, 88);
            this.cpuwarnvalue.Name = "cpuwarnvalue";
            this.cpuwarnvalue.Size = new System.Drawing.Size(100, 20);
            this.cpuwarnvalue.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "分钟监测一次";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "连续";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "连续";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "连续";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 206);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "连续";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(172, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "次监测CPU百分率超过";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(172, 131);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(125, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "次监测CPU百分率超过";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(172, 168);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(127, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "次监测内存百分率超过";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(172, 205);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(127, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "次监测内存百分率超过";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(412, 91);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(108, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "% ，触发\"警告\"警报";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(412, 129);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(108, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "% ，触发\"错误\"警报";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(412, 170);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(111, 13);
            this.label13.TabIndex = 21;
            this.label13.Text = "% ， 触发\"警告\"警报";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(412, 207);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(111, 13);
            this.label14.TabIndex = 22;
            this.label14.Text = "%  ，触发\"错误\"警报";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(109, 244);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 23;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(269, 244);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 24;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CPUMEM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 293);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.memerrorvalue);
            this.Controls.Add(this.memwarnvalue);
            this.Controls.Add(this.cpuerrorvalue);
            this.Controls.Add(this.cpuwarnvalue);
            this.Controls.Add(this.memerrortime);
            this.Controls.Add(this.memwarntime);
            this.Controls.Add(this.cpuerrortime);
            this.Controls.Add(this.cpuwarntime);
            this.Controls.Add(this.time);
            this.Controls.Add(this.label1);
            this.Name = "CPUMEM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "监控参数设置：cpu和内存占有率";
            this.Load += new System.EventHandler(this.CPUMEM_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox time;
        private System.Windows.Forms.TextBox cpuwarntime;
        private System.Windows.Forms.TextBox cpuerrortime;
        private System.Windows.Forms.TextBox memwarntime;
        private System.Windows.Forms.TextBox memerrortime;
        private System.Windows.Forms.TextBox memerrorvalue;
        private System.Windows.Forms.TextBox memwarnvalue;
        private System.Windows.Forms.TextBox cpuerrorvalue;
        private System.Windows.Forms.TextBox cpuwarnvalue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer1;
    }
}