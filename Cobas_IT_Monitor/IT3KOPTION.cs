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
    public partial class IT3KOPTION : Form
    {
        Tool_Class.IO_tool io = new Tool_Class.IO_tool();
        public IT3KOPTION()
        {
            InitializeComponent();
            textBox1.Text = io.readconfig("IT3K_OPTION", "OPTION_CHECK");
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            int out_time = 43200;
            bool result = io.isNumberic(textBox1.Text.ToString(), out out_time);
            if (!result)
                textBox1.Text = "只能输入数字，默认单位秒";
            else
            {
                io.writeconfig("IT3K_OPTION", "OPTION_CHECK", out_time.ToString());
                this.Dispose();
            }
        }
    }
}
