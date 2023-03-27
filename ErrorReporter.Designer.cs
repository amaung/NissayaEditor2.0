namespace NissayaEditor
{
    partial class ErrorReporter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorReporter));
            this.label_Wrong = new System.Windows.Forms.Label();
            this.label_Correct = new System.Windows.Forms.Label();
            this.button_Change = new System.Windows.Forms.Button();
            this.pictureBox_Correct = new System.Windows.Forms.PictureBox();
            this.pictureBox_Wrong = new System.Windows.Forms.PictureBox();
            this.label_ErrNo = new System.Windows.Forms.Label();
            this.button_Quit = new System.Windows.Forms.Button();
            this.button_Prev = new System.Windows.Forms.Button();
            this.button_Next = new System.Windows.Forms.Button();
            this.textBox_Ref = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Correct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Wrong)).BeginInit();
            this.SuspendLayout();
            // 
            // label_Wrong
            // 
            this.label_Wrong.AutoSize = true;
            this.label_Wrong.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Wrong.Location = new System.Drawing.Point(52, 10);
            this.label_Wrong.Name = "label_Wrong";
            this.label_Wrong.Size = new System.Drawing.Size(61, 29);
            this.label_Wrong.TabIndex = 2;
            this.label_Wrong.Text = "Wrong";
            // 
            // label_Correct
            // 
            this.label_Correct.AutoSize = true;
            this.label_Correct.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Correct.Location = new System.Drawing.Point(53, 43);
            this.label_Correct.Name = "label_Correct";
            this.label_Correct.Size = new System.Drawing.Size(64, 29);
            this.label_Correct.TabIndex = 3;
            this.label_Correct.Text = "Correct";
            // 
            // button_Change
            // 
            this.button_Change.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Change.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Change.Location = new System.Drawing.Point(23, 150);
            this.button_Change.Name = "button_Change";
            this.button_Change.Size = new System.Drawing.Size(65, 28);
            this.button_Change.TabIndex = 5;
            this.button_Change.Text = "Correct";
            this.button_Change.UseVisualStyleBackColor = true;
            this.button_Change.Click += new System.EventHandler(this.button_Change_Click);
            // 
            // pictureBox_Correct
            // 
            this.pictureBox_Correct.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_Correct.Image")));
            this.pictureBox_Correct.InitialImage = null;
            this.pictureBox_Correct.Location = new System.Drawing.Point(20, 47);
            this.pictureBox_Correct.Name = "pictureBox_Correct";
            this.pictureBox_Correct.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_Correct.TabIndex = 1;
            this.pictureBox_Correct.TabStop = false;
            // 
            // pictureBox_Wrong
            // 
            this.pictureBox_Wrong.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_Wrong.Image")));
            this.pictureBox_Wrong.InitialImage = null;
            this.pictureBox_Wrong.Location = new System.Drawing.Point(20, 14);
            this.pictureBox_Wrong.Name = "pictureBox_Wrong";
            this.pictureBox_Wrong.Size = new System.Drawing.Size(18, 18);
            this.pictureBox_Wrong.TabIndex = 0;
            this.pictureBox_Wrong.TabStop = false;
            // 
            // label_ErrNo
            // 
            this.label_ErrNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_ErrNo.AutoSize = true;
            this.label_ErrNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ErrNo.Location = new System.Drawing.Point(310, 16);
            this.label_ErrNo.Name = "label_ErrNo";
            this.label_ErrNo.Size = new System.Drawing.Size(26, 16);
            this.label_ErrNo.TabIndex = 9;
            this.label_ErrNo.Text = "0/0";
            // 
            // button_Quit
            // 
            this.button_Quit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Quit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Quit.Location = new System.Drawing.Point(280, 150);
            this.button_Quit.Name = "button_Quit";
            this.button_Quit.Size = new System.Drawing.Size(65, 28);
            this.button_Quit.TabIndex = 10;
            this.button_Quit.Text = "Quit";
            this.button_Quit.UseVisualStyleBackColor = true;
            this.button_Quit.Click += new System.EventHandler(this.button_Quit_Click);
            // 
            // button_Prev
            // 
            this.button_Prev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Prev.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Prev.Image = ((System.Drawing.Image)(resources.GetObject("button_Prev.Image")));
            this.button_Prev.Location = new System.Drawing.Point(133, 150);
            this.button_Prev.Name = "button_Prev";
            this.button_Prev.Size = new System.Drawing.Size(28, 28);
            this.button_Prev.TabIndex = 11;
            this.button_Prev.UseVisualStyleBackColor = true;
            this.button_Prev.Click += new System.EventHandler(this.button_Prev_Click);
            // 
            // button_Next
            // 
            this.button_Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Next.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Next.Image = ((System.Drawing.Image)(resources.GetObject("button_Next.Image")));
            this.button_Next.Location = new System.Drawing.Point(204, 150);
            this.button_Next.Name = "button_Next";
            this.button_Next.Size = new System.Drawing.Size(28, 28);
            this.button_Next.TabIndex = 12;
            this.button_Next.UseVisualStyleBackColor = true;
            this.button_Next.Click += new System.EventHandler(this.button_Next_Click);
            // 
            // textBox_Ref
            // 
            this.textBox_Ref.Font = new System.Drawing.Font("Myanmar Text", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Ref.Location = new System.Drawing.Point(20, 77);
            this.textBox_Ref.Multiline = true;
            this.textBox_Ref.Name = "textBox_Ref";
            this.textBox_Ref.ReadOnly = true;
            this.textBox_Ref.Size = new System.Drawing.Size(325, 60);
            this.textBox_Ref.TabIndex = 13;
            // 
            // ErrorReporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 188);
            this.Controls.Add(this.textBox_Ref);
            this.Controls.Add(this.button_Next);
            this.Controls.Add(this.button_Prev);
            this.Controls.Add(this.button_Quit);
            this.Controls.Add(this.label_ErrNo);
            this.Controls.Add(this.button_Change);
            this.Controls.Add(this.label_Correct);
            this.Controls.Add(this.label_Wrong);
            this.Controls.Add(this.pictureBox_Correct);
            this.Controls.Add(this.pictureBox_Wrong);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErrorReporter";
            this.Text = "Error Reporter";
            this.Load += new System.EventHandler(this.ErrorReporter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Correct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Wrong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Wrong;
        private System.Windows.Forms.PictureBox pictureBox_Correct;
        private System.Windows.Forms.Label label_Wrong;
        private System.Windows.Forms.Label label_Correct;
        private System.Windows.Forms.Button button_Change;
        private System.Windows.Forms.Label label_ErrNo;
        private System.Windows.Forms.Button button_Quit;
        private System.Windows.Forms.Button button_Prev;
        private System.Windows.Forms.Button button_Next;
        private System.Windows.Forms.TextBox textBox_Ref;
    }
}