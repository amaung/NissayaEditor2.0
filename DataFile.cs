using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Spell_Checker;
using NissayaEditor.File_Manager;
using NissayaEditor;

namespace NissayaEditor_DataFile
{
    public class DataInfo
    {
        Form1 form;
        TabControl tabControl;
        TabPage tabPage;
        FileManager fMgr;
        public string MN = string.Empty;
        int countTotal;
        string paliText = string.Empty;
            
        public const int dvTextBox = 1;
        public const int dvDataGrid = 2;
        public const int dvBoth = 3;

        public int ThreadAction = 0;
        public int ViewCode = Form1.ViewFullDoc;
        public bool UpdateFileContent = false;
        public string PageToLoad = string.Empty;

        public bool flagPali;
        public bool flagPlain;
        public bool UseSecondaryGridContainer = false;
        public RichTextBox richTextBox, richTextBox2;
        public DataGridView dataGridView; //, dataGridView2;
        public string fileContent;
        public SortedDictionary<int, string> SortedPages = new SortedDictionary<int, string>();
        public Dictionary<string, string> Pages = new Dictionary<string, string>();
        public Dictionary<int, int> NISRecCount = new Dictionary<int, int>();
        public Dictionary<string, string> MFU_Words = new Dictionary<string, string>();
        public Dictionary<string, int> MFU_WordCount = new Dictionary<string, int>();
        public struct KeyMap
        {
            public string curKey;
            public string orgKey;

            public KeyMap(string key, string org) { curKey = key; orgKey = org; }
        }
        public List<KeyMap> KeyMapper = new List<KeyMap>();

        public List<string> GetPageList()
        {
            List<string> l = new List<string>();
            foreach (KeyMap k in KeyMapper)
                l.Add(k.curKey);
            return l;
        }
        private List<Form1._NissayaDataRecord> NissayaRecordList = new List<Form1._NissayaDataRecord>();
        public List<Form1._NissayaDataRecord> GetDataList() { return NissayaRecordList; }

        private string curPage = string.Empty;
        string paraNewLine = "\n\n";
        private delegate void SafeCallDelegate(string s, int start, int end);
        private delegate void SafeDeleteRowsDelegate();
        private delegate void safeCallAddDataGridView(string srno, char marker, string pali, string plain, string footnote);
        private delegate void safeCallRichTextAppend(int codeView, string s);
        private delegate void safeCallRichTextUpdate(Color color, string s);
        private delegate void safeCallGetRichText();
        private delegate void safeTextBox2Clear();
        public string GetCurrentPage() { return curPage; }

        public SQLite.SQLiteObj sqlObj;// = new SQLite.SQLiteObj();
        public SpellChecker spellChecker;// = new SpellChecker();

        List<string> sqlUnityQtyNames;

        public DataInfo(Form1 f, TabControl tc, TabPage tb, RichTextBox rtb, DataGridView dgv, string fName)
        {
            form = f;
            tabControl = tc;
            tabPage = tb;
            richTextBox = rtb;
            dataGridView = dgv;
            string fn = Path.GetFileNameWithoutExtension(fName);
            if (fn.Length > 15) fn = fn.Substring(0, 5) + " ... " + fn.Substring(fn.Length - 5, 5);
            tb.Text = fn;

            fMgr = new FileManager(fName, richTextBox);
            sqlObj = form.sqlObj;
            spellChecker = form.spellChecker;

            //sqlUnityQtyNames = sqlObj.GetUnitQtyNouns();
            //spellChecker.SetDictionaries(form.PhoneticWords, form.UnitQtyNouns);
        }

        public DataInfo(RichTextBox rtb, DataGridView dgv, string fName)
        {
            richTextBox = rtb;
            dataGridView = dgv;
            string fn = Path.GetFileNameWithoutExtension(fName);
            if (fn.Length > 15) fn = fn.Substring(0, 5) + " ... " + fn.Substring(fn.Length - 5, 5);
            fMgr = new FileManager(fName, richTextBox);
            sqlUnityQtyNames = sqlObj.GetUnitQtyNouns();
            spellChecker.SetDictionaries(form.PhoneticWords, form.UnitQtyNouns);
        }
        public DataInfo(string fName)
        {
            fMgr = new FileManager(fName, richTextBox);
        }
        public void SetDataContainers2(RichTextBox rtb2) //, DataGridView dgv2)
        {
            richTextBox2 = rtb2;
            //dataGridView2 = dgv2;
        }

        public void SetFileName(string fname)
        {
            fMgr.fName = fname;
            string fn = Path.GetFileNameWithoutExtension(fname);
            if (fn.Length > 15) fn = fn.Substring(0, 5) + " ... " + fn.Substring(fn.Length - 5, 5);
            tabPage.Text = fn;
        }
            
        public void ClearMemory()
        {
            tabPage.Text = "<New>";
            clearDataGridView();
        }

