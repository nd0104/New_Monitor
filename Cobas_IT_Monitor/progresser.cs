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
    public partial class progresser : Form
    {
        public progresser()
        {
            InitializeComponent();
        }
        public void SetProgressValue(int value)
        {
            this.progressBar1.Value = value;
            this.label1.Text = "Progress :" + value.ToString() + "%";
 
            if (value == this.progressBar1.Maximum - 1) this.Close();
        }

        private void progresser_Load(object sender, EventArgs e)
        {

        }  
    }
}
