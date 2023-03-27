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
    public partial class MessagingForm2 : Form
    {
        public Form parent;
        public MessagingForm2(Form parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.StartPosition = FormStartPosition.Manual;
        }
        public void ShowDialog(string message)
        {
            label1.Text = message;
            Cursor = Cursors.WaitCursor;
            if (parent != null)
            {
                Location = new Point(parent.Location.X + 100, parent.Location.Y + parent.Height / 2);
            }
            base.ShowDialog();
        }
        public void Show(string message)
        {
            label1.Text = message;
            base.Show();
        }
        public new void Close()
        {
            Cursor = Cursors.Default;
            base.Close();
        }
    }
}
