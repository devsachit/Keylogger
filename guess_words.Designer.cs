namespace KeyLogger_new
{
    partial class guess_words
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_add_new = new System.Windows.Forms.CheckBox();
            this.button_save = new System.Windows.Forms.Button();
            this.textBox_sentence = new System.Windows.Forms.TextBox();
            this.textBox_initial = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Miramonte", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(12, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 19);
            this.label2.TabIndex = 13;
            this.label2.Text = "Sentence :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Miramonte", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 19);
            this.label1.TabIndex = 12;
            this.label1.Text = "Initial :";
            // 
            // checkBox_add_new
            // 
            this.checkBox_add_new.AutoSize = true;
            this.checkBox_add_new.Font = new System.Drawing.Font("Miramonte", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_add_new.ForeColor = System.Drawing.Color.Green;
            this.checkBox_add_new.Location = new System.Drawing.Point(118, 107);
            this.checkBox_add_new.Name = "checkBox_add_new";
            this.checkBox_add_new.Size = new System.Drawing.Size(88, 23);
            this.checkBox_add_new.TabIndex = 11;
            this.checkBox_add_new.Text = "Add new";
            this.checkBox_add_new.UseVisualStyleBackColor = true;
            // 
            // button_save
            // 
            this.button_save.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_save.Enabled = false;
            this.button_save.Font = new System.Drawing.Font("Miramonte", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_save.Location = new System.Drawing.Point(118, 149);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(104, 28);
            this.button_save.TabIndex = 10;
            this.button_save.Text = "Save";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // textBox_sentence
            // 
            this.textBox_sentence.Font = new System.Drawing.Font("Miramonte", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_sentence.ForeColor = System.Drawing.Color.Red;
            this.textBox_sentence.Location = new System.Drawing.Point(118, 56);
            this.textBox_sentence.Name = "textBox_sentence";
            this.textBox_sentence.Size = new System.Drawing.Size(302, 27);
            this.textBox_sentence.TabIndex = 9;
            this.textBox_sentence.TextChanged += new System.EventHandler(this.textBox_sentence_TextChanged);
            // 
            // textBox_initial
            // 
            this.textBox_initial.Font = new System.Drawing.Font("Miramonte", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_initial.ForeColor = System.Drawing.Color.Blue;
            this.textBox_initial.Location = new System.Drawing.Point(118, 18);
            this.textBox_initial.Name = "textBox_initial";
            this.textBox_initial.Size = new System.Drawing.Size(302, 27);
            this.textBox_initial.TabIndex = 8;
            this.textBox_initial.TextChanged += new System.EventHandler(this.textBox_initial_TextChanged);
            // 
            // guess_words
            // 
            this.AcceptButton = this.button_save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(430, 189);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox_add_new);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.textBox_sentence);
            this.Controls.Add(this.textBox_initial);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "guess_words";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Guess Word";
            this.Load += new System.EventHandler(this.guess_words_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox_add_new;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.TextBox textBox_sentence;
        private System.Windows.Forms.TextBox textBox_initial;
    }
}