using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using NissayaEditor;

namespace Spell_Checker
{
    public class SpellChecker
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
        const string pattern = "[^\u1000-\u1027\u1029-\u1032\u1036-\u104F\u0020\u0030-\u0039\\-#*^@(){}\r\n]" +
                    "|စျ|ဥ်|ဦ|သြ|သြော်";
        Dictionary<string, string> baseErrMapping = new Dictionary<string, string>()
        {
            {"စျ", "ဈ"}, {"ဥ်", "ဉ်"}, {"ဥ္", "ဉ္"}
        };

        Dictionary<string, string> baseErrDescription = new Dictionary<string, string>()
        {
            {"စျ", "စ + \u103B|ဈ - ဈမျဉ်းဆွဲ"}, {"ဥ်", "အက္ခရာ 'ဥ' + \u103A|ညကလေး 'ဉ' + \u103A"}, {"ဥ္", "အက္ခရာ 'ဥ'|ညကလေး 'ဉ'"}
        };

        List<Form1._ErrDesc> listErrDesc = new List<NissayaEditor.Form1._ErrDesc>();
        Dictionary<string, string> PhoneticWords = null;
        List<string> UnitQtyNouns = null;

        string pattern1;
        string mmChars;
        string UnitQtyPattern;
        string PhoneticNounPattern;
        string errMsg;

        MatchCollection matches;
        Form1 parentForm;

        public SpellChecker(Form1 parentForm = null)
        {
            this.parentForm = parentForm;
            // When walone is typed in as zero it replaces with proper walone.
            mmChars = new string(Consonants) + new string(Vowels) +
                                new string(ReducedConjCons) + new string(consAssociates);
            pattern1 = "[" + mmChars + "]၀";
            pattern1 += "|၀[" + mmChars + "]";
        }

