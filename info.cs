using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace KeyLogger_new
{
    public partial class info : Form
    {
        public info()
        {
            InitializeComponent();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x80;  // Turn on WS_EX_TOOLWINDOW
                return cp;
            }
        }
        private void info_Load(object sender, EventArgs e)
        {
            //System.Drawing.Drawing2D.GraphicsPath shape = new System.Drawing.Drawing2D.GraphicsPath();
            //shape.AddRectangle(new Rectangle(label_about.Location.X, label_about.Location.Y, label_about.Width, label_about.Height));
            //shape.AddRectangle(new Rectangle(label_name.Location, label_name.Size));
            //shape.AddRectangle(new Rectangle(label_contact.Location, label_contact.Size));
            //shape.AddRectangle(new Rectangle(label_facebook.Location, label_facebook.Size));
            //shape.AddRectangle(new Rectangle(label_no.Location, label_no.Size));
            //this.Region = new Region(shape);
            ProgramID();
        }

     

        private void ProgramID()
        {
            string ID;
            string first = randomnum(1000, 5000);
            string second = randomnum(6000, 8000);
            string third = randomnum(9000, 9999);
            string forth, fifth, sixth, seventh;

            if (int.Parse(first) > 4000)
                forth = (int.Parse(first) + int.Parse(second)).ToString();
            else if (int.Parse(first) > 3000)
                forth = (int.Parse(second) - int.Parse(first)).ToString();
            else if (int.Parse(first) > 2000)
                forth = (int.Parse(second) * int.Parse(first)).ToString();       
            else
                forth = (int.Parse(first) + int.Parse(third)).ToString();

            if (int.Parse(second) >7000)
                fifth = (int.Parse(third) + int.Parse(second)).ToString();
            else if (int.Parse(second) > 6500)
                fifth = (int.Parse(third) - int.Parse(first)).ToString();           
            else
                fifth = (int.Parse(second) + int.Parse(forth)).ToString();


            sixth = (int.Parse(forth) + int.Parse(fifth)).ToString();
            seventh = (int.Parse(first) + int.Parse(sixth)).ToString();
            ID = first + second + third + forth + fifth + sixth + seventh;

            try
            {
                if (Properties.Settings.Default.ID == "id")
                {
                    Properties.Settings.Default.ID = ID;
                    textBox1.Text = ID;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    textBox1.Text = Properties.Settings.Default.ID;
                   
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
          

        }
        private string randomnum(int start, int end)
        {
            Random rand = new Random();
            return rand.Next(start, end).ToString();

        }
     
        private void info_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
            {
                this.Dispose();
            }
          
        }

        private void label_facebook_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"http:\\www.facebook.com\sachit.devkota");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Process.Start(@"http:\\www.maxotheme.com");
        }
    }
}
