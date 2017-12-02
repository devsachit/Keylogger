namespace KeyLogger_new
{
    partial class new_process
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_date_modified = new System.Windows.Forms.Label();
            this.label_date_createds = new System.Windows.Forms.Label();
            this.label_location = new System.Windows.Forms.Label();
            this.label_size = new System.Windows.Forms.Label();
            this.label_name = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label_suggestion = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.textBox1.Font = new System.Drawing.Font("Miramonte", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(8, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(465, 33);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.Font = new System.Drawing.Font("Miramonte", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(479, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 40);
            this.button1.TabIndex = 1;
            this.toolTip1.SetToolTip(this.button1, "Click here to start this process");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Miramonte", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enter Process\'s Path";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label_date_modified);
            this.groupBox1.Controls.Add(this.label_date_createds);
            this.groupBox1.Controls.Add(this.label_location);
            this.groupBox1.Controls.Add(this.label_size);
            this.groupBox1.Font = new System.Drawing.Font("Miramonte", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(4, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(527, 127);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 19);
            this.label5.TabIndex = 9;
            this.label5.Text = "Size:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "Modified on :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 19);
            this.label3.TabIndex = 7;
            this.label3.Text = "Created on :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "Path :";
            // 
            // label_date_modified
            // 
            this.label_date_modified.AutoSize = true;
            this.label_date_modified.Location = new System.Drawing.Point(102, 76);
            this.label_date_modified.Name = "label_date_modified";
            this.label_date_modified.Size = new System.Drawing.Size(12, 19);
            this.label_date_modified.TabIndex = 5;
            this.label_date_modified.Text = " ";
            // 
            // label_date_createds
            // 
            this.label_date_createds.AutoSize = true;
            this.label_date_createds.Location = new System.Drawing.Point(102, 49);
            this.label_date_createds.Name = "label_date_createds";
            this.label_date_createds.Size = new System.Drawing.Size(12, 19);
            this.label_date_createds.TabIndex = 2;
            this.label_date_createds.Text = " ";
            // 
            // label_location
            // 
            this.label_location.AutoSize = true;
            this.label_location.Location = new System.Drawing.Point(102, 22);
            this.label_location.Name = "label_location";
            this.label_location.Size = new System.Drawing.Size(12, 19);
            this.label_location.TabIndex = 1;
            this.label_location.Text = " ";
            // 
            // label_size
            // 
            this.label_size.AutoSize = true;
            this.label_size.Location = new System.Drawing.Point(102, 103);
            this.label_size.Name = "label_size";
            this.label_size.Size = new System.Drawing.Size(12, 19);
            this.label_size.TabIndex = 0;
            this.label_size.Text = " ";
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_name.ForeColor = System.Drawing.Color.Red;
            this.label_name.Location = new System.Drawing.Point(107, 207);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(11, 16);
            this.label_name.TabIndex = 3;
            this.label_name.Text = " ";
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 0;
            this.toolTip1.ReshowDelay = 100;
            // 
            // label_suggestion
            // 
            this.label_suggestion.AutoSize = true;
            this.label_suggestion.Font = new System.Drawing.Font("Miramonte", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_suggestion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label_suggestion.Location = new System.Drawing.Point(12, 69);
            this.label_suggestion.Name = "label_suggestion";
            this.label_suggestion.Size = new System.Drawing.Size(0, 16);
            this.label_suggestion.TabIndex = 4;
            this.toolTip1.SetToolTip(this.label_suggestion, "Press Right Arrow Key to choose Process Suggession Path");
            this.label_suggestion.Click += new System.EventHandler(this.label_suggestion_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(516, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(12, 12);
            this.button2.TabIndex = 5;
            this.toolTip1.SetToolTip(this.button2, "Press Down Arrow Key to open Opened Process List");
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // new_process
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(531, 92);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label_suggestion);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_name);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "new_process";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Start New Process";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.new_process_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.listview_open_commands_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.listview_open_commands_DragEnter);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_size;
        private System.Windows.Forms.Label label_location;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.Label label_date_createds;
        private System.Windows.Forms.Label label_date_modified;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label_suggestion;
        private System.Windows.Forms.Button button2;
    }
}