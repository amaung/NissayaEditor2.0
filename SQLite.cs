using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;
using Microsoft.VisualBasic.ApplicationServices;
using System.Security.Cryptography;
//using System.Windows.Documents;
using System.Windows.Forms;
using NissayaEditor;
using System.Diagnostics.Contracts;
//using System.Security.RightsManagement;

namespace SQLite
{
    public class WordCount
    {
        public string word;
        public Int32 count;
        public WordCount() { }
        public WordCount(string word, Int32 count)
        {
            this.word = word; this.count = count;
        }
    }

    public class WorkAssignmentRecord
    {
        public string userID;
        public string suttaNo;
        public int pageNo;
        public int NIS_Pages;
        public string assignedDate;
        public string startDate;
        public string status;
        public float timeSpent;
        public int turnAroundTime;
        public bool updateFlag;
        public WorkAssignmentRecord() 
        {
            suttaNo = userID = assignedDate = startDate = status = string.Empty;
            NIS_Pages = turnAroundTime = pageNo = 0; timeSpent = 0F;
            updateFlag = false;
        }
    }
    public class DataFileInfo
    {
        public string fileName;
        public string userID;
        public int startPageNo;
        public int endPageNo;
        public DataFileInfo() { fileName = userID = string.Empty; startPageNo = endPageNo = -1; }
    }
    public class SourceFileInfo
    {
        public string SourceID;
        public int StartSuttaNo;
        public int StartSubSuttaNo;
        public int EndSuttaNo;
        public int EndSubSuttaNo;
        public string SourceName;
    }
    public class SQLiteObj
    {
        string conStr = "Data Source=:memory:";
        string sqlStatement;
        string sqlVersion;
        string sqlDBName = "URI=file:";
        SQLiteConnection sqlConn = null;
        SQLiteCommand sqlCommand = null;

        public SQLiteObj()
        {
            string path = Directory.GetCurrentDirectory();
            sqlDBName = "URI=file:.\\ne.db";
            //sqlConn = new SQLiteConnection(conStr);
            using (sqlConn = new SQLiteConnection(conStr))
            {
                sqlConn.Open();
                sqlStatement = "SELECT SQLITE_VERSION()";
                sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
                sqlVersion = sqlCommand.ExecuteScalar().ToString();
                sqlConn.Close();
            }
            // open the database
            sqlConn = new SQLiteConnection(sqlDBName);
            sqlConn.Open();
        }

