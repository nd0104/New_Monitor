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
    public partial class diskconfig : Form
    {
        public diskconfig()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void diskconfig_Load(object sender, EventArgs e)
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
                time.Text = tool.readconfig("rf", "diskrefresh");
                cwran.Text = tool.readconfig("jb", "Cwarn");
                dwran.Text = tool.readconfig("jb", "Dwarn");
                ewran.Text = tool.readconfig("jb", "Ewarn");
                fwran.Text = tool.readconfig("jb", "Fwarn");
                cerror.Text = tool.readconfig("bj", "Cerror");
                derror.Text = tool.readconfig("bj", "Derror");
                eerror.Text = tool.readconfig("bj", "Eerror");
                ferror.Text = tool.readconfig("bj", "Ferror");
                this.timer1.Interval = 1000; //1秒1次
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tool_Class.IO_tool tool = new Tool_Class.IO_tool();
            tool.writeconfig("rf", "diskrefresh", time.Text);
            tool.writeconfig("jb", "Cwarn", cwran.Text);
            tool.writeconfig("jb", "Dwarn", dwran.Text);
            tool.writeconfig("jb", "Ewarn", ewran.Text);
            tool.writeconfig("jb", "Fwarn", fwran.Text);
            tool.writeconfig("bj", "Cerror", cerror.Text);
            tool.writeconfig("bj", "Derror", derror.Text);
            tool.writeconfig("bj", "Eerror", eerror.Text);
            tool.writeconfig("bj", "Ferror", ferror.Text);
            MessageBox.Show("修改成功");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void diskconfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
