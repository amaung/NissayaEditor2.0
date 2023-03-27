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

namespace NissayaEditor
{
    public partial class MN_Catalog : Form
    {
        Form1 parent;
        string jsonMNCatalog = "MN_Catalog.json";
        JSON.Json mnJson;
        DataGridView dataGridView2 = new DataGridView();
        int curMNIndex = 0;
        string curMNTitle;
        string curMN_No = string.Empty;
        List<int> MN_List = new List<int>();
        Dictionary<int, List<string>> MN_Files = new Dictionary<int, List<string>>();
        Dictionary<int, string> MN_Titles;
        ExportConfirm exportConfirm = null;

        public string CurrentMN_No() { return curMNIndex.ToString(); }
        public string CurrentMNTitle() { return curMNTitle; }
        public List<string> CurrentMNFileList() { return MN_Files[curMNIndex]; }
        public int GetFileCount(int MN_No)
        {
            if (MN_Files.ContainsKey(MN_No))
                return MN_Files[MN_No].Count;
            return 0;
        }
        public MN_Catalog(Form1 parent)
        {
            this.parent = parent;
            InitializeComponent();
            InitializeDataGridViews();
            // read in the MNCatalog.json file
            mnJson = new JSON.Json(jsonMNCatalog);
            if (!File.Exists(jsonMNCatalog)) mnJson.CreateFile(jsonMNCatalog);
            MN_LoadData();
            MN_Titles = parent.MN_Titles;
            button_Save.Enabled = dataGridView1.Rows.Count > 1;
            button_Files.Enabled = dataGridView1.SelectedRows.Count > 0;
            button_Open.Enabled = dataGridView1.SelectedRows.Count > 0;
            button_UpArrow.Visible = false;
            button_DownArrow.Visible = false;
            button_Export.Visible = false;
            EnableOpenButton(0, 0);
            if (dataGridView1.Rows.Count <= 1)
            {
                curMNIndex = -1; curMNTitle = "";
            }
            else
            {
                curMNIndex = Convert.ToInt16(dataGridView1[0, 0].Value.ToString());
                curMNTitle = MN_Titles[curMNIndex];
            }
            button_Files.Enabled = (dataGridView1[0, 0] != null && dataGridView1[0, 0].Value != null &&
                dataGridView1[0, 0].Value.ToString() != string.Empty);
            if (exportConfirm == null) exportConfirm = new ExportConfirm(this);
        }

        private void InitializeDataGridViews()
        {
            dataGridView1.Enabled = true;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells | DataGridViewAutoSizeRowsMode.AllHeaders;
            dataGridView1.ColumnCount = 3;
            // column header setup
            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.RowHeadersWidth = 20;
            dataGridView1.ColumnHeadersHeight = 35;
            // set column header text and font
            dataGridView1.Columns[0].HeaderText = "MN#";
            dataGridView1.Columns[1].HeaderText = "Name of Sutta";
            dataGridView1.Columns[2].HeaderText = "Files";
            dataGridView1.Columns[0].HeaderCell.Style.Font = parent.fontMYA;
            dataGridView1.Columns[1].HeaderCell.Style.Font = parent.fontMYA;
            dataGridView1.Columns[2].HeaderCell.Style.Font = parent.fontMYA;
            dataGridView1.Columns[0].DefaultCellStyle.Font = parent.fontMYA;
            dataGridView1.Columns[1].DefaultCellStyle.Font = parent.fontMYA;
            dataGridView1.Columns[2].DefaultCellStyle.Font = parent.fontMYA;
            // set col widths
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[1].Width = dataGridView1.Width - 
                dataGridView1.RowHeadersWidth - dataGridView1.Columns[0].Width - 
                dataGridView1.Columns[2].Width;
            int w = 0;
            foreach (DataGridViewColumn col in dataGridView1.Columns) w += col.Width;
            this.Width = w + 65;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // colors
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.LightGray;

            // set other attributes
            dataGridView1.Columns[0].ReadOnly = false;
            dataGridView1.Columns[1].ReadOnly = false;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            //dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.Automatic;
            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.CellValidating += dataGridView1_CellValidating;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            //dataGridView1.RowsAdded += dataGridView1_RowsAdded;

            // dataGridView2
            this.Controls.Add(dataGridView2);
            dataGridView2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView2.Visible = false;
            dataGridView2.Location = dataGridView1.Location;
            dataGridView2.Size = dataGridView1.Size;
            // settings
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells | DataGridViewAutoSizeRowsMode.AllHeaders;
            dataGridView2.ColumnCount = 2;
            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // column header setup
            dataGridView2.RowTemplate.Height = 35;
            dataGridView2.RowHeadersWidth = 20;
            dataGridView2.ColumnHeadersHeight = 35;
            // set column header text and font
            dataGridView2.Columns[0].HeaderText = "Sr#";
            dataGridView2.Columns[1].HeaderText = "File Name";
            dataGridView2.Columns[0].HeaderCell.Style.Font = parent.fontMYA;
            dataGridView2.Columns[1].HeaderCell.Style.Font = parent.fontMYA;
            dataGridView2.Columns[0].DefaultCellStyle.Font = parent.fontMYA;
            dataGridView2.Columns[1].DefaultCellStyle.Font = parent.fontMYA;
            dataGridView2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // set col widths
            dataGridView2.Columns[0].Width = 40;
            dataGridView2.Columns[1].Width = dataGridView2.Width - dataGridView2.RowHeadersWidth - dataGridView2.Columns[0].Width - 3;
            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView2.RowHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView2.SelectionChanged += dataGridView2_SelectionChanged;
        }