        public void SetDictionaries(Dictionary<string, string> PhoneticWords, List<string> UnitQtyNouns)
        {
            this.PhoneticWords = PhoneticWords;
            this.UnitQtyNouns = UnitQtyNouns;

            UnitQtyPattern = string.Empty;
            string pattern = string.Empty;
            string v = new string(consAssociates);
            if (UnitQtyNouns.Contains("ခု"))
            {
                string p1 = "ခု";
                string finalAsat = "[.^" + Asat + "]";
                string t = 
                    "[^" + virama + "]တ" + p1 + finalAsat +
                    "|^တ" + p1 + finalAsat +
                    "|တ" + p1 + finalAsat +
                    "|တ" + p1 + 
                    "|တ" + p1 + "$";
                t = "[^" + virama + "]တ" + p1 + finalAsat
                    + "|^တ" + p1 + finalAsat
                    + "|တ" + p1 + finalAsat
                    + "|တ" + p1;
                t = "[^|^" + virama + "]တ|" + consAssociates + Consonants +
                    ReducedConjCons + Vowels + "]တ" + p1 + "[$|.[^" + Asat + "]";
                t = "[^|^" + virama + "]တ" + p1 + "[$|.^" + Asat + "]";
                    //ReducedConjCons + Vowels + "]တ" + p1 + "[$|.[^" + Asat + "]";
                t = "^တ" + p1 + ".[^" + Asat + "]";//|[^" + Vowels + "]";
                t = "^တ" + p1 + "[^" + Vowels +"]"; ;
                t = "^တ" + p1 + ".[^" + Asat + "]"; // good 01100
                t = "^တ" + p1 + ".[^" + Asat + "]|^တ" + p1 + ".$"; // good 01101

                t = "[^" + virama + "]တ" + p1 + "$|^တ" + p1 + "$";  // good 11010, 25, 9, --, 0, --

                string s1 = "တခုန် တစုံတခု တခု ခေတ္တခု တခု";
                string s2 = "တခု တခုန် တခု";
                string s3 = "တခုက ခေတ္တခု";
                string s4 = "တခု";
                string s5 = "တခု ";
                MatchCollection matches1, matches2, matches3, matches4, matches5;
                int n1, n2, n3, n4, n5;
                matches1 = Regex.Matches(s1, t); // တစုံတခုန်တခု
                n1 = matches1.Count;
                matches2 = Regex.Matches(s2, t); // တစုံတခုန်တခု
                n2 = matches2.Count;
                matches3 = Regex.Matches(s3, t); // တစုံတခုန်တခု
                n3 = matches3.Count;
                matches4 = Regex.Matches(s4, t); // တစုံတခုန်တခု
                n4 = matches4.Count;
                matches5 = Regex.Matches(s5, t); // တစုံတခုန်တခု
                n5 = matches5.Count;
                var e = Console.OutputEncoding;

                if (n1 > 0) Console.WriteLine("Index = " + matches1[0].Index.ToString());// + "\t" + m.Value);
                if (n2 > 0) Console.WriteLine("Index = " + matches2[0].Index.ToString());// + "\t" + m.Value);
                if (n3 > 0) Console.WriteLine("Index = " + matches3[0].Index.ToString());// + "\t" + m.Value);
                if (n4 > 0) Console.WriteLine("Index = " + matches4[0].Index.ToString());// + "\t" + m.Value);
                if (n5 > 0) Console.WriteLine("Index = " + matches5[0].Index.ToString());// + "\t" + m.Value);

                //if (n1 > 0)
                //{
                //    foreach (Match m in matches)
                //        Console.WriteLine("Index = " + m.Index.ToString());// + "\t" + m.Value);
                //}
            }
            foreach (string p in UnitQtyNouns)
            {
                if (p.Length == 1 || consAssociates.Contains(p[p.Length - 1]))
                {
                    pattern = "[^" + virama + "]တ" + p + "[^" + v + "]";
                    pattern += "|^တ" + p + "[^" + v + "]|တ" + p + "[^" + v + "]";
                    pattern += "|တ" + p + "$";
                    UnitQtyPattern += (UnitQtyPattern.Length == 0) ? pattern : "|" + pattern;
                }
                else
                {
                    // the rest of letters cannot have vowels
                    pattern = "[^" + virama + "]တ" + p + "|^တ" + p + "|တ" + p + "$";
                    UnitQtyPattern += (UnitQtyPattern.Length == 0) ? pattern : "|" + pattern;
                    string t = "[^" + virama + "]တ" + p + "|^တ" + p + "|တ" + p + "$";
                    MatchCollection matches = Regex.Matches("တခု", t);
                    int n = matches.Count;
                }
            }

            PhoneticNounPattern = string.Empty;
            foreach (string k in PhoneticWords.Keys)
            {
                PhoneticNounPattern += (PhoneticNounPattern.Length == 0) ? k : "|" + k;
            }

        }

        public string doSpellCheck(string s)
        {
            s = MyanmarCharsCleanup(s);
            return s;
        }

        public string MyanmarCharsCleanup(string s)
        {
            s = s.Replace("ဦ", "ဦ");
            s = s.Replace("သြ", "ဩ");
            s = s.Replace("သြော်", "ဪ");

            return s;
        }

        public MatchCollection MyanmarBasicErrors(string s)
        {
            listErrDesc.Clear();
            string[] pattern = { "စျ", "ဥ်", "ဥ္" };
            MatchCollection matches = null, matches1 = null;
            foreach (string p in pattern)
            {
                matches = Regex.Matches(s, p);
                if (matches.Count > 0)
                {
                    string m = matches[0].Value;
                    Form1._ErrDesc d = new Form1._ErrDesc();
                    string[] f = baseErrDescription[m].Split('|');
                    d.incorrectMsg = f[0];
                    d.correctMsg = f[1];
                    d.incorrectText = m;
                    d.correctText = baseErrMapping[m];
                    d.errDesc = "Incorrect Myanmar grammar.  Click 'Correct' to correct.";
                    listErrDesc.Add(d);
                    matches1 = matches;
                }
            }
            return matches1;
        }