        //public void logElapsedTime(string s, long milliSec)
        //{
        //    FileStream fileStream = new FileStream("log.txt", FileMode.Append);
        //    StreamWriter writer = new StreamWriter(fileStream);
        //    writer.WriteLine(s + " Elapsed Time = " + milliSec.ToString() + " ms");
        //    writer.Flush();
        //    writer.Close();
        //}
        public void FileLoadData()
        {
            if (!ReadFileData()) return;
            CleanupMarkers();
            paraNewLine = getCR2();
            ExtractHeader();
            parseIntoPages();
        }
        private bool ReadFileData()
        {
            bool flag = true;
            fileContent = fMgr.ReadFile().Trim();
            if (fileContent.Length == 0 || !ValidateData())
            {
                string msg;
                if (fileContent.Length == 0)
                {
                    msg = fMgr.fName + " is empty.";
                    MessageBox.Show(msg);
                    flag = false;
                }
            }
            return (flag);
        }
        private void CleanupMarkers()
        {
            fileContent = fileContent.Replace("* ", "*");
            fileContent = fileContent.Replace("^ ", "^");
            fileContent = fileContent.Replace("^*", "^ *");
            fileContent = fileContent.Replace("*^", "* ^");
            if (fileContent[fileContent.Length - 1] == '^') fileContent += " ";
        }
        public void ReadFileLoadData(int viewCode)
        {
            fileContent = fMgr.ReadFile().Trim();
            if (fileContent.Length == 0 || !ValidateData())
            {
                string msg;
                if (fileContent.Length == 0)
                {
                    msg = fMgr.fName + " is empty.";
                    MessageBox.Show(msg);
                    return;
                }
                //msg = fMgr.fName + " has mismatched number of (*) and (^).\n\n";
                //msg += "The program will attempt to fix them.";
                //DialogResult res = MessageBox.Show(msg);
                //FixNISRecords();
            }
                
            BuildWordDictionary();

            // clean up the space after markers
            fileContent = fileContent.Replace("* ", "*");
            fileContent = fileContent.Replace("^ ", "^");
            fileContent = fileContent.Replace("^*", "^ *");
            fileContent = fileContent.Replace("*^", "* ^");
            if (fileContent[fileContent.Length - 1] == '^') fileContent += " ";

            paraNewLine = getCR2();
            // check for header info
            ExtractHeader();
            parseIntoPages();
            //logElapsedTime("DataInfo.ReadFileLoadData.ParseIntoPages():",
            //    (DateTime.Now.Ticks - start_time) / 10000);
            string s = (PageToLoad.Length == 0) ? Pages.Keys.First<string>() : PageToLoad;
            PageToLoad = s;
            LoadPageData(viewCode);
        }

        private bool ValidateData()
        {
            if (fileContent.Length == 0) return false;
            string pattern = "\\*";
            MatchCollection matches1 = Regex.Matches(fileContent, pattern);
            pattern = "\\^";
            MatchCollection matches2 = Regex.Matches(fileContent, pattern);
            return (matches1.Count == matches2.Count);
        }
            
        private void FixNISRecords()
        {
            Dictionary<int, string> updatedRecs = new Dictionary<int, string>();
            //Dictionary<int, string> updatedPage = new Dictionary<int, string>();
            List<string> updatedPage = new List<string>();
            List<string> pageList = new List<string>();
            string compoundPaliText = string.Empty;
            string[] pages = fileContent.Split('#');
            int pageNo = -1;
            string t0 = string.Empty;
            string pg, pgno = string.Empty;
            foreach (string page in pages)
            {
                ++pageNo;
                pg = page.Trim();
                if (pg.Length == 0) continue;
                string pattern = "\\*";
                MatchCollection matches1 = Regex.Matches(page, pattern);
                pattern = "\\^";
                MatchCollection matches2 = Regex.Matches(page, pattern);
                if (matches1.Count != matches2.Count)
                {
                    string[] nisRecords = page.Split('*');
                    int n1 = -1;
                    foreach (string nisRec in nisRecords)
                    {
                        t0 = string.Empty;
                        if (++n1 == 0)
                        {
                            pgno = nisRec;
                            continue; // this is to skip the page no
                        }
                        string[] trans = nisRec.Split('^');
                        if (trans.Length > 0) t0 = trans[0].Trim();
                        switch (trans.Length)
                        {
                            case 0:
                            case 1:
                                // found Pali text only
                                if (t0.Length > 0)
                                    updatedPage.Add("*" + t0 + " ^");
                                break;
                            case 2:
                                // normal NIS record
                                updatedPage.Add("*" + t0 + " ^" + trans[1]);
                                break;
                            default:
                                int n2 = -1; string s2 = string.Empty;
                                foreach (string t in trans)
                                {
                                    ++n2;
                                    if (n2 == 0) s2 = "*" + t;
                                    else
                                    {
                                        if (n2 == 1) updatedPage.Add(s2 + " ^" + t);
                                        else
                                        {
                                            if (t.Trim().Length > 0)
                                                updatedPage.Add("* ^" + t);
                                        }
                                    }
                                }
                                break;
                        }
                    }
                    // update NIS records
                    string s = pgno;
                    foreach (string p in updatedPage)
                        s += p;
                    pageList.Add(s);
                    updatedPage.Clear();
                    pgno = string.Empty;
                }
                else
                {
                    // no errors in this page
                    pageList.Add(page);
                }
            }
                
            UpdateFileContent = true;
            pageNo = -1;
            fileContent = string.Empty;
            foreach (string p in pageList)
            {
                //if (fileContent.Length > 0) fileContent += Environment.NewLine;
                fileContent += "#" + p;
            }
        }

        public Dictionary<string, SQLite.WordCount> highFreqWords = new Dictionary<string, SQLite.WordCount>();

        //public void LoadWordDictionary()
        //{
        //    highFreqWords = sqlObj.LoadWordDictionary();
        //}

        private void BuildWordDictionary()
        {
            highFreqWords.Clear();
            List<string> pageList = new List<string>();
            string[] pages = fileContent.Split('#');
            string pg;
            foreach (string page in pages)
            {
                pg = page.Trim();
                if (pg.Length == 0 || pg[0] == '{') continue;

                string[] nisRecords = page.Split('*');
                foreach (string nisRec in nisRecords)
                {
                    string[] pali = nisRec.Split('^');
                    if (pali.Length > 1)
                    {
                        string[] trans = pali[1].Split('@');
                        if (trans[0].Trim().Length > 1)
                        {
                            UpdateWordFrequency(pali[0].Trim(), trans[0].Trim());
                        }
                    }
                }
            }
            // save in db
            //SQLite.WordCount dbEntry = new SQLite.WordCount();
            foreach (KeyValuePair<string, SQLite.WordCount> kv in highFreqWords)
            {
                string word = kv.Value.word;
                if (!form.highFreqWords.ContainsKey(kv.Key))
                    form.highFreqWords.Add(kv.Key, kv.Value);
                //if (kv.Value.word.Length >= 2 && kv.Value.count > 1)
                //    sqlObj.UpdateWordCount(kv.Key, kv.Value.word, kv.Value.count);
            }
            //sqlObj.CloseDB();
                
        }

