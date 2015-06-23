using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CobasITMonitor
{
    public partial class CPUMEM : Form
    {
        public CPUMEM()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tool_Class.IO_tool tool = new Tool_Class.IO_tool();
            tool.writeconfig("rf", "cpumemrefresh", time.Text);
            tool.writeconfig("jb", "cpuwarntime", cpuwarntime.Text);
            tool.writeconfig("jb", "cpuwarnvalue", cpuwarnvalue.Text);
            tool.writeconfig("bj", "cpuerrortime", cpuerrortime.Text);
            tool.writeconfig("bj", "cpuerrorvalue", cpuerrorvalue.Text);
            tool.writeconfig("jb", "memwarntime", memwarntime.Text);
            tool.writeconfig("jb", "memwarnvalue", memwarnvalue.Text);
            tool.writeconfig("bj", "memerrortime", memerrortime.Text);
            tool.writeconfig("bj", "memerrorvalue", memerrorvalue.Text);
            MessageBox.Show("修改成功");

        }

        private void CPUMEM_Load(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
        }

        int l = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (l == 0)
            {
                l++;
                Tool_Class.IO_tool tool = new Tool_Class.IO_tool();
                time.Text = tool.readconfig("rf", "cpumemrefresh");
                cpuwarntime.Text = tool.readconfig("jb", "cpuwarntime");
                cpuwarnvalue.Text = tool.readconfig("jb", "cpuwarnvalue");
                cpuerrortime.Text = tool.readconfig("bj", "cpuerrortime");
                cpuerrorvalue.Text = tool.readconfig("bj", "cpuerrorvalue");
                memwarntime.Text = tool.readconfig("jb", "memwarntime");
                memwarnvalue.Text = tool.readconfig("jb", "memwarnvalue");
                memerrortime.Text = tool.readconfig("bj", "memerrortime");
                memerrorvalue.Text = tool.readconfig("bj", "memerrorvalue");
                this.timer1.Interval = 1000; //1秒1次
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
