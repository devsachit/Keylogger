using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace KeyLogger_new
{
    public partial class new_process : Form
    {
        public new_process()
        {
            InitializeComponent();
        }
        public static bool startnew_process
        {
            get;
            set;

        }
        string path;
        List<string> suggestion = new List<string> ( );

        private void new_process_Load(object sender, EventArgs e)
        {
            try
            {
                suggestion = readwrite_registy.read_all_valuenames ( @"User Input\ProcessList" ).ToList<string> ( );
            }
            catch
            {

            }
            textBox1.Text = null;
            if (startnew_process == true)
            {
                label1.Text = "Enter Path of New Process";
               // button1.Text = "Start New Process";
                this.Text = "Start New Process";

            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ///start new process
            if (startnew_process == true)
            {
                try
                {
                    System.Diagnostics.Process.Start(path);
                    readwrite_registy.write ( @"User Input\ProcessList", path, path );
                    suggestion = readwrite_registy.read_all_valuenames ( @"User Input\ProcessList" ).ToList<string> ( );
                }
                catch(Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }

      
            this.Dispose();
        }

        private void listview_open_commands_DragDrop(object sender, DragEventArgs e)
        {
            // Extract the data from the DataObject-Container into a string list
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            DirectoryInfo filename = new DirectoryInfo(FileList[0]);

            textBox1.Text = filename.FullName;
        }

        private void listview_open_commands_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
            path = textBox1.Text.Trim();
            displayfileinfo();
            try
            {
                foreach ( string word in suggestion )
                {
                    if ( word.Contains ( path ) )
                    {
                        label_suggestion.Text = word;

                    }

                }
            }
            catch
            {

            }

            if ( textBox1.Text.Length == 0 )
            {
                label_suggestion.Text = null;
            }
            this.AcceptButton = button1;
        }

        private void displayfileinfo()
        {
            textBox1.ForeColor = Color.Red;
            try
            {
                if (Directory.Exists(path) == true)
                {
                    this.Height = 260;
                   
                    textBox1.ForeColor = Color.Blue;
                    label_date_createds.ForeColor = Color.Blue;
                    label_location.ForeColor = Color.Blue;
                    DirectoryInfo info = new DirectoryInfo(path);
                    label_location.Cursor = Cursors.Hand;

                    label_name.Text = info.Name;
                    label_location.Text = info.FullName;
                    label_date_createds.Text = info.CreationTime.ToLongDateString() + "  " + info.CreationTime.ToLongTimeString();
                    label_date_modified.Text = info.LastWriteTime.ToLongDateString() + "  " + info.LastWriteTime.ToLongTimeString();

                   button1.BackgroundImage = ShellIcon.GetLargeIcon(path).ToBitmap();
                   

                }

                else if (File.Exists(path) == true)
                {
                    this.Height = 260;
                   
                    textBox1.ForeColor = Color.Blue;
                    label_date_createds.ForeColor = Color.Blue;
                    label_location.ForeColor = Color.Blue;
                    FileInfo info = new FileInfo(path);
                    label_location.Cursor = Cursors.Hand;

                    label_name.Text = info.Name;
                    label_location.Text = info.FullName;
                    label_date_createds.Text = info.CreationTime.ToLongDateString() + "  " + info.CreationTime.ToLongTimeString();
                    label_date_modified.Text = info.LastWriteTime.ToLongDateString() + "  " + info.LastWriteTime.ToLongTimeString();

                    button1.BackgroundImage= ShellIcon.GetLargeIcon(path).ToBitmap();
                   

                    long size = info.Length;
                    if (size < 1024)
                    {
                        label_size.Text = size.ToString() + "Bytes";
                    }
                    else if (size < 1048576)
                    {
                        label_size.Text = (((double)size / 1024)).ToString() + "KB";
                    }
                    else if (size < 1073741824)
                    {

                        label_size.Text = (((double)size / (1024 * 1024))).ToString() + "MB";
                    }
                    else if (size < 1096290402304)
                    {
                        label_size.Text = (((double)size / (1024 * 1024 * 1024))).ToString() + "GB";
                    }
                }
                else
                {
                    label_date_createds.Text = "";
                    label_location.Text = path;
                    label_size.Text = "";
                    label_name.Text = "";
                    label_date_modified.Text = "";
                    button1.BackgroundImage = null;
                  
                    label_location.Cursor = Cursors.Arrow;
                    label_date_createds.ForeColor = Color.Black;
                    label_location.ForeColor = Color.Black;


                }

            }

            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

        }

        private void textBox1_KeyDown ( object sender, KeyEventArgs e )
        {
            if ( e.KeyCode == Keys.Right )
            {
                if(label_suggestion.Text!=null)
                textBox1.Text = label_suggestion.Text;
            }
            else if ( e.KeyCode == Keys.Down )
            {
                button2_Click ( sender, EventArgs.Empty );
            }
            else if ( e.KeyCode == Keys.Escape )
            {
                this.Dispose ( );
            }
        }

        private void label_suggestion_Click ( object sender, EventArgs e )
        {
            textBox1.Text = label_suggestion.Text;
        }

        private void button2_Click ( object sender, EventArgs e )
        {
            Listofprocess processst = new Listofprocess ( );
            processst.ShowDialog ( );
            if ( processst.select == true )
            {

                textBox1.Text = processst.selected;
            }

            try
            {
                suggestion = readwrite_registy.read_all_valuenames ( @"User Input\ProcessList" ).ToList<string> ( );
            }
            catch
            {
                suggestion = null;
            }
            processst = null;
            GC.Collect ( );
            GC.WaitForPendingFinalizers ( );
            GC.Collect ( );
        }
    }




    /// <summary>
    /// CLASS FOR FOLDER ICONS
    /// </summary>

    public static class ShellIcon
    {
        #region Interop constants

        private const uint FILE_ATTRIBUTE_NORMAL = 0x80;
        private const uint FILE_ATTRIBUTE_DIRECTORY = 0x10;

        #endregion

        #region Interop data types

        [StructLayout(LayoutKind.Sequential)]
        private struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        [Flags]
        private enum SHGFI : int
        {
            /// <summary>get icon</summary>
            Icon = 0x000000100,
            /// <summary>get display name</summary>
            DisplayName = 0x000000200,
            /// <summary>get type name</summary>
            TypeName = 0x000000400,
            /// <summary>get attributes</summary>
            Attributes = 0x000000800,
            /// <summary>get icon location</summary>
            IconLocation = 0x000001000,
            /// <summary>return exe type</summary>
            ExeType = 0x000002000,
            /// <summary>get system icon index</summary>
            SysIconIndex = 0x000004000,
            /// <summary>put a link overlay on icon</summary>
            LinkOverlay = 0x000008000,
            /// <summary>show icon in selected state</summary>
            Selected = 0x000010000,
            /// <summary>get only specified attributes</summary>
            Attr_Specified = 0x000020000,
            /// <summary>get large icon</summary>
            LargeIcon = 0x000000000,
            /// <summary>get small icon</summary>
            SmallIcon = 0x000000001,
            /// <summary>get open icon</summary>
            OpenIcon = 0x000000002,
            /// <summary>get shell size icon</summary>
            ShellIconSize = 0x000000004,
            /// <summary>pszPath is a pidl</summary>
            PIDL = 0x000000008,
            /// <summary>use passed dwFileAttribute</summary>
            UseFileAttributes = 0x000000010,
            /// <summary>apply the appropriate overlays</summary>
            AddOverlays = 0x000000020,
            /// <summary>Get the index of the overlay in the upper 8 bits of the iIcon</summary>
            OverlayIndex = 0x000000040,
        }

        #endregion

        private class Win32
        {
            [DllImport("shell32.dll")]
            public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

            [DllImport("User32.dll")]
            public static extern int DestroyIcon(IntPtr hIcon);

        }

        public static Icon GetSmallFolderIcon(string path)
        {
            return GetIcon(path, SHGFI.SmallIcon | SHGFI.UseFileAttributes, true);
        }

        public static Icon GetLargeFolderIcon(string path)
        {
            return GetIcon(path, SHGFI.LargeIcon | SHGFI.UseFileAttributes, true);
        }

        public static Icon GetSmallIcon(string fileName)
        {
            return GetIcon(fileName, SHGFI.SmallIcon);
        }

        public static Icon GetLargeIcon(string fileName)
        {
            return GetIcon(fileName, SHGFI.LargeIcon);
        }

        public static Icon GetSmallIconFromExtension(string extension)
        {
            return GetIcon(extension, SHGFI.SmallIcon | SHGFI.UseFileAttributes);
        }

        public static Icon GetLargeIconFromExtension(string extension)
        {
            return GetIcon(extension, SHGFI.LargeIcon | SHGFI.UseFileAttributes);
        }

        private static Icon GetIcon(string fileName, SHGFI flags, bool isFolder = false)
        {
            SHFILEINFO shinfo = new SHFILEINFO();

            IntPtr hImgSmall = Win32.SHGetFileInfo(fileName, isFolder ? FILE_ATTRIBUTE_DIRECTORY : FILE_ATTRIBUTE_NORMAL, ref shinfo, (uint)Marshal.SizeOf(shinfo), (uint)(SHGFI.Icon | flags));

            Icon icon = (Icon)System.Drawing.Icon.FromHandle(shinfo.hIcon).Clone();
            Win32.DestroyIcon(shinfo.hIcon);
            return icon;
        }
    }


    ///ICON CLASS END
}
