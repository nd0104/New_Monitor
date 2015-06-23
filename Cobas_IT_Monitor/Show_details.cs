using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tool_Class;

namespace CobasITMonitor
{
    public partial class Show_details : Form
    {
        public Show_details(string para_name)
        {
            InitializeComponent();
            string db_dir = System.Windows.Forms.Application.StartupPath + "\\db.accdb";
            IO_tool io = new IO_tool();
            string SQL_stat = "select details from Status_Now where para_name = '" + para_name + "'";
            DataTable dt = io.DbToDatatable(SQL_stat, db_dir);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            string list_box_text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
            textBox1.Text = list_box_text;
        }
    }
}