        private void UpdateWordFrequency(string key, string value)
        {
            if (value.Length >= 5 && value.Length <= 30)
            {
                if (!highFreqWords.ContainsKey(key))
                {
                    highFreqWords.Add(key, new SQLite.WordCount(value, 1));
                }
                else
                {
                    if (highFreqWords[key].word == value)
                    {
                        // update word count
                        highFreqWords[key].count++;
                    }
                }
            }
        }

        private void ExtractHeader()
        {
            int pos = fileContent.IndexOf("}\r");
            if (pos == -1) pos = fileContent.IndexOf("}\n");
            if (pos == -1) return;
            string s = fileContent.Substring(0, pos + 1).Trim();
            if (s[0] == '{' && s[s.Length - 1] == '}')
            {
                s = s.Substring(1, s.Length - 2);
                string[] f = s.Split(':');
                if (f[0] == "MN") MN = f[1];
                pos = fileContent.IndexOf('#', pos);
                fileContent = fileContent.Substring(pos);
            }
        }

        private void parseIntoPages()
        {
            //string paraNewLine = "\n\n";
            string key;
            List<string> paraPages = new List<string>();
            int start = 0;
            int end = fileContent.IndexOf(paraNewLine);
            try
            {
                while (end != -1)
                {
                    paraPages.Add(fileContent.Substring(start, end - start + 1));
                    start = end + paraNewLine.Length;
                    end = fileContent.IndexOf(paraNewLine, start);
                }
                string lastPage = fileContent.Substring(start).Trim();
                if (lastPage.Length > 0) paraPages.Add(lastPage);

                List<string> listParaPages = new List<string>();
                // cleanup
                foreach (string s in paraPages)
                {
                    string s1 = s.Trim();
                    if (s1.Length > 0)
                    {
                        listParaPages.Add(s1);
                        end = s1.IndexOf('*');
                        // 2022-05-29 v1.0.3
                        if (end == -1) key = s1;
                        else
                        {
                            if (end == 0)
                            {
                                MessageBox.Show("Page number missing in '" + s1.Substring(0, 20) + "'");
                                key = s1;
                            }
                            else
                            {

                                key = s1.Substring(0, end);
                                // if there is no space between page# and *, add one
                                if (s1[end - 1] != ' ')
                                {
                                    s1 = key + " " + s1.Substring(end);
                                }
                            }
                        }
                        if (key[0] == '#') key = key.Substring(1);
                        key = key.Trim();
                        // key cannot have spaces, so use the first non-space contiguous chars
                        int n = key.IndexOf(' ');
                        if (n != -1) key = key.Substring(0, n);
                        KeyMapper.Add(new KeyMap(key, key));
                        Pages.Add(key, s1);
                        //string pgKey = String.Format("MN-{0}-{1}", MN, key.PadLeft(3, '0'));
                        //int count = s1.Count(x => x == '*');
                        NISRecCount.Add(Convert.ToInt32(key), s1.Count(x => x == '*'));
                    }
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Error in DataFile::ParseIntoPages() : " + ex.Message);
            }
        }

        public void UpdatePageKey(string newKey, string oldKey)
        {
            int i = 0;
            while (i < KeyMapper.Count)
            {
                if (KeyMapper[i].curKey == oldKey)
                {
                    KeyMap km = new KeyMap(newKey, KeyMapper[i].orgKey);
                    KeyMapper[i] = km;
                    break;
                }
                ++i;
            }
        }

        public string GetOrigKey(string curKey)
        {
            int i = 0;
            while (i < KeyMapper.Count)
            {
                if (KeyMapper[i].curKey == curKey)
                    return KeyMapper[i].orgKey;
                ++i;
            }
            return curKey;
        }

        public void LoadPageData(int viewCode = Form1.ViewFullDoc)
        {
            if (curPage.Length > 0 && UpdateFileContent) Pages[curPage] = fileContent;
            string orgKey = GetOrigKey(PageToLoad);
            if (orgKey.Length > 0)
            {
                fileContent = Pages[orgKey];
                curPage = PageToLoad;
                loadData(viewCode);
            }
        }

        public void LoadPageData2(int viewCode = Form1.ViewFullDoc)
        {
            string orgKey = GetOrigKey(PageToLoad);
            if (orgKey.Length > 0)
            {
                fileContent = Pages[orgKey];
                curPage = PageToLoad;
                loadData(viewCode);
            }
        }

        public void loadData(int viewCode = Form1.ViewFullDoc)
        {
            if (fileContent != null && fileContent.Length == 0) return;

            flagPali = flagPlain = true;
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //updateDataView(Form1.ViewFullDoc, dvBoth);
            //updateDataView(viewCode, dvBoth);
            updateDataView(viewCode); //, viewCode == 4 ? dvDataGrid : dvTextBox);
            //stopwatch.Stop();
            //TimeSpan ts = stopwatch.Elapsed;
            //if (dbgTrace != null) dbgTrace.OutElpasedTime(ts);
        }
        public int GetNISCount(string pgNo)
        {
            int count = 0;
            if (Pages.ContainsKey(pgNo))
            {
                count = Pages[pgNo].Count(x  => x == '*');
            }
            return count;
        }
        // codeView
        // 1 = Pali text only
        // 2 = Plain text only
        // 3 = Full doc
        public void RefreshData(int codeView)
        {
            if (fileContent != null && fileContent.Length == 0) return;

            updateDataView(codeView); //, dvTextBox);
        }

        private string getCR2()
        {
            string WinCR2 = Environment.NewLine + Environment.NewLine;
            string UnixCR2 = "\n\n";
            if (fileContent.Length == 0) return UnixCR2;
            if (fileContent.IndexOf(WinCR2) != -1) return WinCR2;
            if (fileContent.IndexOf(UnixCR2) != -1) return UnixCR2;
            return UnixCR2;
        }

        //static string KeyPageNo = string.Empty;
        public string GetNextPageNo(string curPgNo)
        {
            string nextPageNo = curPgNo;
            string orgKey = GetOrigKey(curPgNo);
            if (Pages.ContainsKey(orgKey))
            {
                List<string> list = Pages.Keys.ToList();
                int n = list.IndexOf(orgKey);
                //KeyPageNo = orgKey;
                //Predicate<string> predicate = SearchKey;
                //n = list.FindIndex(predicate);
                if (n < list.Count - 1) nextPageNo = list[++n];
            }
            return nextPageNo;
        }
        // 2022-05-29 v1.0.3
        public Dictionary<string, MatchCollection> SearchText(string s)
        {
            Dictionary<string, MatchCollection> findResults = new Dictionary<string, MatchCollection>();
            MatchCollection matches;
            string fname = fMgr.fName; 
            foreach (KeyValuePair<string, string> kv in Pages)
            {
                matches = Regex.Matches(kv.Value, s);
                if (matches.Count > 0) findResults.Add(fname + "-P" + kv.Key, matches);
            }
            return findResults;
        }

        public void updateDataView(int codeView) //, int dview)
        {
            countTotal = 0;
            string s;
            char marker;
            int start, end;
            start = 0;
                
            if (fileContent.Length == 0) return;

            int dview = codeView == Form1.ViewTabular ? dvDataGrid : dvTextBox;
            // clear view
            //if ((dview & dvTextBox) == dview) richTextBox.Text = string.Empty;
            if ((dview & dvDataGrid) == dvDataGrid) clearDataGridView();

            //nissayaData.Clear();
            end = fileContent.IndexOfAny(Form1.recMarkers);
            if (end == 0) end = fileContent.IndexOfAny(Form1.recMarkers, 1);

            while (end != -1)
            {
                s = fileContent.Substring(start, end - start).Trim(' ');
                marker = fileContent[start];
                populateData(codeView, dview, marker, s);
                start = end;
                end = fileContent.IndexOfAny(Form1.recMarkers, start + 1);
            }

            s = fileContent.Substring(start, fileContent.Length - start).Trim();
            //string t = richTextBox.Text.Substring(100);
            marker = fileContent[start];
            populateData(codeView, dview, marker, s);
            return;
        }
        private void TextBox2Clear() { richTextBox2.Text = string.Empty; }

        public string GetPageContent(string pgno, int CodeView)
        {
            string content = string.Empty;
            return Pages[GetOrigKey(pgno)];
        }

        public List<Form1._GridViewSelection> FindGridPageMatches(string pgno, string s)
        {
            List<Form1._GridViewSelection> listMatches = new List<Form1._GridViewSelection>();
            if (!Pages.ContainsKey(pgno)) return listMatches;
            string data = Pages[pgno];
            if (data.Length == 0) return listMatches;
            int pos1 = data.IndexOf('*');
            int pos2 = data.IndexOf('^');
            string p;
            int row = 0; // first row is page number

            if (s == Form1.EmptyCell) s = string.Empty;

            // col0 = row number; col1 = *; col2 = pali; col3 = translation; col4 = footnote
            while (pos1 != -1 && pos2 != -1)
            {
                row++;
                // find in Pali text
                p = data.Substring(pos1, pos2 - pos1 - 1);
                if ((s.Length == 0 && p.Trim().Length == 0) || (s.Length > 0 && p.IndexOf(s) != -1))
                {
                    // search string found in Pali
                    listMatches.Add(new Form1._GridViewSelection(row, 2));
                }
                // find in translation text
                pos1 = data.IndexOf('*', pos2 + 1);
                p = pos1 == -1 ? data.Substring(pos2 + 1) : data.Substring(pos2 + 1, pos1 - pos2 - 1);
                string[] f = p.Split('@');
                if ((s.Length == 0 && f[0].Trim().Length == 0) || 
                    (s.Length > 0 && f[0].IndexOf(s) != -1))
                //if (f[0].IndexOf(s) != -1)
                {
                    // search string found in translation
                    listMatches.Add(new Form1._GridViewSelection(row, 3));
                }
                // check in footnote if it exists
                if (f.Length == 2 && 
                    ((s.Length == 0 && f[1].Trim().Length > 0) || 
                    (s.Length > 0 && f[1].IndexOf(s) != -1)))
                //if (f.Length == 2 && f[1].IndexOf(s) != -1)
                {
                    // search string found in footnote
                    listMatches.Add(new Form1._GridViewSelection(row, 4));
                }
                pos1 = data.IndexOf('*', pos2 + 1);
                pos2 = pos1 == -1 ? -1 : data.IndexOf('^', pos1 + 1);
            }
            return listMatches;
        }

        public List<Form1._GridViewSelection> SpellCheckGridPageMatches(string pgno)
        {
            List<Form1._GridViewSelection> listMatches = new List<Form1._GridViewSelection>();
            if (!Pages.ContainsKey(pgno)) return listMatches;
            string data = Pages[pgno];
            if (data.Length == 0) return listMatches;
            int pos1 = data.IndexOf('*');
            int pos2 = data.IndexOf('^');
            string p;
            string s = string.Empty;
            string errMsg = string.Empty;
            int row = 0; // first row is page number

            while (pos1 != -1 && pos2 != -1)
            {
                row++;
                SpellChecker(data.Substring(pos1 + 1, pos2 - pos1 - 1), row, 2, listMatches);
                pos1 = data.IndexOf('*', pos2 + 1);
                p = pos1 == -1 ? data.Substring(pos2 + 1) : data.Substring(pos2 + 1, pos1 - pos2 - 1);
                string[] f = p.Split('@');
                s = f[0];
                if (f.Length > 0) SpellChecker(f[0], row, 3, listMatches); // ^
                if (f.Length > 1) SpellChecker(f[1], row, 4, listMatches); // @
                pos1 = data.IndexOf('*', pos2 + 1);
                pos2 = pos1 == -1 ? -1 : data.IndexOf('^', pos1 + 1);
            }
            return listMatches;
        }

        private void SpellChecker(string s, int row, int col, List<Form1._GridViewSelection> listMatches)
        {
            MatchCollection matches;
            string errMsg = string.Empty;
            bool[] spellCheckerSettings = form.spellCheckerSettings.GetSettings();

            // find invalid Myanmar characters
            s = spellChecker.MyanmarCharsCleanup(s);
            if (spellCheckerSettings[SpellingCheckerSettings.InvMyanChars])
            {
                matches = spellChecker.FindInvalidMyanmarLetters(s);
                AddListMatches(listMatches, matches, row, col, 1);
            }
            // quote pair check
            if (spellCheckerSettings[SpellingCheckerSettings.MismatchedPairs])
            {
                matches = spellChecker.PairCheck(s);
                if (matches != null) AddListMatches(listMatches, matches, row, col, 0);
            }
            // find basic Myanmar letter errors
            if (spellCheckerSettings[SpellingCheckerSettings.GrammarErrs])
            {
                matches = spellChecker.MyanmarBasicErrors(s);
                AddListMatches(listMatches, matches, row, col, 2);
            }
            // find zeroes
            if (spellCheckerSettings[SpellingCheckerSettings.ZeroWalone])
            {
                matches = spellChecker.FindZeroes(s);
                AddListMatches(listMatches, matches, row, col, 3);
            }

            if (col <= 2) return;   // Pali & Footnote do not need to check the rest
            if (spellCheckerSettings[SpellingCheckerSettings.PhoneticWords])
            {
                matches = spellChecker.FindPhoneticWords(s);
                AddListMatches(listMatches, matches, row, col, 4);
            }
            // One unit error
            if (spellCheckerSettings[SpellingCheckerSettings.OneUnitErrs])
            {
                matches = spellChecker.FindInCorrectQty(s);
                AddListMatches(listMatches, matches, row, col, 4);
            }
        }

        private void AddListMatches(List<Form1._GridViewSelection> listMatches, MatchCollection matches, int row, int col, int errType)
        {
            if (matches != null && matches.Count > 0)
            {
                string errMsg = spellChecker.GetErrMsg();
                List<Form1._ErrDesc> listErrDesc = spellChecker.GetErrDesc();
                Form1._GridViewSelection gvs;
                switch (errType)
                {
                    case 0:
                    case 2:
                        foreach (Form1._ErrDesc errDesc in listErrDesc)
                        {
                            gvs = new Form1._GridViewSelection(row, col, matches, errMsg, errType, true);
                            gvs.listErrDesc = new List<Form1._ErrDesc>();
                            gvs.listErrDesc.Add(errDesc);
                            gvs.corrected = errType == 0;
                            listMatches.Add(gvs);
                        }
                        break;
                    case 3:
                            gvs = new Form1._GridViewSelection(row, col, matches, errMsg, errType, true);
                            gvs.listErrDesc = new List<Form1._ErrDesc>();
                            gvs.listErrDesc.AddRange(listErrDesc);
                            gvs.corrected = false;
                            listMatches.Add(gvs);
                        break;
                    default:
                        foreach (Form1._ErrDesc errDesc in listErrDesc)
                        {
                            gvs = new Form1._GridViewSelection(row, col, matches, errMsg, errType, true);
                            gvs.listErrDesc = new List<Form1._ErrDesc>();
                            gvs.listErrDesc.Add(errDesc);
                            gvs.corrected = false;
                            listMatches.Add(gvs);
                        }
                    break;
                }
            }
        }

        public void GetPageData(string pgno, int codeView)
        {
            countTotal = 0;
            string s, pageContent;
            char marker;
            int start, end;
            start = 0;

            int dview = codeView == Form1.ViewTabular ? dvDataGrid : dvTextBox;
            if (dview == dvTextBox)
            {
                if (richTextBox2.InvokeRequired)
                {
                    var d = new safeTextBox2Clear(TextBox2Clear);
                    richTextBox2.Invoke(d, new object[] { });
                }
                else richTextBox2.Text = string.Empty;
            }
            // clear view
            // if the data will be retrieved in 
            if (codeView == Form1.ViewTabular)
            {
                if (UseSecondaryGridContainer) NissayaRecordList.Clear(); // removeDataGridViewRows2();
                else clearDataGridView();
            }

            pageContent = Pages[GetOrigKey(pgno)];

            end = pageContent.IndexOfAny(Form1.recMarkers);
            if (end == 0) end = pageContent.IndexOfAny(Form1.recMarkers, 1);

            while (end != -1)
            {
                s = pageContent.Substring(start, end - start).Trim(' ');
                marker = pageContent[start];
                GetRecord(codeView, dview, marker, s);
                start = end;
                end = pageContent.IndexOfAny(Form1.recMarkers, start + 1);
            }

            s = pageContent.Substring(start, pageContent.Length - start).Trim();
            marker = pageContent[start];
            GetRecord(codeView, dview, marker, s);
        }

        private void GetRecord(int codeView, int dview, char marker, string s)
        {
            string dgvPali, dgvPlain, dgvFootnote;
            string rtbPali, rtbPlain, rtbFootnote;
            dgvPali = dgvPlain = dgvFootnote = string.Empty;
            rtbPali = rtbPlain = rtbFootnote = string.Empty;
            s = s.Trim(' ');
            //richTextBox.Text = "AZM";
            switch (marker)
            {
                case '*':
                    parseNissayaDataRecord(s, out dgvPali, out dgvPlain, out dgvFootnote);
                    rtbPali = (dgvPali.Length > 0) ? "*" + dgvPali : string.Empty;
                    rtbPlain = (dgvPlain.Length > 0) ? "^" + dgvPlain : string.Empty;
                    rtbFootnote = (dgvFootnote.Length > 0) ? "@" + dgvFootnote : string.Empty;
                    break;
                case '#':
                    dgvPali = s.Substring(1);
                    rtbPali = s;
                    break;
                default:
                    // assume this is a comment line
                    marker = '#';
                    rtbPali = marker + s;
                    dgvPali = s;
                    dgvPlain = dgvFootnote = rtbPali = rtbFootnote = string.Empty;
                    break;
            }
            if ((dview & dvDataGrid) == dvDataGrid)
                populateDataGridView(marker, dgvPali, dgvPlain, dgvFootnote);
            if ((dview & dvTextBox) == dvTextBox)
                AddViewText(codeView, rtbPali, rtbPlain, rtbFootnote);
        }

        void AddViewText(int codeView, string rtbPali, string rtbPlain, string rtbFootnote)
        {
            if (codeView == Form1.ViewTabular)
            {
            }
            else
            {
                if ((codeView == Form1.ViewFullDoc || codeView == Form1.ViewPali) && rtbPali.Length > 0)
                {
                    if (richTextBox2.Text.Length > 0) richTextBox2.AppendText(" ");
                    richTextBox2.AppendText(rtbPali);
                }
                if ((codeView == Form1.ViewFullDoc || codeView == Form1.ViewPlain) && rtbPlain.Length > 0)
                {
                    if (richTextBox2.Text.Length > 0) richTextBox2.AppendText(" ");
                    richTextBox2.AppendText(rtbPlain);
                }
                if ((codeView == Form1.ViewFullDoc) && rtbFootnote.Length > 0)
                {
                    if (richTextBox2.Text.Length > 0) richTextBox2.AppendText(" ");
                    richTextBox2.AppendText(rtbFootnote);
                }
            }
        }

        private void populateData(int codeView, int dview, char marker, string s)
        {
            string dgvPali, dgvPlain, dgvFootnote;
            string rtbPali, rtbPlain, rtbFootnote;
            dgvPali = dgvPlain = dgvFootnote = string.Empty;
            rtbPali = rtbPlain = rtbFootnote = string.Empty;
            s = s.Trim(' ');
            //richTextBox.Text = "AZM";
            switch (marker)
            {
                case '*':
                    parseNissayaDataRecord(s, out dgvPali, out dgvPlain, out dgvFootnote);
                    rtbPali = (dgvPali.Length > 0) ? "*" + dgvPali : string.Empty;
                    rtbPlain = (dgvPlain.Length > 0) ? "^" + dgvPlain : string.Empty;
                    rtbFootnote = (dgvFootnote.Length > 0) ? "@" + dgvFootnote : string.Empty;
                    break;
                case '#':
                    dgvPali = s.Substring(1);
                    rtbPali = s;
                    break;
                default:
                    // assume this is a comment line
                    marker = '#';
                    rtbPali = marker + s;
                    dgvPali = s;
                    dgvPlain = dgvFootnote = rtbPali = rtbFootnote = string.Empty;
                    break;
            }
            if ((dview & dvDataGrid) == dvDataGrid) 
                populateDataGridView(marker, dgvPali, dgvPlain, dgvFootnote);
            if ((dview & dvTextBox) == dvTextBox)
            {
                populateRichTextBoxView(codeView, rtbPali);
                if (rtbPlain.Length > 0) populateRichTextBoxView(codeView, rtbPlain);
                if (rtbFootnote.Length > 0) populateRichTextBoxView(codeView, rtbFootnote);
            }
        }

        private void populateDataGridView(char marker, string pali, string plain, string footnote)
        {
            string srno;

            srno = (++countTotal).ToString();
            if (dataGridView.InvokeRequired)
            {
                var d = new safeCallAddDataGridView(safeCallDataGridViewAddRow);
                richTextBox.Invoke(d, new object[] { srno, marker, pali, plain, footnote });
            }
            else
                safeCallDataGridViewAddRow(srno, marker, pali, plain, footnote);
        }

        private void populateRichTextBoxView(int codeView, string s)
        {
            if (s.Length <= 1) return;

            // check for Pali text
            if (displayOn(s[0], codeView))
            {
                Color color = GetColor(s[0]);
                if (codeView < Form1.ViewFullDoc) s = s.Substring(1);

                //safeCallRichTextAppend
                if (richTextBox.InvokeRequired)
                {
                    var d = new safeCallRichTextUpdate(appendRichTextBox);
                    richTextBox.Invoke(d, new object[] { color, s });
                }
                else
                    appendRichTextBox(color, s);
            }
        }

        private void appendRichTextBox(Color color, string s)
        {
            int start = richTextBox.Text.Length;
            // add a trailing blank to the existing text
            //if (start > 0 && richTextBox.Text[start - 1] != ' ') richTextBox.Text += " ";
            s += " ";
            int end = start + s.Length;
            richTextBox.Select(start, end);
            richTextBox.SelectionColor = color;
            richTextBox.AppendText(s);
        }

        public void clearDataGridView()
        {
            if (dataGridView.InvokeRequired)
            {
                var d = new SafeDeleteRowsDelegate(removeDataGridViewRows);
                richTextBox.Invoke(d, new object[] { });
            }
            else
                removeDataGridViewRows();
        }

        private void removeDataGridViewRows()
        {
            try
            {
                while (dataGridView.Rows.Count > 1)
                    dataGridView.Rows.RemoveAt(0);

                if (dataGridView.Rows.Count == 1 && dataGridView.Rows[0].IsNewRow)
                {
                    dataGridView.Rows[0].Cells[0].Value = null;// string.empty;
                    dataGridView.Rows[0].Cells[1].Value = "*";
                    dataGridView.Rows[0].Cells[2].Value = string.Empty;
                    if (dataGridView.Columns[4].Visible) dataGridView.Rows[0].Cells[4].Value = string.Empty;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Error in DataFile::remoceDataGridViewRows() = " + e.Message);
            }
        }
        public void RefreshDataGridView()
        {
            //updateDataView(ViewFullDoc, dvDataGrid);
            updateDataView(Form1.ViewTabular); //, dvDataGrid);
            return;
        }

        public void RefreshRichTextBox(int viewCode)
        {
            updateDataView(viewCode); //, dvTextBox);
            return;
        }

        private void appendRichTextAppend(int code, string s)
        {
            //richTextBoxdataGridView.Rows.Add(row);
            switch (code)
            {
                case 0:
                    richTextBox.Clear();
                    break;
                case 1:
                    richTextBox.Select(richTextBox.Text.Length, s.Length);
                    richTextBox.SelectionColor = GetColor(s[0]);
                    richTextBox.AppendText(s);
                    break;
            }
        }

        public void DataGridViewToFileContent()
        {
            string s = string.Empty;
            fileContent = string.Empty;
            dataGridView.EndEdit();
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                // for newly added row, assign empty strings to null fields
                if (row.Cells[Form1.dgvColRecordType].Value == null) row.Cells[Form1.dgvColRecordType].Value = "*";
                if (row.Cells[Form1.dgvColPali].Value == null) row.Cells[Form1.dgvColPali].Value = string.Empty;
                if (row.Cells[2].Value == null) row.Cells[2].Value = string.Empty;
                if (row.Cells[3].Value == null) row.Cells[3].Value = string.Empty;
                if (row.Cells[4].Value == null) row.Cells[4].Value = string.Empty;
                if (!row.IsNewRow)// && row.Cells[dgvColPali].Value != null && row.Cells[dgvColPali].Value.ToString().Length > 0)
                {
                    switch (row.Cells[Form1.dgvColRecordType].FormattedValue.ToString())
                    {
                        case "#": // comment record
                            s = "#" + row.Cells[2].FormattedValue.ToString();
                            break;
                        case "*": // nissaya data record
                            s = "*" + row.Cells[2].FormattedValue.ToString();
                            s += " ^" + row.Cells[3].FormattedValue.ToString();
                            if (row.Cells[4].FormattedValue != null && row.Cells[4].FormattedValue.ToString().Length > 0)
                                s += " @" + row.Cells[4].FormattedValue.ToString();
                            break;
                    }
                    fileContent += (fileContent.Length == 0) ? s : " " + s;
                }
            }
        }

        public void RefreshRichTextBox()
        {
            string s = string.Empty;
            int code = 0;
            fileContent = string.Empty;

            if (richTextBox.InvokeRequired)
            {
                var d = new safeCallRichTextAppend(appendRichTextAppend);
                richTextBox.Invoke(d, new object[] { code, s });
            }
            else
                appendRichTextAppend(code, s);

            code = 1;
            char c;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                for (int i = 1; i <= 2; i++)
                {
                    switch(i)
                    {
                        case 1:
                            s = "*";
                            break;
                        case 2:
                            c = row.Cells[i].Value.ToString()[0];
                            if (c != '#' && c != '@') s = "^";
                            break;
                    }
                    s += row.Cells[i].Value.ToString();
                    if (richTextBox.InvokeRequired)
                    {
                        var d = new safeCallRichTextAppend(appendRichTextAppend);
                        richTextBox.Invoke(d, new object[] { code, s });
                    }
                    else
                        appendRichTextAppend(code, s);
                }
                fileContent += s;
            }
        }

