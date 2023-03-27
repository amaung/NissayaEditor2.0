namespace NissayaEditor
{
    partial class Form_Messages
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
            this.label1 = new System.Windows.Forms.Label();
            this.label_UserID = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_Find = new System.Windows.Forms.Button();
            this.checkBox_Date = new System.Windows.Forms.CheckBox();
            this.comboBox_Type = new System.Windows.Forms.ComboBox();
            this.dateTimePicker_End = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_Start = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox_Body = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_Subject = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button_Close = new System.Windows.Forms.Button();
            this.textBox_UserID = new System.Windows.Forms.TextBox();
            this.button_Send = new System.Windows.Forms.Button();
            this.button_New = new System.Windows.Forms.Button();
            this.button_Download = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Historic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(26, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Messages";
            // 
            // label_UserID
            // 
            this.label_UserID.AutoSize = true;
            this.label_UserID.Location = new System.Drawing.Point(318, 26);
            this.label_UserID.Name = "label_UserID";
            this.label_UserID.Size = new System.Drawing.Size(64, 20);
            this.label_UserID.TabIndex = 1;
            this.label_UserID.Text = "User ID :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_Find);
            this.groupBox1.Controls.Add(this.checkBox_Date);
            this.groupBox1.Controls.Add(this.comboBox_Type);
            this.groupBox1.Controls.Add(this.dateTimePicker_End);
            this.groupBox1.Controls.Add(this.dateTimePicker_Start);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(26, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(720, 72);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // button_Find
            // 
            this.button_Find.Location = new System.Drawing.Point(648, 26);
            this.button_Find.Name = "button_Find";
            this.button_Find.Size = new System.Drawing.Size(55, 28);
            this.button_Find.TabIndex = 6;
            this.button_Find.Text = "Find";
            this.button_Find.UseVisualStyleBackColor = true;
            this.button_Find.Click += new System.EventHandler(this.button_Find_Click);
            // 
            // checkBox_Date
            // 
            this.checkBox_Date.AutoSize = true;
            this.checkBox_Date.Location = new System.Drawing.Point(540, 35);
            this.checkBox_Date.Name = "checkBox_Date";
            this.checkBox_Date.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Date.TabIndex = 5;
            this.checkBox_Date.UseVisualStyleBackColor = true;
            this.checkBox_Date.CheckedChanged += new System.EventHandler(this.checkBox_Date_CheckedChanged);
            // 
            // comboBox_Type
            // 
            this.comboBox_Type.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox_Type.FormattingEnabled = true;
            this.comboBox_Type.Items.AddRange(new object[] {
            "All",
            "New",
            "Read",
            "Sent"});
            this.comboBox_Type.Location = new System.Drawing.Point(571, 28);
            this.comboBox_Type.Name = "comboBox_Type";
            this.comboBox_Type.Size = new System.Drawing.Size(55, 25);
            this.comboBox_Type.TabIndex = 4;
            this.comboBox_Type.SelectedIndexChanged += new System.EventHandler(this.comboBox_Type_SelectedIndexChanged);
            // 
            // dateTimePicker_End
            // 
            this.dateTimePicker_End.Location = new System.Drawing.Point(325, 27);
            this.dateTimePicker_End.Name = "dateTimePicker_End";
            this.dateTimePicker_End.Size = new System.Drawing.Size(200, 27);
            this.dateTimePicker_End.TabIndex = 3;
            // 
            // dateTimePicker_Start
            // 
            this.dateTimePicker_Start.Location = new System.Drawing.Point(63, 27);
            this.dateTimePicker_Start.Name = "dateTimePicker_Start";
            this.dateTimePicker_Start.Size = new System.Drawing.Size(200, 27);
            this.dateTimePicker_Start.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(279, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "End :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Start :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(26, 143);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(380, 326);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Message Log";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(19, 35);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 29;
            this.dataGridView1.Size = new System.Drawing.Size(340, 272);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox_Body);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.textBox_Subject);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(418, 143);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(327, 326);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Message Detail";
            // 
            // textBox_Body
            // 
            this.textBox_Body.BackColor = System.Drawing.Color.White;
            this.textBox_Body.Location = new System.Drawing.Point(9, 123);
            this.textBox_Body.Multiline = true;
            this.textBox_Body.Name = "textBox_Body";
            this.textBox_Body.ReadOnly = true;
            this.textBox_Body.Size = new System.Drawing.Size(303, 181);
            this.textBox_Body.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "Body :";
            // 
            // textBox_Subject
            // 
            this.textBox_Subject.BackColor = System.Drawing.Color.White;
            this.textBox_Subject.Location = new System.Drawing.Point(9, 60);
            this.textBox_Subject.Name = "textBox_Subject";
            this.textBox_Subject.ReadOnly = true;
            this.textBox_Subject.Size = new System.Drawing.Size(302, 27);
            this.textBox_Subject.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Subject :";
            // 
            // button_Close
            // 
            this.button_Close.Location = new System.Drawing.Point(670, 483);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(75, 28);
            this.button_Close.TabIndex = 5;
            this.button_Close.Text = "Close";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // textBox_UserID
            // 
            this.textBox_UserID.BackColor = System.Drawing.Color.White;
            this.textBox_UserID.Location = new System.Drawing.Point(386, 22);
            this.textBox_UserID.Name = "textBox_UserID";
            this.textBox_UserID.ReadOnly = true;
            this.textBox_UserID.Size = new System.Drawing.Size(360, 27);
            this.textBox_UserID.TabIndex = 6;
            // 
            // button_Send
            // 
            this.button_Send.Location = new System.Drawing.Point(556, 484);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(75, 28);
            this.button_Send.TabIndex = 7;
            this.button_Send.Text = "Send";
            this.button_Send.UseVisualStyleBackColor = true;
            this.button_Send.Click += new System.EventHandler(this.button_Send_Click);
            // 
            // button_New
            // 
            this.button_New.Location = new System.Drawing.Point(439, 484);
            this.button_New.Name = "button_New";
            this.button_New.Size = new System.Drawing.Size(75, 28);
            this.button_New.TabIndex = 8;
            this.button_New.Text = "New";
            this.button_New.UseVisualStyleBackColor = true;
            this.button_New.Click += new System.EventHandler(this.button_New_Click);
            // 
            // button_Download
            // 
            this.button_Download.Location = new System.Drawing.Point(297, 484);
            this.button_Download.Name = "button_Download";
            this.button_Download.Size = new System.Drawing.Size(100, 28);
            this.button_Download.TabIndex = 9;
            this.button_Download.Text = "Download";
            this.button_Download.UseVisualStyleBackColor = true;
            // 
            // Form_Messages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 529);
            this.Controls.Add(this.button_Download);
            this.Controls.Add(this.button_New);
            this.Controls.Add(this.button_Send);
            this.Controls.Add(this.textBox_UserID);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label_UserID);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_Messages";
            this.Text = "Messages";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_UserID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox_Type;
        private System.Windows.Forms.DateTimePicker dateTimePicker_End;
        private System.Windows.Forms.DateTimePicker dateTimePicker_Start;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox_Body;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_Subject;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.TextBox textBox_UserID;
        private System.Windows.Forms.Button button_Send;
        private System.Windows.Forms.Button button_New;
        private System.Windows.Forms.Button button_Download;
        private System.Windows.Forms.CheckBox checkBox_Date;
        private System.Windows.Forms.Button button_Find;
    }
}