        private void MN_LoadData()
        {
            DataGridViewRow row;
            MN_Files.Clear();
            string[] mnCatalog = mnJson.GetData();
            int mnNo = -1, prevNo = -1, srNo, count = 0;
            string suttaName = string.Empty;
            List<string> fileList = new List<string>();
            Dictionary<int, string> suttaNames = new Dictionary<int, string>();
            foreach (string s in mnCatalog)
            {
                string[] f = s.Split('|');
                string[] ff = f[0].Split('-');
                if (ff.Length == 2)
                {
                    mnNo = Convert.ToInt16(ff[1]);
                    MN_List.Add(mnNo);
                    if (prevNo != -1 && fileList.Count > 0)
                    {
                        MN_Files.Add(prevNo, fileList);
                        suttaNames.Add(prevNo, suttaName);
                    }
                    suttaName = f[1];
                    prevNo = mnNo;
                    fileList = fileList = new List<string>();
                }
                if (ff.Length == 3)
                {
                    if (mnNo == Convert.ToInt16(ff[1]))
                    {
                        srNo = Convert.ToInt16(ff[2]);
                        fileList.Add(f[1]);
                    }
                }
            }
            if (mnNo > 0 && fileList.Count > 0)
            {
                MN_Files.Add(mnNo, fileList);
                suttaNames.Add(mnNo, suttaName);
            }
            MN_List.Sort();
            count = dataGridView1.Rows.Count;
            foreach (int n in MN_List)
            {
                row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                row.Cells[0].Value = n.ToString();
                row.Cells[1].Value = suttaNames[n];
                row.Cells[2].Value = MN_Files[n].Count.ToString();
                dataGridView1.Rows.Add(row);
            }
        }

        private void button_Done_Click(object sender, EventArgs e)
        {
            Hide();
        }
        
        private void Sort()
        {
            if (dataGridView1.Rows.Count <= 2) return;
            DataGridViewColumn col = dataGridView1.Columns[0];
            ListSortDirection direction = ListSortDirection.Ascending;
            dataGridView1.Sort(col, direction);
        }

        private void button_Open_Click(object sender, EventArgs e)
        {
            // open the selected book(s)
            switch (((Button)sender).Text)
            {
                case "Open":
                    EventHandler<EventArgs> handler = OnOpen;
                    if (handler != null)
                    {
                        MNOpenEventArgs eargs = new MNOpenEventArgs();
                        eargs.MNTitle = CurrentMNTitle();
                        eargs.MN_No = CurrentMN_No();
                        handler(this, eargs);
                    }
                    Hide();
                    break;
                case "Delete":
                    string recno = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                    dataGridView2.Rows.RemoveAt(dataGridView2.CurrentRow.Index);
                    if (dataGridView2.CurrentRow.Index < dataGridView2.Rows.Count - 2)
                    {
                        dataGridView2.CurrentRow.Cells[0].Value = recno;
                        for (int index = dataGridView2.CurrentRow.Index; index < dataGridView2.Rows.Count - 2; ++index)
                        {
                            dataGridView2.Rows[index].Cells[0].Value = (index + 1).ToString();
                        }
                    }
                    if (dataGridView2.Rows.Count <= 1) button_Open.Enabled = false;
                    break;
            }
        }

