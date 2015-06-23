using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Work;
using Tool_Class;
using System.Reflection;
using System.Threading;
namespace CobasITMonitor
{
    public partial class Main : Form
    {
        public string[] sql_area_bak = {"select para_value,flag from Status_Now where para_name = 'db_size' ",
                                       "select para_value,flag from Status_Now where para_name = 'table_count' ",
                                       "select para_value,flag from Status_Now where para_name = 'db_backup'",
                                        "select para_value,flag from Status_Now where para_name = 'para_check'",
                                        "select para_value,flag from Status_Now where para_name = 'log_error'",
                                        "select para_value,flag from Status_Now where para_name = 'syslog_warn'",
                                       "select para_value,flag from Status_Now where para_name = 'instrument_connection' ",
                                        "select para_value,flag from Status_Now where para_name = 'disk_size'",
                                       "select para_value,flag from Status_Now where para_name = 'cpu_running' ",
                                        "select para_value,flag from Status_Now where para_name = 'memory_running'"};
        public string[] sql_area = {"select para_value,flag from Status_Now where para_name = 'db_size' ",
                                       "select para_value,flag from Status_Now where para_name = 'table_count' ",
                                       "select para_value,flag from Status_Now where para_name = 'db_backup'",
                                        "select para_value,flag from Status_Now where para_name = 'para_check'",
                                        "select para_value,flag from Status_Now where para_name = 'log_error'",
                                        "select para_value,flag from Status_Now where para_name = 'syslog_warn'",
                                       "select para_value,flag from Status_Now where para_name = 'instrument_connection' ",
                                        "select para_value,flag from Status_Now where para_name = 'disk_size'",
                                       "select para_value,flag from Status_Now where para_name = 'cpu_running' ",
                                        "select para_value,flag from Status_Now where para_name = 'memory_running'"};
        int exec_1,exec_2,exec_3,exec_4,exec_5,exec_6,exec_7,exec_8,exec_9 = 0; 
        progresser process_form = new progresser();
        IO_tool io = new IO_tool();
        Work.Work worker = new Work.Work();
        string db_dir = System.Windows.Forms.Application.StartupPath + "\\db.accdb";
        servermonitor ServerMonitor = new servermonitor();
        public Main()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            
      //      process_form.Show();
            Thread[] threads = new Thread[2];
            threads[0] = new Thread(new ThreadStart(main_thread));
            threads[1] = new Thread(new ThreadStart(monitor_thread));
     /*       process_form.SetProgressValue(10);
            worker.Check_database_para(true,exec_4);
            process_form.SetProgressValue(20);
            worker.Check_database_tablespace_size(true, exec_1);
            process_form.SetProgressValue(40);
            worker.Check_database_db_backup(true,exec_3);
            process_form.SetProgressValue(60);
            worker.Check_database_log_err(true, exec_8);
            process_form.SetProgressValue(80);
            worker.Check_database_table_num(true,exec_2);
            process_form.SetProgressValue(85);
      //      ServerMonitor.threadDisk(true);
            process_form.SetProgressValue(95);*/
            process_form.Close();
            threads[1].Start();
            threads[0].Start();
        }
        #region 主线程
        void main_thread()
        {
            int counter = 0;
            while (true)
            {
                
                for (; counter < 10; counter++)
                {
                    if (sql_area[counter] != null)
                    {
                        DataTable dt = io.DbToDatatable(sql_area[counter], db_dir);
                        DataSet ds = new DataSet();
                        ds.Tables.Add(dt);
                        string dd = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                        char flag = Convert.ToChar(ds.Tables[0].Rows[0].ItemArray[1]);
                        string list_box_text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                        Select_Light(counter, flag, list_box_text);
                    }
                }
                counter = 0;
                Thread.Sleep(10000);
            }
        }
        #endregion
        void monitor_thread()
        {
            recommeded_value();
            while (true)
            {
                worker.Check_database_para(false, exec_4);
                worker.Check_database_tablespace_size(false, exec_1);
                worker.Check_database_db_backup(false, exec_3);
                worker.Check_database_log_err(false, exec_8);
                worker.Check_database_table_num(false, exec_2);
                ServerMonitor.threadDisk(false, exec_7);
                ServerMonitor.threadCpu(false, exec_9);
                ServerMonitor.threadlog(false, exec_5);
                ServerMonitor.threadMem(false, exec_9);
                ServerMonitor.threadIp(false, exec_6);
                Thread.Sleep(10000);
            }
        }
        #region 根据返回值逐行改变灯的颜色，后期考虑用映射动态组成变量名来缩短代码量

