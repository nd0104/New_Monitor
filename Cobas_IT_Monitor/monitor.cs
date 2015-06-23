using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Management;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;

namespace CobasITMonitor
{
    class monitor
    {
        #region 获取磁盘可用空间
        /// <summary>
        /// 获取本地磁盘，不包括光驱等
        /// </summary>
        /// <returns></returns>
        public List<string> getFixDisk()
        {
            List<string> fixDiskList = new List<string>();
            foreach (DriveInfo di in DriveInfo.GetDrives())
            {
                if (di.DriveType == DriveType.Fixed)
                {
                    fixDiskList.Add(di.Name);
                }
            }
            return fixDiskList;
        }

        [DllImport("kernel32.dll")]
        private static extern bool GetDiskFreeSpaceEx(string lpDirectoryName, out ulong lpFreeBytesAvailable, out ulong lpTotalNumberOfBytes, out ulong lpTotalNumberOfFreeBytes);

        public float getDiskFreePercent(string driveName)
        { 
            ulong freeBytesAvailable, totalNumberOfBytes, totalNumberOfFreeBytes;
            if (!driveName.EndsWith(":\\")) driveName += ":\\";
            GetDiskFreeSpaceEx(driveName, out freeBytesAvailable, out totalNumberOfBytes, out totalNumberOfFreeBytes);
            float freeNum = float.Parse(totalNumberOfFreeBytes.ToString());
            float totalNum = float.Parse(totalNumberOfBytes.ToString());
            
            float result = 0;
            if(freeNum>0 && totalNum>0)
            {
                result = freeNum / (1024 * 1024 * 1024); 
            }
            return result;
        }
        #endregion
        
        #region 获取CPU使用百分百
        public float getCpuUsedPercent()
        {
            PerformanceCounter cpuCounter;
            

            cpuCounter = new PerformanceCounter();

            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";
            float cpu = cpuCounter.NextValue();
             Thread.Sleep(500);
            cpu = cpuCounter.NextValue();


            return cpu;
 


        }
        #endregion 

        #region 内存使用率

        public struct MEMORY_INFO
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public uint dwTotalPhys;
            public uint dwAvailPhys;
            public uint dwTotalPageFile;
            public uint dwAvailPageFile;
            public uint dwTotalVirtual;
            public uint dwAvailVirtual;
        }
        [DllImport("kernel32.dll")]
        private static extern void GlobalMemoryStatus(ref MEMORY_INFO meminfo);
        public uint getRamUsePercent()
        {
            MEMORY_INFO MemInfo;
            MemInfo = new MEMORY_INFO();
            GlobalMemoryStatus(ref MemInfo);
            return MemInfo.dwMemoryLoad;

            //PerformanceCounter ramCounter;
            //ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            //return ramCounter.NextValue() + "MB";
            //return MemoryLoad;
        }

         
        #endregion 


        #region IP连通性
        public bool CmdPing(string strIp)
        {
            bool result = false;
            Process p = new Process(); p.StartInfo.FileName = "cmd.exe";//设定程序名
            p.StartInfo.UseShellExecute = false; //关闭Shell的使用
            p.StartInfo.RedirectStandardInput = true;//重定向标准输入
            p.StartInfo.RedirectStandardOutput = true;//重定向标准输出
            p.StartInfo.RedirectStandardError = true;//重定向错误输出
            p.StartInfo.CreateNoWindow = true;//设置不显示窗口
            p.Start(); 
            p.StandardInput.WriteLine("ping " + strIp);
            p.StandardInput.WriteLine("exit");
            string strRst = p.StandardOutput.ReadToEnd();
            p.Close();

            if (strRst.IndexOf("TTL=") > -1 && strRst.IndexOf("Reply from") > -1)
            {
                result = true;            
            }
            if (strRst.IndexOf("TTL=") > -1 && strRst.IndexOf("回复") > -1)
            {
                result = true;            
            }
            if (strRst.IndexOf("(100% loss)") > -1 )
            {
                result = false;
            }
            if (strRst.IndexOf("timed out") > -1)
            {
                result = false;
            }
            if (strRst.IndexOf("超时") > -1)
            {
                result = false;
            }
            return result;
        }
        #endregion
        #region  日志读取

        public List<EventLogEntry> getErrorEventLog(string eventType)
        {
            List<EventLogEntry> eventLogList = new List<EventLogEntry>();
            EventLog MySystemEvent = new EventLog();                             //日志对象
            MySystemEvent.Log = eventType.ToString();                            //日志类型
            EventLogEntryCollection MyEventCollection = MySystemEvent.Entries;   //获得日志的记录集合
            try
            {
                int Count = MyEventCollection.Count;                                 //记录的长度
                for (int i = 0; i < Count; i++)
                {

                    EventLogEntry MyEntry = MyEventCollection[Count - i - 1];

                    if (MyEntry.EntryType == EventLogEntryType.Error)
                    {
                        if (MyEntry.TimeGenerated.Date == DateTime.Today) //只发送当日的警报
                        {
                            eventLogList.Add(MyEntry);
                        }
                    }

                }
            }
            catch
            {

            }
            return eventLogList;
        }
        public List<EventLogEntry> getWarnEventLog(string eventType)
        {
            List<EventLogEntry> eventLogList = new List<EventLogEntry>();
            EventLog MySystemEvent = new EventLog();                             //日志对象
            MySystemEvent.Log = eventType.ToString();                            //日志类型
            EventLogEntryCollection MyEventCollection = MySystemEvent.Entries;   //获得日志的记录集合
            try
            {
                int Count = MyEventCollection.Count;                                 //记录的长度
                for (int i = 0; i < Count; i++)
                {

                    EventLogEntry MyEntry = MyEventCollection[Count - i - 1];

                    if (MyEntry.EntryType == EventLogEntryType.Warning)
                    {
                        if (MyEntry.TimeGenerated.Date == DateTime.Today) //只发送当日的警报
                        {
                            eventLogList.Add(MyEntry);
                        }
                    }

                }
            }
            catch
            {

            }
            return eventLogList;
        }
        #endregion
       
