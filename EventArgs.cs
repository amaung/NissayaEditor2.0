using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NissayaEditor
{
    internal class MNOpenEventArgs : EventArgs
    {
        public string MNTitle { get; set; }
        public string MN_No { get; set; }
        public int MN_PageNo { get; set; }
        public int MN_startPageNo { get; set; }
        public int MN_endPageNo { get; set; }
    }
    internal class WorkAssignmentEventArgs : EventArgs
    {
        public string MN_No { get; set; }
        public int pageNo { get; set; }
        public double timeSpent { get; set; }
        public int NISRec { get; set; }
    }
}
