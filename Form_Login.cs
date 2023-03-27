using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Tipitaka_DB;
using Tipitaka_DBTables;
using NissayaEditor.Properties;
using System.Resources;
using SQLite;
using System.IO;

namespace NissayaEditor
{
    public partial class Form_Login : Form
    {
        ClientTipitakaDB clientTipitakaDB = null;
        public ClientTipitakaDBLogin clientTipitakaDBLogin = null;
        Form1 parent;
        bool showPassword = true;
        Image showPswdEye, hidePswdEye;
        ToolStripMenuItem toolStripMenuItem;
        public string LoginStatus = string.Empty;
        const string showEyeBMP = "./Images/showPasswordEye.bmp";
        const string hideEyeBMP = "./Images/hidePasswordEye.bmp";
        public Form_Login(Form1 parent)
        {
            InitializeComponent();
            this.clientTipitakaDB = parent.clientTipitakaDB;
            this.parent = parent;
#pragma warning disable CA1416
            if (File.Exists(showEyeBMP)) showPswdEye = Image.FromFile(showEyeBMP);
            else { MessageBox.Show(showEyeBMP + " not found."); return; }
            if (File.Exists(hideEyeBMP)) hidePswdEye = Image.FromFile(hideEyeBMP);
            else { MessageBox.Show(hidePswdEye + " not found."); return; }
#pragma warning restore CA1416

            button_ShowPswd.Image = showPswdEye;
        }
        public string GetLoginStatus() { return LoginStatus; }
        private void Form_Login_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
        }
        public DialogResult ShowDialog(object sender)
        {
            toolStripMenuItem = (ToolStripMenuItem)sender;
            switch (toolStripMenuItem.Text)
            {
                case "Login":
                    Text = "Server Login - " + parent.CloudServerName;
                    break;
                case "Work Assignments":
                    Text = "Client Login";
                    break;
            }
            return base.ShowDialog();
        }
        private void button_Login_Click(object sender, EventArgs e)
        {
            LoginStatus = string.Empty;
            string userID = GetUserID();
            if (toolStripMenuItem.Text == "Work Assignments")
            {
                LoginStatus = CheckLocalDB();
                DialogResult = DialogResult.Cancel;
                switch (LoginStatus)
                {
                    case "ERR_PSWD":
                        MessageBox.Show("Incorrect password.");
                        break;
                    case "ERR_NONE":
                        //MessageBox.Show("User not found on the system. Connect to server first.");
                        break;
                    case "SUCCESS":
                        DialogResult = DialogResult.OK;
                        break;
                }
                if (DialogResult == DialogResult.Cancel) return;
                parent.LocalLoginUserID = userID;
                parent.LocalLoginPassword = GetPassword();
                return;
            }
            if (clientTipitakaDBLogin == null) clientTipitakaDBLogin = new ClientTipitakaDBLogin();

            clientTipitakaDBLogin.Login(userID, GetPassword());
            if (clientTipitakaDBLogin.StatusCode == 404)
            {
                MessageBox.Show(String.Format("'{0}' is not registered in the system.", userID));
                return;
            }
            if (clientTipitakaDBLogin.StatusCode == 200)
            {
                if (clientTipitakaDBLogin.loggedinUser.Status == "IncorrectPassword")
                {
                    MessageBox.Show("Incorrect password.");
                    return;
                }
            }
            if (clientTipitakaDBLogin.StatusCode == 204)
            {
                // update local database if necessary
                SQLiteObj sqliteObj = new SQLiteObj();
                if (sqliteObj != null)
                {
                    //var ver = sqliteObj.GetVersion();
                    //string[] tableNames = sqliteObj.GetTableNames();
                    string[] pswd = parent.sqlObj.GetUserLoginData(GetUserID());
                    if (pswd.Length == 0)
                    {
                        // user is not in the local database
                        string encryptedPswd = Encrypt(textBox_Password.Text, clientTipitakaDBLogin.loggedinUser.PID);
                        //string encryptedPswd1 = Encrypt(encryptedPswd, clientTipitakaDBLogin.loggedinUser.PID, false);
                        // user info not in local database; add
                        sqliteObj.AddUser(GetUserID(), encryptedPswd, clientTipitakaDBLogin.loggedinUser.PID);
                        //string[] pp = sqliteObj.GetUserLoginData(GetUserID());
                        //encryptedPswd1 = Encrypt(pp[1], pp[0], false);
                    }
                }
                parent.LocalLoginUserID = userID;
                parent.LocalLoginPassword = GetPassword();
                DialogResult = DialogResult.OK;
            }
            else MessageBox.Show(String.Format("Login error : {0}", clientTipitakaDBLogin.StatusCode));
            return;
        }
        private string Encrypt(string p, string pid, bool encrypt = true)
        {
            string pwd = string.Empty, s = String.Empty;
            if (p.Length == 0) return pwd;
            int offset = 50;
            int n = p.Length / pid.Length;
            while (n-- >= 0) s += pid;
            List<char> newPwd = new List<char>();
            n = 0;
            foreach (char c in p)
            {
                int m = (int)c;
                if (encrypt) m += (int)s[n] + offset; else m -= (int)s[n] + offset;
                char c1 = Convert.ToChar(m);
                newPwd.Add(Convert.ToChar(m));
                n++;
            }
            pwd = new string(newPwd.ToArray());
            return pwd;
        }
        private void button_Cancel_Click(object sender, EventArgs e)
        {
            LoginStatus = string.Empty;
            Hide();
            DialogResult = DialogResult.Cancel;
        }
        public string GetUserID() { return textBox_UserID.Text; }
        public string GetPassword() { return textBox_Password.Text; }
        private void button_ShowPswd_Click(object sender, EventArgs e)
        {
            if (showPassword)
            {
                textBox_Password.PasswordChar = '\0';
                button_ShowPswd.Image = hidePswdEye;
            }
            else
            {
                textBox_Password.PasswordChar = '*';
                button_ShowPswd.Image = showPswdEye;
            }
            showPassword = !showPassword;
        }
        private string CheckLocalDB()
        {
            string[] f = parent.sqlObj.GetUserLoginData(GetUserID());
            if (f.Length == 0) return "ERR_NONE";
            string pid = f[0];
            string pswd = f[1];
            string decryptPswd = Encrypt(pswd, pid, false);
            if (decryptPswd == GetPassword()) { return "SUCCESS"; }
            return "ERR_PSWD";
        }
        public bool ServerLogin()
        {
            string userID = GetUserID();
            if (clientTipitakaDBLogin != null) return true;
            if (clientTipitakaDBLogin == null) clientTipitakaDBLogin = new ClientTipitakaDBLogin();

            clientTipitakaDBLogin.Login(userID, GetPassword());
            if (clientTipitakaDBLogin.StatusCode == 404)
            {
                MessageBox.Show(String.Format("'{0}' is not registered in the system.", userID));
                return false;
            }
            if (clientTipitakaDBLogin.StatusCode == 200)
            {
                if (clientTipitakaDBLogin.loggedinUser.Status == "IncorrectPassword")
                {
                    MessageBox.Show("Incorrect password.");
                    return false;
                }
            }
            if (clientTipitakaDBLogin.StatusCode == 204)
            {
                // update local database if necessary
                SQLiteObj sqliteObj = new SQLiteObj();
                if (sqliteObj != null)
                {
                    //var ver = sqliteObj.GetVersion();
                    //string[] tableNames = sqliteObj.GetTableNames();
                    string[] pswd = parent.sqlObj.GetUserLoginData(GetUserID());
                    if (pswd.Length == 0)
                    {
                        // user is not in the local database
                        string encryptedPswd = Encrypt(textBox_Password.Text, clientTipitakaDBLogin.loggedinUser.PID);
                        //string encryptedPswd1 = Encrypt(encryptedPswd, clientTipitakaDBLogin.loggedinUser.PID, false);
                        // user info not in local database; add
                        sqliteObj.AddUser(GetUserID(), encryptedPswd, clientTipitakaDBLogin.loggedinUser.PID);
                        //string[] pp = sqliteObj.GetUserLoginData(GetUserID());
                        //encryptedPswd1 = Encrypt(pp[1], pp[0], false);
                    }
                }
                parent.LocalLoginUserID = userID;
                parent.LocalLoginPassword = GetPassword();
                parent.LoadAvailableSources();
                parent.EnableMenuItems();
                return true;
            }
            MessageBox.Show(String.Format("Login error : {0}", clientTipitakaDBLogin.StatusCode));
            return false;
        }
    }
}
