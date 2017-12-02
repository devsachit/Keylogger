namespace KeyLogger_new
{
    partial class Edit_commands
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
            this.textBox_title = new System.Windows.Forms.TextBox();
            this.textBox_command = new System.Windows.Forms.TextBox();
            this.button_save = new System.Windows.Forms.Button();
            this.textBox_path = new System.Windows.Forms.TextBox();
            this.checkBox_enable_disable = new System.Windows.Forms.CheckBox();
            this.checkBox_add_new = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_title
            // 
            this.textBox_title.Font = new System.Drawing.Font("Miramonte", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_title.ForeColor = System.Drawing.Color.Blue;
            this.textBox_title.Location = new System.Drawing.Point(118, 17);
            this.textBox_title.Name = "textBox_title";
            this.textBox_title.Size = new System.Drawing.Size(302, 27);
            this.textBox_title.TabIndex = 0;
            this.textBox_title.TextChanged += new System.EventHandler(this.textBox_title_TextChanged);
            // 
            // textBox_command
            // 
            this.textBox_command.Font = new System.Drawing.Font("Miramonte", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_command.ForeColor = System.Drawing.Color.Red;
            this.textBox_command.Location = new System.Drawing.Point(118, 55);
            this.textBox_command.Name = "textBox_command";
            this.textBox_command.Size = new System.Drawing.Size(302, 27);
            this.textBox_command.TabIndex = 1;
            this.textBox_command.TextChanged += new System.EventHandler(this.textBox_title_TextChanged);
            this.textBox_command.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_command_KeyPress);
            // 
            // button_save
            // 
            this.button_save.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_save.Enabled = false;
            this.button_save.Font = new System.Drawing.Font("Miramonte", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_save.Location = new System.Drawing.Point(118, 190);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(104, 28);
            this.button_save.TabIndex = 2;
            this.button_save.Text = "Save";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox_path
            // 
            this.textBox_path.Font = new System.Drawing.Font("Miramonte", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_path.ForeColor = System.Drawing.Color.OrangeRed;
            this.textBox_path.Location = new System.Drawing.Point(118, 90);
            this.textBox_path.Name = "textBox_path";
            this.textBox_path.Size = new System.Drawing.Size(302, 27);
            this.textBox_path.TabIndex = 3;
            this.textBox_path.TextChanged += new System.EventHandler(this.textBox_title_TextChanged);
            // 
            // checkBox_enable_disable
            // 
            this.checkBox_enable_disable.AutoSize = true;
            this.checkBox_enable_disable.Font = new System.Drawing.Font("Miramonte", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_enable_disable.ForeColor = System.Drawing.Color.Fuchsia;
            this.checkBox_enable_disable.Location = new System.Drawing.Point(118, 132);
            this.checkBox_enable_disable.Name = "checkBox_enable_disable";
            this.checkBox_enable_disable.Size = new System.Drawing.Size(174, 23);
            this.checkBox_enable_disable.TabIndex = 4;
            this.checkBox_enable_disable.Text = "Enable the command";
            this.checkBox_enable_disable.UseVisualStyleBackColor = true;
            // 
            // checkBox_add_new
            // 
            this.checkBox_add_new.AutoSize = true;
            this.checkBox_add_new.Font = new System.Drawing.Font("Miramonte", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_add_new.ForeColor = System.Drawing.Color.Green;
            this.checkBox_add_new.Location = new System.Drawing.Point(118, 161);
            this.checkBox_add_new.Name = "checkBox_add_new";
            this.checkBox_add_new.Size = new System.Drawing.Size(88, 23);
            this.checkBox_add_new.TabIndex = 5;
            this.checkBox_add_new.Text = "Add new";
            this.checkBox_add_new.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Miramonte", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "Name :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Miramonte", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 19);
            this.label2.TabIndex = 7;
            this.label2.Text = "Command :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Miramonte", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.OrangeRed;
            this.label3.Location = new System.Drawing.Point(12, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 19);
            this.label3.TabIndex = 8;
            this.label3.Text = "Path :";
            // 
            // Edit_commands
            // 
            this.AcceptButton = this.button_save;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(430, 239);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox_add_new);
            this.Controls.Add(this.checkBox_enable_disable);
            this.Controls.Add(this.textBox_path);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.textBox_command);
            this.Controls.Add(this.textBox_title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Edit_commands";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Commands";
            this.Load += new System.EventHandler(this.Edit_commands_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.form_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.form_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_title;
        private System.Windows.Forms.TextBox textBox_command;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.TextBox textBox_path;
        private System.Windows.Forms.CheckBox checkBox_enable_disable;
        private System.Windows.Forms.CheckBox checkBox_add_new;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}