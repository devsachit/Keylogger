namespace KeyLogger_new
{
    partial class Listofprocess
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose ( );
            }
            base.Dispose ( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ( )
        {
            this.components = new System.ComponentModel.Container ( );
            this.listBox1 = new System.Windows.Forms.ListBox ( );
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip ( this.components );
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ( );
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ( );
            this.rEmoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ( );
            this.removeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ( );
            this.contextMenuStrip1.SuspendLayout ( );
            this.SuspendLayout ( );
            // 
            // listBox1
            // 
            this.listBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.Font = new System.Drawing.Font ( "Miramonte", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ) );
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point ( 0, 0 );
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size ( 281, 362 );
            this.listBox1.TabIndex = 0;
            this.listBox1.DoubleClick += new System.EventHandler ( this.listBox1_DoubleClick );
            this.listBox1.KeyDown += new System.Windows.Forms.KeyEventHandler ( this.listBox1_KeyDown );
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange ( new System.Windows.Forms.ToolStripItem[] {
            this.selectToolStripMenuItem,
            this.runToolStripMenuItem,
            this.rEmoveToolStripMenuItem,
            this.removeAllToolStripMenuItem} );
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size ( 135, 92 );
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler ( this.contextMenuStrip1_Opening );
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            this.selectToolStripMenuItem.Size = new System.Drawing.Size ( 152, 22 );
            this.selectToolStripMenuItem.Text = "Select";
            this.selectToolStripMenuItem.Click += new System.EventHandler ( this.selectToolStripMenuItem_Click );
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size ( 152, 22 );
            this.runToolStripMenuItem.Text = "Run";
            this.runToolStripMenuItem.Click += new System.EventHandler ( this.runToolStripMenuItem_Click );
            // 
            // rEmoveToolStripMenuItem
            // 
            this.rEmoveToolStripMenuItem.Name = "rEmoveToolStripMenuItem";
            this.rEmoveToolStripMenuItem.Size = new System.Drawing.Size ( 152, 22 );
            this.rEmoveToolStripMenuItem.Text = "Remove";
            this.rEmoveToolStripMenuItem.Click += new System.EventHandler ( this.rEmoveToolStripMenuItem_Click );
            // 
            // removeAllToolStripMenuItem
            // 
            this.removeAllToolStripMenuItem.Name = "removeAllToolStripMenuItem";
            this.removeAllToolStripMenuItem.Size = new System.Drawing.Size ( 152, 22 );
            this.removeAllToolStripMenuItem.Text = "Remove All";
            this.removeAllToolStripMenuItem.Click += new System.EventHandler ( this.removeAllToolStripMenuItem_Click );
            // 
            // Listofprocess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size ( 281, 362 );
            this.Controls.Add ( this.listBox1 );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Listofprocess";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Opened Process List";
            this.TopMost = true;
            this.Load += new System.EventHandler ( this.Listofprocess_Load );
            this.contextMenuStrip1.ResumeLayout ( false );
            this.ResumeLayout ( false );

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rEmoveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAllToolStripMenuItem;
    }
}