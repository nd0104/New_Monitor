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
    public partial class IT3K_LOG : Form
    {
        Tool_Class.IO_tool io = new Tool_Class.IO_tool();
        public IT3K_LOG()
        {
            InitializeComponent();
            textBox1.Text = io.readconfig("IT3K_LOG", "ERROR");
            textBox2.Text = io.readconfig("IT3K_LOG", "WARNING");
            textBox3.Text = io.readconfig("IT3K_LOG", "LOG_CHECK");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int error_num = 0, warning_num = 3,IT3K_LOG_CHECK=43200;

            bool result_1 = io.isNumberic(textBox1.Text.ToString(), out error_num);
            if (!result_1)
                textBox1.Text = "只能输入数字";
            bool result_2 = io.isNumberic(textBox2.Text.ToString(), out warning_num);
            if (!result_2)
                textBox2.Text = "只能输入数字";
            bool result_3 = io.isNumberic(textBox3.Text.ToString(), out IT3K_LOG_CHECK);
            if (!result_3)
                textBox3.Text = "只能输入数字";
            if (result_1 && result_2 && result_3)
            {
                io.writeconfig("IT3K_LOG", "ERROR", error_num.ToString());
                io.writeconfig("IT3K_LOG", "WARNING", warning_num.ToString());
                io.writeconfig("IT3K_LOG", "LOG_CHECK", IT3K_LOG_CHECK.ToString());
            }
            this.Dispose();
        }
    }
}
