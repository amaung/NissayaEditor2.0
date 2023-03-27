namespace NissayaEditor
{
    partial class SpellingCheckerSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpellingCheckerSettings));
            this.checkBox_InvMyanChars = new System.Windows.Forms.CheckBox();
            this.checkBox_GrammarErrs = new System.Windows.Forms.CheckBox();
            this.checkBox_MismatchedPairs = new System.Windows.Forms.CheckBox();
            this.checkBox_ZeroWalone = new System.Windows.Forms.CheckBox();
            this.checkBox_PhoneticWords = new System.Windows.Forms.CheckBox();
            this.checkBox_OneUnitErrs = new System.Windows.Forms.CheckBox();
            this.button_Check = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkBox_InvMyanChars
            // 
            this.checkBox_InvMyanChars.AutoSize = true;
            this.checkBox_InvMyanChars.Checked = true;
            this.checkBox_InvMyanChars.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_InvMyanChars.Location = new System.Drawing.Point(27, 29);
            this.checkBox_InvMyanChars.Name = "checkBox_InvMyanChars";
            this.checkBox_InvMyanChars.Size = new System.Drawing.Size(157, 17);
            this.checkBox_InvMyanChars.TabIndex = 0;
            this.checkBox_InvMyanChars.Text = "Invalid Myanmar Characters";
            this.checkBox_InvMyanChars.UseVisualStyleBackColor = true;
            // 
            // checkBox_GrammarErrs
            // 
            this.checkBox_GrammarErrs.AutoSize = true;
            this.checkBox_GrammarErrs.Checked = true;
            this.checkBox_GrammarErrs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_GrammarErrs.Location = new System.Drawing.Point(27, 63);
            this.checkBox_GrammarErrs.Name = "checkBox_GrammarErrs";
            this.checkBox_GrammarErrs.Size = new System.Drawing.Size(173, 17);
            this.checkBox_GrammarErrs.TabIndex = 1;
            this.checkBox_GrammarErrs.Text = "Basic Myanmar Grammar Errors";
            this.checkBox_GrammarErrs.UseVisualStyleBackColor = true;
            // 
            // checkBox_MismatchedPairs
            // 
            this.checkBox_MismatchedPairs.AutoSize = true;
            this.checkBox_MismatchedPairs.Checked = true;
            this.checkBox_MismatchedPairs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_MismatchedPairs.Location = new System.Drawing.Point(27, 97);
            this.checkBox_MismatchedPairs.Name = "checkBox_MismatchedPairs";
            this.checkBox_MismatchedPairs.Size = new System.Drawing.Size(146, 17);
            this.checkBox_MismatchedPairs.TabIndex = 2;
            this.checkBox_MismatchedPairs.Text = "Mismatched Symbol Pairs";
            this.checkBox_MismatchedPairs.UseVisualStyleBackColor = true;
            // 
            // checkBox_ZeroWalone
            // 
            this.checkBox_ZeroWalone.AutoSize = true;
            this.checkBox_ZeroWalone.Checked = true;
            this.checkBox_ZeroWalone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_ZeroWalone.Location = new System.Drawing.Point(27, 132);
            this.checkBox_ZeroWalone.Name = "checkBox_ZeroWalone";
            this.checkBox_ZeroWalone.Size = new System.Drawing.Size(120, 17);
            this.checkBox_ZeroWalone.TabIndex = 3;
            this.checkBox_ZeroWalone.Text = "Zero/Walone Errors";
            this.checkBox_ZeroWalone.UseVisualStyleBackColor = true;
            // 
            // checkBox_PhoneticWords
            // 
            this.checkBox_PhoneticWords.AutoSize = true;
            this.checkBox_PhoneticWords.Checked = true;
            this.checkBox_PhoneticWords.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_PhoneticWords.Location = new System.Drawing.Point(27, 168);
            this.checkBox_PhoneticWords.Name = "checkBox_PhoneticWords";
            this.checkBox_PhoneticWords.Size = new System.Drawing.Size(102, 17);
            this.checkBox_PhoneticWords.TabIndex = 4;
            this.checkBox_PhoneticWords.Text = "Phonetic Words";
            this.checkBox_PhoneticWords.UseVisualStyleBackColor = true;
            // 
            // checkBox_OneUnitErrs
            // 
            this.checkBox_OneUnitErrs.AutoSize = true;
            this.checkBox_OneUnitErrs.Checked = true;
            this.checkBox_OneUnitErrs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_OneUnitErrs.Location = new System.Drawing.Point(27, 205);
            this.checkBox_OneUnitErrs.Name = "checkBox_OneUnitErrs";
            this.checkBox_OneUnitErrs.Size = new System.Drawing.Size(98, 17);
            this.checkBox_OneUnitErrs.TabIndex = 5;
            this.checkBox_OneUnitErrs.Text = "One Unit Errors";
            this.checkBox_OneUnitErrs.UseVisualStyleBackColor = true;
            // 
            // button_Check
            // 
            this.button_Check.Location = new System.Drawing.Point(233, 202);
            this.button_Check.Name = "button_Check";
            this.button_Check.Size = new System.Drawing.Size(75, 28);
            this.button_Check.TabIndex = 6;
            this.button_Check.Text = "Check";
            this.button_Check.UseVisualStyleBackColor = true;
            this.button_Check.Click += new System.EventHandler(this.button_Check_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(233, 163);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 28);
            this.button_Cancel.TabIndex = 7;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // SpellingCheckerSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 247);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Check);
            this.Controls.Add(this.checkBox_OneUnitErrs);
            this.Controls.Add(this.checkBox_PhoneticWords);
            this.Controls.Add(this.checkBox_ZeroWalone);
            this.Controls.Add(this.checkBox_MismatchedPairs);
            this.Controls.Add(this.checkBox_GrammarErrs);
            this.Controls.Add(this.checkBox_InvMyanChars);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SpellingCheckerSettings";
            this.Text = "Spelling Checker Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_InvMyanChars;
        private System.Windows.Forms.CheckBox checkBox_GrammarErrs;
        private System.Windows.Forms.CheckBox checkBox_MismatchedPairs;
        private System.Windows.Forms.CheckBox checkBox_ZeroWalone;
        private System.Windows.Forms.CheckBox checkBox_PhoneticWords;
        private System.Windows.Forms.CheckBox checkBox_OneUnitErrs;
        private System.Windows.Forms.Button button_Check;
        private System.Windows.Forms.Button button_Cancel;
    }
}