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
using CobasITMonitor;




namespace CobasITMonitor
{
    public partial class servermonitor : Form
    {
        string oldsb1 = "";
        string oldsb2 = "";
        public servermonitor()
        {
            InitializeComponent();
            Thread[] threads = new Thread[1];
            threads[0] = new Thread(new ThreadStart(ServerTotalMonitor));
            threads[0].Start();
            
            //threads[2] = new Thread(new ThreadStart(totalmonitor2));
            //threads[2].Start();
        }
        
        public void ServerTotalMonitor()
        {
            start();
            //threadlog(true);
            /*while (true)
            {
                threadDisk(false);
                threadCpu(false);
                threadlog(false);
                threadMem(false);
                threadIp(false);
                Thread.Sleep(10000);
            }*/
            
 
        }
        public void start()
        {
            string sql3 = "update Status_Histroy set sign = '1'";
            string sql4 = "update Status_Now set flag = 'N',details = '正常' where para_name = 'disk_size'";
            string value = "正常";
            string sql5 = "update Status_Now set para_value='正常',details ='" + value + "',create_date = '" + DateTime.Now + "',flag = 'N' where para_name = 'instrument_connection'";
            string sql111 = "update Status_Now set para_value='正常',details ='" + value + "',create_date = '" + DateTime.Now + "',flag = 'N' where para_name = 'cpu_running'";
            string sql222 = "update Status_Now set para_value='正常',details ='" + value + "',create_date = '" + DateTime.Now + "',flag = 'N' where para_name = 'memory_running'";


            Tool_Class.IO_tool tool = new Tool_Class.IO_tool();
            tool.AccessDbclass(sql3);
            tool.AccessDbclass(sql4);
            tool.AccessDbclass(sql5);
            tool.AccessDbclass(sql111);
            tool.AccessDbclass(sql222);

        }
        Tool_Class.IO_tool tool = new Tool_Class.IO_tool();
        List<string> ipList = new List<string>();
        monitor mm = new monitor();
        int eventlogErrorNum = 0;
        int eventlogWarnNum = 0;
        public string way { get; set; }
        public class DriverInfo
        {
            /// <summary>
            /// 警告时间
            /// </summary>
            public DateTime WarningDateTime { get; set; }
            /// <summary>
            /// 警告百分值
            /// </summary>
            public float WarningValue { get; set; }
            /// <summary>
            /// 盘符
            /// </summary>
            public string WarningDrivers { get; set; }
        }
        public class RamCpuInfo
        {
            /// <summary>
            /// 警告时间
            /// </summary>
            public DateTime WarningDateTime { get; set; }
            /// <summary>
            /// 警告百分值
            /// </summary>
            public int WarningValue { get; set; }

        }
        public class IpInfo
        {
            /// <summary>
            /// Ping有问题的IP地址
            /// </summary>
            public string IpAddress { get; set; }
            public string Iphost { get; set; }
            /// <summary>
            /// ping连通有问题的时间
            /// </summary>
            public DateTime WarningDateTime { get; set; }

            /// <summary>
            /// ping 次数
            /// </summary>
            public int PingTimes { get; set; }
        }
        public class eventLogTypeList
        {
            /// <summary>
            /// 日志类型
            /// </summary>

            public string eventLogType { get; set; }
            /// <summary>
            /// 日志详情
            /// </summary>
            public EventLogEntry eventLogEntry { get; set; }
        }
        /// <summary>
        /// cpu达到警告值后的列表
        /// </summary>
        List<RamCpuInfo> WarningListOfCpu = new List<RamCpuInfo>();
        /// <summary>
        /// 内存达到警告值后的列表
        /// </summary>
        List<RamCpuInfo> WarningListOfMem = new List<RamCpuInfo>();
        /// <summary>
        /// 系统日志警告列表
        /// </summary>
        List<eventLogTypeList> WarningListOfEventLog = new List<eventLogTypeList>();
        /// <summary>
        /// 硬盘警告列表
        /// </summary>
        List<DriverInfo> WarningListErrorOfDisk = new List<DriverInfo>();
        List<DriverInfo> WarningListWarnOfDisk = new List<DriverInfo>();
        /// <summary>
        /// ip警告列表
        /// </summary>
        List<IpInfo> WarningListOfIp = new List<IpInfo>();
        List<EventLogEntry> ExsitListOfErrorEventLog = new List<EventLogEntry>();
        List<EventLogEntry> ExsitListOfWarnEventLog = new List<EventLogEntry>();
        List<eventLogTypeList> WarningListOfErrorEventLog = new List<eventLogTypeList>();
        List<eventLogTypeList> WarningListOfWarnEventLog = new List<eventLogTypeList>();

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            //this.Hide();
            

        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Environment.Exit(0);
        }