            public string[] readparameter(string jiedian)
            {
                Tool_Class.DESFileClass.DecryptFile("config.txt", "345.txt", "123");
                FileStream fs = new FileStream(@"345.txt", FileMode.Open);
                Tool_Class.IO_tool dd = new Tool_Class.IO_tool();
                string w = dd.Readfile(fs);
                string[] s = Regex.Split(w, jiedian, RegexOptions.IgnoreCase);
                string ee = s[1];
                string[] ss = Regex.Split(ee, ";", RegexOptions.IgnoreCase);
                string sourceDir = @"..\Debug";
                string[] txtList = Directory.GetFiles(sourceDir, "345.txt");
                foreach (string f in txtList)
                {
                    //File.Delete(f);
                }
                return ss;
            }
              public class eeeee
                {
                    public string eee {get; set;}
                }
              eeeee bbb = new eeeee();
              eeeee ddd = new eeeee();
            public string readconfig(string jiedian,string set)
            {
                Tool_Class.DESFileClass.DecryptFile("config.txt", "345.txt", "123");
                FileStream fs = new FileStream(@"345.txt", FileMode.Open);
                Tool_Class.IO_tool dd = new Tool_Class.IO_tool();
                string w = dd.Readfile(fs);
                string[] s = Regex.Split(w, jiedian, RegexOptions.IgnoreCase);
                string ee = s[1];
                string[] ss = Regex.Split(ee, ";", RegexOptions.IgnoreCase);
               
                List<string> ipList = new List<string>();
                foreach (string aa in ss)
                {
                    ipList.Add(aa);
                }
                string ttttt = "\n" + set;
                foreach (string ttt in ipList)
                {
                    string[] ssss = Regex.Split(ttt, "=", RegexOptions.IgnoreCase);
                    string tttt = ttttt;
                    
                    if (ssss[0].Substring(1).ToString() == tttt)
                    {

                        bbb.eee = ssss[1];
                        

                    }
                    
                    //return ssss[0].Substring(1).ToString();
               
                }
                string sourceDir = @"..\Debug";
                string[] txtList = Directory.GetFiles(sourceDir, "345.txt");
                foreach (string f in txtList)
                {
                    File.Delete(f);
                }
                return bbb.eee;
            }
            public void writeconfig(string jiedian, string set,string value)
            {
                Tool_Class.DESFileClass.DecryptFile("config.txt", "345.txt", "123");
                FileStream fs = new FileStream(@"345.txt", FileMode.Open);
                Tool_Class.IO_tool dd = new Tool_Class.IO_tool();
                string w = dd.Readfile(fs);
                string[] s = Regex.Split(w, jiedian, RegexOptions.IgnoreCase);
                string ee = s[1];
                string[] ss = Regex.Split(ee, ";", RegexOptions.IgnoreCase);

                List<string> ipList = new List<string>();
                foreach (string aa in ss)
                {
                    ipList.Add(aa);
                }
                string ttttt = "\n" + set;
                foreach (string ttt in ipList)
                {
                    string[] ssss = Regex.Split(ttt, "=", RegexOptions.IgnoreCase);
                    string tttt = ttttt;

                    if (ssss[0].Substring(1).ToString() == tttt)
                    {

                        bbb.eee = ssss[0] + "=" + ssss[1];
                        ddd.eee = ssss[0] + "=";


                    }

                    //return ssss[0].Substring(1).ToString();

                }
                
                string values = bbb.eee;
                string[] wt = Regex.Split(w, values, RegexOptions.IgnoreCase);
                string valuee = ddd.eee + "=" + value;
                string total = wt[0] + valuee + wt[1];
                string sourceDir = @"..\Debug";
                string[] txtList = Directory.GetFiles(sourceDir, "345.txt");
                foreach (string f in txtList)
                {
                    File.Delete(f);
                }
                dd.Write2file(@"345.txt",total);
                string[] configList = Directory.GetFiles(sourceDir, "config.txt");
                foreach (string f in configList)
                {
                    File.Delete(f);
                }
                Tool_Class.DESFileClass.EncryptFile("345.txt", "config.txt", "123");
                string[] txtList2 = Directory.GetFiles(sourceDir, "345.txt");
                foreach (string f in txtList2)
                {
                    File.Delete(f);
                }
                

                
            }

            public void jiamiconfig()
             {
                Tool_Class.DESFileClass.EncryptFile("345.txt", "config.txt", "123");
                string sourceDir = @"..\Debug";
                string[] txtList2 = Directory.GetFiles(sourceDir, "345.txt");

                foreach (string f in txtList2)
                {
                    File.Delete(f);
                }
              }
            public void jiemiconfig()
            {
                Tool_Class.DESFileClass.DecryptFile("config.txt", "345.txt", "123");
                string sourceDir = @"..\Debug";
                string[] txtList2 = Directory.GetFiles(sourceDir, "config.txt");

                foreach (string f in txtList2)
                {
                    File.Delete(f);
                }
 
            }
    }
}
