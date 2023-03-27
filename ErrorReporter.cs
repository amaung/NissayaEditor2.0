using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace NissayaEditor
{
    public delegate bool CallBack(Object sender, NissayaEditor.Form1._GridViewSelection gvs);  

    public partial class ErrorReporter : Form
    {
        Form1 parent;
        CallBack callback = null;
        Object extSender;
        NissayaEditor.Form1._GridViewSelection gvs;
        int curErrIndex, totalErrors;
        IntPtr handle;
        public List<string> CorrectedGVS = new List<string>();

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hwnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public ErrorReporter(Form1 parent)
        {
            this.parent = parent;
            InitializeComponent();
        }

        private void ErrorReporter_Load(object sender, EventArgs e)
        {

        }

        public void SetHandle(IntPtr handle) { this.handle = handle; }

        public void AddCorrectedKey(Form1._GridViewSelection gvs)
        {
            string key = GetCorrectedKey(gvs);
            if (!CorrectedGVS.Contains(key)) CorrectedGVS.Add(key);
        }

        string GetCorrectedKey(Form1._GridViewSelection gvs)
        {
            return gvs.row.ToString() + gvs.col.ToString() + gvs.listErrDesc[0].incorrectText;
        }

        public void SetMsg(Object sender, NissayaEditor.Form1._GridViewSelection gvs)
        {
            extSender = sender;
            this.gvs = gvs;
            string[] err = gvs.errKey.Split('|');
            if (gvs.listErrDesc.Count > 0)
            {
                label_Wrong.Text = gvs.listErrDesc[0].incorrectMsg;
                label_Correct.Text = gvs.listErrDesc[0].correctMsg;
                textBox_Ref.Text = gvs.listErrDesc[0].errDesc;
                if (CorrectedGVS.Contains(GetCorrectedKey(gvs)))
                {
                    textBox_Ref.Text += " Corrected.";
                    gvs.corrected = true;
                }
                textBox_Ref.SelectionLength = 0;
                button_Change.Enabled = !gvs.corrected;
            }
            Show();

        }

        public void Reset()
        {
            curErrIndex = totalErrors = 0; CorrectedGVS.Clear();
        }

        public void UpdateTotalCount(int n) 
        {
            totalErrors += n; EnableButtons();
        }

        public void SetCallback(CallBack callback)
        {
            this.callback = callback;
        }

        public void EnableButtons()
        {
            button_Prev.Enabled = button_Next.Enabled = true;
            if (curErrIndex == 1) button_Prev.Enabled = false;
            if (curErrIndex == totalErrors) button_Next.Enabled = false;
            if (button_Change.Enabled)
                button_Change.Focus();
            else
            {
                if (button_Next.Enabled) button_Next.Focus();
                else
                {
                    if (button_Prev.Enabled) button_Prev.Focus();
                    else label_Wrong.Focus();
                }
            }
        }

        public void EnableCorrectButton(bool flag) { button_Change.Enabled = flag; }

        private void button_Quit_Click(object sender, EventArgs e)
        {
            Hide();
            SendMessage(handle, Form1.WM_QUIT_ERROR, IntPtr.Zero, IntPtr.Zero);
        }

        private void button_Change_Click(object sender, EventArgs e)
        {
            if (callback != null)
            {
                callback(extSender, gvs);
                textBox_Ref.Text += " Corrected.";
                button_Change.Enabled = false;
                gvs.corrected = true;
            }
        }

        public void ErrorIndexUpdate(int curErrIndex)
        {
            if (this.curErrIndex + curErrIndex > totalErrors ||
                this.curErrIndex + curErrIndex < 1) return;
            this.curErrIndex += curErrIndex;
            label_ErrNo.Text = string.Format("{0}/{1}", this.curErrIndex.ToString(), totalErrors.ToString());
            EnableButtons();
        }

        private void button_Next_Click(object sender, EventArgs e)
        {
            button_Change.Enabled = true;
            SendMessage(handle, Form1.WM_NEXT_ERROR, IntPtr.Zero, IntPtr.Zero);
        }

        private void button_Prev_Click(object sender, EventArgs e)
        {
            button_Change.Enabled = true;
            SendMessage(handle, Form1.WM_PREV_ERROR, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
