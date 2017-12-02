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
    public partial class Edit_commands : Form
    {
        public  Edit_commands()
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
        public static string title
        {
            get;
            set;
        }
        public static string command
        {
            get;
            set;
        }
        public static string path
        {
            get;
            set;
        }
        public static bool check_state
        {
            get;
            set;
        }
        public static bool addnew_check_state
        {
            get;
            set;
        }

        public static bool editison
        {
            get;
            set;
           

        }

        private void Edit_commands_Load(object sender, EventArgs e)
        {
            editison = false;
            textBox_title.Text = title;
            textBox_command.Text = command;
            textBox_path.Text = path;
            checkBox_enable_disable.Checked = check_state;
            checkBox_add_new.Checked = addnew_check_state;
        }
        /// <summary>
        /// saving the edited command,name and path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            editison = true;
            title = textBox_title.Text;
            command = textBox_command.Text;
            path = textBox_path.Text;
            if (checkBox_enable_disable.CheckState == CheckState.Checked)
                check_state = true;
            else
                check_state = false;

            if (checkBox_add_new.CheckState == CheckState.Checked)
                addnew_check_state = true;
            else
                addnew_check_state= false;

            this.Dispose();
        }

        /// <summary>
        /// dragdrop action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void form_DragDrop(object sender, DragEventArgs e)
        {
            // Extract the data from the DataObject-Container into a string list
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            DirectoryInfo filename = new DirectoryInfo(FileList[0]);           
            textBox_path.Text = filename.FullName;  ///adding full path of droped file in the textbox of addnew window
            textBox_title.Text = filename.Name;

            checkBox_add_new.CheckState = CheckState.Checked;
      
        }
        /// <summary>
        /// drag enter action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void form_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void textBox_title_TextChanged(object sender, EventArgs e)
        {
            if (textBox_command.TextLength > 0 && textBox_path.TextLength > 0 && textBox_title.TextLength > 0)
            {
                button_save.Enabled = true;
            }
            else
            {
                button_save.Enabled = false;
            }
        }

        private void textBox_command_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 'a' && e.KeyChar <= 'z' || e.KeyChar >= 'A' && e.KeyChar <= 'Z' || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
