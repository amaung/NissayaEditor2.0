using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NissayaEditor
{
    public class NissayaBook
    {
        string title;
        string author;
        string bookFileName;
        int ChapCount = 0;
        public class Chapter
        {
            public bool checkBox;
            public string chapName;
            public string fileName;

            public Chapter(bool chk, string cName, string fName) { checkBox = chk; chapName = cName; fileName = fName; }
        }
        public List<Chapter> chapters = new List<Chapter>();
        public string GetTitle() { return title; }
        public string GetAuthor() { return author; }
        public NissayaBook(string bfname, string tle, string auth) { title = tle; author = auth; bookFileName = bfname; }
        public NissayaBook(string bfname)
        {
            JSON.Json nbk = new JSON.Json(bfname);
            bookFileName = bfname;
            title = nbk.GetData("TITLE");
            author = nbk.GetData("AUTHOR");
            ChapCount = Convert.ToInt32(nbk.GetData("CHCOUNT"));
            string chName, chFile;
            for (int i = 0; i < ChapCount; i++)
            {
                chName = nbk.GetData("CHAP" + (i + 1).ToString());
                chFile = nbk.GetData("FILE" + (i + 1).ToString());
                chapters.Add(new Chapter(true, chName, chFile));
            }
        }
        public NissayaBook()
        {
        }
        public void AddChapter(bool chkbox, string cName, string fName)
        {
            chapters.Add(new Chapter(chkbox, cName, fName));
        }
        public string[] NissayaBookFileData()
        {
            List<string> info = new List<string>();
            info.Add("AUTHOR|" + author.Trim());
            info.Add("TITLE|" + title.Trim());
            info.Add("CHCOUNT|" + chapters.Count.ToString());
            int n = 0;
            foreach (Chapter ch in chapters)
            {
                ++n;
                info.Add("CHAP" + n.ToString() + "|" + ch.chapName.Trim());
                info.Add("FILE" + n.ToString() + "|" + ch.fileName.Trim());
            }

            return info.ToArray();
        }
    }
}
