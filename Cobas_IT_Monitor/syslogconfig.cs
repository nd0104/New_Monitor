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
    public partial class syslogconfig : Form
    {
        public syslogconfig()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        Tool_Class.IO_tool tool = new Tool_Class.IO_tool();
        private void syslogconfig_Load(object sender, EventArgs e)
        {
            string syslogrefresh = tool.readconfig("rf", "syslogrefresh");
            textBox1.Text = syslogrefresh;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tool.writeconfig("rf", "syslogrefresh", textBox1.Text);
            MessageBox.Show("修改成功");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