        public MatchCollection FindInvalidMyanmarLetters(string s)
        {
            listErrDesc.Clear();
            const string pattern = "[^\u1000-\u1027\u1029-\u1032\u1036-\u104F\u0020\u0030-\u0039\\-\\#\\*\\^(){}\\[\\]'\"\r\n]";
            int prevPos = -1;
            errMsg = string.Empty;
            string errTxt = string.Empty;
            MatchCollection matches = Regex.Matches(s, pattern);
            foreach (Match m in matches)
            {
                if (prevPos != -1)
                {
                    if (prevPos + 1 == m.Index)
                    {
                        errMsg += m.Value;
                        errTxt += m.Value;
                    }
                    else
                    {
                        Form1._ErrDesc d = new Form1._ErrDesc();
                        d.incorrectMsg = errMsg;
                        d.incorrectText = errTxt;
                        d.correctMsg = string.Empty;
                        d.errDesc = "Invalid character(s). Click 'Correct' to remove them.";
                        listErrDesc.Add(d);
                        errMsg += " " + m.Value;
                        errTxt = m.Value;
                    }
                }
                else
                    errMsg = errTxt = m.Value;
                prevPos = m.Index;
            }
            if (errMsg.Length > 0)
            {
                //errMsg += "||" + "Invalid character(s).";
                if (listErrDesc.Count > 0)
                {
                    Form1._ErrDesc d0 = listErrDesc[0];
                    d0.incorrectMsg = errMsg;
                    listErrDesc[0] = d0;
                }
                Form1._ErrDesc d = new Form1._ErrDesc();
                d.incorrectMsg = errMsg;
                d.incorrectText = errTxt;
                d.correctMsg = string.Empty;
                d.errDesc = "Invalid character(s). Click 'Correct' to remove them.";
                listErrDesc.Add(d);
            }
            return matches;
        }

        public MatchCollection PairCheck(string s)
        {
            listErrDesc.Clear();
            MatchCollection matches = Regex.Matches(s, "\"");
            MatchCollection matches1= matches;
            MatchCollection matches0 = matches;
            if (matches.Count % 2 == 1)
            {
                Form1._ErrDesc d = new Form1._ErrDesc();
                d.incorrectMsg = errMsg = "\" \" mismatched.";
                d.errDesc = "Mismatched double quotes. Correct manually.";
                listErrDesc.Add(d);
                matches0 = matches;
            }

            matches = Regex.Matches(s, "'");
            if (matches.Count % 2 == 1)
            {
                Form1._ErrDesc d = new Form1._ErrDesc();
                d.incorrectMsg = errMsg = "' ' mismatched.";
                d.errDesc = "Mismatched single quotes. Correct manually.";
                listErrDesc.Add(d);
                matches0 = matches;
            }

            matches = Regex.Matches(s, "\\(");
            matches1 = Regex.Matches(s, "\\)");
            if (matches.Count != matches1.Count)
            {
                Form1._ErrDesc d = new Form1._ErrDesc();
                d.incorrectMsg = "( ) mismatched.";
                d.errDesc = (matches.Count > matches1.Count) ? " ')'" : " '('";
                d.errDesc += " missing.";
                d.errDesc += " Correct manually.";
                listErrDesc.Add(d);
                matches0 = (matches.Count > 0) ? matches : matches1;
            }

            matches = Regex.Matches(s, "\\[");
            matches1 = Regex.Matches(s, "\\]");
            if (matches.Count != matches1.Count)
            {
                Form1._ErrDesc d = new Form1._ErrDesc();
                d.incorrectMsg = "[ ] mismatched.";
                d.errDesc = (matches.Count > matches1.Count) ? " ']'" : " '['";
                d.errDesc += " missing.";
                d.errDesc += " Correct manually.";
                listErrDesc.Add(d);
                matches0 = (matches.Count > 0) ? matches : matches1;
            }

            matches = Regex.Matches(s, "\\{");
            matches1 = Regex.Matches(s, "\\}");
            if (matches.Count != matches1.Count)
            {
                Form1._ErrDesc d = new Form1._ErrDesc();
                d.incorrectMsg = "{ } mismatched.";
                d.errDesc = (matches.Count > matches1.Count) ? " '}'" : " '{'";
                d.errDesc += " missing.";
                d.errDesc += " Correct manually.";
                listErrDesc.Add(d);
                matches0 = (matches.Count > 0) ? matches : matches1;
            }
            return matches0;
        }

