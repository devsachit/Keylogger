namespace KeyLogger_new
{
    partial class display_pressed_key
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
            this.label1 = new System.Windows.Forms.Label ( );
            this.timer1 = new System.Windows.Forms.Timer ( this.components );
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip ( this.components );
            this.shownTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ( );
            this.backgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ( );
            this.defaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ( );
            this.selectColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ( );
            this.textToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ( );
            this.selectColorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem ( );
            this.selectFintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ( );
            this.defaultToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem ( );
            this.longToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ( );
            this.longerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ( );
            this.contextMenuStrip1.SuspendLayout ( );
            this.SuspendLayout ( );
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ContextMenuStrip = this.contextMenuStrip1;
            this.label1.Font = new System.Drawing.Font ( "Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ) );
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Image = global::KeyLogger_new.Properties.Resources.backkk;
            this.label1.Location = new System.Drawing.Point ( 1, 20 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size ( 86, 31 );
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.TextChanged += new System.EventHandler ( this.label1_TextChanged );
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler ( this.label1_MouseDown );
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler ( this.label1_MouseMove );
            this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler ( this.label1_MouseUp );
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 30;
            this.timer1.Tick += new System.EventHandler ( this.timer1_Tick );
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange ( new System.Windows.Forms.ToolStripItem[] {
            this.shownTimeToolStripMenuItem,
            this.backgroundToolStripMenuItem,
            this.textToolStripMenuItem} );
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size ( 153, 92 );
            // 
            // shownTimeToolStripMenuItem
            // 
            this.shownTimeToolStripMenuItem.DropDownItems.AddRange ( new System.Windows.Forms.ToolStripItem[] {
            this.defaultToolStripMenuItem1,
            this.longToolStripMenuItem,
            this.longerToolStripMenuItem} );
            this.shownTimeToolStripMenuItem.Name = "shownTimeToolStripMenuItem";
            this.shownTimeToolStripMenuItem.Size = new System.Drawing.Size ( 152, 22 );
            this.shownTimeToolStripMenuItem.Text = "Shown time";
            // 
            // backgroundToolStripMenuItem
            // 
            this.backgroundToolStripMenuItem.DropDownItems.AddRange ( new System.Windows.Forms.ToolStripItem[] {
            this.defaultToolStripMenuItem,
            this.selectColorToolStripMenuItem} );
            this.backgroundToolStripMenuItem.Name = "backgroundToolStripMenuItem";
            this.backgroundToolStripMenuItem.Size = new System.Drawing.Size ( 152, 22 );
            this.backgroundToolStripMenuItem.Text = "Background";
            // 
            // defaultToolStripMenuItem
            // 
            this.defaultToolStripMenuItem.Name = "defaultToolStripMenuItem";
            this.defaultToolStripMenuItem.Size = new System.Drawing.Size ( 152, 22 );
            this.defaultToolStripMenuItem.Text = "Default";
            this.defaultToolStripMenuItem.Click += new System.EventHandler ( this.defaultToolStripMenuItem_Click );
            // 
            // selectColorToolStripMenuItem
            // 
            this.selectColorToolStripMenuItem.Name = "selectColorToolStripMenuItem";
            this.selectColorToolStripMenuItem.Size = new System.Drawing.Size ( 152, 22 );
            this.selectColorToolStripMenuItem.Text = "Select Color";
            this.selectColorToolStripMenuItem.Click += new System.EventHandler ( this.selectColorToolStripMenuItem_Click );
            // 
            // textToolStripMenuItem
            // 
            this.textToolStripMenuItem.DropDownItems.AddRange ( new System.Windows.Forms.ToolStripItem[] {
            this.selectColorToolStripMenuItem1,
            this.selectFintToolStripMenuItem} );
            this.textToolStripMenuItem.Name = "textToolStripMenuItem";
            this.textToolStripMenuItem.Size = new System.Drawing.Size ( 152, 22 );
            this.textToolStripMenuItem.Text = "Text";
            // 
            // selectColorToolStripMenuItem1
            // 
            this.selectColorToolStripMenuItem1.Name = "selectColorToolStripMenuItem1";
            this.selectColorToolStripMenuItem1.Size = new System.Drawing.Size ( 152, 22 );
            this.selectColorToolStripMenuItem1.Text = "Select Color";
            this.selectColorToolStripMenuItem1.Click += new System.EventHandler ( this.selectColorToolStripMenuItem1_Click );
            // 
            // selectFintToolStripMenuItem
            // 
            this.selectFintToolStripMenuItem.Name = "selectFintToolStripMenuItem";
            this.selectFintToolStripMenuItem.Size = new System.Drawing.Size ( 152, 22 );
            this.selectFintToolStripMenuItem.Text = "Select Font";
            this.selectFintToolStripMenuItem.Click += new System.EventHandler ( this.selectFintToolStripMenuItem_Click );
            // 
            // defaultToolStripMenuItem1
            // 
            this.defaultToolStripMenuItem1.Name = "defaultToolStripMenuItem1";
            this.defaultToolStripMenuItem1.Size = new System.Drawing.Size ( 152, 22 );
            this.defaultToolStripMenuItem1.Text = "Default";
            this.defaultToolStripMenuItem1.Click += new System.EventHandler ( this.defaultToolStripMenuItem1_Click );
            // 
            // longToolStripMenuItem
            // 
            this.longToolStripMenuItem.Name = "longToolStripMenuItem";
            this.longToolStripMenuItem.Size = new System.Drawing.Size ( 152, 22 );
            this.longToolStripMenuItem.Text = "Long";
            this.longToolStripMenuItem.Click += new System.EventHandler ( this.longToolStripMenuItem_Click );
            // 
            // longerToolStripMenuItem
            // 
            this.longerToolStripMenuItem.Name = "longerToolStripMenuItem";
            this.longerToolStripMenuItem.Size = new System.Drawing.Size ( 152, 22 );
            this.longerToolStripMenuItem.Text = "Longer";
            this.longerToolStripMenuItem.Click += new System.EventHandler ( this.longerToolStripMenuItem_Click );
            // 
            // display_pressed_key
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size ( 156, 83 );
            this.Controls.Add ( this.label1 );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "display_pressed_key";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "display_pressed_key";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.Activated += new System.EventHandler ( this.display_pressed_key_Activated );
            this.Load += new System.EventHandler ( this.display_pressed_key_Load );
            this.LocationChanged += new System.EventHandler ( this.display_pressed_key_LocationChanged );
            this.contextMenuStrip1.ResumeLayout ( false );
            this.ResumeLayout ( false );
            this.PerformLayout ( );

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem shownTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backgroundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectColorToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem selectFintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem longToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem longerToolStripMenuItem;
    }
}