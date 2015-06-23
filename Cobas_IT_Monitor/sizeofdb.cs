using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tool_Class;
namespace CobasITMonitor
{
    public partial class sizeofdb : Form
    {
        Tool_Class.IO_tool io = new Tool_Class.IO_tool();
        public sizeofdb()
        {
            InitializeComponent();
            textBox1.Text = io.readconfig("DATABASE", "DB_SIZE");
            textBox2.Text = io.readconfig("DATABASE", "DB_CHECK");

        }
        private void button1_Click(object sender, EventArgs e)
        {
            int out_size = Convert.ToInt32(textBox1.Text);
            int out_time = Convert.ToInt32(textBox2.Text);
            bool result = io.isNumberic(textBox1.Text.ToString(), out out_size);
            if (!result)
                textBox1.Text = "只能输入数字，默认单位G";
            else
            {
                io.writeconfig("DATABASE", "DB_SIZE", textBox1.Text);
            }
            result = io.isNumberic(textBox1.Text.ToString(), out out_time);
            if (!result)
                textBox2.Text = "只能输入数字，默认单位秒";
            else
            {
                io.writeconfig("DATABASE", "DB_CHECK", textBox2.Text);
            }
            this.Dispose();
        }
    }
}
