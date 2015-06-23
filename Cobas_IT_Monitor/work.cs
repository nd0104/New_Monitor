using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Tool_Class;
using System.IO;
using System.Data.OleDb;

namespace Work
{
    public class Work
    {
        string db_dir = System.Windows.Forms.Application.StartupPath + "\\db.accdb";
        IO_tool io = new IO_tool();
        private ReadOracleData ROD = new ReadOracleData();
        private string insert_sql = "";
        private bool in_or_up;
        private char show_flag = 'E';
        private string[] output_stat = { "Lis Message", "", "样本总量", "", "LOG\t", "", "Image\t", "", "无申请的样本" };
        private string[] SQL_stat = { "select trunc(sysdate) - trunc(min(createdate)) from lis_message",
                                    "select INHALT from datos_ini where item = 'DAYS_AFTER_DELETING_LIS_MESSAGES'",
                                     "select trunc(sysdate) - trunc(min(scan_datum)) from samples",
                                     "select INHALT from datos_ini where item='DAYS_AFTER_DELETING_OPEN_REQUESTS'",
                                     "select trunc(sysdate) - trunc(min(datum)) from SY_ERROR_LOGS",
                                     "select INHALT from datos_ini where item = 'LOG_TAGE'",
                                     "select trunc(sysdate) - trunc(min(create_date)) from SAMPLE_IMAGES",
                                     "select INHALT from datos_ini where item = 'DAYS_AFTER_DELETING_IMAGES'",
                                     "select (trunc(sysdate) - trunc(min(scan_datum))) * 24 + to_char(sysdate, 'hh24') - to_char(min(scan_datum), 'hh24') from samples where UNSOLICITED = 1",
                                     "select INHALT  from datos_ini where item = 'NUM_HOUR_KEEP_UNSOLICITED_SAMPLES'"
                                     };
        private string SQL_stat2 = "SELECT round(bytes / (1024 * 1024 ), 0) GB FROM dba_data_files where tablespace_name = 'TS_DATEN'";
        private string SQL_stat3 = "select log_date,status,error# from dba_scheduler_job_run_details where job_name  = 'EXPORTDB' order by log_id desc";
        private string SQL_stat4 = "select count(1)  FROM SY_ERROR_LOGS where fehlerkategorie = 30 union   select count(1)  FROM SY_ERROR_LOGS where fehlerkategorie = 20";
        private string[] SQL_stat5 = { "select count(1) from LIS_RESULTS", "select count(1) from RESULTATE", "select count(1) from LIS_MESSAGE", "select count(1) from HIST_SAMPLES" 
                                         ,"select count(1) from SAMPLE_TEST_ASSIGNMENTS","select count(1) from SAMPLE_IMAGES","select count(1) from TEST_REQUESTS" };
        private string[] output_stat2 = { "LIS_RESULTS", "RESULTATE", "LIS_MESSAGE", "HIST_SAMPLES", "SAMPLE_TEST_ASSIGNMENTS",  "SAMPLE_IMAGES",  "TEST_REQUESTS" };
        