        public static Color GetColor(char marker)
        {
            Color color = Color.Black;
            //if (color == Color.Black) return Color.Silver;
            switch (marker)
            {
                case '*':
                    color = Color.Brown;
                    break;
                case '#':
                    color = Color.RoyalBlue;
                    break;
                case '@':
                    color = Color.FromArgb(160, 125, 40); //227, 184, 74);
                    //color = Color.LightSalmon;
                    break;
                case '^':
                default:
                    color = Color.DimGray; //FromArgb(88, 88, 88); //DimGray;
                    break;
            }
            return color;
        }

        public RichTextBox GetRichTextBox()
        {
            return richTextBox;
        }

        private bool displayOn(char marker, int codeView)
        {
            if (marker == '*' && codeView == 1 ||
                marker == '^' && codeView == 2) return true;
            return codeView == 3 ? true : false;
        }

        private void safeCallDataGridViewAddRow(string srno, char recType, string pali, string plain, string footnote)
        {
            string[] row = { srno, recType.ToString(), pali, plain, footnote};
            if (UseSecondaryGridContainer)
            {
                Form1._NissayaDataRecord nisDataRec = new Form1._NissayaDataRecord();
                nisDataRec.srno = srno;
                nisDataRec.marker = recType.ToString();
                nisDataRec.pali = pali;
                nisDataRec.plain = plain;
                nisDataRec.footnote = footnote;
                NissayaRecordList.Add(nisDataRec);
                //dataGridView2.Rows.Add(row);
            }
            else dataGridView.Rows.Add(row);
        }

