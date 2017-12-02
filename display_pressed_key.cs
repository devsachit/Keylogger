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
    public partial class display_pressed_key : Form
    {
        public display_pressed_key (Keys pressed )
        {
            InitializeComponent ( );
            key = pressed;
        }
        Keys key;
        ResizeOrRelocate.FormOnly.Relocate relocate = new ResizeOrRelocate.FormOnly.Relocate ( );


        private void loadregistry ( )
        {
            try
            {

                if ( readwrite_registy.read ( "KeyForm", "Backcolor" ) == "image" )
                    label1.Image = Properties.Resources.backkk;
                else
                {
                    label1.Image = null;
                    label1.BackColor = Color.FromArgb ( int.Parse ( readwrite_registy.read ( "KeyForm", "Backcolor" ) ) );

                }
            }
            catch
            {

            }
            try
            {
                label1.ForeColor = Color.FromArgb ( int.Parse ( readwrite_registy.read ( "KeyForm", "Textcolor" ) ) );
            }
            catch
            {

            }
            try
            {
                Font fnt;
                string fontname = readwrite_registy.read ( "KeyForm", "Fontname" );
                float fontsize = float.Parse ( readwrite_registy.read ( "KeyForm", "Fontsize" ) );
                string fontsyle = readwrite_registy.read ( "KeyForm", "Fontstyle" );

                if ( fontsyle == FontStyle.Bold.ToString ( ) )
                    fnt = new Font ( new FontFamily ( fontname ), fontsize, FontStyle.Bold );
                else if ( fontsyle == FontStyle.Italic.ToString ( ) )
                    fnt = new Font ( new FontFamily ( fontname ), fontsize, FontStyle.Italic );
                else if ( fontsyle == FontStyle.Regular.ToString ( ) )
                    fnt = new Font ( new FontFamily ( fontname ), fontsize, FontStyle.Regular );
                else if ( fontsyle == FontStyle.Strikeout.ToString ( ) )
                    fnt = new Font ( new FontFamily ( fontname ), fontsize, FontStyle.Strikeout );
                else
                    fnt = new Font ( new FontFamily ( fontname ), fontsize, FontStyle.Underline );

                label1.Font = fnt;
            }
            catch
            {
             
            }
            try
            {

             timer1.Interval=int.Parse(   readwrite_registy.read ( "KeyForm", "time" ));
            }
            catch
            {

            }

        }
        private void saveregistry ( )
        {
        
         readwrite_registy.write ( "KeyForm", "Textcolor", label1.ForeColor.ToArgb().ToString() );
         readwrite_registy.write ( "KeyForm", "Fontname",label1.Font.FontFamily.Name );
         readwrite_registy.write ( "KeyForm", "Fontsize", label1.Font.Size.ToString() );
         readwrite_registy.write ( "KeyForm", "Fontstyle", label1.Font.Style.ToString() );
            
        }
        private void display_pressed_key_Load ( object sender, EventArgs e )
        {
            
            try
            {
                string x = readwrite_registy.read ( "KeyForm", "locationx" );
                string y = readwrite_registy.read ( "KeyForm", "locationy" );
                this.Location = new Point ( int.Parse ( x ), int.Parse ( y ) );
            }
            catch
            {
                this.Location = new Point ( 600, 500 );
            }
            label1.Text = key.ToString ( );
            loadregistry ( );
        }
        int count = 0;
        private void timer1_Tick ( object sender, EventArgs e )
        {
            count++;
            if ( count == 10 )
            {

                Opacity = 0;
                label1.Text = null;
            }
        }

        private void label1_TextChanged ( object sender, EventArgs e )
        {
            count = 0;
            Opacity = 1;
        }

        private void label1_MouseDown ( object sender, MouseEventArgs e )
        {
            relocate.control_mouse_down ( this);
            timer1.Stop ( );
        }

        private void label1_MouseMove ( object sender, MouseEventArgs e )
        {
            relocate.control_mouse_move (e,this,System.Windows.Forms.MouseButtons.Left );
        }

        private void label1_MouseUp ( object sender, MouseEventArgs e )
        {
            relocate.control_mouse_up ( );
            timer1.Start ( );
        }

        private void display_pressed_key_Activated ( object sender, EventArgs e )
        {
            InvokeLostFocus ( this, e );
        }

        private void display_pressed_key_LocationChanged ( object sender, EventArgs e )
        {
            readwrite_registy.write ( "KeyForm", "locationx", this.Location.X.ToString ( ) );
            readwrite_registy.write ( "KeyForm", "locationy", this.Location.Y.ToString ( ) );
        }


        private int selectcolor (Color color )
        {
            Color ret=color;
            ColorDialog dialog = new ColorDialog ( );
            if ( dialog.ShowDialog ( ) == DialogResult.OK )
            {
                ret = dialog.Color;
            }
            dialog = null;
            GC.Collect ( );
            GC.WaitForPendingFinalizers ( );
            GC.Collect ( );

            return ret.ToArgb ( );
           

        }
        private void defaultToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            label1.Image = Properties.Resources.backkk;
            readwrite_registy.write ( "KeyForm", "locationy", "image" );
            saveregistry ( );
        }

        private void selectColorToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            label1.BackColor =Color.FromArgb( selectcolor (label1.BackColor ));
            label1.Image = null;
            readwrite_registy.write ( "KeyForm", "locationy", "color" );
            saveregistry ( );
        }

        private void selectColorToolStripMenuItem1_Click ( object sender, EventArgs e )
        {
            label1.ForeColor = Color.FromArgb ( selectcolor ( label1.ForeColor) );
            saveregistry ( );
        }

        private void selectFintToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            FontDialog dia = new FontDialog ( );
            if ( dia.ShowDialog ( ) == DialogResult.OK )
            {
                label1.Font = dia.Font;
                saveregistry ( );
            }
            dia = null;
            GC.Collect ( );
            GC.WaitForPendingFinalizers ( );
            GC.Collect ( );
           
        }

        private void defaultToolStripMenuItem1_Click ( object sender, EventArgs e )
        {
            readwrite_registy.write ( "KeyForm", "time", "30" );
            timer1.Interval = 30;
        }

        private void longToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            readwrite_registy.write ( "KeyForm", "time", "60" );
            timer1.Interval = 60;
        }

        private void longerToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            readwrite_registy.write ( "KeyForm", "time", "120" );
            timer1.Interval = 120;
        }
    }
}
