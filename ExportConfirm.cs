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
    public partial class ExportConfirm : Form
    {
        MN_Catalog parent;
        public ExportConfirm(MN_Catalog p)
        {
            parent = p;
            InitializeComponent();
        }
        public void SetExportFileName(string fname) { textBox_FileName.Text = fname; }
        public void SetFirstPage(string pgno) { textBox_PageNo.Text = pgno; }
        public string GetFirstPage() { return textBox_PageNo.Text; }
        public string GetExportFileName() { return textBox_FileName.Text; }

        private void button_Ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Hide();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Hide();
        }
    }
}
