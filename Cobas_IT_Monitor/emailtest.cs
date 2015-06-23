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
    public partial class emailtest : Form
    {
        public emailtest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mailBody = "测试邮件发送成功！";
            Class.SendMail sendMail = new Class.SendMail();
            sendMail.mailPwd = "zhouweicheng";
            sendMail.sendFrom = "chengok007@163.com";
            sendMail.sendTo = textBox1.Text;
            sendMail.subject = "测试邮件发送成功！";
            sendMail.mailBody = mailBody;
            sendMail.send();
            MessageBox.Show("邮件发送成功");
        }
    }
}
