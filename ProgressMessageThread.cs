using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NissayaEditor
{
    public class ProgressMessageThread
    {
        MessagingForm2 message2 = null;
        //https://stackoverflow.com/questions/1195896/threadstart-with-parameters
        public ProgressMessageThread() { }
        public void Open(String msg, Form parent = null)
        {
            parent.Cursor = Cursors.WaitCursor;
            message2 = null;
            Thread msgThread = new Thread(() => MessageThread(msg, parent));
            msgThread.Start();
        }
        private delegate void SafeWaitMessageClose();
        private delegate void SafeSetCursor();
        private void MessageThread(string msg, Form parent)
        {
            if (msg.Length > 0)
            {
                message2 = new MessagingForm2(parent);
                message2.ShowDialog(msg);
            }
        }
        public void Close()
        {
            if (message2 == null) return;
            if (message2.parent.InvokeRequired)
            {
                var d = new SafeSetCursor(SetCursor);
                message2.parent.Invoke(d, new object[] { });

            }
            else SetCursor();

            if (message2.InvokeRequired)
            {
                var d = new SafeWaitMessageClose(MsgClose);
                message2.Invoke(d, new object[] { });
            }
            else MsgClose();
        }
        private void MsgClose()
        {
            if (message2 != null)
            {
                message2.Close(); message2 = null;
            }
        }
        private void SetCursor()
        {
            if (message2 != null) message2.parent.Cursor = Cursors.Default;
        }
    }
}
