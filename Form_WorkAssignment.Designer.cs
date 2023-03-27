namespace NissayaEditor
{
    partial class Form_WorkAssignment
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
            dataGridView1 = new System.Windows.Forms.DataGridView();
            button_Close = new System.Windows.Forms.Button();
            button_Open = new System.Windows.Forms.Button();
            button_Download = new System.Windows.Forms.Button();
            textBox_UserID = new System.Windows.Forms.TextBox();
            button_Upload = new System.Windows.Forms.Button();
            button_Update = new System.Windows.Forms.Button();
            button_Import = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            textBox_Total = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(24, 21);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(60, 20);
            label1.TabIndex = 0;
            label1.Text = "User ID:";
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new System.Drawing.Point(23, 62);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new System.Drawing.Size(625, 329);
            dataGridView1.TabIndex = 1;
            // 
            // button_Close
            // 
            button_Close.Location = new System.Drawing.Point(568, 406);
            button_Close.Name = "button_Close";
            button_Close.Size = new System.Drawing.Size(80, 32);
            button_Close.TabIndex = 2;
            button_Close.Text = "Close";
            button_Close.UseVisualStyleBackColor = true;
            button_Close.Click += button_Close_Click;
            // 
            // button_Open
            // 
            button_Open.Location = new System.Drawing.Point(468, 406);
            button_Open.Name = "button_Open";
            button_Open.Size = new System.Drawing.Size(80, 32);
            button_Open.TabIndex = 3;
            button_Open.Text = "Open";
            button_Open.UseVisualStyleBackColor = true;
            button_Open.Click += button_Open_Click;
            // 
            // button_Download
            // 
            button_Download.Location = new System.Drawing.Point(358, 406);
            button_Download.Name = "button_Download";
            button_Download.Size = new System.Drawing.Size(88, 32);
            button_Download.TabIndex = 4;
            button_Download.Text = "Download";
            button_Download.UseVisualStyleBackColor = true;
            button_Download.Click += button_Download_Click;
            // 
            // textBox_UserID
            // 
            textBox_UserID.BackColor = System.Drawing.SystemColors.ControlLight;
            textBox_UserID.Location = new System.Drawing.Point(90, 19);
            textBox_UserID.Name = "textBox_UserID";
            textBox_UserID.ReadOnly = true;
            textBox_UserID.Size = new System.Drawing.Size(417, 27);
            textBox_UserID.TabIndex = 5;
            // 
            // button_Upload
            // 
            button_Upload.Location = new System.Drawing.Point(156, 406);
            button_Upload.Name = "button_Upload";
            button_Upload.Size = new System.Drawing.Size(80, 32);
            button_Upload.TabIndex = 6;
            button_Upload.Text = "Upload";
            button_Upload.UseVisualStyleBackColor = true;
            button_Upload.Click += button_Upload_Click;
            // 
            // button_Update
            // 
            button_Update.Location = new System.Drawing.Point(256, 406);
            button_Update.Name = "button_Update";
            button_Update.Size = new System.Drawing.Size(80, 32);
            button_Update.TabIndex = 7;
            button_Update.Text = "Source";
            button_Update.UseVisualStyleBackColor = true;
            button_Update.Click += button_Source_Click;
            // 
            // button_Import
            // 
            button_Import.Location = new System.Drawing.Point(56, 406);
            button_Import.Name = "button_Import";
            button_Import.Size = new System.Drawing.Size(80, 32);
            button_Import.TabIndex = 8;
            button_Import.Text = "Import";
            button_Import.UseVisualStyleBackColor = true;
            button_Import.Click += button_Import_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(535, 22);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(45, 20);
            label2.TabIndex = 9;
            label2.Text = "Total:";
            // 
            // textBox_Total
            // 
            textBox_Total.Location = new System.Drawing.Point(590, 19);
            textBox_Total.Name = "textBox_Total";
            textBox_Total.ReadOnly = true;
            textBox_Total.Size = new System.Drawing.Size(58, 27);
            textBox_Total.TabIndex = 10;
            // 
            // Form_WorkAssignment
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(664, 453);
            Controls.Add(textBox_Total);
            Controls.Add(label2);
            Controls.Add(button_Import);
            Controls.Add(button_Update);
            Controls.Add(button_Upload);
            Controls.Add(textBox_UserID);
            Controls.Add(button_Download);
            Controls.Add(button_Open);
            Controls.Add(button_Close);
            Controls.Add(dataGridView1);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Name = "Form_WorkAssignment";
            Text = "Work Assignments";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.Button button_Open;
        private System.Windows.Forms.Button button_Download;
        private System.Windows.Forms.TextBox textBox_UserID;
        private System.Windows.Forms.Button button_Upload;
        private System.Windows.Forms.Button button_Update;
        private System.Windows.Forms.Button button_Import;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Total;
    }
}