        private void parseNissayaDataRecord(string s, out string pali, out string plain, out string footnote)
        {
            // all parsed strings are returned with a trailing blank
            pali = plain = footnote = string.Empty;
            if (s[0] == '#')
            {
                // comment record. no need to parse anything
                pali = s.Substring(1).Trim(' ');
                return;
            }

            int n = s.IndexOf('^');
            if (n != -1)
            {
                pali = s.Substring(1, n - 1).Trim(' ');
                int n1 = s.IndexOf('@', n + 1);
                if (n1 == -1) plain = s.Substring(n + 1).Trim(' ');
                else
                {
                    plain = s.Substring(n + 1, n1 - n - 1).Trim(' ');
                    footnote = s.Substring(n1 + 1).Trim(' ');
                }
            }
            else pali = s.Substring(1);
            return;
        }
        public void SortPages()
        {
            int pgNo;
            SortedPages.Clear();
            foreach (KeyValuePair<string, string> item in Pages)
            {
                pgNo = int.Parse(item.Key);
                SortedPages[pgNo] = item.Value;
            }
            Pages.Clear();
            foreach (KeyValuePair<int, string> item in SortedPages)
            {
                Pages[item.Key.ToString()] = item.Value;
            }
        }
        public void SaveFile(string outFile = "")
        {
            int n = 0;
            if (Pages.Count == 0 && fileContent.Length > 0)
            {
                // this is a new file and has no pages parsed
                // parse the pages into memory
                parseIntoPages();
            }
            foreach(KeyValuePair<string, string> item in Pages)
            {
                if (n == 0) fileContent = item.Value;
                else
                    fileContent += paraNewLine + item.Value;
                ++n;
            }
            try
            {
                fMgr.SaveFile(GetHeader() + fileContent, outFile);
                UpdateFileContent = false;
            }
            catch (UnauthorizedAccessException)
            {
                FileAttributes attr = (new FileInfo(outFile)).Attributes;
                string msg = "DataFile::SaveFile() - UnAuthorizedAccessException: Unable to access file ";
                msg += outFile;
                MessageBox.Show(msg);
                //if ((attr & FileAttributes.ReadOnly) > 0)
                //    Console.Write("The file is read-only.");
            }
        }
            
