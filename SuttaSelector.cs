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
    public partial class SuttaSelector : Form
    {
        Form1 parent;
        public SuttaSelector(Form1 parent)
        {
            this.parent = parent;
            InitializeComponent();
            listBox_Sutta.MultiColumn = false;
            listBox_Sutta.ColumnWidth = 100;
            listBox_Sutta.SelectionMode = SelectionMode.One;
            label_SuttaCat.Text = "MN";
        }

        public void Init(List<string> list)
        {
            if (list.Count == 0) return;
            foreach (string s in list)
            {
                Object item = new Object();
                listBox_Sutta.Items.Add(s);
            }
        }

        public void AddItem(string s)
        {
            if (s == null || s.Length == 0) return;
            listBox_Sutta.Items.Add(s);
        }

        public KeyValuePair<int, string> GetSelection()
        {
            KeyValuePair<int, string> kv = new KeyValuePair<int, string>(-1, "");
            if (listBox_Sutta.SelectedIndex != -1)
            {
                string s = (string)listBox_Sutta.SelectedItem;
                int pos = s.IndexOf(". ");
                int key = Convert.ToInt16(s.Substring(0, pos));
                string val = s.Substring(pos + 2).Trim();
                kv = new KeyValuePair<int, string>(key, val);
            }
 
            return kv;
        }
        private void button_Select_Click(object sender, EventArgs e)
        {
            if (listBox_Sutta.SelectedIndex == -1)
            {
                MessageBox.Show("Nothing is selected.");
                return;
            }
            string s = (string)listBox_Sutta.SelectedItem;
            Hide();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
