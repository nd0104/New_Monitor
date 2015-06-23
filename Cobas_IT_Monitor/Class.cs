using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.IO;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace CobasITMonitor
{
    class Class
    {
        public class AccessDbClass1
        {


            public OleDbConnection Conn;
            public string ConnString;




            public string AccessDbClass2(string Dbpath)
            {
                ConnString = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=";
                ConnString += Dbpath;
                Conn = new OleDbConnection(ConnString);
                Conn.Open();
                string b = "true";
                return b;
            }


            public OleDbConnection DbConn()
            {
                Conn.Open();
                return Conn;
            }


            public void Close()
            {
                Conn.Close();
            }

            public DataTable SelectToDataTable(string SQL)
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                OleDbCommand command = new OleDbCommand(SQL, Conn);
                adapter.SelectCommand = command;
                DataTable Dt = new DataTable();
                adapter.Fill(Dt);
                return Dt;
            }


            public DataSet SelectToDataSet(string SQL, string subtableName)
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                OleDbCommand command = new OleDbCommand(SQL, Conn);
                adapter.SelectCommand = command;
                DataSet Ds = new DataSet();
                Ds.Tables.Add(subtableName);
                adapter.Fill(Ds, subtableName);
                return Ds;
            }

            public DataSet SelectToDataSet(string SQL, string subtableName, DataSet DataSetName)
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                OleDbCommand command = new OleDbCommand(SQL, Conn);
                adapter.SelectCommand = command;
                DataTable Dt = new DataTable();
                DataSet Ds = new DataSet();
                Ds = DataSetName;
                adapter.Fill(DataSetName, subtableName);
                return Ds;
            }


            public OleDbDataAdapter SelectToOleDbDataAdapter(string SQL)
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                OleDbCommand command = new OleDbCommand(SQL, Conn);
                adapter.SelectCommand = command;
                return adapter;
            }

            public bool ExecuteSQLNonquery(string SQL)
            {
                OleDbCommand cmd = new OleDbCommand(SQL, Conn);
                try
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public class SendMail
        {



            /// <summary>
            /// 发送方邮箱
            /// </summary>
            public string sendFrom { get; set; }
            /// <summary>
            /// 密码
            /// </summary>
            public string mailPwd { get; set; }
            /// <summary>
            /// 接收方邮箱
            /// </summary>
            public string sendTo { get; set; }
            /// <summary>
            /// 邮箱主题
            /// </summary>
            public string subject { get; set; }
            /// <summary>
            /// 邮箱正文
            /// </summary>
            public string mailBody { get; set; }


            public void send()
            {
                try
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.To.Add(sendTo);
                    mailMessage.From = new MailAddress(sendFrom);
                    mailMessage.Subject = subject;
                    mailMessage.Body = mailBody;//可为html格式文本
                    mailMessage.BodyEncoding = Encoding.UTF8; ;//邮件内容编码
                    mailMessage.IsBodyHtml = true;//邮件内容是否为html格式
                    mailMessage.Priority = System.Net.Mail.MailPriority.High;//邮件的优先级:高

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "smtp." + mailMessage.From.Host;
                    smtpClient.Credentials = new NetworkCredential(mailMessage.From.Address, mailPwd);// 发件人用户名和密码
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network; //通过网络发送到SMTP服务器


                    smtpClient.SendAsync(mailMessage, mailMessage.Body);//发送邮件
                }
                catch (Exception)
                {
                }
            }
        }
    }
    
}
