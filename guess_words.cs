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
    public partial class guess_words : Form
    {
        public guess_words ( )
        {
            InitializeComponent ( );
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
        public bool save
        {
            get;
            set;
        }
        public string initial
        {
            get;
            set;
        }
        public string sentence
        {
            get;
            set;

        }

        public bool addnew
        {

            get;
            set;
        }
        private void guess_words_Load ( object sender, EventArgs e )
        {
            save = false;
            textBox_initial.Text = initial;
            textBox_sentence.Text = sentence;
            checkBox_add_new.Checked = addnew;
        }

        private void button_save_Click ( object sender, EventArgs e )
        {
            save = true;
            sentence = textBox_sentence.Text;
            initial = textBox_initial.Text;
            addnew = checkBox_add_new.Checked;
            this.Dispose ( );
        }

        private void textBox_initial_TextChanged ( object sender, EventArgs e )
        {
            if ( textBox_initial.TextLength > 0 && textBox_sentence.TextLength > 0 )
            {
                button_save.Enabled = true;
            }
            else
            {
                button_save.Enabled = false;
            }
        }

        private void textBox_sentence_TextChanged ( object sender, EventArgs e )
        {
            if ( textBox_initial.TextLength > 0 && textBox_sentence.TextLength > 0 )
            {
                button_save.Enabled = true;
            }
            else
            {
                button_save.Enabled = false;
            }
        }
    }
}
