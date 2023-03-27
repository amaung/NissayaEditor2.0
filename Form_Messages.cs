using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using Tipitaka_DB;
using Tipitaka_DBTables;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using Microsoft.VisualBasic.Logging;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Windows;

namespace NissayaEditor
{
    public partial class Form_Messages : Form
    {
        Form1 parent;
        ClientTipitakaDB clientTipitakaDB = null;
        ClientMessageLog clientMessageLog = null;
        ClientTipitakaDBLogin clientTipitakaDBLogin = null;
        List<MessageLog> listMessageLog = new List<MessageLog>();
        List<MessageLogLocal> listMessageLogLocal = new List<MessageLogLocal>();
        public class MessageLogLocal
        {
            public MessageLog messageLog;
            public string localTime;
            ClientTipitakaDB clientTipitakaDB;
            public string MessageLog { get; set; }
            public MessageLogLocal(MessageLog messageLog, ClientTipitakaDB clientTipitakaDB)
            {
                this.messageLog= messageLog;
                this.clientTipitakaDB= clientTipitakaDB;
                if (clientTipitakaDB != null)
                {
                    localTime = clientTipitakaDB.GetLocalTime(messageLog.RowKey);
                }
            }
        }
        public Form_Messages(Form1 parent)
        {
            InitializeComponent();
            this.parent = parent;
            InitDataGridView();
            clientTipitakaDB = parent.clientTipitakaDB;
            clientMessageLog = clientTipitakaDB.GetClientMessageLog();
            clientTipitakaDBLogin = clientTipitakaDB.GetClientTipitakaDBLogin();
            button_New.Enabled = true;
            button_Send.Enabled = !button_New.Enabled;
            dateTimePicker_Start.Enabled = dateTimePicker_End.Enabled = false;
            checkBox_Date.Checked = false;
        }
        private void InitDataGridView()
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells | DataGridViewAutoSizeRowsMode.AllHeaders;
            dataGridView1.ColumnCount = 2;
            dataGridView1.RowTemplate.Height = 30;
            dataGridView1.RowHeadersWidth = dataGridView1.Width / 2;
            //dataGridView1.RowHeadersWidth = dataGridView1.Width / 3;
            // Set the background color for all rows and for alternating rows. 
            // The value for alternating rows overrides the value for all rows.
            //dataGridView1.RowsDefaultCellStyle.BackColor = Color.White;
            //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = parent.CellBackgroundColor;
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = parent.HeaderBackGroundColor;
            dataGridView1.EnableHeadersVisualStyles = false;
            // Set the background color for all rows and for alternating rows. 
            // The value for alternating rows overrides the value for all rows.
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 255, 236);


            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersHeight = 50;
            dataGridView1.Columns[0].HeaderText = "DateTime(local)";
            dataGridView1.Columns[1].HeaderText = "Subject";
            dataGridView1.Columns[0].Width = dataGridView1.Columns[1].Width = dataGridView1.Width / 2 - 2;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.MultiSelect = false;
            dataGridView1.CellClick += dataGridView_CellClick!;
            dataGridView1.Columns[0].HeaderCell.SortGlyphDirection = SortOrder.Descending;

        }
        public void AddMessage(MessageLogLocal messageLocal)
        {
            DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            row.Cells[0].Value = messageLocal.localTime.ToString();
            int pos = messageLocal.messageLog.Subject.IndexOf(':');
            row.Cells[1].Value = (pos == -1) ? messageLocal.messageLog.Subject : messageLocal.messageLog.Subject.Substring(pos + 1);
            dataGridView1.Rows.Add(row);
        }
        public void Show(string userID, List<MessageLog> listMsgLog = null)
        {
            dataGridView1.Rows.Clear(); textBox_Subject.Text = textBox_Body.Text = String.Empty;
            textBox_UserID.Text = userID;
            if (listMsgLog == null || listMsgLog.Count == 0)
            {
                if (clientMessageLog!= null && clientTipitakaDBLogin != null)
                {
                    clientMessageLog.GetMessages(parent.GetLoginUserID());
                    listMessageLog = (List<MessageLog>)clientMessageLog.objResult;
                }
            }
            else
            {
                listMessageLog.Clear();
                listMessageLog.AddRange(listMsgLog);
                MessageLogLocal messageLogLocal;
                foreach(MessageLog log in listMessageLog)
                {
                    messageLogLocal = new MessageLogLocal(log, clientTipitakaDB);
                    listMessageLogLocal.Add(messageLogLocal);
                    AddMessage(messageLogLocal);
                }
                comboBox_Type.Text = "New";
                DetailMessage(listMessageLogLocal[0]);
                // the user has seen this message; so update the record if necessary
                UpdateMessageRead(listMessageLogLocal[0].messageLog);
            }
            base.Show();
        }
        private void UpdateMessageRead(MessageLog messageLog)
        {
            // the user has seen this message; so update the record if necessary
            if (messageLog.Read == false)
            {
                messageLog.Read = true;
                clientMessageLog.UpdateMessage(messageLog);
            }
        }
        private void DetailMessage(MessageLog message)
        {
            int pos = message.Subject.IndexOf(':');
            textBox_Subject.Text = (pos == -1) ? message.Subject : message.Subject.Substring(pos + 1);
            textBox_Body.Text = string.Empty;
            string[] f = message.Body.Split('|');
            foreach(string s in f)
            {
                if (textBox_Body.Text.Length > 0) textBox_Body.Text += "\r\n";
                textBox_Body.Text += s;
            }
            button_Download.Enabled = (message.MsgType.ToLower().Contains("assigned"));
            textBox_Subject.ReadOnly = textBox_Body.ReadOnly = true;
        }
        private void DetailMessage(MessageLogLocal messageLocal)
        {
            int pos = messageLocal.messageLog.Subject.IndexOf(':');
            textBox_Subject.Text = (pos == -1) ? messageLocal.messageLog.Subject : messageLocal.messageLog.Subject.Substring(pos + 1);
            textBox_Body.Text = string.Empty;
            string[] f = messageLocal.messageLog.Body.Split('|');
            foreach (string s in f)
            {
                if (textBox_Body.Text.Length > 0) textBox_Body.Text += "\r\n";
                textBox_Body.Text += s;
            }
            //textBox_Body.Text += "\r\n" + "RowKey = " + messageLocal.messageLog.RowKey;
            button_Download.Enabled = (messageLocal.messageLog.MsgType.ToLower().Contains("assigned"));
            textBox_Subject.ReadOnly = textBox_Body.ReadOnly = true;

        }
        private void button_Close_Click(object sender, EventArgs e)
        {
            Hide();
        }
        private void button_New_Click(object sender, EventArgs e)
        {
            button_New.Enabled = false;
            button_Send.Enabled = !button_New.Enabled;
            textBox_Subject.ReadOnly = textBox_Body.ReadOnly = false;
            textBox_Subject.Text = textBox_Body.Text = string.Empty;
        }
        private void button_Send_Click(object sender, EventArgs e)
        {
            List<UserProfile> admins = clientTipitakaDB.GetClientUserProfile().GetAllAdmins();
            ClientMessageLog clientMessageLog = clientTipitakaDB.GetClientMessageLog();
            List<string> listAdmins = new List<string>();
            string subject = String.Format("<{0}>:", textBox_UserID.Text, textBox_Subject.Text);
            string body = "From : " + textBox_UserID.Text + "\r\n\r\n" + textBox_Body.Text;
            foreach (UserProfile admin in admins) if (admin.RowKey != "tipitaka.manager@gmail.com") listAdmins.Add(admin.RowKey);
            clientTipitakaDB.SendMultipleViaMailTrapSMTP("tipitaka.manager@gmail.com", listAdmins.ToArray(), subject, body);
            // update MessageLog
            clientMessageLog.AddMessageLog(textBox_UserID.Text, "tipitaka.manager@gmail.com",
                textBox_UserID.Text + ": " + textBox_Subject.Text, textBox_Body.Text, "Reply", true); // true = message read
            button_New.Enabled = true;
            button_Send.Enabled = !button_New.Enabled;
            textBox_Subject.ReadOnly = textBox_Body.ReadOnly = true;
            ClientTipitakaDB.AutoClosingMessageBox.Show("Message sent successfully.", "Message", 1000);
        }
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (listMessageLog.Count > 0 && e.RowIndex < listMessageLog.Count)
            {
                DetailMessage(listMessageLogLocal[e.RowIndex]);
                UpdateMessageRead(listMessageLogLocal[e.RowIndex].messageLog);
            }
            else
            {
                textBox_Subject.Text = textBox_Body.Text = string.Empty;
                button_New.Enabled = true;
                button_Send.Enabled = !button_New.Enabled;
            }
        }
        private void comboBox_Type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_Date_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker_Start.Enabled = dateTimePicker_End.Enabled = checkBox_Date.Checked;
        }

        private void button_Find_Click(object sender, EventArgs e)
        {
            List<MessageLogLocal> listMessagesLocal = new List<MessageLogLocal>();
            string userID = parent.GetLoginUserID();
            dataGridView1.Rows.Clear(); textBox_Subject.Text = textBox_Body.Text = String.Empty;
            switch (comboBox_Type.Text)
            {
                case "Sent":
                    listMessagesLocal = listMessageLogLocal.FindAll(x => x.messageLog.From == userID);
                    break;
                case "Read":
                    listMessagesLocal = listMessageLogLocal.FindAll(x => x.messageLog.Read == true);
                    break;
                case "New":
                    listMessagesLocal = listMessageLogLocal.FindAll(x => x.messageLog.Read == false);
                    break;
                default:
                    listMessagesLocal = listMessageLogLocal;
                    break;
            }
            // if date range is specified check dates
            if (checkBox_Date.Checked)
            {
                string dt1 = dateTimePicker_Start.Value.ToString("yyyy'-'MM'-'dd");
                string dt2 = dateTimePicker_End.Value.AddDays(1).ToString("yyyy'-'MM'-'dd");
                var x = listMessageLogLocal[0].localTime;
                listMessagesLocal = listMessagesLocal.FindAll(x => String.Compare(x.localTime, dt1) >= 0 &&
                                                        String.Compare(x.localTime, dt2) < 0);
            }
            if (listMessagesLocal.Count > 0)
            {
                listMessagesLocal.ForEach(x => { AddMessage(x); DetailMessage(x); });
            }
        }
    }
}