        private void Select_Light(int counter, char flag, string list_box_text)
        {
            label10.Text = io.GetLastExeTime("db_size", db_dir);
            label15.Text = io.GetLastExeTime("table_count", db_dir);
            label17.Text = io.GetLastExeTime("db_backup", db_dir);
            label24.Text = io.GetLastExeTime("para_check", db_dir);
            label25.Text = io.GetLastExeTime("log_error", db_dir);
            switch (counter)
            {
                case 0:
                    textBox1.Clear();
                    textBox1.Text = list_box_text;
                    switch (flag)
                    {
                        case 'E':
                            pictureBox1.Image = CobasITMonitor.Properties.Resources.red;
                            break;
                        case 'N':
                            pictureBox1.Image = CobasITMonitor.Properties.Resources.green;
                            break;
                        case 'W':
                            pictureBox1.Image = CobasITMonitor.Properties.Resources.yellow;
                            break;
                        default:
                            break;
                    };
                    break;
                case 1:
                     textBox2.Clear();
                    textBox2.Text = list_box_text;
                    switch (flag)
                    {
                        case 'E':
                            pictureBox2.Image = CobasITMonitor.Properties.Resources.red;
                            break;
                        case 'N':
                            pictureBox2.Image = CobasITMonitor.Properties.Resources.green;
                            break;
                        case 'W':
                            pictureBox2.Image = CobasITMonitor.Properties.Resources.yellow;
                            break;
                        default:
                            break;
                    };
                    break;
                case 2:
                     textBox3.Clear();
                    textBox3.Text = list_box_text;
                    switch (flag)
                    {
                        case 'E':
                            pictureBox3.Image = CobasITMonitor.Properties.Resources.red;
                            break;
                        case 'N':
                            pictureBox3.Image = CobasITMonitor.Properties.Resources.green;
                            break;
                        case 'W':
                            pictureBox3.Image = CobasITMonitor.Properties.Resources.yellow;
                            break;
                        default:
                            break;
                    };
                    break;
                case 3:
                   textBox4.Clear();
                    textBox4.Text = list_box_text;
                    switch (flag)
                    {
                        case 'E':
                            pictureBox4.Image = CobasITMonitor.Properties.Resources.red;
                            break;
                        case 'N':
                            pictureBox4.Image = CobasITMonitor.Properties.Resources.green;
                            break;
                        case 'W':
                            pictureBox4.Image = CobasITMonitor.Properties.Resources.yellow;
                            break;
                        default:
                            break;
                    };
                    break;
                case 4:
                   textBox5.Clear();
                    textBox5.Text = list_box_text;
                    switch (flag)
                    {
                        case 'E':
                            pictureBox5.Image = CobasITMonitor.Properties.Resources.red;
                            break;
                        case 'N':
                            pictureBox5.Image = CobasITMonitor.Properties.Resources.green;
                            break;
                        case 'W':
                            pictureBox5.Image = CobasITMonitor.Properties.Resources.yellow;
                            break;
                        default:
                            break;
                    };
                    break;
                case 5:
                    textBox6.Clear();
                    textBox6.Text = list_box_text;
                    switch (flag)
                    {
                        case 'E':
                            pictureBox16.Image = CobasITMonitor.Properties.Resources.red;
                            break;
                        case 'N':
                            pictureBox16.Image = CobasITMonitor.Properties.Resources.green;
                            break;
                        case 'W':
                            pictureBox16.Image = CobasITMonitor.Properties.Resources.yellow;
                            break;
                        default:
                            break;
                    };
                    break;
                case 6:
                    textBox7.Clear();
                    textBox7.Text = list_box_text;
                    switch (flag)
                    {
                        case 'E':
                            pictureBox10.Image = CobasITMonitor.Properties.Resources.red;
                            break;
                        case 'N':
                            pictureBox10.Image = CobasITMonitor.Properties.Resources.green;
                            break;
                        case 'W':
                            pictureBox10.Image = CobasITMonitor.Properties.Resources.yellow;
                            break;
                        default:
                            break;
                    };
                    break;
                case 7:
                    textBox8.Clear();
                    textBox8.Text = list_box_text;
                    switch (flag)
                    {
                        case 'E':
                            pictureBox12.Image = CobasITMonitor.Properties.Resources.red;
                            break;
                        case 'N':
                            pictureBox12.Image = CobasITMonitor.Properties.Resources.green;
                            break;
                        case 'W':
                            pictureBox12.Image = CobasITMonitor.Properties.Resources.yellow;
                            break;
                        default:
                            break;
                    };
                    break;
                case 8:
                    textBox9.Clear();
                    textBox9.Text = list_box_text;
                    switch (flag)
                    {
                        case 'E':
                            pictureBox14.Image = CobasITMonitor.Properties.Resources.red;
                            break;
                        case 'N':
                            pictureBox14.Image = CobasITMonitor.Properties.Resources.green;
                            break;
                        case 'W':
                            pictureBox14.Image = CobasITMonitor.Properties.Resources.yellow;
                            break;
                        default:
                            break;
                    };
                    break;
                

                default: break;
            }
        }
        #endregion
        void recommeded_value()
        {
            Tool_Class.IO_tool tool = new IO_tool();
            string l61 = tool.readconfig("jb", "netwarn");
            string disk_c = tool.readconfig("jb", "Cwarn");
            string disk_d = tool.readconfig("jb", "Dwarn");
            string disk_e = tool.readconfig("jb", "Ewarn");
            string disk_f = tool.readconfig("jb", "Fwarn");
            string cpu = tool.readconfig("jb", "cpuwarnvalue");
            string memery = tool.readconfig("jb", "memwarnvalue");
            label61.Text = "连通不通次数少于" + l61 + "次";
            label62.Text = "C>" + disk_c + "G;" + "D>" + disk_d + "G;" + "\n\r" + "E>" + disk_e + "G;" + "F>" + disk_f + "G;";
            label63.Text = "使用率低于" + cpu + "%";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Numoftable not = new Numoftable();
            not.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            sizeofdb sdb = new sizeofdb();
            sdb.Show();
        }
        private void checkbox1_changed(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                sql_area[0] = sql_area_bak[0];
                exec_1 = 0;
            }
            else
            {
                sql_area[0] = null;
                pictureBox1.Image = CobasITMonitor.Properties.Resources.pause_;
                exec_1 = 1;
            }
        }
      


        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox16.Checked)
            {
                sql_area[5] = sql_area_bak[5];
                exec_5 = 0;
            }
            else
            {
                sql_area[5] = null;
                pictureBox16.Image = CobasITMonitor.Properties.Resources.pause_;
                exec_5 = 1;
            }
        }


        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked)
            {
                sql_area[6] = sql_area_bak[6];
                exec_6 = 0;
            }
            else
            {
                sql_area[6] = null;
                pictureBox10.Image = CobasITMonitor.Properties.Resources.pause_;
                exec_6 = 1;
            }

        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox12.Checked)
            {
                sql_area[7] = sql_area_bak[7];
                exec_7 = 0;
            }
            else
            {
                sql_area[7] = null;
                pictureBox12.Image = CobasITMonitor.Properties.Resources.pause_;
                exec_7 = 1;
            }

        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox14.Checked)
            {
                sql_area[8] = sql_area_bak[8];
                exec_9 = 0;
            }
            else
            {
                sql_area[8] = null;
                pictureBox14.Image = CobasITMonitor.Properties.Resources.pause_;
                exec_9 = 1;
            }

        }


        private void button5_Click(object sender, EventArgs e)
        {
            CPUMEM cc = new CPUMEM();
            cc.ShowDialog();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            CPUMEM cc = new CPUMEM();
            cc.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            diskconfig dd = new diskconfig();
            dd.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            netconfig ip = new netconfig();
            ip.ShowDialog();

        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked)
            {
                sql_area[4] = sql_area_bak[4];
                exec_8 = 0;
            }
            else
            {
                sql_area[4] = null;
                pictureBox5.Image = CobasITMonitor.Properties.Resources.pause_;
                exec_8 = 1;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            syslogconfig dd = new syslogconfig();
            dd.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            IT3K_LOG it3k = new IT3K_LOG();
            it3k.Show();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                sql_area[2] = sql_area_bak[2];
                exec_3 = 0;
            }
            else
            {
                sql_area[2] = null;
                pictureBox3.Image = CobasITMonitor.Properties.Resources.pause_;
                exec_3 = 1;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                sql_area[3] = sql_area_bak[3];
                exec_4 = 0;
            }
            else
            {
                sql_area[3] = null;
                pictureBox4.Image = CobasITMonitor.Properties.Resources.pause_;
                exec_4 = 1;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                sql_area[1] = sql_area_bak[1];
                exec_2 = 0;
            }

            else
            {
                sql_area[1] = null;
                pictureBox2.Image = CobasITMonitor.Properties.Resources.pause_;
                exec_2 = 1;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            backup back_up = new backup();
            back_up.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            IT3KOPTION it3k_option = new IT3KOPTION();
            it3k_option.Show();
        }


        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Show_details details_windows = new Show_details("para_check");
            details_windows.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Show_details details_windows = new Show_details("db_size");
            details_windows.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Show_details details_windows = new Show_details("table_count");
            details_windows.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Show_details details_windows = new Show_details("db_backup");
            details_windows.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Show_details details_windows = new Show_details("log_error");
            details_windows.Show();
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            Show_details details_windows = new Show_details("syslog_warn");
            details_windows.Show();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Show_details details_windows = new Show_details("instrument_connection");
            details_windows.Show();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Show_details details_windows = new Show_details("disk_size");
            details_windows.Show();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            Show_details details_windows = new Show_details("cpu_running");
            details_windows.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Show_details details_windows = new Show_details("memory_running");
            details_windows.Show();
        }


        Tool_Class.IO_tool tool = new IO_tool();

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
            e.Cancel = true;
            
        }

       

        private void softwareToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            tool.login("softwareconfig");
        }

        private void customerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            customerconfig cc = new customerconfig();
            cc.ShowDialog();

        }

        private void diskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            diskconfig disk = new diskconfig();
            disk.ShowDialog();
        }

        private void cpuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CPUMEM ip = new CPUMEM();
            ip.ShowDialog();
        }

        private void netToolStripMenuItem_Click(object sender, EventArgs e)
        {
            netconfig ip = new netconfig();
            ip.ShowDialog();
        }

        private void syslogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            syslogconfig syslog = new syslogconfig();
            syslog.ShowDialog();
        }

        private void dbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sizeofdb sdb = new sizeofdb();
            sdb.Show();
        }

        private void tableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Numoftable not = new Numoftable();
            not.Show();
        }

        private void databackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backup back_up = new backup();
            back_up.Show();
        }

        private void iT3KOPTIONSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IT3KOPTION it3k_option = new IT3KOPTION();
            it3k_option.Show();
        }

        private void iT3KlogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IT3K_LOG it3k = new IT3K_LOG();
            it3k.Show();
        }

        private void exceloutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            excelout excel = new excelout();
            excel.ShowDialog();
        }

        private void emailtestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            emailtest dd = new emailtest();
            dd.ShowDialog();
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            tool.login("exsit");
        }

        private void mainToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Visible = true;
        }
     
    }

}
