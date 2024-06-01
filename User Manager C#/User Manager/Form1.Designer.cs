
using System.Drawing;

namespace User_Manager
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.username_label = new System.Windows.Forms.Label();
            this.password_label = new System.Windows.Forms.Label();
            this.password_input = new System.Windows.Forms.TextBox();
            this.username_input = new System.Windows.Forms.TextBox();
            this.createUser_button = new System.Windows.Forms.Button();
            this.displayBox = new System.Windows.Forms.ListBox();
            this.createUser_title = new System.Windows.Forms.Label();
            this.createMultipleUsers_title = new System.Windows.Forms.Label();
            this.output_title = new System.Windows.Forms.Label();
            this.selectFile_button = new System.Windows.Forms.Button();
            this.selectFileHelper_label = new System.Windows.Forms.Label();
            this.saveData_button = new System.Windows.Forms.Button();
            this.revealPassword_checkBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // createUser_title
            // 
            this.createUser_title.AutoSize = true;
            this.createUser_title.Font = new System.Drawing.Font("MS Reference Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createUser_title.Location = new System.Drawing.Point(85, 69);
            this.createUser_title.Name = "createUser_title";
            this.createUser_title.Size = new System.Drawing.Size(394, 34);
            this.createUser_title.TabIndex = 9;
            this.createUser_title.Text = "Create one User at a time";
            // 
            // username_input
            // 
            this.username_input = new RoundedTextBox();
            this.username_input.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(238)))), ((int)(((byte)(201)))));
            this.username_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username_input.ForeColor = System.Drawing.Color.Gray;
            this.username_input.Location = new System.Drawing.Point(101, 140);
            this.username_input.Multiline = true;
            this.username_input.Name = "username_input";
            this.username_input.Size = new System.Drawing.Size(280, 40);
            this.username_input.TabIndex = 16;
            this.username_input.Text = "Username";
            this.username_input.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            FormStyles.ApplyPlaceholder(username_input, "Username");
            // 
            // password_input
            // 
            this.password_input = new RoundedTextBox();
            this.password_input.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(238)))), ((int)(((byte)(201)))));
            this.password_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password_input.ForeColor = System.Drawing.Color.Gray;
            this.password_input.Location = new System.Drawing.Point(101, 193);
            this.password_input.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.password_input.Multiline = true;
            this.password_input.Name = "password_input";
            this.password_input.Size = new System.Drawing.Size(280, 40);
            this.password_input.TabIndex = 3;
            this.password_input.Text = "Password";
            this.password_input.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            FormStyles.ApplyPlaceholder(password_input, "Password");
            // 
            // revealPassword_checkBox
            // 
            this.revealPassword_checkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.revealPassword_checkBox.Location = new System.Drawing.Point(101, 248);
            this.revealPassword_checkBox.Name = "revealPassword_checkBox";
            this.revealPassword_checkBox.Size = new System.Drawing.Size(180, 20);
            this.revealPassword_checkBox.TabIndex = 15;
            this.revealPassword_checkBox.Text = "Show Password";
            this.revealPassword_checkBox.UseVisualStyleBackColor = true;
            FormStyles.SetupPasswordVisibilityToggle(revealPassword_checkBox, password_input);
            // 
            // createUser_button
            // 
            this.createUser_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createUser_button.Location = new System.Drawing.Point(101, 290);
            this.createUser_button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.createUser_button.Name = "createUser_button";
            this.createUser_button.Size = new System.Drawing.Size(129, 37);
            this.createUser_button.TabIndex = 6;
            this.createUser_button.Text = "Create User";
            this.createUser_button.UseVisualStyleBackColor = true;
            this.createUser_button.Click += new System.EventHandler(this.CreateUserManually);
            // 
            // saveData_button
            // 
            this.saveData_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveData_button.Location = new System.Drawing.Point(252, 290);
            this.saveData_button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.saveData_button.Name = "saveData_button";
            this.saveData_button.Size = new System.Drawing.Size(129, 37);
            this.saveData_button.TabIndex = 14;
            this.saveData_button.Text = "Save Data";
            this.saveData_button.UseVisualStyleBackColor = true;
            this.saveData_button.Click += new System.EventHandler(this.SaveToFile);
            // 
            // createMultipleUsers_title
            // 
            this.createMultipleUsers_title.AutoSize = true;
            this.createMultipleUsers_title.Font = new System.Drawing.Font("MS Reference Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.createMultipleUsers_title.Location = new System.Drawing.Point(590, 69);
            this.createMultipleUsers_title.Name = "createMultipleUsers_title";
            this.createMultipleUsers_title.Size = new System.Drawing.Size(453, 34);
            this.createMultipleUsers_title.TabIndex = 10;
            this.createMultipleUsers_title.Text = "Create multiple Users at Once";
            // 
            // selectFile_button
            // 
            this.selectFile_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectFile_button.Location = new System.Drawing.Point(773, 140);
            this.selectFile_button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.selectFile_button.Name = "selectFile_button";
            this.selectFile_button.Size = new System.Drawing.Size(129, 37);
            this.selectFile_button.TabIndex = 12;
            this.selectFile_button.Text = "Select File";
            this.selectFile_button.UseVisualStyleBackColor = true;
            this.selectFile_button.Click += new System.EventHandler(this.CreateUserWithFile);
            // 
            // selectFileHelper_label
            // 
            this.selectFileHelper_label.AutoSize = true;
            this.selectFileHelper_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectFileHelper_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(78)))), ((int)(((byte)(78)))));
            this.selectFileHelper_label.Location = new System.Drawing.Point(689, 193);
            this.selectFileHelper_label.Name = "selectFileHelper_label";
            this.selectFileHelper_label.Size = new System.Drawing.Size(282, 17);
            this.selectFileHelper_label.TabIndex = 13;
            this.selectFileHelper_label.Text = "The file needs to be either .txt or .csv";
            // 
            // output_title
            // 
            this.output_title.AutoSize = true;
            this.output_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.output_title.Location = new System.Drawing.Point(85, 415);
            this.output_title.Name = "output_title";
            this.output_title.Size = new System.Drawing.Size(96, 31);
            this.output_title.TabIndex = 11;
            this.output_title.Text = "Output";
            // 
            // displayBox
            // 
            this.displayBox = new RoundedListBox();
            this.displayBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(238)))), ((int)(((byte)(201)))));
            this.displayBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayBox.FormattingEnabled = true;
            this.displayBox.ItemHeight = 20;
            this.displayBox.Location = new System.Drawing.Point(85, 450);
            this.displayBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.displayBox.Name = "displayBox";
            this.displayBox.Size = new System.Drawing.Size(958, 304);
            this.displayBox.TabIndex = 8;
            // 
            // Form1
            // 
            this.AccessibleName = "";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(147)))), ((int)(((byte)(81)))));
            this.ClientSize = new System.Drawing.Size(1108, 867);
            this.Controls.Add(this.revealPassword_checkBox);
            this.Controls.Add(this.saveData_button);
            this.Controls.Add(this.selectFileHelper_label);
            this.Controls.Add(this.selectFile_button);
            this.Controls.Add(this.output_title);
            this.Controls.Add(this.createMultipleUsers_title);
            this.Controls.Add(this.createUser_title);
            this.Controls.Add(this.displayBox);
            this.Controls.Add(this.createUser_button);
            this.Controls.Add(this.username_input);
            this.Controls.Add(this.password_input);
            this.Controls.Add(this.password_label);
            this.Controls.Add(this.username_label);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(221)))), ((int)(((byte)(112)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(15, 15);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.CheckBox revealPassword_checkBox;

        private System.Windows.Forms.Button saveData_button;

        private System.Windows.Forms.Button selectFile_button;
        private System.Windows.Forms.Label selectFileHelper_label;

        private System.Windows.Forms.Label output_title;

        private System.Windows.Forms.Label createMultipleUsers_title;

        private System.Windows.Forms.Label createUser_title;

        #endregion

        private System.Windows.Forms.Label username_label;
        private System.Windows.Forms.Label password_label;
        private System.Windows.Forms.TextBox password_input;
        private System.Windows.Forms.TextBox username_input;
        private System.Windows.Forms.Button createUser_button;
        private System.Windows.Forms.ListBox displayBox;
    }
}