        public List<string> GetUnitQtyNouns()
        {
            List<string> list = new List<string>();
            try
            {
                //sqlConn = new SQLiteConnection(sqlDBName);
                //sqlConn.Open();
                sqlStatement = "SELECT * FROM UnitQuantityNouns;";
                sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
                SQLiteDataReader rdr = sqlCommand.ExecuteReader();
                while (rdr.Read())
                {
                    list.Add(rdr.GetString(0));
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            return list;
        }

        public Dictionary<string, string> GetPhoneticWords()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                sqlStatement = "SELECT * FROM PhoneticWords;";
                sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
                SQLiteDataReader rdr = sqlCommand.ExecuteReader();
                string inCorrectWord, correctWord;
                while (rdr.Read())
                {
                    inCorrectWord = rdr.GetString(0); correctWord = rdr.GetString(1);
                    dict.Add(inCorrectWord, correctWord);
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            return dict;
        }

        public Dictionary<string, string> GetWordDict(string keyword)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            //sqlConn = new SQLiteConnection(sqlDBName);
            //sqlConn.Open();
            sqlStatement = "SELECT * FROM " + keyword;
            sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
            SQLiteDataReader rdr = sqlCommand.ExecuteReader();
            while (rdr.Read())
            {
                dict.Add(rdr.GetString(0), rdr.GetString(1));
            }
            return dict;
        }

        public string GetPaliTranslation(string paliWord)
        {
            sqlStatement = "SELECT * FROM HighFreqWords WHERE Pali = '" + paliWord + "';";
            sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
            SQLiteDataReader rdr = sqlCommand.ExecuteReader();
            if (rdr.HasRows)
            {
                rdr.Read();
                return rdr.GetString(1);
            }
            return "";
        }

        public string[] GetUserLoginData(string userID)
        {
            string password = string.Empty;
            List<string> list = new List<string>();
            try
            {
                sqlStatement = String.Format("SELECT {0} FROM UserProfile WHERE UserID = '{1}';", "*", userID);
                //sqlStatement = "SELECT * FROM UserProfile;";
                sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
                SQLiteDataReader rdr = sqlCommand.ExecuteReader();
                while (rdr.Read())
                {
                    list.Add(rdr.GetString(1)); // PID
                    list.Add(rdr.GetString(2)); // password
                }
            }
            catch (Exception)
            {
                //System.Windows.Forms.MessageBox.Show(e.Message);
            }

            return list.ToArray();
        }
        public void AddUser(string userID, string userPswd, string PID)
        {
            try
            {
                sqlStatement = String.Format("INSERT INTO UserProfile (UserID, PID, Password) VALUES ('{0}', '{1}', '{2}');", userID, PID, userPswd);
                //sqlStatement = "SELECT * FROM UserProfile;";
                sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }
        }
        public DataFileInfo GetDataFileInfo(string filename)
        {
            DataFileInfo dataFileInfo= new DataFileInfo();
            sqlStatement = String.Format("SELECT {0} FROM DataFileInfo WHERE FileName = '{1}';", "*", filename);
            sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
            SQLiteDataReader rdr = sqlCommand.ExecuteReader();
            if (rdr.Read())
            {
                dataFileInfo.fileName = rdr.GetString(0);
                dataFileInfo.userID= rdr.GetString(1);
                dataFileInfo.startPageNo = rdr.GetInt32(2);
                dataFileInfo.endPageNo  = rdr.GetInt32(3);
            }
            return dataFileInfo;
        }
        public void AddDataFileInfo(DataFileInfo dataFileInfo)
        {
            if (dataFileInfo == null) return;
            try
            {
                sqlStatement = String.Format("SELECT {0} FROM DataFileInfo WHERE FileName = '{1}';", "*", dataFileInfo.fileName);
                sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
                SQLiteDataReader rdr = sqlCommand.ExecuteReader();
                if (rdr.Read())
                {
                    // delete the file info
                    sqlStatement = String.Format("UPDATE DataFileInfo SET UserID = '{0}', StartPageNo = {1}," +
                        " EndPageNo = {2} WHERE FileName = '{3}';", dataFileInfo.userID, dataFileInfo.startPageNo,
                        dataFileInfo.endPageNo, dataFileInfo.fileName);
                    sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
                    sqlCommand.ExecuteNonQuery();
                    return;
                }
                sqlStatement = String.Format("INSERT INTO DataFileInfo (FileName, UserID, StartPageNo, EndPageNo) VALUES ('{0}', '{1}', {2}, {3});",
                dataFileInfo.fileName, dataFileInfo.userID, dataFileInfo.startPageNo, dataFileInfo.endPageNo);
                sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, e.StackTrace);
            }
        }
        public List<WorkAssignmentRecord> GetWorkAssignmentRecords(string userID, bool updateFlag = false)
        {
            List<WorkAssignmentRecord> listWorkAssignmentRecord = new List<WorkAssignmentRecord>();
            if (userID == null) { return listWorkAssignmentRecord; }
            try
            {
                if (updateFlag)
                {
                    sqlStatement = String.Format("SELECT {0} FROM UserPageAssignments WHERE UserID = '{1}' and UpdateFlag = 1;", "*", userID);
                }
                else
                    sqlStatement = String.Format("SELECT {0} FROM UserPageAssignments WHERE UserID = '{1}';", "*", userID);
                
                sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
                SQLiteDataReader rdr = sqlCommand.ExecuteReader();
                int n = rdr.FieldCount;
                while (rdr.Read())
                {
                    WorkAssignmentRecord rec = new WorkAssignmentRecord()
                    {
                        suttaNo = rdr.GetString(1),
                        pageNo = rdr.GetInt32(2),
                        NIS_Pages = rdr.GetInt32(3),
                        assignedDate = rdr.GetString(4),
                        startDate = rdr.GetString(5),
                        status = rdr.GetString(6),
                        timeSpent = rdr.GetFloat(7),
                        turnAroundTime = rdr.GetInt32(8),
                        updateFlag = rdr.GetInt32(9) == 1 ? true : false,
                        userID = userID
                    };
                    listWorkAssignmentRecord.Add(rec);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return listWorkAssignmentRecord;
        }
        public void AddUserWorkAssignment(WorkAssignmentRecord rec)
        {
            try
            {
                sqlStatement = String.Format("INSERT INTO UserPageAssignments (UserID, SuttaNo, PageNo, NIS_Pages," +
                    " AssignedDate, StartDate, Status, TimeSpent, TurnAroundTime, UpdateFlag) " +
                    "VALUES ('{0}', '{1}', {2}, {3}, '{4}', '{5}', '{6}', {7}, {8}, {9});",
                    rec.userID, rec.suttaNo, rec.pageNo, rec.NIS_Pages, rec.assignedDate, rec.startDate,
                    rec.status, rec.timeSpent, rec.turnAroundTime, rec.updateFlag ? 1 : 0);
                //sqlStatement = "SELECT * FROM UserProfile;";
                sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, e.StackTrace);
            }
        }
        public void UpdateUserWorkAssignment(WorkAssignmentRecord record)
        {
            try
            {
                sqlStatement = String.Format("UPDATE UserPageAssignments SET NIS_Pages = {0}, AssignedDate = '{1}', " +
                "StartDate = '{2}', Status = '{3}', TimeSpent = {4}, TurnAroundTime = {5}, UpdateFlag = {6} " +
                "WHERE UserID = '{7}' and SuttaNo = '{8}' and PageNo = {9};", record.NIS_Pages, record.assignedDate,
                record.startDate, record.status, record.timeSpent, record.turnAroundTime, record.updateFlag ? 1 : 0,
                record.userID, record.suttaNo, record.pageNo);
                sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, e.StackTrace);
            }
        }
        public ImageSettings? GetSourcePageInfo(string  sourcePageName)
        {
            try
            {
                sqlStatement = String.Format("SELECT {0} FROM SourcePageInfo WHERE SourceFileName = '{1}';", "*", sourcePageName);
                sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
                SQLiteDataReader rdr = sqlCommand.ExecuteReader();
                if (rdr.Read())
                {
                    ImageSettings imageSettings = new ImageSettings();
                    imageSettings.imageFileName = rdr.GetString(0);
                    imageSettings.cropW = rdr.GetInt32(1);
                    imageSettings.cropH = rdr.GetInt32(2);
                    imageSettings.X0 = rdr.GetInt32(3);
                    imageSettings.Y0 = rdr.GetInt32(4);
                    imageSettings.W0 = rdr.GetInt32(5);
                    imageSettings.H0 = rdr.GetInt32(6);
                    imageSettings.readView_X = rdr.GetInt32(7);
                    imageSettings.readView_Y = rdr.GetInt32(8);
                    imageSettings.readView_W = rdr.GetInt32(9);
                    imageSettings.readView_H = rdr.GetInt32(10);
                    rdr.Close();
                    return imageSettings;
                }
            }
            catch(Exception e)
            {
                return null;
            }
            return null;
        }
        public void UpdateSourcePageInfo(ImageSettings imageSettings)
        {
            try
            {
                sqlStatement = String.Format("SELECT {0} FROM SourcePageInfo WHERE SourceFileName = '{1}';", "*", imageSettings.imageFileName);
                sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
                SQLiteDataReader rdr = sqlCommand.ExecuteReader();
                if (rdr.Read())
                {
                    sqlStatement = string.Format("UPDATE SourcePageInfo SET readY = {0} WHERE SourceFileName = '{1}';", imageSettings.readView_Y, imageSettings.imageFileName);
                    sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
                    try
                    {
                        var res = sqlCommand.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    rdr.Close();
                    return;
                }
               // insert a new record
                rdr.Close();
                sqlStatement = String.Format("INSERT INTO SourcePageInfo (SourceFileName, CropWidth, CropHeight, X0, Y0, W0, H0," +
                    " ReadX, ReadY, ReadW, ReadH) " +
                    "VALUES ('{0}', {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10});",
                    imageSettings.imageFileName, imageSettings.cropW, imageSettings.cropH, imageSettings.X0, imageSettings.Y0, imageSettings.W0,
                    imageSettings.H0, imageSettings.readView_X, imageSettings.readView_Y, imageSettings.readView_W, imageSettings.readView_H);
                sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, e.StackTrace);
            }
        }
        public string[] GetTableNames()
        {
            List<string> tableNames = new List<string>();
            //SELECT name FROM sqlite_schema WHERE type='table' ORDER BY name;
            try
            {
                sqlStatement = "SELECT name FROM sqlite_schema WHERE type='table' ORDER BY name;";
                sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
                SQLiteDataReader rdr = sqlCommand.ExecuteReader();
                while (rdr.Read())
                {
                    tableNames.Add(rdr.GetString(0));
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            return tableNames.ToArray();
        }
        public List<SourceFileInfo> GetSuttaFileInfo()
        {
            List<SourceFileInfo> sourceFileInfos = new List<SourceFileInfo>();
            try
            {
                sqlStatement = "SELECT * FROM SourceFileInfo;";
                sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
                SQLiteDataReader rdr = sqlCommand.ExecuteReader();
                while (rdr.Read())
                {
                    SourceFileInfo sourceFileInfo = new SourceFileInfo()
                    {
                        SourceID = rdr.GetString(0),
                        StartSuttaNo = rdr.GetInt16(1),
                        StartSubSuttaNo= rdr.GetInt16(2),
                        EndSuttaNo= rdr.GetInt16(3),
                        EndSubSuttaNo= rdr.GetInt16(4),
                        SourceName = rdr.GetString(5),
                    };
                    sourceFileInfos.Add(sourceFileInfo);
                }
                rdr.Close();
            }
            catch (Exception e)
            {
            }
            return sourceFileInfos;
        }
        public void AddSuttaFileInfo(SourceFileInfo sourceFileInfo)
        {
            try
            {
                sqlStatement = String.Format("INSERT INTO SourceFileInfo (SourceID, StartSuttaNo, " +
                    "StartSubSuttaNo, EndSuttaNo, EndSubSuttaNo, SourceName) " +
                    "VALUES ('{0}', {1}, {2}, {3}, {4}, '{5}');",
                    sourceFileInfo.SourceID, sourceFileInfo.StartSuttaNo, sourceFileInfo.StartSubSuttaNo,
                    sourceFileInfo.EndSuttaNo, sourceFileInfo.EndSubSuttaNo, sourceFileInfo.SourceName);
                sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, e.StackTrace);
            }
        }

        public float GetVersion()
        {
            sqlStatement = "SELECT * FROM Version;";
            sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
            SQLiteDataReader rdr = sqlCommand.ExecuteReader();
            float version = 0F;
            if (rdr.Read()) version = rdr.GetFloat(0);
            else rdr.Close();
            return version;
        }
        public int UpdateWordCount(string pali, string trans, Int32 count)
        {
            sqlStatement = "SELECT * FROM HighFreqWords WHERE Pali = '" + pali + "';";
            sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
            SQLiteDataReader rdr = sqlCommand.ExecuteReader();
            if (rdr.Read())
            {
                // found
                if (trans == rdr.GetString(1) && count > rdr.GetInt32(2))
                {
                    sqlStatement = string.Format("UPDATE HighFreqWords SET Count = {0} WHERE Pali = '{1}';", count, pali);
                    sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
                    try
                    {
                        var res = sqlCommand.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    } 
                }
            }
            else
            {
                rdr.Close();
                // new record
                sqlStatement = "INSERT INTO HighFreqWords (Pali, Translation, Count)";
                sqlStatement += string.Format(" VALUES ('{0}', '{1}', {2});", pali, trans, count);
                sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
                try
                {
                    var res = sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                } 
            }
            return count;
        }
        public Dictionary<string, WordCount> LoadWordDictionary()
        {
            Dictionary<string, WordCount> dict = new Dictionary<string, WordCount>();
            sqlStatement = "SELECT * FROM HighFreqWords;";
            sqlCommand = new SQLiteCommand(sqlStatement, sqlConn);
            SQLiteDataReader rdr = sqlCommand.ExecuteReader();
            string pali, trans;
            Int32 count;
            while (rdr.Read())
            {
                pali = rdr.GetString(0); trans = rdr.GetString(1); count = rdr.GetInt32(2);
                WordCount wc = new WordCount(trans, count);
                dict.Add(pali, wc);
            }
            return dict;
        }
        public void CloseDB() { if (sqlConn != null) sqlConn.Close(); }
    }
}
