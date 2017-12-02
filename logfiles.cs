using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace KeyLogger_new
{
    public partial class logfiles : Form
    {
        public logfiles()
        {
            InitializeComponent();
        }

        string path;
        string[] filename;
        string name;
        private void logfiles_Load(object sender, EventArgs e)
        {
            add_logs_in_listview();
            
        }

        private void add_logs_in_listview()
        {
            listView1.Items.Clear();
            path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\KeyLogger Data";
            filename = Directory.GetFiles(path, "*.htm");
            foreach (string file in filename)
            {
                FileInfo info = new FileInfo(file);
                ListViewItem item = new ListViewItem();

                item.Text = get_name(info.FullName);
                item.Tag = info.FullName;

                listView1.Items.Add(item);

            }

        }

        private string get_name(string filename)
        {
           FileInfo fileinfo = new FileInfo(filename);

            return fileinfo.CreationTime.ToLongDateString();
       
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                webBrowser1.Navigate(listView1.SelectedItems[0].Tag.ToString());
            }
            catch
            {

            }
        }

        private void button_clear_all_Click(object sender, EventArgs e)
        {
          if(  MessageBox.Show("Are you sure to delete all the logs","Delete Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1)==DialogResult.Yes)
            {
                try
                {
                    foreach (string file in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\KeyLogger Data"))
                    {
                        FileInfo inf = new FileInfo(file);
                       
                        inf.Delete();

                    }
                    Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\KeyLogger Data");
                }
                catch
                {

                }

             
            }
          try
          {
              Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\KeyLogger Data");
          }
          catch
          {

          }
          add_logs_in_listview();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            add_logs_in_listview();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                removeToolStripMenuItem.Enabled = false;
            }
            else
            {
                removeToolStripMenuItem.Enabled = true;
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            foreach (ListViewItem item in listView1.SelectedItems)
            {
                try
                {
                    FileInfo inf = new FileInfo(item.Tag.ToString());
                    inf.Delete();
                }
                catch(Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
            add_logs_in_listview();
           
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
         //string selected = dateTimePicker1.Value.DayOfWeek + ", " + dateTimePicker1.Value.Month + dateTimePicker1.Value.Day + ", " + dateTimePicker1.Value.Year;

            string name =Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\KeyLogger Data\"+ "Log " + dateTimePicker1.Value.Year + dateTimePicker1.Value.Month + dateTimePicker1.Value.Day + dateTimePicker1.Value.DayOfWeek + ".htm";
         try
         {
             if (!File.Exists(name))
             {

                // MessageBox.Show("No key logs file is existed of this date");
             }
             else
             {
                 webBrowser1.Navigate(name);
             }
         }
         catch
         {
            // MessageBox.Show("No key logs file is existed of this date");
         }
        }
    }
}
