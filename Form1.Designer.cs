namespace NissayaEditor
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
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            newCtrlNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            bookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            mNCatalogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            printCtrlPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            paliTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            plainTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            fullDocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            tableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            dataBoxCtrlDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            checkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            checkSpellingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            autoFillToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            messagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            downloadSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            profileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage2 = new System.Windows.Forms.TabPage();
            richTextBox1 = new System.Windows.Forms.RichTextBox();
            button_Find = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label_DataStatus = new System.Windows.Forms.Label();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            textBox_Find = new System.Windows.Forms.TextBox();
            button_X = new System.Windows.Forms.Button();
            button_FindNext = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            textBox_FindStatus = new System.Windows.Forms.TextBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            textBox1 = new System.Windows.Forms.TextBox();
            button_FindPrev = new System.Windows.Forms.Button();
            button_jjha = new System.Windows.Forms.Button();
            button_kinsi = new System.Windows.Forms.Button();
            button_ddha = new System.Windows.Forms.Button();
            button_ttha = new System.Windows.Forms.Button();
            label_MN = new System.Windows.Forms.Label();
            textBox_MN = new System.Windows.Forms.TextBox();
            label_MNTitle = new System.Windows.Forms.Label();
            label_MNTitle_ROM = new System.Windows.Forms.Label();
            menuStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, viewToolStripMenuItem, dataToolStripMenuItem, serverToolStripMenuItem, aboutToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(774, 25);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { newCtrlNToolStripMenuItem, loadToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem, closeToolStripMenuItem, mNCatalogToolStripMenuItem, printCtrlPToolStripMenuItem, exitToolStripMenuItem, toolStripSeparator1 });
            fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            fileToolStripMenuItem.Text = "File";
            // 
            // newCtrlNToolStripMenuItem
            // 
            newCtrlNToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem1, bookToolStripMenuItem });
            newCtrlNToolStripMenuItem.Name = "newCtrlNToolStripMenuItem";
            newCtrlNToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            newCtrlNToolStripMenuItem.Text = "New";
            newCtrlNToolStripMenuItem.Click += newCtrlNToolStripMenuItem_Click;
            // 
            // fileToolStripMenuItem1
            // 
            fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            fileToolStripMenuItem1.Size = new System.Drawing.Size(105, 22);
            fileToolStripMenuItem1.Text = "File";
            fileToolStripMenuItem1.Click += fileToolStripMenuItem1_Click;
            // 
            // bookToolStripMenuItem
            // 
            bookToolStripMenuItem.Enabled = false;
            bookToolStripMenuItem.Name = "bookToolStripMenuItem";
            bookToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            bookToolStripMenuItem.Text = "Book";
            bookToolStripMenuItem.Click += bookToolStripMenuItem_Click;
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            loadToolStripMenuItem.Text = "Open <Ctrl-O>";
            loadToolStripMenuItem.Click += loadToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            saveToolStripMenuItem.Text = "Save <Ctrl-S>";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            saveAsToolStripMenuItem.Text = "Save As";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            closeToolStripMenuItem.Text = "Close <Ctrl-W>";
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            // 
            // mNCatalogToolStripMenuItem
            // 
            mNCatalogToolStripMenuItem.Name = "mNCatalogToolStripMenuItem";
            mNCatalogToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            mNCatalogToolStripMenuItem.Text = "Work Assignments";
            mNCatalogToolStripMenuItem.Click += mNCatalogToolStripMenuItem_Click;
            // 
            // printCtrlPToolStripMenuItem
            // 
            printCtrlPToolStripMenuItem.Name = "printCtrlPToolStripMenuItem";
            printCtrlPToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            printCtrlPToolStripMenuItem.Text = "Print <Ctrl-P>";
            printCtrlPToolStripMenuItem.Click += printCtrlPToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            exitToolStripMenuItem.Text = "Exit <Ctrl-Q>";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(180, 6);
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { paliTextToolStripMenuItem, plainTextToolStripMenuItem, fullDocToolStripMenuItem, tableToolStripMenuItem, dataBoxCtrlDToolStripMenuItem });
            viewToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
            viewToolStripMenuItem.Text = "View";
            // 
            // paliTextToolStripMenuItem
            // 
            paliTextToolStripMenuItem.Name = "paliTextToolStripMenuItem";
            paliTextToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            paliTextToolStripMenuItem.Text = "Pāḷi Text Only";
            paliTextToolStripMenuItem.Click += paliTextToolStripMenuItem_Click;
            // 
            // plainTextToolStripMenuItem
            // 
            plainTextToolStripMenuItem.Name = "plainTextToolStripMenuItem";
            plainTextToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            plainTextToolStripMenuItem.Text = "Translation Only";
            plainTextToolStripMenuItem.Click += plainTextToolStripMenuItem_Click;
            // 
            // fullDocToolStripMenuItem
            // 
            fullDocToolStripMenuItem.Name = "fullDocToolStripMenuItem";
            fullDocToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            fullDocToolStripMenuItem.Text = "Full Doc";
            fullDocToolStripMenuItem.Click += fullDocToolStripMenuItem_Click;
            // 
            // tableToolStripMenuItem
            // 
            tableToolStripMenuItem.Name = "tableToolStripMenuItem";
            tableToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            tableToolStripMenuItem.Text = "Tabular";
            tableToolStripMenuItem.Click += tableToolStripMenuItem_Click;
            // 
            // dataBoxCtrlDToolStripMenuItem
            // 
            dataBoxCtrlDToolStripMenuItem.Checked = true;
            dataBoxCtrlDToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            dataBoxCtrlDToolStripMenuItem.Name = "dataBoxCtrlDToolStripMenuItem";
            dataBoxCtrlDToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            dataBoxCtrlDToolStripMenuItem.Text = "Data Box <Ctrl-D>";
            dataBoxCtrlDToolStripMenuItem.Click += dataBoxCtrlDToolStripMenuItem_Click;
            // 
            // dataToolStripMenuItem
            // 
            dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { checkToolStripMenuItem, checkSpellingsToolStripMenuItem, autoFillToolStripMenuItem });
            dataToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            dataToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
            dataToolStripMenuItem.Text = "Data";
            // 
            // checkToolStripMenuItem
            // 
            checkToolStripMenuItem.Name = "checkToolStripMenuItem";
            checkToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            checkToolStripMenuItem.Text = "Check Blank-entries and @";
            checkToolStripMenuItem.Click += checkToolStripMenuItem_Click;
            // 
            // checkSpellingsToolStripMenuItem
            // 
            checkSpellingsToolStripMenuItem.Name = "checkSpellingsToolStripMenuItem";
            checkSpellingsToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            checkSpellingsToolStripMenuItem.Text = "Check Spellings";
            checkSpellingsToolStripMenuItem.Click += checkSpellingsToolStripMenuItem_Click;
            // 
            // autoFillToolStripMenuItem
            // 
            autoFillToolStripMenuItem.Checked = true;
            autoFillToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            autoFillToolStripMenuItem.Name = "autoFillToolStripMenuItem";
            autoFillToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            autoFillToolStripMenuItem.Text = "Auto Fill";
            autoFillToolStripMenuItem.Click += autoFillToolStripMenuItem_Click;
            // 
            // serverToolStripMenuItem
            // 
            serverToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { loginToolStripMenuItem, messagesToolStripMenuItem, downloadToolStripMenuItem, downloadSourceToolStripMenuItem, profileToolStripMenuItem, logoutToolStripMenuItem });
            serverToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            serverToolStripMenuItem.Size = new System.Drawing.Size(57, 21);
            serverToolStripMenuItem.Text = "Server";
            // 
            // loginToolStripMenuItem
            // 
            loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            loginToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            loginToolStripMenuItem.Text = "Login";
            loginToolStripMenuItem.Click += loginToolStripMenuItem_Click;
            // 
            // messagesToolStripMenuItem
            // 
            messagesToolStripMenuItem.Name = "messagesToolStripMenuItem";
            messagesToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            messagesToolStripMenuItem.Text = "Messages";
            messagesToolStripMenuItem.Click += messagesToolStripMenuItem_Click;
            // 
            // downloadToolStripMenuItem
            // 
            downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            downloadToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            downloadToolStripMenuItem.Text = "Download Sutta";
            downloadToolStripMenuItem.Click += downloadToolStripMenuItem_Click_1;
            // 
            // downloadSourceToolStripMenuItem
            // 
            downloadSourceToolStripMenuItem.Name = "downloadSourceToolStripMenuItem";
            downloadSourceToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            downloadSourceToolStripMenuItem.Text = "Download Source Files";
            downloadSourceToolStripMenuItem.Click += downloadSourceToolStripMenuItem_Click;
            // 
            // profileToolStripMenuItem
            // 
            profileToolStripMenuItem.Name = "profileToolStripMenuItem";
            profileToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            profileToolStripMenuItem.Text = "Profile";
            profileToolStripMenuItem.Click += profileToolStripMenuItem_Click;
            // 
            // logoutToolStripMenuItem
            // 
            logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            logoutToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            logoutToolStripMenuItem.Text = "Logout";
            logoutToolStripMenuItem.Click += logoutToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new System.Drawing.Size(55, 21);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tabControl1.Location = new System.Drawing.Point(70, 70);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(694, 445);
            tabControl1.TabIndex = 2;
            tabControl1.SelectedIndexChanged += SizeUpdate;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(richTextBox1);
            tabPage2.Location = new System.Drawing.Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(3);
            tabPage2.Size = new System.Drawing.Size(686, 417);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "File1";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            richTextBox1.Enabled = false;
            richTextBox1.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            richTextBox1.Location = new System.Drawing.Point(5, 8);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new System.Drawing.Size(674, 402);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            richTextBox1.Visible = false;
            // 
            // button_Find
            // 
            button_Find.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            button_Find.Location = new System.Drawing.Point(242, 37);
            button_Find.Name = "button_Find";
            button_Find.Size = new System.Drawing.Size(58, 25);
            button_Find.TabIndex = 5;
            button_Find.Text = "Find";
            button_Find.UseVisualStyleBackColor = true;
            button_Find.Click += button_Find_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(15, 41);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(82, 16);
            label1.TabIndex = 1;
            label1.Text = "Data Status :";
            // 
            // label_DataStatus
            // 
            label_DataStatus.AutoSize = true;
            label_DataStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label_DataStatus.Location = new System.Drawing.Point(94, 41);
            label_DataStatus.Name = "label_DataStatus";
            label_DataStatus.Size = new System.Drawing.Size(116, 16);
            label_DataStatus.TabIndex = 6;
            label_DataStatus.Text = "Read-Only Ready";
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Enabled = false;
            dataGridView1.Location = new System.Drawing.Point(35, 469);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new System.Drawing.Size(74, 40);
            dataGridView1.TabIndex = 7;
            dataGridView1.Visible = false;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            dataGridView1.CellLeave += dataGridView1_CellLeave;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            dataGridView1.EditingControlShowing += dataGridView1_EditingControlShow;
            dataGridView1.RowsAdded += dataGridView1_RowsAdded;
            dataGridView1.KeyDown += dataGridView1_KeyDown;
            dataGridView1.MouseClick += dataGridView1_MouseClick;
            // 
            // textBox_Find
            // 
            textBox_Find.AcceptsReturn = true;
            textBox_Find.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox_Find.Font = new System.Drawing.Font("Myanmar Text", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBox_Find.Location = new System.Drawing.Point(314, 38);
            textBox_Find.Name = "textBox_Find";
            textBox_Find.Size = new System.Drawing.Size(102, 25);
            textBox_Find.TabIndex = 8;
            textBox_Find.KeyDown += textBox_Find_KeyDown;
            // 
            // button_X
            // 
            button_X.BackColor = System.Drawing.Color.White;
            button_X.FlatAppearance.BorderSize = 0;
            button_X.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button_X.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            button_X.Location = new System.Drawing.Point(522, 39);
            button_X.Name = "button_X";
            button_X.Size = new System.Drawing.Size(18, 23);
            button_X.TabIndex = 1;
            button_X.TabStop = false;
            button_X.Text = "X";
            button_X.UseVisualStyleBackColor = false;
            button_X.Click += button_X_Click;
            // 
            // button_FindNext
            // 
            button_FindNext.BackColor = System.Drawing.Color.White;
            button_FindNext.FlatAppearance.BorderSize = 0;
            button_FindNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button_FindNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            button_FindNext.Image = Properties.Resources.DownArrowHead;
            button_FindNext.Location = new System.Drawing.Point(500, 39);
            button_FindNext.Name = "button_FindNext";
            button_FindNext.Size = new System.Drawing.Size(23, 23);
            button_FindNext.TabIndex = 12;
            button_FindNext.TabStop = false;
            button_FindNext.UseVisualStyleBackColor = false;
            button_FindNext.Click += button_FindNext_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = System.Drawing.Color.White;
            label2.ForeColor = System.Drawing.SystemColors.GrayText;
            label2.Location = new System.Drawing.Point(466, 39);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(14, 20);
            label2.TabIndex = 13;
            label2.Text = "|";
            // 
            // textBox_FindStatus
            // 
            textBox_FindStatus.AcceptsReturn = true;
            textBox_FindStatus.BackColor = System.Drawing.Color.White;
            textBox_FindStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox_FindStatus.Enabled = false;
            textBox_FindStatus.Font = new System.Drawing.Font("Myanmar Text", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBox_FindStatus.ForeColor = System.Drawing.SystemColors.GrayText;
            textBox_FindStatus.Location = new System.Drawing.Point(420, 43);
            textBox_FindStatus.Name = "textBox_FindStatus";
            textBox_FindStatus.ReadOnly = true;
            textBox_FindStatus.Size = new System.Drawing.Size(46, 23);
            textBox_FindStatus.TabIndex = 14;
            textBox_FindStatus.Text = "100/100";
            textBox_FindStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            textBox_FindStatus.WordWrap = false;
            // 
            // textBox1
            // 
            textBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBox1.Location = new System.Drawing.Point(316, 495);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(100, 20);
            textBox1.TabIndex = 15;
            textBox1.Visible = false;
            // 
            // button_FindPrev
            // 
            button_FindPrev.BackColor = System.Drawing.Color.White;
            button_FindPrev.FlatAppearance.BorderSize = 0;
            button_FindPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button_FindPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            button_FindPrev.Image = Properties.Resources.UpArrowHead;
            button_FindPrev.Location = new System.Drawing.Point(484, 38);
            button_FindPrev.Name = "button_FindPrev";
            button_FindPrev.Size = new System.Drawing.Size(18, 23);
            button_FindPrev.TabIndex = 11;
            button_FindPrev.TabStop = false;
            button_FindPrev.UseVisualStyleBackColor = false;
            button_FindPrev.Click += button_FindPrev_Click;
            // 
            // button_jjha
            // 
            button_jjha.Font = new System.Drawing.Font("Myanmar Text", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            button_jjha.Location = new System.Drawing.Point(561, 36);
            button_jjha.Name = "button_jjha";
            button_jjha.Size = new System.Drawing.Size(25, 28);
            button_jjha.TabIndex = 16;
            button_jjha.Text = "ဇ္ဈ";
            button_jjha.UseVisualStyleBackColor = true;
            button_jjha.Click += HotKey;
            // 
            // button_kinsi
            // 
            button_kinsi.Font = new System.Drawing.Font("Myanmar Text", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            button_kinsi.Location = new System.Drawing.Point(584, 36);
            button_kinsi.Name = "button_kinsi";
            button_kinsi.Size = new System.Drawing.Size(25, 28);
            button_kinsi.TabIndex = 17;
            button_kinsi.Text = "င်္";
            button_kinsi.UseVisualStyleBackColor = true;
            button_kinsi.Click += HotKey;
            // 
            // button_ddha
            // 
            button_ddha.Font = new System.Drawing.Font("Myanmar Text", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            button_ddha.Location = new System.Drawing.Point(630, 36);
            button_ddha.Name = "button_ddha";
            button_ddha.Size = new System.Drawing.Size(25, 28);
            button_ddha.TabIndex = 19;
            button_ddha.Text = "ဍ္ဎ";
            button_ddha.UseVisualStyleBackColor = true;
            button_ddha.Click += HotKey;
            // 
            // button_ttha
            // 
            button_ttha.Font = new System.Drawing.Font("Myanmar Text", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            button_ttha.Location = new System.Drawing.Point(606, 36);
            button_ttha.Name = "button_ttha";
            button_ttha.Size = new System.Drawing.Size(25, 28);
            button_ttha.TabIndex = 18;
            button_ttha.Text = "ဋ္ဌ";
            button_ttha.UseVisualStyleBackColor = true;
            button_ttha.Click += HotKey;
            // 
            // label_MN
            // 
            label_MN.AutoSize = true;
            label_MN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label_MN.Location = new System.Drawing.Point(669, 44);
            label_MN.Name = "label_MN";
            label_MN.Size = new System.Drawing.Size(30, 15);
            label_MN.TabIndex = 20;
            label_MN.Text = "MN:";
            // 
            // textBox_MN
            // 
            textBox_MN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBox_MN.Location = new System.Drawing.Point(697, 41);
            textBox_MN.Name = "textBox_MN";
            textBox_MN.Size = new System.Drawing.Size(50, 22);
            textBox_MN.TabIndex = 21;
            textBox_MN.MouseClick += MN_Click;
            textBox_MN.TextChanged += MN_TextChanged;
            textBox_MN.Leave += MN_Leave;
            textBox_MN.MouseLeave += MN_Leave;
            textBox_MN.Validating += MN_Validating;
            // 
            // label_MNTitle
            // 
            label_MNTitle.AutoSize = true;
            label_MNTitle.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label_MNTitle.Location = new System.Drawing.Point(753, 39);
            label_MNTitle.Name = "label_MNTitle";
            label_MNTitle.Size = new System.Drawing.Size(0, 29);
            label_MNTitle.TabIndex = 22;
            // 
            // label_MNTitle_ROM
            // 
            label_MNTitle_ROM.AutoSize = true;
            label_MNTitle_ROM.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label_MNTitle_ROM.Location = new System.Drawing.Point(455, 10);
            label_MNTitle_ROM.Name = "label_MNTitle_ROM";
            label_MNTitle_ROM.Size = new System.Drawing.Size(86, 15);
            label_MNTitle_ROM.TabIndex = 23;
            label_MNTitle_ROM.Text = "MNTitle_ROM";
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Gainsboro;
            ClientSize = new System.Drawing.Size(774, 521);
            Controls.Add(label_MNTitle_ROM);
            Controls.Add(label_MNTitle);
            Controls.Add(textBox_MN);
            Controls.Add(label_MN);
            Controls.Add(button_ddha);
            Controls.Add(button_ttha);
            Controls.Add(button_kinsi);
            Controls.Add(button_jjha);
            Controls.Add(textBox1);
            Controls.Add(textBox_FindStatus);
            Controls.Add(label2);
            Controls.Add(button_FindNext);
            Controls.Add(button_FindPrev);
            Controls.Add(button_X);
            Controls.Add(textBox_Find);
            Controls.Add(dataGridView1);
            Controls.Add(label_DataStatus);
            Controls.Add(label1);
            Controls.Add(button_Find);
            Controls.Add(tabControl1);
            Controls.Add(menuStrip1);
            Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(4);
            Name = "Form1";
            Text = "Nissaya Editor";
            FormClosing += Form1_Closing;
            Load += Form1_Load;
            SizeChanged += SizeUpdate;
            Paint += Form1_Paint;
            KeyDown += Form1_KeyDown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paliTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plainTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button_Find;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_DataStatus;
        private System.Windows.Forms.ToolStripMenuItem tableToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem fullDocToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox_Find;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button button_X;
        private System.Windows.Forms.Button button_FindPrev;
        private System.Windows.Forms.Button button_FindNext;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_FindStatus;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printCtrlPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newCtrlNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataBoxCtrlDToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem bookToolStripMenuItem;
        private System.Windows.Forms.Button button_jjha;
        private System.Windows.Forms.Button button_kinsi;
        private System.Windows.Forms.Button button_ddha;
        private System.Windows.Forms.Button button_ttha;
        private System.Windows.Forms.Label label_MN;
        private System.Windows.Forms.TextBox textBox_MN;
        private System.Windows.Forms.ToolStripMenuItem mNCatalogToolStripMenuItem;
        private System.Windows.Forms.Label label_MNTitle;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkSpellingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoFillToolStripMenuItem;
        private System.Windows.Forms.Label label_MNTitle_ROM;
        private System.Windows.Forms.ToolStripMenuItem serverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem messagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadSourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem profileToolStripMenuItem;
    }
}

