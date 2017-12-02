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
    public partial class Listofprocess : Form
    {
        public Listofprocess ( )
        {
            InitializeComponent ( );
        }

        public bool select
        {

            get;
            set;
        }
        public string selected
        {
            get;
            set;

        }
        List<string> suggestion = new List<string> ( );
        private void Listofprocess_Load ( object sender, EventArgs e )
        {
            select = false;
            try
            {
                suggestion = readwrite_registy.read_all_valuenames ( @"User Input\ProcessList" ).ToList<string> ( );
                foreach ( string st in suggestion )
                {
                    listBox1.Items.Add ( st );
                }
            }
            catch
            {

            }
        }

        private void contextMenuStrip1_Opening ( object sender, CancelEventArgs e )
        {
            if ( listBox1.SelectedItems.Count >= 1 )
            {
                
                runToolStripMenuItem.Visible = true;
                rEmoveToolStripMenuItem.Visible = true;
            }
            else
            {
               
                runToolStripMenuItem.Visible = false;
                rEmoveToolStripMenuItem.Visible = false;
            }
            if ( listBox1.SelectedItems.Count == 1 )
            {
                selectToolStripMenuItem.Visible = true;
            }
            else
            {
                selectToolStripMenuItem.Visible = false;
            }
        }

        private void runToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            try
            {
                for(int i=0;i<listBox1.SelectedItems.Count;i++)
                System.Diagnostics.Process.Start ( listBox1.SelectedItems[i].ToString ( ) );
            }
            catch
            {

            }
        }

        private void selectToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            select = true;
            selected = listBox1.SelectedItem.ToString ( );
            this.Dispose ( );
        }

        private void listBox1_DoubleClick ( object sender, EventArgs e )
        {
            if ( listBox1.SelectedItems.Count == 1 )
            {
                selectToolStripMenuItem_Click ( sender, e );

            }
        }

        private void rEmoveToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            for ( int i = 0; i < listBox1.SelectedItems.Count; i++ )
            {
                readwrite_registy.delete_value ( @"User Input\ProcessList", listBox1.SelectedItems[i].ToString ( ) );
                listBox1.Items.Remove ( listBox1.SelectedItems[i] );
            }
        }

        private void removeAllToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            try
            {
                readwrite_registy.delete_main_keys_from_registry ( new string[] { @"User Input\ProcessList" } );
                listBox1.Items.Clear ( );
            }
            catch
            {

            }
        }

        private void listBox1_KeyDown ( object sender, KeyEventArgs e )
        {
            if ( e.KeyCode == Keys.Enter )
            {
                listBox1_DoubleClick ( sender, EventArgs.Empty );

            }
            else if ( e.KeyCode == Keys.Delete )
            {
                rEmoveToolStripMenuItem_Click ( sender, EventArgs.Empty );

            }
            else if ( e.KeyCode == Keys.Escape )
            {
                this.Dispose ( );
            }
        }
    }
}