        private string GetHeader()
        {
            string header = string.Empty;
            if (MN.Length > 0) header = "{MN:" + MN + "}" + System.Environment.NewLine; 
            return header;
        }
            
        public string SaveCurrentPage(string pgno = "")
        {
            try
            {
                // save the fileContent to the current page
                string orgKey = (pgno.Length > 0) ? GetOrigKey(pgno) : curPage;
                if (orgKey.Length > 0 && Pages.ContainsKey(orgKey))
                    Pages[orgKey] = fileContent;
                else
                {
                    Pages.Add(pgno, fileContent);
                    orgKey = pgno;
                    KeyMapper.Add(new KeyMap(pgno, orgKey));
                }
                // update NIS count
                NISRecCount[Convert.ToInt32(pgno)] = Pages[orgKey].Count(x => x == '*');
                // check the page no in fileContent
                char[] pageNoDelimiters = {' ', '\n', '\r'};
                int pos = fileContent.IndexOfAny(pageNoDelimiters);
                string newKey = (pos != -1) ? fileContent.Substring(0, pos) : fileContent;
                if (newKey[0] == '#') newKey = newKey.Substring(1);
                if (newKey.Length > 3) newKey = newKey.Substring(0, 3); // shorten new key
                if (newKey != orgKey) UpdatePageKey(newKey, orgKey);
                // save current page to file
                //SaveFile();
                return newKey;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error in DataFile::SaveCurrentPage() : " + ex.Message);
            }
            return "";
        }
            
