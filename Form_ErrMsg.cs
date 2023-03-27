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
    public partial class Form_ErrMsg : Form
    {
        public Form_ErrMsg()
        {
            InitializeComponent();
        }

        public void SetErrMsg(string s)
        {
            textBox_ErrMsg.Text = s;
        }

        private void button_Ok_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OnShown(object sender, EventArgs e)
        {
            button_Ok.Focus();
        }
    }
}
