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
    public partial class Numoftable : Form
    {
        Tool_Class.IO_tool io = new Tool_Class.IO_tool();
        public Numoftable()
        {
            InitializeComponent();
            textBox1.Text = io.readconfig("TABLE_CHECK", "TABLE_NUM_1");
            textBox2.Text = io.readconfig("TABLE_CHECK", "TABLE_NUM_2");
            textBox3.Text = io.readconfig("TABLE_CHECK", "TABLE_NUM_3");
            textBox4.Text = io.readconfig("TABLE_CHECK", "TABLE_NUM_4");
            textBox5.Text = io.readconfig("TABLE_CHECK", "TABLE_NUM_5");
            textBox6.Text = io.readconfig("TABLE_CHECK", "TABLE_NUM_6");
            textBox7.Text = io.readconfig("TABLE_CHECK", "TABLE_NUM_7");
            textBox8.Text = io.readconfig("TABLE_CHECK", "TABLE_NUM_8");
            textBox9.Text = io.readconfig("TABLE_CHECK", "TABLE_NUM_9");
            textBox10.Text = io.readconfig("TABLE_CHECK", "TABLE_NUM_10");
            textBox11.Text = io.readconfig("TABLE_CHECK", "TABLE_NUM_11");
            textBox12.Text = io.readconfig("TABLE_CHECK", "TABLE_NUM_12");
            textBox13.Text = io.readconfig("TABLE_CHECK", "TABLE_NUM_13");
            textBox14.Text = io.readconfig("TABLE_CHECK", "TABLE_NUM_14");
            textBox15.Text = io.readconfig("TABLE_CHECK", "TABLE_NUM_CHECK");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            io.writeconfig("TABLE_CHECK", "TABLE_NUM_1", textBox1.Text);
            io.writeconfig("TABLE_CHECK", "TABLE_NUM_2", textBox2.Text);
            io.writeconfig("TABLE_CHECK", "TABLE_NUM_3", textBox3.Text);
            io.writeconfig("TABLE_CHECK", "TABLE_NUM_4", textBox4.Text);
            io.writeconfig("TABLE_CHECK", "TABLE_NUM_5", textBox5.Text);
            io.writeconfig("TABLE_CHECK", "TABLE_NUM_6", textBox6.Text);
            io.writeconfig("TABLE_CHECK", "TABLE_NUM_7", textBox7.Text);
            io.writeconfig("TABLE_CHECK", "TABLE_NUM_8", textBox8.Text);
            io.writeconfig("TABLE_CHECK", "TABLE_NUM_9", textBox9.Text);
            io.writeconfig("TABLE_CHECK", "TABLE_NUM_10", textBox10.Text);
            io.writeconfig("TABLE_CHECK", "TABLE_NUM_11", textBox11.Text);
            io.writeconfig("TABLE_CHECK", "TABLE_NUM_12", textBox12.Text);
            io.writeconfig("TABLE_CHECK", "TABLE_NUM_13", textBox13.Text);
            io.writeconfig("TABLE_CHECK", "TABLE_NUM_14", textBox14.Text);
            io.writeconfig("TABLE_CHECK", "TABLE_NUM_CHECK", textBox15.Text);
            this.Dispose();
        }

        private void Numoftable_Load(object sender, EventArgs e)
        {
            
        }
    }
}
