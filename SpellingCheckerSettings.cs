using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NissayaEditor
{
    public partial class SpellingCheckerSettings : Form
    {
        Form1 parent;
        public const int InvMyanChars = 0;
        public const int GrammarErrs = 1;
        public const int MismatchedPairs = 2;
        public const int ZeroWalone = 3;
        public const int PhoneticWords = 4;
        public const int OneUnitErrs = 5;

        public SpellingCheckerSettings(Form1 parent)
        {
            this.parent = parent;
            InitializeComponent();
            checkBox_OneUnitErrs.Enabled = checkBox_OneUnitErrs.Checked = false;
            checkBox_PhoneticWords.Enabled = checkBox_PhoneticWords.Checked = false;
            button_Check.Focus();
        }

        private void button_Check_Click(object sender, EventArgs e)
        {
            Hide();
            this.DialogResult = DialogResult.Yes;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            Hide();
            this.DialogResult = DialogResult.Cancel;
        }

        public bool[] GetSettings()
        {
            bool[] results = new bool[OneUnitErrs + 1];
            results[InvMyanChars] = checkBox_InvMyanChars.Checked;
            results[GrammarErrs] = checkBox_GrammarErrs.Checked;
            results[MismatchedPairs] = checkBox_MismatchedPairs.Checked;
            results[ZeroWalone] = checkBox_ZeroWalone.Checked;
            results[PhoneticWords] = checkBox_PhoneticWords.Checked;
            results[OneUnitErrs] = checkBox_OneUnitErrs.Checked;
            return results;
        }
    }
}
