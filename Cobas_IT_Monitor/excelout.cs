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
    public partial class excelout : Form
    {
        public excelout()
        {
            InitializeComponent();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void excelout_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xlsx";
            saveDialog.Filter = "Excel文件|*.xlsx";
            saveDialog.FileName = "IT3000服务器状态监控报告";
            saveDialog.ShowDialog();
            string saveFileName = saveDialog.FileName;
            if (saveFileName.IndexOf(":") < 0) return; //被点了取消
            textBox1.Text = saveFileName;
        }
        private StringBuilder history(string name)
        {
            StringBuilder sb1 = new StringBuilder();
            string sql = "select * from Status_Histroy where para_name = '" + name + "' and sign is null";
            Tool_Class.IO_tool tool = new Tool_Class.IO_tool();
            DataTable dt = tool.DbToDatatable(sql);
            int dtr;
            if (dt.Rows.Count < 5)
            {
                dtr = dt.Rows.Count;

            }
            else
            {
                dtr = 5;
 
            }
            for (int i = 0; i < dtr; i++)
            {
                sb1.Append(dt.Rows[i][6].ToString() + " " + dt.Rows[i][1].ToString() + " " + dt.Rows[i][7].ToString() + dt.Rows[i][2].ToString() + "\r\n");
            }
            return sb1;

        }
        private void exceloutclass(string savename)
        {
            string sql = "select para_title,details,ref_value,flag,'',para_type,para_level,'' from Status_Now";
            Tool_Class.IO_tool tool = new Tool_Class.IO_tool();
            DataTable dt = tool.DbToDatatable(sql);
            StringBuilder sb1 = history("syslog_warn");
            StringBuilder sb2 = history("instrument_connection");
            StringBuilder sb3 = history("disk_size");
            StringBuilder sb4 = history("cpu_running");
            StringBuilder sb5 = history("memory_running");
            StringBuilder sb6 = history("table_count");
            StringBuilder sb7 = history("db_size");
            StringBuilder sb8 = history("para_check");
            StringBuilder sb9 = history("log_error");
            StringBuilder sb10 = history("db_backup");
            dt.Rows[0][7] = sb1;
            dt.Rows[1][7] = sb2;
            dt.Rows[2][7] = sb3;
            dt.Rows[3][7] = sb4;
            dt.Rows[4][7] = sb5;
            dt.Rows[5][7] = sb6;
            dt.Rows[6][7] = sb7;
            dt.Rows[7][7] = sb8;
            dt.Rows[8][7] = sb9;
            dt.Rows[9][7] = sb10;
            tool.DataTableToExcel(dt,savename);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Tool_Class.IO_tool tool = new Tool_Class.IO_tool();
            exceloutclass(textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