        int disk;
        int syslog;
        int ip;
        int cpumem;
        
        private void 主界面_Load(object sender, EventArgs e)
        {
            this.Hide();
            this.timer2.Enabled = true;
            this.timer1.Enabled = true;
            oldsb1 = "";
            oldsb2 = "";
            //this.notifyIcon1.Visible = true;
            Tool_Class.IO_tool tool = new Tool_Class.IO_tool();
            disk = int.Parse(tool.readconfig("rf", "diskrefresh")) * 60;
            syslog = int.Parse(tool.readconfig("rf", "syslogrefresh")) * 60;
            ip = int.Parse(tool.readconfig("rf", "netrefresh")) * 60;
            cpumem = int.Parse(tool.readconfig("rf", "cpumemrefresh")) * 60;
            //单实例运行
            Process[] p = Process.GetProcessesByName("CobasITMonitor");
            if (p.Length > 1)
            {
                MessageBox.Show("程序已经打开");
                Environment.Exit(0);
            }

            Main main = new Main();
            main.ShowDialog();
            


        }
        

        //硬盘监控

        public void threadDisk(bool is_first, int exec)
        {
            string db_dir = System.Windows.Forms.Application.StartupPath + "\\db.accdb";
            bool begin = tool.execute_or_not("disk_size", db_dir, disk, is_first, exec);
            if (begin == true)
            {
                monitor mtt = new monitor();
                List<string> driverList = mtt.getFixDisk();//获取硬盘列表
                string cd = "";
                foreach (string driver in driverList)
                {
                    float per = mtt.getDiskFreePercent(driver);
                    string drive = driver.Substring(0, 1);

                    DriverInfo driverInfo = new DriverInfo();
                    driverInfo.WarningDrivers = driver;
                    driverInfo.WarningValue = per;
                    driverInfo.WarningDateTime = DateTime.Now;

                    cd += driverInfo.WarningDrivers.Substring(0, 1) + "： " + driverInfo.WarningValue.ToString("0") + "G;\r\n";
                    int csizewarn = int.Parse(tool.readconfig("jb", "Cwarn"));
                    int dsizewarn = int.Parse(tool.readconfig("jb", "Dwarn"));
                    int esizewarn = int.Parse(tool.readconfig("jb", "Ewarn"));
                    int fsizewarn = int.Parse(tool.readconfig("jb", "Fwarn"));
                    int csizeerror = int.Parse(tool.readconfig("bj", "Cerror"));
                    int dsizeerror = int.Parse(tool.readconfig("bj", "Derror"));
                    int esizeerror = int.Parse(tool.readconfig("bj", "Eerror"));
                    int fsizeerror = int.Parse(tool.readconfig("bj", "Ferror"));
                    ////textBox2.Text = csizewarn;
                    if ((drive == "C" && per < csizeerror) || (drive == "D" && per < dsizeerror) || (drive == "E" && per < esizeerror) || (drive == "F" && per < fsizeerror))
                    {
                        WarningListErrorOfDisk.Add(driverInfo);//达到报错条件硬盘

                    }
                    if ((drive == "C" && per < csizewarn) || (drive == "D" && per < dsizewarn) || (drive == "E" && per < esizewarn) || (drive == "F" && per < fsizewarn))
                    {
                        WarningListWarnOfDisk.Add(driverInfo);//达到警告条件硬盘

                    }


                }
                string str5 = System.Windows.Forms.Application.StartupPath;
                string a = str5 + "\\db.accdb";
                Tool_Class.AccessDbClass1 db = new Tool_Class.AccessDbClass1();
                db.AccessDbClass2(a);
                ////textBox1.Text = c;

                string sql3 = "update Status_Now set details ='" + cd + "',create_date = '" + DateTime.Now + "' where para_name = 'disk_size'";

                bool dd = db.ExecuteSQLNonquery(sql3);


                string fgg = "";
                if (WarningListWarnOfDisk.Count == 0 && WarningListErrorOfDisk.Count == 0) //正常
                {
                    fgg = "N";
                    string sql4 = "update Status_Now set para_value='正常',flag ='N' where para_name = 'disk_size'";
                    bool cc = db.ExecuteSQLNonquery(sql4);
                    //////textBox3.Text = cc.ToString();
                }
                if (WarningListWarnOfDisk.Count > 0) //大于设置警告值
                {
                    fgg = "W";
                    string sql4 = "update Status_Now set para_value='警告',flag ='W' where para_name = 'disk_size'";
                    bool cc = db.ExecuteSQLNonquery(sql4);
                    //////textBox3.Text = cc.ToString();
                }
                if (WarningListErrorOfDisk.Count > 0) //大于设置错误值
                {
                    fgg = "E";
                    string sql4 = "update Status_Now set para_value='错误',flag ='E' where para_name = 'disk_size'";
                    bool cc = db.ExecuteSQLNonquery(sql4);
                    //////textBox3.Text = cc.ToString();
                }
                if (WarningListWarnOfDisk.Count > 0 || WarningListErrorOfDisk.Count > 0)
                {
                    string sql11 = "insert into Status_Histroy (para_name,details,flag,create_date) values ('disk_size','" + cd + "','" + fgg + "','" + DateTime.Now + "')";
                    bool ee = db.ExecuteSQLNonquery(sql11);
                    ////textBox3.Text = ee.ToString();
                }
            }

        }
        //cpu监控
        public void threadCpu(bool is_first, int exec)
        {
            string db_dir = System.Windows.Forms.Application.StartupPath + "\\db.accdb";
            bool begin = tool.execute_or_not("cpu_running", db_dir, cpumem, is_first, exec);
            if (begin == true)
            {
                monitor cpu = new monitor();
                float cpuValue = cpu.getCpuUsedPercent();
                int perOfCpu = int.Parse(cpuValue.ToString().Split('.').ElementAt(0));
                RamCpuInfo ramCpuInfo = new RamCpuInfo();
                ramCpuInfo.WarningDateTime = DateTime.Now;
                ramCpuInfo.WarningValue = perOfCpu;
                int cpuwarnvalue = int.Parse(tool.readconfig("jb", "cpuwarnvalue"));
                if (perOfCpu >= cpuwarnvalue)
                {
                    string str5 = System.Windows.Forms.Application.StartupPath;
                    string a = str5 + "\\db.accdb";
                    Tool_Class.AccessDbClass1 db = new Tool_Class.AccessDbClass1();
                    db.AccessDbClass2(a);
                    string sql11 = "insert into Status_Histroy (para_name,details,create_date) values ('cpu_running','" + perOfCpu + "','" + DateTime.Now + "')";
                    bool ee = db.ExecuteSQLNonquery(sql11);
                    ////textBox3.Text = ee.ToString();

                   

                }
            }
        }
        //内存监控
        public void threadMem(bool is_first, int exec)
        {
            string memvalue = "内存正常;\n\r";
            string memsign = "N";
            string cpuvalue = "CPU正常;\n\r";
            string cpusign = "N";
            string db_dir = System.Windows.Forms.Application.StartupPath + "\\db.accdb";
            bool begin = tool.execute_or_not("memory_running", db_dir, cpumem, is_first, exec);
            if (begin == true)
            {
                monitor mem = new monitor();
                uint memValue = mem.getRamUsePercent();
                int perOfRam = int.Parse(memValue.ToString().Split('.').ElementAt(0));
                int memerrorvalue = int.Parse(tool.readconfig("bj", "memerrorvalue"));
                int memerrortime = int.Parse(tool.readconfig("bj", "memerrortime"));
                int memwarnvalue = int.Parse(tool.readconfig("jb", "memwarnvalue"));
                int memwarntime = int.Parse(tool.readconfig("jb", "memwarntime"));
                if (perOfRam >= memwarnvalue)
                {
                    RamCpuInfo ramCpuInfo = new RamCpuInfo();
                    ramCpuInfo.WarningDateTime = DateTime.Now;
                    ramCpuInfo.WarningValue = perOfRam;
                    WarningListOfMem.Add(ramCpuInfo);
                    string str5 = System.Windows.Forms.Application.StartupPath;
                    string a = str5 + "\\db.accdb";
                    Tool_Class.AccessDbClass1 db = new Tool_Class.AccessDbClass1();
                    db.AccessDbClass2(a);
                    string sql11 = "insert into Status_Histroy (para_name,details,create_date) values ('memory_running','" + perOfRam + "','" + DateTime.Now + "')";
                    bool ee = db.ExecuteSQLNonquery(sql11);
                }
                    ////textBox3.Text = ee.ToString();
                    string sql5 = "select details from Status_Histroy where para_name = 'memory_running' and sign is null";
                    DataTable memerrorcount = tool.DbToDatatable(sql5);
                    int num = 0;
                    int errornum = 0;
                    for (int i = 0; i < memerrorcount.Rows.Count; i++)
                    {
                        int numm = int.Parse(memerrorcount.Rows[i][0].ToString());
                        if (numm > memwarnvalue)
                        {
                            num++;//达到警告值的次数
                        }
                        if (numm > memerrorvalue)
                        {
                            errornum++;//达到错误值的次数
                        }
                    }
                    if (num < memwarntime)
                    {
                        memvalue = "内存正常;\n\r";
                        memsign = "N";
                        

                    }
                    if (num > memwarntime)
                    {
                        memvalue = "内存>" + memwarnvalue + "% 共:" + num + "次;\n\r";
                        memsign = "W";
                        

                    }
                    if (errornum > memerrortime)
                    {
                        memvalue = "内存>" + memerrorvalue + "% 共:" + errornum + "次;\n\r";
                        memsign = "E";

                    }
                    string sql6 = "select details from Status_Histroy where para_name = 'cpu_running' and sign is null";
                    DataTable cpuerrorcount = tool.DbToDatatable(sql6);
                    int numcpu = 0;
                    int errornumcpu = 0;
                    int cpuerrorvalue = int.Parse(tool.readconfig("bj", "cpuerrorvalue"));
                    int cpuerrortime = int.Parse(tool.readconfig("bj", "cpuerrortime"));
                    int cpuwarnvalue = int.Parse(tool.readconfig("jb", "cpuwarnvalue"));
                    int cpuwarntime = int.Parse(tool.readconfig("jb", "cpuwarntime"));
                    for (int i = 0; i < cpuerrorcount.Rows.Count; i++)
                    {
                        int numm = int.Parse(cpuerrorcount.Rows[i][0].ToString());
                        if (numm > cpuwarnvalue)
                        {
                            numcpu++;//达到警告值的次数
                        }
                        if (numm > cpuerrorvalue)
                        {
                            errornumcpu++;//达到错误值的次数
                        }
                    }
                    if (numcpu < cpuwarntime)
                    {
                        cpuvalue = "正常;\n\r";
                        cpusign = "N";
                        

                    }
                    if (numcpu > cpuwarntime)
                    {
                        cpuvalue = "CPU>" + cpuwarnvalue + "% 共:" + numcpu + "次;\n\r";
                        cpusign = "W";

                    }
                    if (errornumcpu > cpuerrortime)
                    {
                        cpuvalue = "CPU>" + cpuerrorvalue + "% 共:" + errornumcpu + "次;\n\r";
                        cpusign = "E";

                    }
                    if (cpusign == "N" && memsign == "N")
                    {
                        string sql10 = "update Status_Now set para_value='正常',flag ='N',details = '" + cpuvalue+ "',create_date = '" + DateTime.Now + "' where para_name = 'cpu_running'";
                        tool.AccessDbclass(sql10);
                    }
                    else
                    {
                        if (cpusign == "E" || memsign == "E")
                        {
                            string sql10 = "update Status_Now set para_value='错误',flag ='E',details = '" + cpuvalue + memvalue + "',create_date = '" + DateTime.Now + "' where para_name = 'cpu_running'";
                            tool.AccessDbclass(sql10);

                        }
                        else
                        {
                            string sql10 = "update Status_Now set para_value='警告',flag ='W',details = '" + cpuvalue + memvalue + "',create_date = '" + DateTime.Now + "' where para_name = 'cpu_running'";
                            tool.AccessDbclass(sql10);
 
                        }
 
                    }
                
                
            }
        }
        public void threadErrorSyslog()
        {


            List<EventLogEntry> eventLogList = new List<EventLogEntry>();
            monitor mmm = new monitor();
            List<string> eventTypelist = new List<string>();
            eventTypelist.Add("system");
            eventTypelist.Add("Application");
            eventTypelist.Add("Security");
            foreach (string eventType in eventTypelist)
            {
                eventLogList = mmm.getErrorEventLog(eventType);
                bool found = false;
                foreach (EventLogEntry newEventLog in eventLogList)
                {
                    //textBox4.Text = newEventLog.Message;
                    foreach (EventLogEntry exsitEventLog in ExsitListOfErrorEventLog)
                    {
                        if (exsitEventLog.TimeGenerated == newEventLog.TimeGenerated
                            && exsitEventLog.Category == newEventLog.Category
                            && exsitEventLog.Message == newEventLog.Message
                            && exsitEventLog.InstanceId == newEventLog.InstanceId
                            && exsitEventLog.UserName == newEventLog.UserName
                            && exsitEventLog.MachineName == newEventLog.MachineName)   //日志相同的只发送一次
                        {
                            found = true;//找到相同的了
                        }

                    }
                    if (!found) //没找到，添加进去，
                    {
                        ExsitListOfErrorEventLog.Add(newEventLog); //填入已存在列表里
                        eventLogTypeList eventList = new eventLogTypeList();
                        eventList.eventLogEntry = newEventLog;
                        eventList.eventLogType = eventType;
                        WarningListOfErrorEventLog.Add(eventList);//发送警报列表里
                    }



                }
                eventLogList.Clear(); //处理完毕，清空
            }

            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();
            StringBuilder sb4 = new StringBuilder();
            StringBuilder sb5 = new StringBuilder();
            if (WarningListOfErrorEventLog.Count > 0)
            {

                eventlogErrorNum += WarningListOfErrorEventLog.Count;
                foreach (eventLogTypeList eventLogType in WarningListOfErrorEventLog)
                {
                    string error = eventLogType.eventLogType + "日志出现错误！";
                    string message = eventLogType.eventLogEntry.Message;
                    string sb = eventLogType.eventLogType + message + eventLogType.eventLogEntry.TimeGenerated;
                    string str5 = System.Windows.Forms.Application.StartupPath;
                    string a = str5 + "\\db.accdb";
                    Tool_Class.AccessDbClass1 db = new Tool_Class.AccessDbClass1();
                    db.AccessDbClass2(a);
                    string egg = "E";
                    string sql11 = "insert into Status_Histroy (para_name,details,flag,create_date,para_value) values ('syslog_warn','" + error + "%','" + egg + "','" + eventLogType.eventLogEntry.TimeGenerated + "','" + message + "')";
                    bool ee = db.ExecuteSQLNonquery(sql11);
                }
                WarningListOfErrorEventLog.Clear();
            }


        }
        public void threadWarnSyslog()
        {


            List<EventLogEntry> eventLogList = new List<EventLogEntry>();
            monitor mmm = new monitor();
            List<string> eventTypelist = new List<string>();
            eventTypelist.Add("system");
            eventTypelist.Add("Application");
            eventTypelist.Add("Security");
            foreach (string eventType1 in eventTypelist)
            {
                eventLogList = mmm.getWarnEventLog(eventType1);
                bool found = false;
                foreach (EventLogEntry newEventLog in eventLogList)
                {
                    ////textBox4.Text = newEventLog.Message;
                    foreach (EventLogEntry exsitEventLog in ExsitListOfWarnEventLog)
                    {
                        if (exsitEventLog.TimeGenerated == newEventLog.TimeGenerated
                            && exsitEventLog.Category == newEventLog.Category
                            && exsitEventLog.Message == newEventLog.Message
                            && exsitEventLog.InstanceId == newEventLog.InstanceId
                            && exsitEventLog.UserName == newEventLog.UserName
                            && exsitEventLog.MachineName == newEventLog.MachineName)   //日志相同的只发送一次
                        {
                            found = true;//找到相同的了
                        }

                    }
                    if (!found) //没找到，添加进去，
                    {
                        ExsitListOfWarnEventLog.Add(newEventLog); //填入已存在列表里
                        eventLogTypeList eventList = new eventLogTypeList();
                        eventList.eventLogEntry = newEventLog;
                        eventList.eventLogType = eventType1;
                        WarningListOfWarnEventLog.Add(eventList);//发送警报列表里
                    }



                }
                eventLogList.Clear(); //处理完毕，清空
            }

            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();
            StringBuilder sb4 = new StringBuilder();
            StringBuilder sb5 = new StringBuilder();
            if (WarningListOfWarnEventLog.Count > 0)
            {
                eventlogWarnNum += WarningListOfWarnEventLog.Count;
                foreach (eventLogTypeList eventLogType in WarningListOfWarnEventLog)
                {

                    string error = eventLogType.eventLogType + "日志出现警告！";
                    string message = eventLogType.eventLogEntry.Message;
                    string sb = eventLogType.eventLogType + message + eventLogType.eventLogEntry.TimeGenerated;
                    string str5 = System.Windows.Forms.Application.StartupPath;
                    string a = str5 + "\\db.accdb";
                    Tool_Class.AccessDbClass1 db = new Tool_Class.AccessDbClass1();
                    db.AccessDbClass2(a);
                    string egg = "W";
                    string sql11 = "insert into Status_Histroy (para_name,details,flag,create_date,para_value) values ('syslog_warn','" + error + "%','" + egg + "','" + eventLogType.eventLogEntry.TimeGenerated + "','" + message + "')";
                    bool ee = db.ExecuteSQLNonquery(sql11);
                }
                WarningListOfWarnEventLog.Clear();
            }


        }
        //日志监控
        public void threadlog(bool is_first, int exec)
        {
            string db_dir = System.Windows.Forms.Application.StartupPath + "\\db.accdb";
            bool begin = tool.execute_or_not("log_error", db_dir, syslog, is_first, exec);
            if (begin == true)
            {
                threadWarnSyslog();
                threadErrorSyslog();
                Tool_Class.AccessDbClass1 db = new Tool_Class.AccessDbClass1();
                string str5 = System.Windows.Forms.Application.StartupPath;
                string a = str5 + "\\db.accdb";
                db.AccessDbClass2(a);
                string error = "警告日志：" + eventlogWarnNum.ToString() + "个，错误日志：" + eventlogErrorNum.ToString() + "个;";
                string sql3 = "";
                if (eventlogWarnNum == 0 && eventlogErrorNum == 0 && is_first== true)
                {
                    sql3 = "update Status_Now set para_value='正常',details ='正常',create_date = '" + DateTime.Now + "',flag = 'N' where para_name = 'syslog_warn'";
                    bool dd = db.ExecuteSQLNonquery(sql3);

                }
                if (eventlogWarnNum > 0)
                {
                    sql3 = "update Status_Now set para_value='警告',details ='" + error + "',create_date = '" + DateTime.Now + "',flag = 'W' where para_name = 'syslog_warn'";
                    bool dd = db.ExecuteSQLNonquery(sql3);

                }
                if (eventlogErrorNum > 0)
                {
                    sql3 = "update Status_Now set para_value='错误',details ='" + error + "',create_date = '" + DateTime.Now + "',flag = 'E' where para_name = 'syslog_warn'";
                    bool dd = db.ExecuteSQLNonquery(sql3);

                }

                eventlogErrorNum = 0;
                eventlogWarnNum = 0;
            }
        }
        //ip监控
        public void threadIp(bool is_first, int exec)
        {
            string db_dir = System.Windows.Forms.Application.StartupPath + "\\db.accdb";
            bool begin = tool.execute_or_not("instrument_connection", db_dir, ip, is_first, exec);
            if (begin == true)
            {
                monitor read = new monitor();
                string[] ss = tool.readconfig("ip");
                foreach (string aa in ss)
                {
                    if (aa != "")
                    {
                        ipList.Add(aa);
                    }
                }
                string ip_detail = "";
                for (int i = 1; i < ipList.Count; i++)
                {
                    if (ipList[i] != "\r\n")
                    {
                        string[] dd = Regex.Split(ipList[i], "#", RegexOptions.IgnoreCase);
                        int start = int.Parse(dd[2]);
                        int end = int.Parse(dd[3]);
                        int crrunt = int.Parse(DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00"));
                        ////textBox4.Text = start.ToString();
                        if (crrunt > start && crrunt < end)
                        {

                            if (!mm.CmdPing(dd[1]))
                            {
                                IpInfo ipinfo = new IpInfo();
                                ipinfo.IpAddress = dd[1];
                                ipinfo.WarningDateTime = DateTime.Now;
                                ipinfo.Iphost = dd[0];
                                ipinfo.PingTimes = 1;//默认第一次
                                ///ping 失败以后累加个数，如果第一次ping失败，先累加，达到一定量以后再发送邮件
                                ///
                                string str5 = System.Windows.Forms.Application.StartupPath;
                                string a = str5 + "\\db.accdb";
                                Tool_Class.AccessDbClass1 db = new Tool_Class.AccessDbClass1();
                                db.AccessDbClass2(a);
                                string egg = "E";
                                string value = dd[0] + "的ip：" + dd[1] + "连接不通";
                                string sql11 = "insert into Status_Histroy (para_name,details,flag,create_date) values ('instrument_connection','" + value + "','" + egg + "','" + DateTime.Now + "')";
                                bool ee = db.ExecuteSQLNonquery(sql11);

                            }
                            string value3 = dd[0] + "的ip：" + dd[1] + "连接不通";
                            string sql4 = "select count(*) from Status_Histroy where details = '" + value3 + "' and sign is null";
                            DataTable count = tool.DbToDatatable(sql4);
                            int num = int.Parse(count.Rows[0][0].ToString());
                            int warntime = int.Parse(tool.readconfig("jb", "netwarn"));
                            int errortime = int.Parse(tool.readconfig("bj", "neterror"));
                            string value11="";
                            if (num > warntime)
                            {
                                value11 = dd[0] + "的ip：" + dd[1] + "有" + num + "次连接不通；\n\r";
                                string sql3 = "update Status_Now set para_value='警告',create_date = '" + DateTime.Now + "',flag = 'W' where para_name = 'instrument_connection'";
                                tool.AccessDbclass(sql3);

                            }
                            if (num > errortime)
                            {
                                value11 = dd[0] + "的ip：" + dd[1] + "有" + num + "次连接不通；\n\r";
                                string sql3 = "update Status_Now set para_value='错误',create_date = '" + DateTime.Now + "',flag = 'E' where para_name = 'instrument_connection'";
                                tool.AccessDbclass(sql3);

                            }
                            ip_detail += value11;
                            
                        }
                        
                    }
                }
                if (ip_detail =="")
                {
                    ip_detail = "网络正常";
                }
                string sql12 = "update Status_Now set details ='" + ip_detail + "' where para_name = 'instrument_connection'";
                tool.AccessDbclass(sql12);
            }
            
            ipList.Clear();
        }
        
        private void sendemail(string mailfrom, string frompassword, string mailto, string flag, string hospitalname)
        {
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb11 = new StringBuilder();
            StringBuilder sb22 = new StringBuilder();
            sb1.Append(hospitalname + "<br>");
            sb2.Append(hospitalname + "<br>");
            sb11.Append(hospitalname + "<br>");
            sb22.Append(hospitalname + "<br>");
            bool needSend = false;
            bool needSend2 = false;
            string sql = "select * from Status_Now";
            DataTable dt = tool.DbToDatatable(sql);
            for (int i = 0; i < 10; i++)
            {
                if (dt.Rows[i][4].ToString() == "E")
                {
                    needSend2 = true;
                    sb1.Append(dt.Rows[i][6].ToString() + " || " + dt.Rows[i][7].ToString() + " || " + dt.Rows[i][8].ToString() + "<br>");
                    sb11.Append(dt.Rows[i][7].ToString() + " || " + dt.Rows[i][8].ToString() + "<br>");


                }
                if (dt.Rows[i][4].ToString() == "W")
                {
                    needSend = true;
                    sb2.Append(dt.Rows[i][6].ToString() + " || " + dt.Rows[i][7].ToString() + " || " + dt.Rows[i][8].ToString() + "<br>");
                    sb22.Append(dt.Rows[i][7].ToString() + " || " + dt.Rows[i][8].ToString() + "<br>");

                }

            }
            string mailBody = "";
            if (flag == "warn" && needSend == true && oldsb2 != sb22.ToString())
            {
                mailBody = sb2.ToString();
                Class.SendMail sendMail = new Class.SendMail();
                sendMail.mailPwd = frompassword;
                sendMail.sendFrom = mailfrom;
                sendMail.sendTo = mailto;
                sendMail.subject = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " IT3000警告信息";
                sendMail.mailBody = mailBody;
                sendMail.send();
                oldsb2 = sb22.ToString();
            }
            if (flag == "error" && needSend2 == true && oldsb1 != sb11.ToString())
            {
                mailBody = sb1.ToString();
                Class.SendMail sendMail = new Class.SendMail();
                sendMail.mailPwd = frompassword;
                sendMail.sendFrom = mailfrom;
                sendMail.sendTo = mailto;
                sendMail.subject = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "IT3000错误信息";
                sendMail.mailBody = mailBody;
                sendMail.send();
                oldsb1 = sb11.ToString();
            }
            
            


        }
        private void send()
        {
            Tool_Class.IO_tool tool = new Tool_Class.IO_tool();
            string centralad = tool.readconfig("email", "centralad");
            string eastad = tool.readconfig("email", "eastad");
            string westad = tool.readconfig("email", "westad");
            string northad = tool.readconfig("email", "northad");
            string southad = tool.readconfig("email", "southad");
            string area = tool.readconfig("customernode", "customerarea");
            string areaad = "";
            switch (area)
            {
                case "east":
                    areaad = eastad;
                    break;
                case "west":
                    areaad = westad;
                    break;
                case "north":
                    areaad = northad;
                    break;
                case "south":
                    areaad = southad;
                    break;

            }

            string localad = tool.readconfig("email", "localad");
            string localadpassword = tool.readconfig("email", "localadpassword");
            string emailaddress = tool.readconfig("customernode", "emailaddress");
            string hospitalname = tool.readconfig("customernode", "hospitalname");
            string warncentral = tool.readconfig("sign", "warncentral");
            string errorcentral = tool.readconfig("sign", "errorcentral");
            string warnarea = tool.readconfig("sign", "warnarea");
            string errorarea = tool.readconfig("sign", "errorarea");
            string warncustomer = tool.readconfig("sign", "warncustomer");
            string errorcustomer = tool.readconfig("sign", "errorcustomer");
            if (warncentral == "true")
            {
                sendemail(localad, localadpassword, centralad, "warn", hospitalname);

            }
            if (errorcentral == "true")
            {
                sendemail(localad, localadpassword, centralad, "error", hospitalname);

            }
            if (warnarea == "true")
            {
                sendemail(localad, localadpassword, areaad, "warn", hospitalname);

            }
            if (errorarea == "true")
            {
                sendemail(localad, localadpassword, areaad, "error", hospitalname);

            }
            if (warncustomer == "true")
            {
                sendemail(localad, localadpassword, emailaddress, "warn", hospitalname);

            }
            if (errorcustomer == "true")
            {
                sendemail(localad, localadpassword, emailaddress, "error", hospitalname);

            }




        }
        /*public void autoExcelOut()
        {
            //softwareconfig dd = new softwareconfig();
            string ways = way;

            string ways1 = ways + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";
            excelout exl = new excelout();
            exl.exceloutclass(ways1);

        }*/



        /*private void button2_Click(object sender, EventArgs e)
        {
            //excelout();

            //threadSyslog();
            //Tool_Class.DESFileClass.EncryptFile("123.txt", "234.txt", "123");
            //monitor read = new monitor();
            // //textBox4.Text = read.readconfig("mt","nem");

            //read.writeconfig("rf", "lpset","555");
            //Tool_Class.config read = new Tool_Class.config();
            //threadMem();
            ///excelout dd = new excelout();
            // CPUMEM dd = new CPUMEM();
            //dd.ShowDialog();
            //string sql3 = "update Status_Histroy set sign = '1'";

            //Tool_Class.IO_tool tool = new Tool_Class.IO_tool();
            //tool.AccessDbclass(sql3);
            string time = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString();
            if (time == "1200")
            {
                textBox4.Text = "1";
            }
            else
            {
                textBox4.Text = "0";

            }

            //Tool_Class.IO_tool tool = new Tool_Class.IO_tool();
            tool.login("netconfig");
            //tool.Encryptconfig();

            //tool.Decryptconfig();
            //Tool_Class.AccessDbClass1 aa = new Tool_Class.AccessDbClass1();


            ////textBox4.Text = ss.Length.ToSt//ing();
            //sendemail();
            //string sql6 = "select count(*) from Status_Histroy where para_name = 'memory_running' and create_date > "+DateTime.Now;
            // DataTable memwarncount = tool.DbToDatatable(sql6);
            ////textBox4.Text = memwarncount.Rows[0][0].ToString();
            //string str5 = Application.StartupPath;




        }*/

        private void button3_Click(object sender, EventArgs e)
        {
            tool.Decryptconfig();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tool.Encryptconfig();
        }

       


        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Interval = (1000 * 5); //1秒1次
            Thread t = new Thread(new ThreadStart(send));
            t.IsBackground = true;
            t.Start();


        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.timer2.Interval = (1000 * 3600); //1秒1次

            string time = DateTime.Now.Hour.ToString();
            if (time == "12")
            {
                string sql3 = "update Status_Histroy set sign = '1'";

                Tool_Class.IO_tool tool = new Tool_Class.IO_tool();
                tool.AccessDbclass(sql3);
            }

        }



   

    }
}