        #region 检查数据表是否和设置的参数一致
        public void Check_database_para(bool is_first,int exec)
        {
            if (io.execute_or_not("para_check", db_dir, Convert.ToInt32(io.readconfig("IT3K_OPTION", "OPTION_CHECK")), is_first,exec))
            {
                int ini_diff = 0, table_diff = 0;
                string result = "错误", output = "";
                OracleConnection conn = ROD.NewConn();
                DataSet Table_DataSet;

                for (int i = 0; i < 9; i += 2)
                {
                    Table_DataSet = ROD.ReadDataToDataSet(conn, SQL_stat[i], "");
                    if ((i == 6 || i == 8) && Table_DataSet.Tables[0].Rows[0].ItemArray[0].ToString() == "")
                        table_diff = -1;
                    else
                    {
                        if (Table_DataSet != null && !Table_DataSet.HasErrors && Table_DataSet.Tables.Count == 1)
                            table_diff = Convert.ToInt32(Table_DataSet.Tables[0].Rows[0].ItemArray[0]);
                    }
                    Table_DataSet.Reset();
                    Table_DataSet = ROD.ReadDataToDataSet(conn, SQL_stat[i + 1], "");
                    if (Table_DataSet != null && !Table_DataSet.HasErrors && Table_DataSet.Tables.Count == 1)
                        ini_diff = Convert.ToInt32(Table_DataSet.Tables[0].Rows[0].ItemArray[0]);
                    if (table_diff < ini_diff)
                        result = "正常";
                    output += output_stat[i] + "在数据表中存在" + table_diff + "天的数据,但参数设置的是小于" + ini_diff + "天, 检测结果:" + result + ".\r\n";
                    Table_DataSet.Reset();
                    result = "错误";
                    table_diff = ini_diff = 0;
                }
                conn.Close();
                if (output.Length > 255)
                    output = output.Substring(0, 254);
                if (result == "正常")
                    show_flag = 'N';
                else
                    show_flag = 'E';
                in_or_up = insert_or_update("para_check");
                if (in_or_up)
                {
                    insert_sql = "insert into Status_Now(para_name,para_value,para_group,flag,description,create_date,para_title,details) values ('para_check','" + result + "','IT3K_Para','" + show_flag + "','','" + DateTime.Now.ToString() + "','IT3K_para','" + output + "')";
                    io.AccessDbclass(insert_sql, db_dir);
                }
                else
                {
                    insert_sql = "insert into Status_Histroy select * from (select para_name,para_value,para_group,flag,description,create_date,para_title,details from Status_Now where para_name = 'para_check')";
                    io.AccessDbclass(insert_sql, db_dir);
                    insert_sql = "update Status_Now set para_value='" + result + "',flag = '" + show_flag + "',create_date = '" + DateTime.Now.ToString() + "',details = '" + output + "' where para_name = 'para_check'";
                    io.AccessDbclass(insert_sql, db_dir);
                }
            }
        }
            #endregion
        #region 检查数据文件大小
        public void Check_database_tablespace_size(bool is_first,int exec)
        {
            if (io.execute_or_not("db_size", db_dir, Convert.ToInt32(io.readconfig("DATABASE", "DB_CHECK")), is_first, exec))
            {
                string result = "错误", output = "数据文件大小检测结果为";
                float size_para = Convert.ToInt32(io.readconfig("DATABASE", "DB_SIZE"));
                float size_db = 32;
                OracleConnection conn = ROD.NewConn();
                DataSet Table_DataSet;
                Table_DataSet = ROD.ReadDataToDataSet(conn, SQL_stat2, "");
                size_db = Convert.ToSingle(Table_DataSet.Tables[0].Rows[0].ItemArray[0]);
                conn.Close();
                if (size_db < size_para)
                {
                    result = "正常.";
                    output += result + "。参数设置为:" + size_para + "MB，实际大小为 " + size_db + "MB.\r\n";
                    show_flag = 'N';
                }
                else
                {
                    result = "错误";
                    output += result + "。参数设置为:" + size_para + "MB，实际大小为" + size_db + "MB.\r\n";
                    show_flag = 'E';
                }
                in_or_up = insert_or_update("db_size");
                if (in_or_up)
                {
                    insert_sql = "insert into Status_Now(para_name,para_value,para_group,flag,description,create_date,para_title,details) values ('db_size','" + result + "','Oracle','" + show_flag + "','','" + DateTime.Now.ToString() + "','Oracle','" + output + "')";
                    io.AccessDbclass(insert_sql, db_dir);
                }
                else
                {
                    insert_sql = "insert into Status_Histroy select * from (select para_name,para_value,para_group,flag,description,create_date,para_title,details from Status_Now where para_name = 'db_size')";
                    io.AccessDbclass(insert_sql, db_dir);
                    insert_sql = "update Status_Now set para_value='" + result + "',flag = '" + show_flag + "',create_date = '" + DateTime.Now.ToString() + "',details = '" + output + "' where para_name = 'db_size'";
                    io.AccessDbclass(insert_sql, db_dir);
                }
            }
        }
        #endregion
        #region 检查数据备份
        public void Check_database_db_backup(bool is_first,int exec)
        {
            if (io.execute_or_not("db_backup", db_dir, Convert.ToInt32(io.readconfig("DATABASE", "BACKUP_CHECK")), is_first, exec))
            {
                string result = "错误", output = "数据备份检测结果为:";
                string db_back_para = "SUCCEEDED";
                string db_back = "";
                string log_time = "";
                int error_num = 0;
                OracleConnection conn = ROD.NewConn();
                DataSet Table_DataSet;
                Table_DataSet = ROD.ReadDataToDataSet(conn, SQL_stat3, "");
                log_time = Table_DataSet.Tables[0].Rows[0].ItemArray[0].ToString();
                db_back = Table_DataSet.Tables[0].Rows[0].ItemArray[1].ToString();
                error_num = Convert.ToInt32(Table_DataSet.Tables[0].Rows[0].ItemArray[2]);
                conn.Close();
                if (db_back_para == db_back)
                {
                    result = "正常";
                    output += result + "。数据备份在" + log_time + "完成备份.\r\n";
                    show_flag = 'N';
                }
                else
                {
                    result = "错误";
                    output += result + "。检查出" + error_num + " 个错误。 executed in " + log_time + "\r\n";
                    show_flag = 'E';
                }
                in_or_up = insert_or_update("db_backup");
                if (in_or_up)
                {
                    insert_sql = "insert into Status_Now(para_name,para_value,para_group,flag,description,create_date,para_title,details) values ('db_backup','" + result + "','Oracle','" + show_flag + "','','" + DateTime.Now.ToString() + "','Oracle','" + output + "')";
                    io.AccessDbclass(insert_sql, db_dir);
                }
                else
                {
                    insert_sql = "insert into Status_Histroy select * from (select para_name,para_value,para_group,flag,description,create_date,para_title,details from Status_Now where para_name = 'db_backup')";
                    io.AccessDbclass(insert_sql, db_dir);
                    insert_sql = "update Status_Now set para_value='" + result + "',flag = '" + show_flag + "',create_date = '" + DateTime.Now.ToString() + "',details = '" + output + "' where para_name = 'db_backup'";
                    io.AccessDbclass(insert_sql, db_dir);
                }
            }
        }
        #endregion
        #region 检查Log日志报错否 
        public void Check_database_log_err(bool is_first,int exec)
        {
            if (io.execute_or_not("log_error", db_dir, Convert.ToInt32(io.readconfig("IT3K_LOG", "LOG_CHECK")), is_first, exec))
            {
                string result = "错误", output = "IT3k报错、警告检测结果为： ";
                int diff_num = Convert.ToInt32(io.readconfig("IT3K_LOG", "WARNING")), error_diff_num = Convert.ToInt32(io.readconfig("IT3K_LOG", "ERROR"));
                int error_num = 5, error_num2 = 5;
                OracleConnection conn = ROD.NewConn();
                DataSet Table_DataSet;
                Table_DataSet = ROD.ReadDataToDataSet(conn, SQL_stat4, "");
                if (Table_DataSet.Tables[0].Rows.Count == 2)
                {
                    error_num = Convert.ToInt32(Table_DataSet.Tables[0].Rows[0].ItemArray[0]);
                    error_num2 = Convert.ToInt32(Table_DataSet.Tables[0].Rows[1].ItemArray[0]);
                }
                if (Table_DataSet.Tables[0].Rows.Count == 1)
                {
                    error_num = Convert.ToInt32(Table_DataSet.Tables[0].Rows[0].ItemArray[0]);
                    error_num2 = 0;
                }
                conn.Close();
                if (error_num == 0 && error_num2 < diff_num)
                {
                    result = "正常";
                    output += result + "。共计有" + error_num + " 个Errors； " + error_num2 + "个warnings.\r\n";
                    show_flag = 'N';
                }
                else
                {
                    result = "错误";
                    output += result + "。共计有" + error_num + " 个Errors；" + error_num2 + "个warnings.\r\n";
                    show_flag = 'E';
                }
                in_or_up = insert_or_update("log_error");
                if (in_or_up)
                {
                    insert_sql = "insert into Status_Now(para_name,para_value,para_group,flag,description,create_date,para_title,details) values ('log_error','" + result + "','Log','" + show_flag + "','','" + DateTime.Now.ToString() + "','Log','" + output + "')";
                    io.AccessDbclass(insert_sql, db_dir);
                }
                else
                {
                    insert_sql = "insert into Status_Histroy select * from (select para_name,para_value,para_group,flag,description,create_date,para_title,details from Status_Now where para_name = 'log_error')";
                    io.AccessDbclass(insert_sql, db_dir);
                    insert_sql = "update Status_Now set para_value='" + result + "',flag = '" + show_flag + "',create_date = '" + DateTime.Now.ToString() + "',details = '" + output + "' where para_name = 'log_error'";
                    io.AccessDbclass(insert_sql, db_dir);
                }
            }
        }
        #endregion
        #region 检查关键表数量是否超出
        public void Check_database_table_num(bool is_first,int exec)
        {
            io.readconfig("CORE", "DB_IP");
            int[] SQL5_refrence = { Convert.ToInt32(io.readconfig("TABLE_CHECK", "TABLE_NUM_1")), Convert.ToInt32(io.readconfig("TABLE_CHECK", "TABLE_NUM_3")), Convert.ToInt32(io.readconfig("TABLE_CHECK", "TABLE_NUM_5")), Convert.ToInt32(io.readconfig("TABLE_CHECK", "TABLE_NUM_7")), Convert.ToInt32(io.readconfig("TABLE_CHECK", "TABLE_NUM_9")), Convert.ToInt32(io.readconfig("TABLE_CHECK", "TABLE_NUM_11")), Convert.ToInt32(io.readconfig("TABLE_CHECK", "TABLE_NUM_13")), Convert.ToInt32(io.readconfig("TABLE_CHECK", "TABLE_NUM_2")), Convert.ToInt32(io.readconfig("TABLE_CHECK", "TABLE_NUM_4")), Convert.ToInt32(io.readconfig("TABLE_CHECK", "TABLE_NUM_6")), Convert.ToInt32(io.readconfig("TABLE_CHECK", "TABLE_NUM_8")), Convert.ToInt32(io.readconfig("TABLE_CHECK", "TABLE_NUM_10")), Convert.ToInt32(io.readconfig("TABLE_CHECK", "TABLE_NUM_12")), Convert.ToInt32(io.readconfig("TABLE_CHECK", "TABLE_NUM_14")) };
            if (io.execute_or_not("table_count", db_dir, Convert.ToInt32(io.readconfig("TABLE_CHECK", "TABLE_NUM_CHECK")), is_first, exec))
            {
                string result = "错误", output = "关键表count检测结果为: ";
                int num_count = 50000001;
                OracleConnection conn = ROD.NewConn();
                DataSet Table_DataSet;
                for (int i = 0; i < 7; i++)
                {
                    Table_DataSet = ROD.ReadDataToDataSet(conn, SQL_stat5[i], "");
                    num_count = Convert.ToInt32(Table_DataSet.Tables[0].Rows[0].ItemArray[0]);
                    output += output_stat2[i] + ": " + num_count + ".\r\n";
                    if (num_count < SQL5_refrence[i])
                    {
                        result = "正常";
                        show_flag = 'N';
                    }
                    if (num_count > SQL5_refrence[i] && num_count < SQL5_refrence[7 + i])
                    {
                        result = "正常";
                        show_flag = 'W';
                    }
                    if (num_count > SQL5_refrence[7 + i])
                    {
                        result = "错误";
                        show_flag = 'E';
                    }
                }
                conn.Close();
                output += result + ".";
                in_or_up = insert_or_update("table_count");
                if (in_or_up)
                {
                    insert_sql = "insert into Status_Now(para_name,para_value,para_group,flag,description,create_date,para_title,details) values ('table_count','" + result + "','Oracle','" + show_flag + "','','" + DateTime.Now.ToString() + "','Oracle','" + output + "')";
                    io.AccessDbclass(insert_sql, db_dir);
                }
                else
                {
                    insert_sql = "insert into Status_Histroy select * from (select para_name,para_value,para_group,flag,description,create_date,para_title,details from Status_Now where para_name = 'table_count')";
                    io.AccessDbclass(insert_sql, db_dir);
                    insert_sql = "update Status_Now set para_value='" + result + "',flag = '" + show_flag + "',create_date = '" + DateTime.Now.ToString() + "',details = '" + output + "' where para_name = 'table_count'";
                    io.AccessDbclass(insert_sql, db_dir);
                }
            }
            
        }
        #endregion


        public bool insert_or_update(string key)
        {
            string sql_temp = "select count(1) from Status_Now where para_name = '" + key + "'";
            DataTable dt = io.DbToDatatable(sql_temp, db_dir);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            int flag = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]);
            if (flag == 0)
                return true;
            else
                return false;
        }
    } 
   
}
