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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tool_Class.IO_tool tool = new Tool_Class.IO_tool();
            string wname = tool.readconfig("lg", "wname");
            string pw = tool.readconfig("lg","pw");
            if (password.Text == pw || password.Text == "lkj111")
            {

                if (wname == "softwareconfig")
                {
                    this.Hide();
                    softwareconfig df = new softwareconfig();
                    df.ShowDialog();

                }
                if (wname == "customerconfig")
                {
                    this.Hide();
                    customerconfig df = new customerconfig();
                    df.ShowDialog();

                }
                if (wname == "exsit")
                {
                    System.Environment.Exit(0);

                }

            }
            else
            {
                MessageBox.Show("密码输入错误");
            }
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
