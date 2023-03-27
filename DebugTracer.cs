using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NissayaEditor
{
    public class DebugTracer
    {
        System.IO.StreamWriter file;

        public DebugTracer()
        {
            file = new System.IO.StreamWriter(@".\DebugTracer.out", true, Encoding.UTF8);
            file.AutoFlush = true;
            file.WriteLine("\nTracer started at " + DateTime.Now);
            file.WriteLine("===============================================================");
        }

        public void ElpasedTimes(double miliSeconds1, double miliSeconds2)
        {
            file.WriteLine("Elpased Time1  = \tTicks1 (milliseconds) = " + miliSeconds1.ToString());
            file.WriteLine("Elpased Time2  = \tTicks2 (milliseconds) = " + miliSeconds2.ToString());
        }

        public void OutElpasedTime(string s, double miliSeconds)
        {
            file.WriteLine(s + "\tTicks (milliseconds) = " + miliSeconds.ToString());
        }

        public void OutElpasedTime(TimeSpan ts)
        {
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds);
            file.WriteLine("Elapsed time hh:mm:ss.nnn = " + elapsedTime);
        }

        public void OutputTrace(string[] lines)
        {
            //System.IO.File.WriteAllLines(@".\DebugTrace.out", lines);
        }

        public void WriteLine(string s)
        {
            file.WriteLine(s);
        }

        public void WriteLineItems(string s1, int n1, string s2, int n2, int nTabs, int nTabs2)
        {
            string line = string.Format("{0}={1} {2}={3}, Tabs={4}, Tabs2={5}", s1, n1, s2, n2, nTabs, nTabs2);
            file.WriteLine(line);
        }
    }
}