        public MatchCollection FindZeroes(string s)
        {
            listErrDesc.Clear();
            string mmChars = new string(Consonants) + new string(Vowels) +
                                new string(ReducedConjCons) + new string(consAssociates);
            string pattern = "[" + mmChars + "]၀";
            pattern += "|၀[" + mmChars + "]";
            matches = Regex.Matches(s, pattern);
            foreach(Match m in matches)
            {
                Form1._ErrDesc d = new Form1._ErrDesc();
                errMsg = "(၀) သုည|ဝ-လုံး|သုညအစား၊ ဝ-လုံးရေးရန်။";
                d.incorrectMsg = "(၀) သုည";
                d.incorrectText = m.Value;
                d.correctText = m.Value.Replace("၀", "ဝ");
                d.correctMsg = "ဝ-လုံး";
                d.errDesc = "သုညအစား၊ ဝ-လုံးရေးရန်။";
                d.index = m.Index;
                listErrDesc.Add(d);
            }
            return matches;
        }

        public MatchCollection FindInCorrectQty(string s)
        {
            listErrDesc.Clear();
            MatchCollection matches;
            int pos = UnitQtyPattern.IndexOf("စုံ");
            pos = UnitQtyPattern.IndexOf("ခု");
            matches = Regex.Matches(s, UnitQtyPattern);
            int idx;
            foreach (Match m in matches)
            {
                Form1._ErrDesc d = new Form1._ErrDesc();
                d.incorrectMsg = d.incorrectText = m.Value.Trim();
                idx = d.incorrectText.IndexOf('တ');
                d.correctMsg = d.correctText = d.incorrectText.Substring(0, idx + 1) + "စ်" + d.incorrectText.Substring(idx + 1);
                d.errDesc = "Misspelled numeral word. Click 'Correct' for correction.";
                d.index = m.Index;
                listErrDesc.Add(d);
            }
            return matches;
        }

        public MatchCollection FindPhoneticWords(string s)
        {
            matches = Regex.Matches(s, PhoneticNounPattern);
            MatchCollection matches1 = null;
            foreach (Match m in matches)
            {
                matches1 = FindWordMatches(s, m.Value);
                foreach (Match m1 in matches1)
                {
                    Form1._ErrDesc d = new Form1._ErrDesc();
                    d.incorrectMsg = d.incorrectText = m1.Value.Trim();
                    if (PhoneticWords.ContainsKey(d.incorrectText))
                        d.correctMsg = d.correctText = PhoneticWords[d.incorrectText];
                    d.errDesc = "Misspelled word. Click 'Correct' for correction.";
                    d.index = m1.Index;
                    listErrDesc.Add(d);
                }
            }
            return matches1;
        }

        private MatchCollection FindWordMatches(string s, string w)
        {
            string pattern = string.Empty;
            MatchCollection m = null;
            pattern = w + "[^" + new string(consAssociates) + new string(Consonants) + "]";
            pattern += "|" + w + "[" + new string(Consonants) + "][^" + Asat + "]";
            pattern += "|" + w + "$";
            m = Regex.Matches(s, pattern);
            return m;
        }

        public string GetErrMsg() { return errMsg; }

        public List<Form1._ErrDesc> GetErrDesc() { return listErrDesc; }

        public string GetErrMsg(MatchCollection matches, int type)
        {
            int prevPos = -1;
            string errMsg = string.Empty;
            foreach (Match m in matches)
            {
                if (prevPos != -1)
                {
                    if (prevPos + 1 == m.Index) errMsg += m.Value;
                    else errMsg += " " + m.Value;
                }
                else errMsg = m.Value;
                prevPos = m.Index;
            }
            if (errMsg.Length > 0)
            {
                switch(type)
                {
                    case 1:
                        errMsg += "||" + "Invalid character(s).";
                        break;
                    case 2:
                        errMsg = "(၀) သုည|ဝ-လုံး|သုညအစား၊ ဝ-လုံးရေးရန်။";
                        break;
                    case 3:
                        errMsg += "(၀) သုည|ဝ-လုံး|သုညအစား ဝ-လုံးရေးရန်။";
                        break;
                    case 4:
                        errMsg += "(၀) သုည|ဝ-လုံး|Basic Myanmar grammar.";
                        break;
                }
            }
            return errMsg;
        }
    }
}
