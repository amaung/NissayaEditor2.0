using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tipitaka_DB;
using Tipitaka_DBTables;
using System.IO;
using System.Security.AccessControl;
//using System.Windows.Controls;
using System.Security.Principal;
using static Tipitaka_DB.TipitakaDB;
using System.Data.Entity.Infrastructure;
using NissayaEditor_DataFile;
using static NissayaEditor.Form_ImportSuttaData;
using Microsoft.VisualBasic.ApplicationServices;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace NissayaEditor
{
    public partial class Form_WorkAssignment : Form
    {
        public Form1 parent;
        ClientTipitakaDB clientTipitakaDB = null;
        ClientUserPageActivity clientUserPageActivity = null;
        ClientSuttaPageAssignment clientSuttaPageAssignment = null;
        SQLiteObj sqlObj = new SQLiteObj();
        List<WorkAssignmentRecord> workAssignmentRecords = new List<WorkAssignmentRecord>();
        Dictionary<string, WorkAssignmentRecord> dictWorkAssignment = new Dictionary<string, WorkAssignmentRecord>();
        public event EventHandler<EventArgs> OnPageOpen = null;
        Form_ImportSuttaData form_ImportSuttaData = null;
        public Form_WorkAssignment(Form1 parent)
        {
            InitializeComponent();
            this.parent = parent;
            clientTipitakaDB = parent.clientTipitakaDB;
            clientUserPageActivity = clientTipitakaDB.GetClientUserPageActivity();
            clientSuttaPageAssignment = clientTipitakaDB.GetClientSuttaAssignment();
            InitDataGridView();
            parent.pageMenuBar.OnWorkAssignmentUpdate += DoUpdateWorkAssignment;

            form_ImportSuttaData = new Form_ImportSuttaData(this);
            MyToolTip myToolTip = new MyToolTip(Form1.DefaultFont);
            myToolTip.SetToolTip(button_Close, "Quit");
            myToolTip.SetToolTip(button_Download, "Download new assignments");
            myToolTip.SetToolTip(button_Update, "Download source pages");
            myToolTip.SetToolTip(button_Open, "Open page for editing");
            myToolTip.SetToolTip(button_Upload, "Upload page(s) to server");
            myToolTip.SetToolTip(button_Import, "Import sutta pages from file");
        }
        public string GetDataDir() { return parent.dataDir; }
        private void InitDataGridView()
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells | DataGridViewAutoSizeRowsMode.AllHeaders;
            dataGridView1.ColumnCount = 8;
            dataGridView1.RowTemplate.Height = 30;
            dataGridView1.RowHeadersWidth = dataGridView1.Width / 2;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = parent.dgvAlternateRowColor;// Color.FromArgb(250, 255, 236);

            dataGridView1.RowHeadersVisible = true;
            dataGridView1.RowHeadersWidth = 20;
            dataGridView1.ColumnHeadersHeight = 50;
            dataGridView1.Columns[0].HeaderText = "Sutta#";
            dataGridView1.Columns[1].HeaderText = "Page";
            dataGridView1.Columns[2].HeaderText = "NIS";
            dataGridView1.Columns[3].HeaderText = "Status";
            dataGridView1.Columns[4].HeaderText = "Mins";
            dataGridView1.Columns[5].HeaderText = "Days";
            dataGridView1.Columns[6].HeaderText = "StartDate";
            dataGridView1.Columns[7].HeaderText = "AssignedDate";
            dataGridView1.Columns[0].Width = 65;
            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 40;
            dataGridView1.Columns[5].Width = 40;
            dataGridView1.Columns[6].Width = 80;
            dataGridView1.Columns[7].Width = 80;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.MultiSelect = true;
            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //dataGridView1.CellClick += dataGridView_CellClick!;
            //dataGridView1.Columns[0].HeaderCell.SortGlyphDirection = SortOrder.Descending;
            //dataGridView1.Rows[0].Cells[0].Value = "MN-152";
            //dataGridView1.Rows[0].Cells[1].Value = "672";
            //dataGridView1.Rows[0].Cells[2].Value = "23";
            //dataGridView1.Rows[0].Cells[3].Value = "In progress";
            //dataGridView1.Rows[0].Cells[4].Value = "0.6";
            //dataGridView1.Rows[0].Cells[5].Value = "3";
            //dataGridView1.Rows[0].Cells[6].Value = "2023-01-05";
            //dataGridView1.Rows[0].Cells[7].Value = "2023-01-05";
        }
        public DialogResult ShowDialog(string userID)
        {
            textBox_UserID.Text = userID;
            // get user page activities from local db
            dictWorkAssignment.Clear();
            dataGridView1.Rows.Clear();
            workAssignmentRecords = parent.sqlObj.GetWorkAssignmentRecords(userID);
            string rowKey = string.Empty;
            foreach (WorkAssignmentRecord record in workAssignmentRecords)
            {
                rowKey = String.Format("{0}-{1}", record.suttaNo, record.pageNo.ToString("D3"));
                if (!dictWorkAssignment.ContainsKey(rowKey))
                {
                    AddUserWorkAssignment(record);
                    dictWorkAssignment.Add(rowKey, record);
                };
            }
            EnableButtons();
            textBox_Total.Text = (dataGridView1.Rows.Count - 1).ToString();
            this.CenterToParent();
            base.ShowDialog();
            return DialogResult.OK;
        }
        private void EnableButtons()
        {
            button_Import.Enabled = button_Open.Enabled = button_Update.Enabled = button_Upload.Enabled = dictWorkAssignment.Count > 0;
        }
        private void button_Close_Click(object sender, EventArgs e)
        {
            Hide();
        }
        private void button_Open_Click(object sender, EventArgs e)
        {
            int pageNo = -1;
            string suttaNo = string.Empty;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                DialogResult res = MessageBox.Show(String.Format("There are more than one page selected. Will open with page {0}.", row.Cells[2].Value.ToString())
                    , "Confirm", MessageBoxButtons.OKCancel);
                if (res == DialogResult.Cancel) { return; }
                pageNo = Convert.ToInt16(row.Cells[1].Value);
                suttaNo = row.Cells[0].Value.ToString();
            }
            if (pageNo == -1)
            {
                if (dataGridView1.SelectedCells.Count == 0) { MessageBox.Show("Select a page first."); return; }
                var row = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                pageNo = Convert.ToInt16(row.Cells[1].Value);
                suttaNo = row.Cells[0].Value.ToString();
            }
            //string srcFName = String.Format("{0}\\{1}-{2}.jpg", parent.sourceDir, suttaNo, pageNo.ToString("D3"));
            //string dataFName = String.Format("{0}\\{1}.txt", parent.dataDir, suttaNo);
            //// check if the page is already downloaded
            //if (!File.Exists(srcFName))
            //{
            //    MessageBox.Show(srcFName + " is not found. Download again."); return;
            //}
            // check if page data file already exists
            if (!Directory.Exists(parent.dataDir))
            {
                try
                {
                    Directory.CreateDirectory(parent.dataDir);
                    DirectoryInfo dInfo = new DirectoryInfo(parent.dataDir);
#pragma warning disable CA1416
                    DirectorySecurity dSecurity = dInfo.GetAccessControl();
                    dSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                    dInfo.SetAccessControl(dSecurity);
#pragma warning restore CA1416
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            EventHandler<EventArgs> handler = OnPageOpen;
            if (handler != null)
            {
                MNOpenEventArgs eargs = new MNOpenEventArgs();
                eargs.MN_No = suttaNo;
                eargs.MN_PageNo = pageNo;
                eargs.MN_startPageNo = pageNo;
                eargs.MN_endPageNo = pageNo;
                handler(this, eargs);
            }
            Hide();
        }
        private void AddUserWorkAssignment(WorkAssignmentRecord rec)
        {
            DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            row.Cells[0].Value = rec.suttaNo;
            row.Cells[1].Value = rec.pageNo.ToString();
            row.Cells[2].Value = rec.NIS_Pages.ToString();      // NIS pages
            row.Cells[3].Value = rec.status;                    // status
            row.Cells[4].Value = rec.timeSpent.ToString();      // hours
            row.Cells[5].Value = rec.turnAroundTime.ToString(); // days
            row.Cells[6].Value = (rec.assignedDate == rec.startDate) ? string.Empty : rec.startDate;  // start date
            row.Cells[7].Value = rec.assignedDate;              // assigned date
            dataGridView1.Rows.Add(row);
        }
        private void button_Source_Click(object sender, EventArgs e)
        {
            bool SourcePDF = true;
            if (!parent.InternetConnection())
            {
                MessageBox.Show("There is no internet connection to check for updates."); return;
            }
            if (SourcePDF)
            {
                parent.downloadSourceToolStripMenuItem_Click(this, new EventArgs());
                return;
            }
            ProgressMessageThread progressMessageThread = new ProgressMessageThread();
            progressMessageThread.Open("Logging in to the server.", this);
            if (parent.GetLoginUserID() == string.Empty)
            {
                if (!parent.formLogin.ServerLogin()) { progressMessageThread.Close(); return; }
            }
            string fileName;
            TipitakaFileStorage tipitakaFileStorage = new TipitakaFileStorage(parent.sourceDir);

            if (dataGridView1.SelectedRows.Count == 0)
            {
                // assigned acount
                int n = workAssignmentRecords.Count(x => x.status == "Assigned");
                if (n > 0)
                {
                    progressMessageThread.Close();
                    DialogResult res = MessageBox.Show(n.ToString() + " assigned pages will be downloaded.", "Confirm", MessageBoxButtons.OKCancel);
                    if (res == DialogResult.Cancel) { progressMessageThread.Close(); return; }
                    if (!Directory.Exists(parent.sourceDir))
                    {
                        try
                        {
                            Directory.CreateDirectory(parent.sourceDir);
                            DirectoryInfo dInfo = new DirectoryInfo(parent.sourceDir);
#pragma warning disable CA1416
                            DirectorySecurity dSecurity = dInfo.GetAccessControl();
                            dSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                            dInfo.SetAccessControl(dSecurity);
#pragma warning restore CA1416
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    else
                    {
                        DirectoryInfo dInfo = new DirectoryInfo(parent.sourceDir);
#pragma warning disable CA1416
                        DirectorySecurity dSecurity = dInfo.GetAccessControl();
                        var listFSAR = dSecurity.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));
                        FileSystemAccessRule fsar = listFSAR.Cast<FileSystemAccessRule>().First(x => x.IdentityReference.Value == "Everyone");
                        if (fsar.FileSystemRights != FileSystemRights.FullControl)
                        {
                            dSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                            dInfo.SetAccessControl(dSecurity);
                        }
                    }
                    progressMessageThread.Open("Please wait while downloading ...", this);
                    parent.Cursor = this.Cursor = Cursors.WaitCursor;
                    List<string> listFilenames = new List<string>();
                    workAssignmentRecords.ForEach(x =>
                    {
                        if (x.status == "Assigned")
                        {
                            fileName = String.Format("{0}-{1}", x.suttaNo, x.pageNo.ToString("D3"));
                            listFilenames.Add(fileName);
                            if (!tipitakaFileStorage.FileDownload(x.suttaNo.Substring(0, 2),
                                Convert.ToInt16(x.suttaNo.Substring(3, 3)), x.pageNo))
                            {
                                progressMessageThread.Close(); MessageBox.Show(tipitakaFileStorage.DBErrMsg); return;
                            };
                            x.status = "Downloaded"; x.updateFlag = true;
                        }
                    });
                    parent.Cursor = this.Cursor = Cursors.Default;
                    listFilenames.Sort();
                    string rowKey = String.Format("{0}-{1}", listFilenames[0], listFilenames[listFilenames.Count - 1].Substring(7, 3));
#nullable enable
                    SuttaPageAssignment? suttaPageAssignment = clientSuttaPageAssignment.GetSuttaPageAssignment(rowKey);
#nullable disable
                    if (suttaPageAssignment != null)
                    {
                        suttaPageAssignment.Status = "Downloaded";
                        clientSuttaPageAssignment.UpdateSuttaPageAssignment(suttaPageAssignment);
                    }
                    progressMessageThread.Close();
                    MessageBox.Show("Source pages downloaded.");
                }
                else
                {
                    progressMessageThread.Close();
                    MessageBox.Show("There are no new assigned pages to download."); return;
                }
            }
            else
            {
                // multiple select
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    string x = row.Cells[0].Value.ToString();
                    tipitakaFileStorage.FileDownload(x.Substring(0, 2),
                        Convert.ToInt16(x.Substring(3, 3)), Convert.ToInt16(row.Cells[1].Value));
                }
            }

            UserPageActivity userPageActivity;
            int index = -1;
            // update UserPageActivity
            workAssignmentRecords.ForEach(x =>
            {
                index++;
                if (x.updateFlag)
                {
                    userPageActivity = new UserPageActivity()
                    {
                        PartitionKey = "UserPageActivity",
                        RowKey = String.Format("{0}-{1}", x.suttaNo, x.pageNo.ToString("D3")),
                        AssignedTo = x.userID,
                        Status = x.status,
                    };
                    dataGridView1.Rows[index].Cells[3].Value = "Downloaded";
                    x.status = "Downloaded"; x.updateFlag = true;
                    clientUserPageActivity.UpdateUserPageActivity(userPageActivity);
                    parent.sqlObj.UpdateUserWorkAssignment(x);
                };
            });
            // update SuttaPageAssignment
            //clientSuttaPageAssignment
            AutoClosingMessageBox.Show("Download complete.", "Notification", 1000);
        }
        private void button_Download_Click(object sender, EventArgs e)
        {
            if (!parent.ServerLogin())
            {
                MessageBox.Show("Server login failed."); return;
            }
            // create dictionary for to store page# for each sutta
            Dictionary<string, List<int>> dictSuttaPages = new Dictionary<string, List<int>>(); 
            ProgressMessageThread progressMessageThread = new ProgressMessageThread();
            progressMessageThread.Open("Please wait while downloading ...", this);
            if (!parent.InternetConnection())
            {
                progressMessageThread.Close();
                MessageBox.Show("There is no internet connection to check for updates."); return;
            }
            if (parent.GetLoginUserID() == string.Empty)
            {
                if (!parent.formLogin.ServerLogin()) { progressMessageThread.Close(); return; }
            }
            if (clientUserPageActivity != null)
            {
                List<UserPageActivity> listUserPageActivity = clientUserPageActivity.QueryUserPageActivity(textBox_UserID.Text);
                int count = listUserPageActivity.Count(x => x.Status == "Assigned" && !dictWorkAssignment.ContainsKey(x.RowKey));
                if (count == 0)
                {
                    progressMessageThread.Close();
                    MessageBox.Show("There are no updates to download from the server."); return;
                }
                List<int> listPageNo = new List<int>();
                foreach (UserPageActivity userPageActivity in listUserPageActivity)
                {
                    if (userPageActivity != null)
                    {
                        if (!dictWorkAssignment.ContainsKey(userPageActivity.RowKey))
                        {
                            DateTime dtStart = (DateTime)userPageActivity.StartDate;
                            WorkAssignmentRecord rec = new WorkAssignmentRecord()
                            {
                                userID = textBox_UserID.Text,
                                suttaNo = userPageActivity.RowKey.Substring(0, 6),
                                pageNo = Convert.ToInt16(userPageActivity.RowKey.Substring(7)),
                                NIS_Pages = userPageActivity.NIS,
                                assignedDate = clientTipitakaDB.GetFormattedDateTimeString(userPageActivity.AssignedDate.ToLocalTime(), true),
                                startDate = clientTipitakaDB.GetFormattedDateTimeString(userPageActivity.StartDate.ToLocalTime(), true),
                                status = userPageActivity.Status,
                                timeSpent = userPageActivity.TimeSpent,
                                turnAroundTime = userPageActivity.TurnAroundTime,
                            };
                            listPageNo.Add(rec.pageNo);
                            if (!dictSuttaPages.ContainsKey(rec.suttaNo))
                            {
                                List<int> ints = new List<int>();
                                ints.Add(rec.pageNo);
                                dictSuttaPages[rec.suttaNo] = ints;
                            }
                            else
                                dictSuttaPages[rec.suttaNo].Add(rec.pageNo);

                            userPageActivity.Status = "Downloaded";
                            clientUserPageActivity.UpdateUserPageActivity(userPageActivity);
                            workAssignmentRecords.Add(rec);
                            AddUserWorkAssignment(rec);
                            parent.sqlObj.AddUserWorkAssignment(rec);
                            dictWorkAssignment.Add(userPageActivity.RowKey, rec);
                        }
                    }
                }
                textBox_Total.Text = (dataGridView1.Rows.Count - 1).ToString();
                // create data table for each sutta assigned
                foreach(KeyValuePair<string, List<int>> kv in dictSuttaPages)
                {
                    string fileName = kv.Key + ".txt";
                    listPageNo = kv.Value;
                    CreateDataPages(fileName, listPageNo);
                    DataFileInfo dataFileInfo = new DataFileInfo()
                    {
                        fileName = fileName,
                        userID = textBox_UserID.Text,
                        startPageNo = listPageNo[0],
                        endPageNo = listPageNo[listPageNo.Count - 1],
                    };
                    sqlObj.AddDataFileInfo(dataFileInfo);
                }
                EnableButtons();
                progressMessageThread.Close();
            }
        }
        private void button_Import_Click(object sender, EventArgs e)
        {
            if (form_ImportSuttaData == null) { return; }

            int rowIndex = (dataGridView1.SelectedCells.Count > 0) ? dataGridView1.SelectedCells[0].RowIndex : -1;
            if (rowIndex == -1 || dataGridView1.Rows[rowIndex].Cells[0].Value == null)
            {
                MessageBox.Show("Sutta not selected."); return;
            }
            string suttaNo = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
            int startPage = -1; int endPage = -1; int pageNo;
            string suttafile = String.Format("{0}\\{1}.txt", parent.dataDir, suttaNo);
            if (!File.Exists(suttafile))
            {
                MessageBox.Show("Import into file '" + suttafile + "' not found."); return;
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row == null || row.Cells[0].Value == null) continue;
                if (row.Cells[0].Value.ToString() == suttaNo)
                {
                    pageNo = Convert.ToInt16(row.Cells[1].Value.ToString());
                    if (startPage == -1 && endPage == -1)
                    {
                        startPage = endPage = pageNo;
                    }
                    else
                    {
                        if (pageNo < startPage) startPage = pageNo;
                        if (pageNo > endPage) endPage = pageNo;
                    }
                }
            }
            form_ImportSuttaData.SetSuttaInfo(suttaNo, startPage, endPage);
            DialogResult res = form_ImportSuttaData.ShowDialog();

            List<string> listOutput = new List<string>();
            List<int> pagesImported = new List<int>();
            Dictionary<int, int> NISRecCountPerPage = new Dictionary<int, int>();
            //int pageCount = 0;
            //int lineCount = 0;
            ProgressMessageThread progressMessageThread = new ProgressMessageThread();
            if (res == DialogResult.OK)
            {
                Dictionary<string, string> dictImportedPages = new Dictionary<string, string>();
                progressMessageThread.Open("Please wait while importing ...", this);
                // get the imported data from form_ImportSuttaData
                foreach (KeyValuePair<string, DataInfoDetails> keyVal in form_ImportSuttaData.dictDataInfo)
                {
                    DataInfoDetails dataInfoDetails = keyVal.Value;
                    int sourceStartPage = dataInfoDetails.sourceStartPage;
                    int fileStartPage = dataInfoDetails.startPage;
                    int fileEndPage = dataInfoDetails.endPage;
                    if (fileStartPage != sourceStartPage)
                    {
                        for (int pgno = fileStartPage; pgno <= fileEndPage; pgno++)
                        {
                            string NISrec = dataInfoDetails.Pages[pgno.ToString()];
                            NISrec = NISrec.Replace("#" + fileStartPage.ToString(), "#" + sourceStartPage.ToString());
                            dictImportedPages.Add(sourceStartPage.ToString(), NISrec);
                            NISRecCountPerPage.Add(sourceStartPage, dataInfoDetails.NISRecCount[fileStartPage]);
                            fileStartPage++; sourceStartPage++;
                        }
                    }
                }
                DataInfo dataInfo = new DataInfo(suttafile);
                dataInfo.FileLoadData();
                foreach (KeyValuePair<string, string> keyValuePair in dictImportedPages)
                {
                    if (dataInfo.Pages.ContainsKey(keyValuePair.Key))
                    {
                        dataInfo.Pages[keyValuePair.Key] = keyValuePair.Value;
                    }
                }
                dataInfo.SaveFile();
                int pgNo;
                dataGridView1.BeginEdit(true);
                WorkAssignmentRecord workAssignmentRecord = new WorkAssignmentRecord();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row == null || row.Cells[0] == null || row.Cells[0].Value == null) continue;
                    pgNo = Convert.ToInt16(row.Cells[1].Value);
                    if (row.Cells[0].Value.ToString() == suttaNo.Trim() && dictImportedPages.ContainsKey(row.Cells[1].Value.ToString()))
                    {
                        row.Cells[3].Value = "Imported";
                        row.Cells[2].Value = NISRecCountPerPage[pgNo].ToString();
                        row.Cells[6].Value = DateTime.Today.ToString("yyyy'-'MM'-'dd");
                        workAssignmentRecord.userID = textBox_UserID.Text;
                        workAssignmentRecord.suttaNo = suttaNo.Trim();
                        workAssignmentRecord.pageNo = pgNo;
                        workAssignmentRecord.NIS_Pages = NISRecCountPerPage[pgNo];
                        workAssignmentRecord.assignedDate = row.Cells[7].Value.ToString();
                        workAssignmentRecord.startDate = row.Cells[6].Value.ToString();
                        workAssignmentRecord.status = "Imported";
                        workAssignmentRecord.timeSpent = 0;
                        workAssignmentRecord.turnAroundTime = 0;
                        sqlObj.UpdateUserWorkAssignment(workAssignmentRecord);
                    }
                }
                dataGridView1.EndEdit();
                dataGridView1.RefreshEdit();
                //StreamWriter fs = new StreamWriter(GetDataDir() + "\\" + suttaNo + ".txt");
                //listOutput.ForEach(x => fs.WriteLine(x));
                //fs.Close();
                progressMessageThread.Close();
            }
        }
        private void button_Upload_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
            if (dataGridView1.Rows[rowIndex].Cells.Count > 0)
            {
                DialogResult res;
                string suttaNo = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
                string suttaPath = String.Format("{0}\\{1}.txt", parent.dataDir, suttaNo);
                string suttaType = suttaNo.Substring(0, 2);
                int suttaNum = Convert.ToInt32(suttaNo.Substring(3));
                if (!File.Exists(suttaPath))
                {
                    MessageBox.Show(String.Format("'{0}' file not found.")); return;
                }

                // load file data to import
                DataInfo dataInfo = new DataInfo(suttaPath);
                dataInfo.FileLoadData();

                ClientTipitakaDB clientTipitakaDB = parent.clientTipitakaDB;
                ClientSuttaPageAssignment clientSuttaPageAssignment = clientTipitakaDB.GetClientSuttaAssignment();
                ClientSuttaInfo clientSuttaInfo = clientTipitakaDB.GetClientSuttaInfo();

                SuttaPageData suttaPageData = new SuttaPageData();
                string partitionKey, rowKey;
                string userID = parent.GetUserID();
                partitionKey = rowKey = suttaNo;// String.Format("{0}-{1}", dataType, suttaNo.ToString("D3"));
                //SetSubPartitionKey(partitionKey);
                // check if user has been assigned to this 
                //clientSuttaAssignment.GetSuttaPageAssignment(partitionKey, userID);
                clientSuttaPageAssignment.QuerySuttaPageAssignment(rowKey);
                List<SuttaPageAssignment> listSuttaPageAssignment = (List<SuttaPageAssignment>)clientSuttaPageAssignment.objResult;
                if (listSuttaPageAssignment.Count == 0 || listSuttaPageAssignment.Count(x => x.AssignedTo == userID) == 0)
                {
                    MessageBox.Show(userID + " is not the assigned user for " + rowKey); return;
                }
                List<int> listAssignedPages = new List<int>();
                foreach (SuttaPageAssignment suttaPageAssignment in listSuttaPageAssignment)
                {
                    string[] f = suttaPageAssignment.RowKey.Split('-');
                    int start = int.Parse(f[f.Length - 2]);
                    int end = int.Parse((f[f.Length - 1]));
                    for (int i = start; i <= end; ++i)
                        listAssignedPages.Add(i);
                }
                List<int> listUnAssignedPages = new List<int>();
                foreach (string pgNo in dataInfo.Pages.Keys)
                {
                    if (!listAssignedPages.Contains(Convert.ToInt32(pgNo))) listUnAssignedPages.Add(Convert.ToInt32(pgNo));
                }
                if (listUnAssignedPages.Count > 0)
                {
                    string unAssignedPages = String.Join(", ", listUnAssignedPages);
                    string suffixNum = listUnAssignedPages.Count > 1 ? "s" : "";
                    string s = String.Format("Unassigned Page{0} Found:   {1}\r\n", suffixNum, unAssignedPages);
                    string s1 = String.Format("Do you want to remove the page{0} and continue?", suffixNum);
                    res = MessageBox.Show(s + s1, "Confirm", MessageBoxButtons.YesNo);
                    if (res == DialogResult.No) { return; }
                    foreach (int pgNo in listUnAssignedPages)
                    {
                        dataInfo.Pages.Remove(pgNo.ToString());
                    }
                }

                Dictionary<string, string> Pages = dataInfo.Pages;
                int firstPage = Convert.ToInt16(Pages.First().Key);
                int lastPage = Convert.ToInt16(Pages.Last().Key);
                string uploadConfirmMsg = String.Format("{0} Page#{1}-{2} ({3} pages) will be uploaded to the server.",
                    suttaPath, firstPage, lastPage, dataInfo.Pages.Count);
                res = MessageBox.Show(uploadConfirmMsg, "Confirm", MessageBoxButtons.OKCancel);
                if (res == DialogResult.Cancel) return;

                // process now
                ProgressMessageThread procMsgThread = new ProgressMessageThread();
                procMsgThread.Open("Please wait while uploading to the server ...", this);
                if (parent.clientTipitakaDB == null || !parent.ServerLogin())
                {
                    procMsgThread.Close(); return;
                }

                // upload for each page and updateHistory
                ClientSuttaPageData clientSuttaPageData = parent.clientTipitakaDB.GetClientSuttaPageData();
                this.Cursor = Cursors.WaitCursor;
                clientSuttaPageData.SetSubPartitionKey(suttaNo);
                clientSuttaPageData.UploadSutta(parent.GetLoginUserID(), suttaType, suttaNum, dataInfo.Pages, dataInfo.NISRecCount);
                parent.SuttaDataAvailable.Add(suttaNum.ToString("D3"));
                parent.SuttaDataAvailable.Sort();

                ClientKeyValueData clientKeyValueData = clientTipitakaDB.GetClientKeyValueData();
                KeyValueData keyValueData = new KeyValueData()
                {
                    PartitionKey = "KeyValueData",
                    RowKey = "SuttaPageData",
                    Value = String.Join(",", parent.SuttaDataAvailable)
                };
                clientKeyValueData.UpdateKeyValueData(keyValueData);
                if (clientKeyValueData.StatusCode != 204)
                {
                    MessageBox.Show("Error: UpdateKeyValueData for " + keyValueData.RowKey);
                }
                // update local DB data
                // https://stackoverflow.com/questions/24083959/using-linq-to-get-datagridview-row-index-where-first-column-has-specific-value
                foreach (string key in dataInfo.Pages.Keys)
                {
                    string pgNo = key;
                    rowIndex = (from r in dataGridView1.Rows.Cast<DataGridViewRow>()
                                where r.Cells[0].Value.ToString() == suttaNo && r.Cells[1].Value != null &&
                                r.Cells[1].Value.ToString() == pgNo
                                select r.Index).First();
                    DataGridViewRow row = dataGridView1.Rows[rowIndex];
                    row.Cells[3].Value = (row.Cells[3].Value.ToString() == "Submitted") ? "Updated" : "Submitted";
                    WorkAssignmentRecord rec = workAssignmentRecords.Find(x => x.suttaNo == suttaNo &&
                                        x.pageNo == Convert.ToInt16(key));
                    if (rec != null)
                    {
                        rec.status = row.Cells[3].Value.ToString();
                        rec.updateFlag = true;
                        sqlObj.UpdateUserWorkAssignment(rec);
                    }
                }
                procMsgThread.Close();
                this.Cursor = Cursors.Default;
                MessageBox.Show(uploadConfirmMsg.Replace(" will be", ""));
            }
        }
        private void CreateDataPages(string suttaNo, List<int> listPgNo)
        {
            string fName = String.Format("{0}\\{1}", parent.dataDir, suttaNo);
            DataInfo dataInfo = new DataInfo(fName);
            dataInfo.MN = suttaNo.Substring(3, 3).TrimStart('0');
            if (File.Exists(fName))
            {
                // read in the existing file
                dataInfo.FileLoadData();
            }
            int lineCount = 0;
            foreach (int pgNo in listPgNo)
            {
                if (!dataInfo.Pages.ContainsKey(pgNo.ToString()))
                {
                    dataInfo.Pages.Add(pgNo.ToString(), String.Format("{0}#{1}", (++lineCount == 1) ? String.Empty : "\r\n", pgNo));
                }
            }
            // if merged with the existing data, page sorting is required
            if (File.Exists(fName)) dataInfo.SortPages();
            dataInfo.SaveFile();
        }
        void DoUpdateWorkAssignment(object sender, EventArgs e)
        {
            WorkAssignmentEventArgs eventArgs = (WorkAssignmentEventArgs)e;
            int n = dataGridView1.Rows.Count;
            var row = (dataGridView1.Rows.Cast<DataGridViewRow>()
                .FirstOrDefault(r => r != null && r.Cells[0].Value != null && r.Cells[0].Value.ToString() == eventArgs.MN_No &&
                       r.Cells[1].Value != null && r.Cells[1].Value.ToString() == eventArgs.pageNo.ToString()));
            if (row == null) return;
            // app does not update for submitted pages
            if (row.Cells[3].Value.ToString() == "Submitted") return;
            int index = row.Index;
            dataGridView1.BeginEdit(true);
            row.Cells[2].Value = eventArgs.NISRec.ToString();
            workAssignmentRecords[index].NIS_Pages = eventArgs.NISRec;
            if (row.Cells[6].Value.ToString().Length == 0)
            {
                // no start date. set today is start date
                row.Cells[6].Value = workAssignmentRecords[index].startDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            row.Cells[3].Value = workAssignmentRecords[index].status = "In progress";
            float t = (float)(Convert.ToDouble(dataGridView1.Rows[index].Cells[4].Value) + eventArgs.timeSpent / 60);
            row.Cells[4].Value = t.ToString();
            workAssignmentRecords[index].timeSpent = t;
            workAssignmentRecords[index].updateFlag = true;
            int turnAroundTime = 0;
            string[] f = workAssignmentRecords[index].startDate.Split("-");
            DateTime startTime = new DateTime(int.Parse(f[0]), int.Parse(f[1]), int.Parse(f[2]));
            TimeSpan ts = DateTime.Now - startTime;
            turnAroundTime = ts.Days;
            if (turnAroundTime == 0) { turnAroundTime = 1; }
            row.Cells[5].Value = turnAroundTime.ToString();
            workAssignmentRecords[index].turnAroundTime = turnAroundTime;
            dataGridView1.EndEdit();
            dataGridView1.RefreshEdit();
            parent.sqlObj.UpdateUserWorkAssignment(workAssignmentRecords[index]);
        }
    }
}
