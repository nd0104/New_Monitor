using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;
using System.IO;


namespace CobasITMonitor
{
    public partial class netconfig : Form
    {
        public netconfig()
        {
            InitializeComponent();
        }

        private void ipconfig_Load(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
            login lg = new login();
            lg.Hide();
           
        }
        public void writeip(string node,string ipp)
        {
            //Tool_Class.DESFileClass.DecryptFile("config.txt", "345.txt", "123");
            FileStream fs = new FileStream(@"345.txt", FileMode.Open);
            Tool_Class.IO_tool dd = new Tool_Class.IO_tool();
            string w = dd.Readfile(fs);
            string[] s = Regex.Split(w, node, RegexOptions.IgnoreCase);
            string ee = s[0];
            string ff = s[2];
            string gg = ee + "ip" + ipp + "ip" + ff;
            string str5 = System.Windows.Forms.Application.StartupPath;
            string sourceDir = @str5;
            string[] txtList = Directory.GetFiles(sourceDir, "345.txt");
            foreach (string f in txtList)
            {
                File.Delete(f);
            }
            dd.Write2file(@"345.txt", gg);
            /*string[] configList = Directory.GetFiles(sourceDir, "config.txt");
            foreach (string f in configList)
            {
                File.Delete(f);
            }
            //Tool_Class.DESFileClass.EncryptFile("345.txt", "config.txt", "123");
            string[] txtList2 = Directory.GetFiles(sourceDir, "345.txt");
            foreach (string f in txtList2)
            {
                File.Delete(f);
            }*/

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tool_Class.IO_tool tool = new Tool_Class.IO_tool();
            string time = textBox1.Text;
            string jb = textBox3.Text;
            string bj = textBox2.Text;
            
            int t = int.Parse(iplist.Rows.Count.ToString());
            textBox4.Text = t.ToString();
            string ip = "";
            for (int i = 0; i < t - 1; i++)
            {
                
                ip += iplist.Rows[i].Cells[0].Value.ToString() + "=" + iplist.Rows[i].Cells[1].Value.ToString() + "=" + iplist.Rows[i].Cells[2].Value.ToString() + "=" + iplist.Rows[i].Cells[3].Value.ToString() + ";\r\n";
                writeip("ip",ip);
 
            }
            monitor mm = new monitor();
            //mm.writeconfig("rff", "ppt", "4");
            tool.writeconfig("rf", "netrefresh", time);
            tool.writeconfig("jb", "netwarn", jb);
            tool.writeconfig("bj", "neterror", bj);
            MessageBox.Show("修改成功");
            //textBox4.Text = iplist.Rows[0].Cells[0].Value.ToString() + "=" + iplist.Rows[0].Cells[1].Value.ToString() + "=" + iplist.Rows[0].Cells[2].Value.ToString() + "=" + iplist.Rows[0].Cells[3].Value.ToString() + ";";
            
        }
        int l = 0;
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (l == 0)
            {
                l++;
                Tool_Class.IO_tool tool = new Tool_Class.IO_tool();
                textBox1.Text = tool.readconfig("rf", "netrefresh");
                textBox3.Text = tool.readconfig("jb", "netwarn");
                textBox2.Text = tool.readconfig("bj", "neterror");
                this.timer1.Interval = 1000; //1秒1次
                
                List<string> ipList = new List<string>();
                DataTable ddtt = new DataTable();
                ddtt.Columns.Add("111");
                
                string[] dt = tool.readconfig("ip");
                
                foreach (string aa in dt)
                {
                    if (aa != "")
                    {
                        ipList.Add(aa);
                    }
                }
                for (int i = 1; i < ipList.Count; i++)
                {
                    string[] dd = Regex.Split(ipList[i], "#", RegexOptions.IgnoreCase);
                    textBox4.Text = dd[0];
                    //ddtt.Rows.Add(dd[0],dd[1],dd[2],dd[3]);
                    iplist.Rows.Add(dd[0], dd[1], dd[2], dd[3]);
                   

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