        public void RichTextBoxToFileContent() 
        {
            if (richTextBox.InvokeRequired)
            {
                var d = new safeCallGetRichText(getRichTextValue);
                    richTextBox.Invoke(d, new object[] { });
            }
            else
                fMgr.fileContent = fileContent = richTextBox.Text;
        }

        private void getRichTextValue()
        {
            fMgr.fileContent = fileContent = richTextBox.Text;
        }
    }

    //public class NissayaBook
    //{
    //    string title;
    //    string author;
    //    string bookFileName;
    //    int ChapCount = 0;
    //    public class Chapter
    //    {
    //        public bool checkBox;
    //        public string chapName;
    //        public string fileName;

    //        public Chapter(bool chk, string cName, string fName) { checkBox = chk; chapName = cName; fileName = fName; }
    //    }
    //    public List<Chapter> chapters = new List<Chapter>();
    //    public string GetTitle() { return title; }
    //    public string GetAuthor() { return author; }
    //    public NissayaBook(string bfname, string tle, string auth) { title = tle; author = auth; bookFileName = bfname; }
    //    public NissayaBook(string bfname)
    //    {
    //        JSON.Json nbk = new JSON.Json(bfname);
    //        bookFileName = bfname;
    //        title = nbk.GetData("TITLE");
    //        author = nbk.GetData("AUTHOR");
    //        ChapCount = Convert.ToInt32(nbk.GetData("CHCOUNT"));
    //        string chName, chFile;
    //        for (int i = 0; i < ChapCount; i++)
    //        {
    //            chName = nbk.GetData("CHAP" + (i + 1).ToString());
    //            chFile = nbk.GetData("FILE" + (i + 1).ToString());
    //            chapters.Add(new Chapter(true, chName, chFile));
    //        }
    //    }
    //    public NissayaBook()
    //    {
    //    }
    //    public void AddChapter(bool chkbox, string cName, string fName)
    //    {
    //        chapters.Add(new Chapter(chkbox, cName, fName));
    //    }

    //    public string[] NissayaBookFileData()
    //    {
    //        List<string> info = new List<string>();
    //        info.Add("AUTHOR|" + author.Trim());
    //        info.Add("TITLE|" + title.Trim());
    //        info.Add("CHCOUNT|" + chapters.Count.ToString());
    //        int n = 0;
    //        foreach (Chapter ch in chapters)
    //        {
    //            ++n;
    //            info.Add("CHAP" + n.ToString() + "|" + ch.chapName.Trim());
    //            info.Add("FILE" + n.ToString() + "|" + ch.fileName.Trim());
    //        }

    //        return info.ToArray();
    //    }
    //}
}