        private void button_Files_Click(object sender, EventArgs e)
        {
            switch (((Button)sender).Text)
            {
                case "Files":
                    dataGridView1.Visible = false;
                    dataGridView2.Visible = true;
                    ((Button)sender).Text = "Back";
                    button_Save.Text = "Add";
                    button_Open.Text = "Delete";
                    button_Save.Enabled = true;
                    button_Done.Enabled = false;
                    button_Open.Enabled = true;
                    dataGridView2.Rows.Clear();
                    int n = Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    if (MN_Files.ContainsKey(n))
                    {
                        List<string> fl = MN_Files[n];
                        DataGridViewRow row;
                        int count = 0;
                        foreach (string fnm in fl)
                        {
                            row = (DataGridViewRow)dataGridView2.Rows[0].Clone();
                            row.Cells[0].Value = (++count).ToString();
                            row.Cells[1].Value = fnm;
                            dataGridView2.Rows.Add(row);
                        }
                    }
                    button_UpArrow.Visible = true;
                    button_DownArrow.Visible = true;
                    button_Export.Visible = true;
                    button_Open.Enabled = true;
                    break;
                case "Back":
                    dataGridView1.Visible = true;
                    dataGridView2.Visible = false;
                    ((Button)sender).Text = "Files";
                    button_Save.Text = "Save";
                    button_Open.Text = "Open";
                    button_Save.Enabled = (dataGridView1.CurrentRow.Cells[2].Value != null);
                    
                    if (MN_Files.ContainsKey(curMNIndex)) MN_Files.Remove(curMNIndex);
                    List<string> fnames = new List<string>();
                    foreach (DataGridViewRow r in dataGridView2.Rows)
                    {
                        if (r.Cells[1].Value != null)
                            fnames.Add(r.Cells[1].Value.ToString());
                    }
                    MN_Files.Add(curMNIndex, fnames);
                    dataGridView1.CurrentRow.Cells[2].Value = fnames.Count;
                    if (fnames.Count > 0)
                    {
                        button_Save.Enabled = true;
                        button_Open.Enabled = true;
                    }
                    button_Done.Enabled = true;
                    button_UpArrow.Visible = false;
                    button_DownArrow.Visible = false;
                    button_Export.Visible = false;
                    dataGridView1.Sort(new RowComparer());
                    break;
            }
        }

