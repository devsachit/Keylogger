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
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }
        Control selectedcontrol;
        int page = 1;
        private void Help_Load(object sender, EventArgs e)
        {
            displaysettings();
            statusStrip1.Parent = this;
            createpicturebox(Properties.Resources.tutorial1);
            createpicturebox(Properties.Resources.tutorial2);
            createpicturebox(Properties.Resources.tutorial3);
            createpicturebox(Properties.Resources.tutorial4);
            createpicturebox(Properties.Resources.tutorial5);
            createpicturebox(Properties.Resources.tutorial6);

            createpicturebox(Properties.Resources.tutorial7);
            createpicturebox(Properties.Resources.tutorial8);
            createpicturebox(Properties.Resources.tutorial9);
            createpicturebox(Properties.Resources.tutorial10);
            createpicturebox(Properties.Resources.tutorial11);
            createpicturebox(Properties.Resources.tutorial12);
            createpicturebox(Properties.Resources.tutorial13);
            createpicturebox(Properties.Resources.tutorial14);
            createpicturebox(Properties.Resources.tutorial15);
            createpicturebox(Properties.Resources.tutorial15a);
            createpicturebox(Properties.Resources.tutorial15b);
            createpicturebox(Properties.Resources.tutorial16);
            createpicturebox(Properties.Resources.tutorial17);
            createpicturebox(Properties.Resources.tutorial18);
            createpicturebox(Properties.Resources.tutorial18a);
            createpicturebox(Properties.Resources.tutorial19);
            createpicturebox(Properties.Resources.tutorial20);

           
        }

        public void clearRAM()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            (sender as PictureBox).Size = new System.Drawing.Size(500, 500);
            selectedcontrol = sender as Control;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            try
            {
               
                    (sender as PictureBox).Size = new Size(303, 252);
            }
            catch
            {

            }

        }

        private void displaysettings()
        {
            foreach (Control c in flowLayoutPanel1.Controls)
            {
                if (c is Label)
                {
                    c.Visible = false;
                }
            }
            if (page == 1)
            {
                label1.Visible = true;
                
                pictureBox2.Image = Properties.Resources.home_1;
            }
            else if (page == 2)
            {
              
                label2.Visible = true;
           
                pictureBox2.Image = Properties.Resources.home_2;

            }
            else if (page == 3)
            {
             
                label3.Visible = true;
                pictureBox2.Image = Properties.Resources.home_3;
         
            }
            else if (page == 4)
            {

                label4.Visible = true;
                pictureBox2.Image = Properties.Resources.home_4;

            }
            else if (page == 5)
            {

                label5.Visible = true;
                pictureBox2.Image = Properties.Resources.home_5;

            }

        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            page++;
            if (page == 1)
            {
                toolStripStatusLabel1.Enabled = false;
            }
            else
            {

                toolStripStatusLabel1.Enabled = true;
            }
            if (page == 5)
            {
                toolStripStatusLabel2.Enabled = false;
            }
            else
            {
                toolStripStatusLabel2.Enabled = true;
            }
            displaysettings();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            page--;
            if (page == 1)
            {
                toolStripStatusLabel1.Enabled = false;
            }
            else
            {

                toolStripStatusLabel1.Enabled = true;
            }

            if (page == 5)
            {
                toolStripStatusLabel2.Enabled = false;
            }
            else
            {
                toolStripStatusLabel2.Enabled = true;
            }
          
            displaysettings();
        }
        int count = 0;
        private void createpicturebox(Image img)
        {
            PictureBox pb = new PictureBox()
            {
                Name = "picturebox" + count++,
                SizeMode = PictureBoxSizeMode.AutoSize,
                BorderStyle = BorderStyle.FixedSingle,
                Image=img,

            };
            flowLayoutPanel2.Controls.Add(pb);

        }
    }
}
