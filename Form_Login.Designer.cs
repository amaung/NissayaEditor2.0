namespace NissayaEditor
{
    partial class Form_Login
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
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            textBox_UserID = new System.Windows.Forms.TextBox();
            textBox_Password = new System.Windows.Forms.TextBox();
            button_Login = new System.Windows.Forms.Button();
            button_Cancel = new System.Windows.Forms.Button();
            button_ShowPswd = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(20, 22);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(50, 15);
            label1.TabIndex = 0;
            label1.Text = "User ID :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(20, 56);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(63, 15);
            label2.TabIndex = 1;
            label2.Text = "Password :";
            // 
            // textBox_UserID
            // 
            textBox_UserID.Location = new System.Drawing.Point(106, 20);
            textBox_UserID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            textBox_UserID.Name = "textBox_UserID";
            textBox_UserID.Size = new System.Drawing.Size(240, 23);
            textBox_UserID.TabIndex = 2;
            // 
            // textBox_Password
            // 
            textBox_Password.Location = new System.Drawing.Point(106, 56);
            textBox_Password.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            textBox_Password.Name = "textBox_Password";
            textBox_Password.PasswordChar = '*';
            textBox_Password.Size = new System.Drawing.Size(240, 23);
            textBox_Password.TabIndex = 3;
            // 
            // button_Login
            // 
            button_Login.Location = new System.Drawing.Point(236, 94);
            button_Login.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            button_Login.Name = "button_Login";
            button_Login.Size = new System.Drawing.Size(66, 28);
            button_Login.TabIndex = 4;
            button_Login.Text = "Login";
            button_Login.UseVisualStyleBackColor = true;
            button_Login.Click += button_Login_Click;
            // 
            // button_Cancel
            // 
            button_Cancel.Location = new System.Drawing.Point(91, 94);
            button_Cancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            button_Cancel.Name = "button_Cancel";
            button_Cancel.Size = new System.Drawing.Size(66, 28);
            button_Cancel.TabIndex = 5;
            button_Cancel.Text = "Cancel";
            button_Cancel.UseVisualStyleBackColor = true;
            button_Cancel.Click += button_Cancel_Click;
            // 
            // button_ShowPswd
            // 
            button_ShowPswd.Location = new System.Drawing.Point(357, 56);
            button_ShowPswd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            button_ShowPswd.Name = "button_ShowPswd";
            button_ShowPswd.Size = new System.Drawing.Size(22, 25);
            button_ShowPswd.TabIndex = 6;
            button_ShowPswd.UseVisualStyleBackColor = true;
            button_ShowPswd.Click += button_ShowPswd_Click;
            // 
            // Form_Login
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(398, 134);
            Controls.Add(button_ShowPswd);
            Controls.Add(button_Cancel);
            Controls.Add(button_Login);
            Controls.Add(textBox_Password);
            Controls.Add(textBox_UserID);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Name = "Form_Login";
            Text = "Login to Server";
            Load += Form_Login_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_UserID;
        private System.Windows.Forms.TextBox textBox_Password;
        private System.Windows.Forms.Button button_Login;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_ShowPswd;
    }
}