        private void CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count - 1) return;
            curMNTitle = dataGridView1[1, e.RowIndex].Value.ToString();
            curMNIndex = Convert.ToInt16(dataGridView1[0, e.RowIndex].Value.ToString());
            EnableOpenButton(e.RowIndex, e.ColumnIndex);
        }
        private void button_Save_Click(object sender, EventArgs e)
        {
            string fileFilters = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            switch (((Button)sender).Text)
            {
                case "Save":
                    if (!MN_Files.ContainsKey(curMNIndex))
                    {
                        MessageBox.Show("There are no files defined for MN-" + curMNIndex.ToString() + ".\n" +
                            "Add files or remove entry.");
                        return;
                    }
                    List<string> MN_data = new List<string>();
                    string item = "MN-" + dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    item += "|" + dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    MN_data.Add(item);
                    List<string> list = MN_Files[curMNIndex];
                    int n = 0;
                    foreach (string s in list)
                    {
                        item = "MN-" + dataGridView1.CurrentRow.Cells[0].Value.ToString();
                        item += "-" + (++n).ToString();
                        item += "|" + s;
                        MN_data.Add(item);
                    }
                    mnJson.UpdateData(MN_data.ToArray());
                    MessageBox.Show("MN Catalog updated.");
                    break;
                case "Add":
                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Filter = fileFilters;
                        openFileDialog.FilterIndex = 1;
                        openFileDialog.RestoreDirectory = true;
                        openFileDialog.Multiselect = true;

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string[] fnames = openFileDialog.FileNames;
                            int count = dataGridView2.Rows.Count;
                            foreach (string f in fnames)
                            {
                                DataGridViewRow row = (DataGridViewRow)dataGridView2.Rows[0].Clone();
                                row.Cells[0].Value = (count++).ToString();
                                row.Cells[1].Value = f;
                                dataGridView2.Rows.Add(row);
                            }
                        }
                    }
                    dataGridView2_SelectionChanged(null, null);
                    break;
            }
        }
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex != 0) return;
            dataGridView1.Rows[e.RowIndex].ErrorText = "";
            int newInteger;

            // Don't try to validate the 'new row' until finished 
            // editing since there
            // is not any point in validating its initial value.
            if (dataGridView1.Rows[e.RowIndex].IsNewRow) { return; }
            if (!int.TryParse(e.FormattedValue.ToString(),
                out newInteger) || newInteger < 0 || newInteger > 152)
            {
                e.Cancel = true;
                dataGridView1.Rows[e.RowIndex].ErrorText = "MN value must be a between 1-152.";
                MessageBox.Show("MN sutta number must be between 1-152.");
            }
            else
            {
                dataGridView1.Rows[e.RowIndex].Cells[1].Value = MN_Titles[newInteger];
                curMNIndex = newInteger;
                if (dataGridView1.Rows.Count <= 2) return;
                //dataGridView1.Sort(new RowComparer());
                //Sort();
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Update the labels to reflect changes to the selection.
            if (dataGridView1.CurrentRow.Index < dataGridView1.Rows.Count - 1)
            {
                button_Files.Enabled = true;
                if (dataGridView1.CurrentRow.Cells[0].Value != null &&
                    dataGridView1.CurrentRow.Cells[1].Value != null)
                {
                    label_Title.Text = "MN:  " + dataGridView1.CurrentRow.Cells[0].Value + " - " +
                                       dataGridView1.CurrentRow.Cells[1].Value;
                    curMN_No = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    curMNTitle = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    curMNIndex = Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                }
                else
                {
                    label_Title.Text = curMNTitle = string.Empty;
                    curMNIndex = 0;
                }
            }
            else
            {
                label_Title.Text = String.Empty;
                button_Files.Enabled = false;
            }
        }
        public List<string> GetFileList(int MN_No) { return new List<string>(MN_Files[MN_No]); }
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            button_UpArrow.Enabled = dataGridView2.CurrentRow != null && dataGridView2.CurrentRow.Index > 0 ? true : false;
            button_DownArrow.Enabled = dataGridView2.CurrentRow != null && dataGridView2.CurrentRow.Index < dataGridView2.Rows.Count - 2 ? true : false;           
        }
        private void button_UpArrow_Click(object sender, EventArgs e)
        {
            int prevRowIndex = dataGridView2.CurrentRow.Index - 1;
            if (prevRowIndex >= 0)
            {
                string t = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                dataGridView2.CurrentRow.Cells[1].Value = dataGridView2.Rows[prevRowIndex].Cells[1].Value;
                dataGridView2.Rows[prevRowIndex].Cells[1].Value = t;
                dataGridView2.CurrentRow.Selected = false;
                dataGridView2.Rows[prevRowIndex].Selected = true;
                dataGridView2.CurrentCell = dataGridView2[1, prevRowIndex];
            }

        }
        private void button_DownArrow_Click(object sender, EventArgs e)
        {
            int nextRowIndex = dataGridView2.CurrentRow.Index + 1;
            if (nextRowIndex < dataGridView2.Rows.Count - 1)
            {
                string t = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                dataGridView2.CurrentRow.Cells[1].Value = dataGridView2.Rows[nextRowIndex].Cells[1].Value;
                dataGridView2.Rows[nextRowIndex].Cells[1].Value = t;
                dataGridView2.CurrentRow.Selected = false;
                dataGridView2.Rows[nextRowIndex].Selected = true;
                dataGridView2.CurrentCell = dataGridView2[1, nextRowIndex];
            }

        }
        private void EnableOpenButton(int rowIndex, int colIndex)
        {
            if (rowIndex < 0 || rowIndex >= dataGridView1.Rows.Count - 1)
            {
                button_Open.Enabled = false;
                return;
            }
            button_Open.Enabled = (dataGridView1[2, rowIndex].Value != null &&
                        Convert.ToInt16(dataGridView1[2, rowIndex].Value.ToString()) > 0);
        }
        public event EventHandler<EventArgs> OnOpen = null;
        public class RowComparer : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                DataGridViewRow DataGridViewRow1 = (DataGridViewRow)x;
                DataGridViewRow DataGridViewRow2 = (DataGridViewRow)y;

                // Try to sort based on the Last Name column.
                int CompareResult = Convert.ToInt16(DataGridViewRow1.Cells[0].Value.ToString()) -
                    Convert.ToInt16(DataGridViewRow2.Cells[0].Value.ToString());
                return CompareResult;
            }
        }

        private void button_Export_Click(object sender, EventArgs e)
        {
            if (exportConfirm == null) return;
            // pad MN_No with leading zeroes
            exportConfirm.SetExportFileName("MN-" + Convert.ToInt16(curMN_No).ToString("D3") + ".txt");
            List<string> fl;
            List<string[]> fContents = new List<string[]>();
            List<string> fullContent = new List<string>();
            bool flagNewPageNo = false;
            string newPgNo = string.Empty;
            string newPage;
            string exportFileName = string.Empty;

            if (curMN_No.Length == 0)
            {
                MessageBox.Show("Sutta number is not avialable.");
                return;
            }
            fl = MN_Files[Convert.ToInt16(curMN_No)];
            try
            {
                StreamReader sr = new StreamReader(fl[0]);
                string content = sr.ReadToEnd();
                string[] para = content.Split('\n');
                fContents.Add((string[])para.Clone());
                string pgno = GetPageNo(para);
                exportConfirm.SetFirstPage(pgno);
                DialogResult res = exportConfirm.ShowDialog();
                if (res == DialogResult.OK)
                {
                    int n = 0;
                    newPgNo = exportConfirm.GetFirstPage();
                    exportFileName = Path.GetDirectoryName(fl[0]) + "\\" + exportConfirm.GetExportFileName();
                    flagNewPageNo = (newPgNo != pgno);
                    
                    // load each file and its pages
                    foreach (string f in fl)
                    {
                        if (n == 0)
                        {
                            ++n; continue;
                        }
                        sr = new StreamReader(f);
                        content = sr.ReadToEnd();
                        para = content.Split('\n');
                        fContents.Add((string[])para.Clone());
                    }
                    // check each file for page number correctness
                    foreach (string[] p in fContents)
                    {
                        bool flag = false;
                        foreach(string page in p)
                        {
                            newPage = page.Trim();
                            if (newPage.Length == 0) continue;
                            if (!flag && IsHeaderRecord(newPage))
                            {
                                flag = true; fullContent.Add(newPage); continue;
                            }
                            string pn = GetPageNo(newPage);
                            if (pn.Length == 0)
                            {
                                // this is just a paragraph; just add 
                                fullContent.Add(newPage); continue;
                            }
                            if (pgno == pn) // if page number is the expected page number - continue
                            {
                                pgno = (Convert.ToInt16(pgno) + 1).ToString();
                                if (flagNewPageNo)
                                {
                                    // replace with the new page no
                                    newPage = "#" + newPgNo + page.Substring(page.IndexOf(" "));
                                    newPgNo = (Convert.ToInt16(newPgNo) + 1).ToString();
                                }
                                fullContent.Add(newPage);
                            }
                            else
                            {
                                Form_ErrMsg errMsg = new Form_ErrMsg();
                                string t = "Page no. #" + pgno + " expected in '" + newPage.Substring(0, 50) + " ...'";
                                t += "\nPlease check and run again.";
                                errMsg.SetErrMsg(t);
                                errMsg.ShowDialog();
                                // ERROR!
                                return;
                            }
                        }
                    }
                    ExportFile(exportFileName, fullContent);
                    MessageBox.Show("Export to '" + exportFileName + "' successful.");
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }
        string GetPageNo(string[] content)
        {
            string pgno = string.Empty;
            if (content.Count() == 0) return string.Empty;
            int n = 0;
            if (IsHeaderRecord(content[n])) n = 1;
            if (content[n][0] == '#')
            {
                int pos = content[n].IndexOf("*");
                if (pos != -1)
                {
                    pgno = content[n].Substring(1, pos - 1).Trim();
                    // in some cases first page no includes the sutta title, if so remove it.
                    pgno = GetDigits(pgno);
                    //string t = pgno.Substring(0, pos-1).Trim();
                    //if (pgno == t)
                    //{
                    //    content[n].Insert(pos, " ");
                    //}
                    //pgno = t;
                }
            }
            return pgno;
        }
        string GetPageNo(string s)
        {
            return GetPageNo(new string[] { s });
        }
        bool IsHeaderRecord(string s)
        {
            return (s.IndexOf("{") != -1 && s.IndexOf("}") != -1);
        }
        void ExportFile(string fname, List<string> content)
        {
            if (fname.Length == 0)
            {
                MessageBox.Show("Export file name is empty.");
                return;
            }
            StreamWriter sw = new StreamWriter(fname);
            int n = 0;
            bool header = IsHeaderRecord(content[0]);
            foreach (string p in content)
            {
                if (!header && n > 0) sw.WriteLine("");
                sw.WriteLine(p.Trim());
                ++n;
            }
            sw.Close();
        }
        string GetDigits(string s)
        {
            List<char> cList = new List<char>();
            foreach (char c in s)
            {
                if (!Char.IsDigit(c)) break;
                cList.Add(c);
            }
            return new string(cList.ToArray());
        }
    }
}
