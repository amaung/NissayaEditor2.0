namespace NissayaEditor
{
    partial class Form_PageReadingView
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
            this.panel_ReadView = new System.Windows.Forms.Panel();
            this.button_UpArrow = new System.Windows.Forms.Button();
            this.button_FullPageView = new System.Windows.Forms.Button();
            this.button_DownArrow = new System.Windows.Forms.Button();
            this.button_X = new System.Windows.Forms.Button();
            this.button_ZoomIn = new System.Windows.Forms.Button();
            this.button_ZoomOut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel_ReadView
            // 
            this.panel_ReadView.BackColor = System.Drawing.SystemColors.Control;
            this.panel_ReadView.Location = new System.Drawing.Point(12, 5);
            this.panel_ReadView.Name = "panel_ReadView";
            this.panel_ReadView.Size = new System.Drawing.Size(515, 100);
            this.panel_ReadView.TabIndex = 0;
            this.panel_ReadView.Paint += new System.Windows.Forms.PaintEventHandler(this.PageReadingView_OnPaint);
            // 
            // button_UpArrow
            // 
            this.button_UpArrow.Location = new System.Drawing.Point(560, 12);
            this.button_UpArrow.Name = "button_UpArrow";
            this.button_UpArrow.Size = new System.Drawing.Size(26, 26);
            this.button_UpArrow.TabIndex = 1;
            this.button_UpArrow.UseVisualStyleBackColor = true;
            this.button_UpArrow.Click += new System.EventHandler(this.button_UpArrow_Click);
            this.button_UpArrow.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UpDown_KeyAction);
            this.button_UpArrow.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UpDown_KeyAction);
            // 
            // button_FullPageView
            // 
            this.button_FullPageView.Location = new System.Drawing.Point(560, 45);
            this.button_FullPageView.Name = "button_FullPageView";
            this.button_FullPageView.Size = new System.Drawing.Size(26, 26);
            this.button_FullPageView.TabIndex = 2;
            this.button_FullPageView.UseVisualStyleBackColor = true;
            this.button_FullPageView.Click += new System.EventHandler(this.button_FullPageView_Click);
            // 
            // button_DownArrow
            // 
            this.button_DownArrow.Location = new System.Drawing.Point(560, 76);
            this.button_DownArrow.Name = "button_DownArrow";
            this.button_DownArrow.Size = new System.Drawing.Size(26, 26);
            this.button_DownArrow.TabIndex = 3;
            this.button_DownArrow.UseVisualStyleBackColor = true;
            this.button_DownArrow.Click += new System.EventHandler(this.button_DownArrow_Click);
            this.button_DownArrow.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UpDown_KeyAction);
            this.button_DownArrow.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UpDown_KeyAction);
            // 
            // button_X
            // 
            this.button_X.Location = new System.Drawing.Point(537, 10);
            this.button_X.Name = "button_X";
            this.button_X.Size = new System.Drawing.Size(20, 28);
            this.button_X.TabIndex = 4;
            this.button_X.Text = "X";
            this.button_X.UseVisualStyleBackColor = true;
            this.button_X.Click += new System.EventHandler(this.button_X_Click);
            // 
            // button_ZoomIn
            // 
            this.button_ZoomIn.Location = new System.Drawing.Point(537, 42);
            this.button_ZoomIn.Name = "button_ZoomIn";
            this.button_ZoomIn.Size = new System.Drawing.Size(20, 28);
            this.button_ZoomIn.TabIndex = 5;
            this.button_ZoomIn.Text = "+";
            this.button_ZoomIn.UseVisualStyleBackColor = true;
            this.button_ZoomIn.Click += new System.EventHandler(this.button_ZoomIn_Click);
            // 
            // button_ZoomOut
            // 
            this.button_ZoomOut.Location = new System.Drawing.Point(537, 75);
            this.button_ZoomOut.Name = "button_ZoomOut";
            this.button_ZoomOut.Size = new System.Drawing.Size(20, 28);
            this.button_ZoomOut.TabIndex = 6;
            this.button_ZoomOut.Text = "-";
            this.button_ZoomOut.UseVisualStyleBackColor = true;
            this.button_ZoomOut.Click += new System.EventHandler(this.button_ZoomOut_Click);
            // 
            // Form_PageReadingView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(598, 110);
            this.Controls.Add(this.button_ZoomOut);
            this.Controls.Add(this.button_ZoomIn);
            this.Controls.Add(this.button_X);
            this.Controls.Add(this.button_DownArrow);
            this.Controls.Add(this.button_FullPageView);
            this.Controls.Add(this.button_UpArrow);
            this.Controls.Add(this.panel_ReadView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(100, 50);
            this.Name = "Form_PageReadingView";
            this.Text = "Source Page Viewer";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PageReadingView_OnClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PageReadingView_OnPaint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_ReadView;
        private System.Windows.Forms.Button button_UpArrow;
        private System.Windows.Forms.Button button_FullPageView;
        private System.Windows.Forms.Button button_DownArrow;
        private System.Windows.Forms.Button button_X;
        private System.Windows.Forms.Button button_ZoomIn;
        private System.Windows.Forms.Button button_ZoomOut;
    }
}