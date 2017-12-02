using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

namespace KeyLogger_new
{
    public partial class Program_info : Form
    {
        public Program_info()
        {
            InitializeComponent();
        }
        public string text;
        public string infotext;

        public Program_info(string cmmd,string info)
        {
            InitializeComponent();
            text = cmmd;
            infotext = info;
           
        }
        public static bool dontdothis
        {
            get;
            set;
        }
        
        private void Program_info_Load(object sender, EventArgs e)
        {
            label_command.Text = text;
            label_info.Text = infotext;
            FileInfo file = new FileInfo(text);
            try
            {
                pictureBox1.Image = ShellIcon.GetLargeIcon(file.FullName).ToBitmap();
            }
            catch
            {

            }
            dontdothis = false;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dontdothis = true;
            this.Dispose();
        }


        private void button_operate_Click(object sender, EventArgs e)
        {
            dontdothis = false;
            this.Dispose();
        }

       

      
    }





   
}
