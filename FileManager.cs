using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
//using Microsoft.Office.Interop.Word;

namespace NissayaEditor.File_Manager
{
    //public partial class Form1 : Form
    //{
        class FileManager
        {
            enum FileType { TXT, RTF, ODT, DOCX };
            public string fName;
            FileType fType = FileType.TXT;
            public string fileContent = string.Empty;
            RichTextBox richTextBox;

            public FileManager(string fname, RichTextBox rtb)
            {
                fName = fname;
                richTextBox = rtb;
                string ext = Path.GetExtension(fname).ToLower();
                if (ext == ".txt") fType = FileType.TXT;
                if (ext == ".rtf") fType = FileType.RTF;
                if (ext == ".odt") fType = FileType.ODT;
                if (ext == ".docx" || ext == ".doc") fType = FileType.DOCX;
            }

            public string ReadFile()
            {
                switch (fType)
                {
                    case FileType.TXT:
                        fileContent = ReadFileTxt();
                        break;
                    case FileType.RTF:
                        richTextBox.LoadFile(fName);
                        fileContent = richTextBox.Text;
                        break;
                    case FileType.DOCX:
                        fileContent = ReadFileDoc();
                        break;
                }
                //string s = MyanmarCharsCleanup(fileContent);
                //if (s != fileContent)
                //{
                //    fileContent = s;
                //    SaveFile(fileContent);
                //}
                return fileContent;
            }

            private string ReadFileTxt()
            {
                using (StreamReader reader = new StreamReader(fName))
                {
                    fileContent = reader.ReadToEnd();
                }
                return fileContent;
            }

            private string ReadFileDoc()
            {
                string s = string.Empty;
                // Open a doc file.
                try
                {
                    //Microsoft.Office.Interop.Word.Application application = new Microsoft.Office.Interop.Word.Application();
                    //Document document = application.Documents.Open(fName);

                    //s = document.Content.Text;
                    //application.Quit();
                }
                catch (FileLoadException e)
                {
                    MessageBox.Show(e.Message);
                }
                return s;
            }

            public string SaveFile(string s, string outFile = "")
            {
                if (outFile.Length == 0) outFile = fName;
                //string[] lines = new string[] { fileContent };
                //File.WriteAllLines(fName, fileContent, Encoding.UTF8);
                s = MyanmarCharsCleanup(s);
                try
                {
                    switch (fType)
                    {
                        case FileType.TXT:
                            using (StreamWriter writer = new StreamWriter(outFile))
                            {
                                writer.WriteLine(s);
                            }
                            break;
                        case FileType.RTF:
                            richTextBox.SaveFile(outFile);
                            break;
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show("Error in FileManager::SaveFile() " + e.Message);
                }
                return s;
            }
        string MyanmarCharsCleanup(string s)
        {
            const char thaythayTin = '\u1036';
            const char thagyi = '\u103F';
            const char nyakalay = '\u1009';
            const char yapin = '\u103B';
            const char yayit = '\u103C';
            const char virama = '\u1039';
            const char Asat = '\u103A';
            const char oughtMyit = '\u1037';
            const char naughtPyin = '\u1032';
            const char waswae = '\u103D';
            const char huthoe = '\u102F';
            const char vassa2lonepauk = '\u1038';

            char[] Consonants = { 'က', 'ခ', 'ဂ', 'ဃ', 'င', 'စ', 'ဆ', 'ဇ', 'ဈ', 'ည', 'ဋ', 'ဌ', 'ဍ', 'ဎ', 'ဏ',
                                    'တ', 'ထ', 'ဒ', 'ဓ', 'န', 'ပ', 'ဖ', 'ဗ', 'ဘ', 'မ', 'ယ', 'ရ', 'လ', 'ဝ', 'သ',
                                    'ဟ', 'ဠ','အ', thaythayTin, thagyi, nyakalay};
            char[] ReducedConjCons = { '\u103D', '\u103E', '\u103B', '\u103C' };
            char[] Vowels = { '\u102C', '\u102B', '\u102D', '\u102E', '\u102F', '\u1030', '\u1031',
                                    '\u1032', '\u1036', '\u1037', '\u1038'};
            char[] consAssociates = { '\u102C', '\u102B', '\u102D', '\u102E', '\u102F', '\u1030', '\u1031',
                                    '\u1032', '\u1036', '\u1037', '\u1038',
                                // vowel killer
                                Asat, virama, 
                                // tone
                                thaythayTin, oughtMyit, naughtPyin, vassa2lonepauk,
                                // semi-vowels
                                yapin, yayit, huthoe, waswae
                                };
            string pattern = "စျ|ဥ်|ဦ|သြ|သြော်";
            s = s.Replace("စျ", "ဈ");
            s = s.Replace("ဥ်", "ဉ်");
            s = s.Replace("ဦ", "ဦ");
            s = s.Replace("သြ", "ဩ");
            s = s.Replace("သြော်", "ဪ");
            s = s.Replace("ဥ္", "ဉ္");

            // When walone is typed in as zero it replaces with proper walone.
            string mmChars = new string(Consonants) + new string(Vowels) +
                             new string(ReducedConjCons) + new string(consAssociates);
            pattern = "[" + mmChars + "]၀";
            pattern += "|၀[" + mmChars + "]";
            //string s1 = "၏ ၀ိပဿနာ";
            //int pos = s.IndexOf(s1);
            //string pattern = "၀[" + mmChars + "]";
            MatchCollection matches = Regex.Matches(s, pattern);
            foreach (Match m in matches)
            {
                //string rep = m.Value.Replace("၀", "ဝ");
                s = s.Replace(m.Value, m.Value.Replace("၀", "ဝ"));
            }
            return s;
        }
    }
    //}
}
