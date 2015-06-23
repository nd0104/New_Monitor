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
    public partial class softwareconfig : Form
    {
        public softwareconfig()
        {
            InitializeComponent();
        }

        private void softwareconfig_Load(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
            string warncentral = tool.readconfig("sign", "warncentral");
            string errorcentral = tool.readconfig("sign", "errorcentral");
            string warnarea = tool.readconfig("sign", "warnarea");
            string errorarea = tool.readconfig("sign", "errorarea");
            string warncustomer = tool.readconfig("sign", "warncustomer");
            string errorcustomer = tool.readconfig("sign", "errorcustomer");
            textBox3.Text =tool.readconfig("email", "localad");
            textBox4.Text =tool.readconfig("email", "localadpassword");
            if (warncentral == "true")
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
            if (errorcentral == "true")
            {
                checkBox6.Checked = true;
            }
            else
            {
                checkBox6.Checked = false;
            }
            if (warnarea == "true")
            {
                checkBox2.Checked = true;
            }
            else
            {
                checkBox2.Checked = false;
            }
            if (errorarea == "true")
            {
                checkBox5.Checked = true;
            }
            else
            {
                checkBox5.Checked = false;
            }
            if (warncustomer == "true")
            {
                checkBox3.Checked = true;
            }
            else
            {
                checkBox3.Checked = false;
            }
            if (errorcustomer == "true")
            {
                checkBox4.Checked = true;
            }
            else
            {
                checkBox4.Checked = false;
            }

        }
        int l = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (l == 0)
            {
                l++;
                this.timer1.Interval = 1000; //1秒1次
                Tool_Class.IO_tool tool = new Tool_Class.IO_tool();
                string centralad = tool.readconfig("email", "centralad");
                string centralps = tool.readconfig("email", "centralps");
                string centraltp = tool.readconfig("email", "centraltp");
                string eastad = tool.readconfig("email", "eastad");
                string eastps = tool.readconfig("email", "eastps");
                string easttp = tool.readconfig("email", "easttp");
                string westad = tool.readconfig("email", "westad");
                string westps = tool.readconfig("email", "westps");
                string westtp = tool.readconfig("email", "westtp");
                string northad = tool.readconfig("email", "northad");
                string northps = tool.readconfig("email", "northps");
                string northtp = tool.readconfig("email", "northtp");
                string southad = tool.readconfig("email", "southad");
                string southps = tool.readconfig("email", "southps");
                string southtp = tool.readconfig("email", "southtp");
                contactlist.Rows.Add("central",centralad, centralps, centraltp);
                contactlist.Rows.Add("东区",eastad, eastps, easttp);
                contactlist.Rows.Add("西区",westad, westps, westtp);
                contactlist.Rows.Add("北区",northad, northps, northtp);
                contactlist.Rows.Add("南区",southad, southps, southtp);


            }
        }

        Tool_Class.IO_tool tool = new Tool_Class.IO_tool();
        private void button1_Click(object sender, EventArgs e)
        {
            tool.writeconfig("email", "centralad", contactlist.Rows[0].Cells[1].Value.ToString());
            tool.writeconfig("email", "centralps", contactlist.Rows[0].Cells[2].Value.ToString());
            tool.writeconfig("email", "centraltp", contactlist.Rows[0].Cells[3].Value.ToString());
            tool.writeconfig("email", "eastad", contactlist.Rows[1].Cells[1].Value.ToString());
            tool.writeconfig("email", "eastps", contactlist.Rows[1].Cells[2].Value.ToString());
            tool.writeconfig("email", "easttp", contactlist.Rows[1].Cells[3].Value.ToString());
            tool.writeconfig("email", "westad", contactlist.Rows[2].Cells[1].Value.ToString());
            tool.writeconfig("email", "westps", contactlist.Rows[2].Cells[2].Value.ToString());
            tool.writeconfig("email", "westtp", contactlist.Rows[2].Cells[3].Value.ToString());
            tool.writeconfig("email", "northad", contactlist.Rows[3].Cells[1].Value.ToString());
            tool.writeconfig("email", "northps", contactlist.Rows[3].Cells[2].Value.ToString());
            tool.writeconfig("email", "northtp", contactlist.Rows[3].Cells[3].Value.ToString());
            tool.writeconfig("email", "southad", contactlist.Rows[4].Cells[1].Value.ToString());
            tool.writeconfig("email", "southps", contactlist.Rows[4].Cells[2].Value.ToString());
            tool.writeconfig("email", "southtp", contactlist.Rows[4].Cells[3].Value.ToString());
            tool.writeconfig("email", "localad", textBox3.Text.ToString());
            tool.writeconfig("email", "localadpassword", textBox4.Text.ToString());
            MessageBox.Show("修改成功");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                tool.writeconfig("sign", "warncentral","true");
            else
            {
                tool.writeconfig("sign", "warncentral", "false");
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
                tool.writeconfig("sign", "errorcentral", "true");
            else
            {
                tool.writeconfig("sign", "errorcentral", "false");
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                tool.writeconfig("sign", "warnarea", "true");
            else
            {
                tool.writeconfig("sign", "warnarea", "false");
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
                tool.writeconfig("sign", "errorarea", "true");
            else
            {
                tool.writeconfig("sign", "errorarea", "false");
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
                tool.writeconfig("sign", "warncustomer", "true");
            else
            {
                tool.writeconfig("sign", "warncustomer", "false");
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
                tool.writeconfig("sign", "errorcustomer", "true");
            else
            {
                tool.writeconfig("sign", "errorcustomer", "false");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*string sql3 = "update Status_Histroy set sign = '1'";
            string sql4 = "update Status_Now set flag = 'N',para_value = '正常' where para_name = 'disk_size'";

            Tool_Class.IO_tool tool = new Tool_Class.IO_tool();
            tool.AccessDbclass(sql3);
            tool.AccessDbclass(sql4);
            MessageBox.Show("修改成功");*/
            //for 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == textBox2.Text)
            {
                tool.writeconfig("lg", "pw", textBox1.Text);
                MessageBox.Show("密码修改成功");
            }
            else
            {
                MessageBox.Show("密码不一致，请重新填写");
            }
        }
    }
}
