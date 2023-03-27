using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tipitaka_DB;

namespace NissayaEditor
{
    public partial class Form_ServerSuttaDownload : Form
    {
        Form1 parent;
        bool flagSourceFile = false;
        public Form_ServerSuttaDownload(Form1 parent)
        {
            InitializeComponent();
            button_Start.Enabled = false;
            this.parent = parent;
            this.Width = 400;
        }
        public void Show(string[] suttas, bool flagSrcFile = false)
        {
            this.flagSourceFile = flagSrcFile;
            CenterToParent();
            if (flagSourceFile)
            {
                //this.Width = 400;
                label1.Text = "Select source file(s) to download:";
            }
            else
            {
                //this.Width = 300;
                label1.Text = "Select sutta(s) to download:";
            }
            listBox1.Items.Clear();
            foreach (string s in suttas)
            {
                if (flagSourceFile)
                    listBox1.Items.Add(s);
                else
                    listBox1.Items.Add(String.Format("MN-{0}", s.PadLeft(3, '0')));
            }
            base.Show();
            if (suttas.Length == 0)
            {
                button_Start.Enabled = false;
                MessageBox.Show("No suttas available on server.", "Notification", MessageBoxButtons.OK);
                return;
            }
            else button_Start.Enabled = true;
        }
        private void button_Start_Click(object sender, EventArgs e)
        {
            //string target = "file://ds118/home/Buddhism/Ashin%20Sarana/Tipitaka%20Files/MNA/ATN%20MN%20Majjhimapannasa%201%20(MN%20051-075).pdf#page=100";
            //OpenPDFfile(target);
            if (listBox1.SelectedItems.Count == 0)
            {
                MessageBox.Show("No file selected."); return;
            }
            if (flagSourceFile)
            {
                TipitakaFileStorage tipitakaFileStorage = new TipitakaFileStorage(parent.sourceDir);
                this.Cursor = Cursors.WaitCursor;
                foreach (string item in listBox1.SelectedItems)
                {
                    tipitakaFileStorage.SourceFileDownload("MN", item);
                }
                this.Cursor = Cursors.Default;
                if (tipitakaFileStorage.DBErrMsg.Length > 0)
                {
                    MessageBox.Show(tipitakaFileStorage.DBErrMsg);
                }
                else MessageBox.Show("Download complete.");
                Hide();
            }
            else
            {
                ClientSuttaPageData clientSuttaPageData = parent.clientTipitakaDB.GetClientSuttaPageData();
                if (clientSuttaPageData != null && listBox1.SelectedItems.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    foreach (string suttaNo in listBox1.SelectedItems)
                    {
                        clientSuttaPageData.SetSubPartitionKey(suttaNo);
                        SortedDictionary<int, string> suttaData = clientSuttaPageData.GetSutta();
                        string fileContent = "{MN:" + suttaNo.Substring(3).TrimStart('0') + "}";
                        foreach (KeyValuePair<int, string> kv in suttaData)
                        {
                            fileContent += System.Environment.NewLine + kv.Value.ToString() + System.Environment.NewLine;
                        }
                        string outFilename = String.Format("{0}\\{1}.txt", parent.dataDir, suttaNo);
                        using (StreamWriter writer = new StreamWriter(outFilename))
                        {
                            writer.WriteLine(fileContent);
                            writer.Close();
                        }
                    }
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Download complete.");
                }
                Hide();
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            //string pdfFileName = "\"\\\\DS118\\home\\Buddhism\\Ashin Sarana\\Tipitaka Files\\MNA\\ATN MN Majjhimapannasa 1 (MN 051-075).PDF\"";
            //OpenPDFfile(pdfFileName, 100);
            DialogResult = DialogResult.Cancel; Hide();
        }
 
    }
}
