using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NissayaEditor_DataFile;

namespace NissayaEditor
{
    public partial class Form_ImportSuttaData : Form
    {
        Form_WorkAssignment parent;
        string[] listFileNames;
        int startPage = 0;
        int pageNoOffset = 0;
        string prevListItem = string.Empty;

        public Dictionary<string, DataInfoDetails> dictDataInfo = new Dictionary<string, DataInfoDetails>();
        public Form_ImportSuttaData(Form_WorkAssignment parent)
        {
            InitializeComponent();
            this.parent = parent;
            button_Ok.Enabled = false;
        }
        public void SetStartPage(int startPage) { this.startPage = startPage; }
        public void SetSuttaInfo(string suttaInfo, int startPage, int endPage)
        {
            textBox_SuttaNo.Text = suttaInfo;
            this.startPage = startPage;
            textBox_StartPage.Text = startPage.ToString();
            textBox_EndPage.Text = endPage.ToString();
            textBox_Pages.Text = (endPage - startPage + 1).ToString();
            textBox_SuttaFile.Text = parent.GetDataDir() + "\\" + suttaInfo + ".txt";
        }
        private void button_Browse_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = Form1.fileFilters,
                FilterIndex = 1,
                Multiselect = true,
                RestoreDirectory = true
            };
            //textBox_FileName.Text = fileName = string.Empty;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                listFileNames = openFileDialog.FileNames;
                foreach (string fname in listFileNames)
                {
                    listBox1.Items.Add(Path.GetFileName(fname).Trim());
                }
            }
        }
        public new DialogResult ShowDialog()
        {
            this.CenterToParent();
            return base.ShowDialog();
        }
        private void button_Ok_Click(object sender, EventArgs e)
        {
            if (prevListItem.Length > 0)
            {
                dictDataInfo[prevListItem].sourceStartPage = Convert.ToInt16(textBox_StartPage3.Text);
            }
            DialogResult = DialogResult.OK;
            return;
        }
        private void button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; return;
        }
        private void button_Load_Click(object sender, EventArgs e)
        {
            DataInfo dataInfo;
            string File_MN;
            dictDataInfo.Clear();
            ProgressMessageThread progressMessageThread = new ProgressMessageThread();
            progressMessageThread.Open("Please wait while loading ...", this);
            List<string> listPageNo = new List<string>();
            foreach (string fname in listFileNames)
            {
                dataInfo = new DataInfo(fname);
                dataInfo.FileLoadData();
                File_MN = (dataInfo.MN.Length > 0) ? String.Format("MN-{0}", dataInfo.MN.PadLeft(3, '0')) : "";
                if (dataInfo.MN.Length > 0 && textBox_SuttaNo.Text != File_MN)
                {
                    MessageBox.Show(String.Format("'{0}' file is from sutta '{1}'.", fname, File_MN));
                    return;
                }
                var kv = dataInfo.Pages.First();
                int first = Convert.ToInt32(kv.Key);
                kv = dataInfo.Pages.Last();
                int last = Convert.ToInt32(kv.Key);
                listPageNo.AddRange(dataInfo.Pages.Keys);
                DataInfoDetails dataInfoDetails = new DataInfoDetails()
                {
                    startPage = first,
                    endPage = last,
                    noPages = dataInfo.Pages.Count,
                    sourceStartPage = startPage,
                    dataInfo = dataInfo,
                    NISRecCount = dataInfo.NISRecCount,
                    Pages = dataInfo.Pages,
                };
                // set for next startPage
                dataInfoDetails.sourceStartPage = startPage;
                startPage += last - first + 1;
                dictDataInfo.Add(Path.GetFileName(fname).Trim(), dataInfoDetails);
            }
            int page1 = (dictDataInfo.First().Value).startPage;
            int page2 = (dictDataInfo.Last().Value).endPage;
            pageNoOffset = Convert.ToInt16(listPageNo[0]) - page1;
            textBox_StartPage3.Text = pageNoOffset.ToString();
            listBox1.SelectedIndex = 0;
            listBox1_Click(this, new EventArgs());
            prevListItem = listBox1.SelectedItem as String;
            progressMessageThread.Close();
            button_Ok.Enabled = true;
        }
        private void listBox1_Click(object sender, EventArgs e)
        {
            string item = listBox1.SelectedItem as String;
            if (item == null || !dictDataInfo.ContainsKey(item)) { return; }
            DataInfoDetails dataItemInfo = dictDataInfo[item];
            if (prevListItem.Length > 0 && prevListItem != item)
            {
                dictDataInfo[prevListItem].sourceStartPage = Convert.ToInt16(textBox_StartPage3.Text);
            }
            if (dataItemInfo != null)
            {
                textBox_StartPage2.Text = dataItemInfo.startPage.ToString();
                textBox_EndPage2.Text = dataItemInfo.endPage.ToString();
                textBox_Pages2.Text = ((dataItemInfo.endPage - dataItemInfo.startPage) + 1).ToString();
                textBox_StartPage3.Text = dataItemInfo.sourceStartPage.ToString();
            }
            prevListItem = item;
        }
        //**********************************************************************************
        // class DataInfoDetails
        //**********************************************************************************
        public class DataInfoDetails
        {
            public int startPage, endPage, sourceStartPage, noPages;
            public DataInfo dataInfo;
            public Dictionary<int, int> NISRecCount = new Dictionary<int, int>();
            public Dictionary<string, string> Pages;
            public DataInfoDetails() { }
        }
    }
}
