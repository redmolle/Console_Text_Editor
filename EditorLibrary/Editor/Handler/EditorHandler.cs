using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EditorLibrary.Editor
{
    public static class EditorHandler
    {
        public static EditorElements editor { get; set; }

        public static void Init()
        {
            editor = new EditorElements();
            editor.Texts = new List<Text>();
            editor.Cursors = new List<Cursor>();
        }

        public static EditorElements Print(string name = null)
            {
            return string.IsNullOrEmpty(name) ?
            editor :
            new EditorElements
            {
                Texts = new List<Text> { editor.Texts.Where(w => w.Name == name).FirstOrDefault() },
                Cursors = new List<Cursor> { editor.Cursors.Where(w => w.Name == name).FirstOrDefault() }
            };
            }

        public static bool HasTextNamed(string name)
        {
            return editor.Texts.Any(a => a.Name == name);
        }
        public static bool HasCursorNamed(string name)
        {
            return editor.Cursors.Any(a => a.Name == name);
        }

        public static void AddText(Text t)
        {
            if (HasTextNamed(t.Name))
            {
                //throw new ConsoleHandler.Editor.Except.Editor.Text.AlreadyContainsTextException(t.Name);
                editor.Texts.Where(w => w.Name == t.Name).FirstOrDefault().Data += t.Data;
            }
            else
            editor.Texts.Add(t);
        }

        public static void AddCursor(Cursor c)
        {
            if (HasCursorNamed(c.Name))
                throw new AlreadyContainsTextException(c.Name);


            editor.Cursors.Add(SetCursorData(c));
        }

        public static List<Text> GetTexts(string name = null)
        {
            if (string.IsNullOrEmpty(name))
                return editor.Texts;
            else
            {
                //if (!HasTextNamed(name))
                //    throw new NoSuchTextException(name);
                return editor.Texts
                    .Where(w => w.Name == name)
                    .ToList();
            }
        }

        public static List<Cursor> GetCursors(string name = null)
        {
            if (string.IsNullOrEmpty(name))
                return editor.Cursors;
            else
                return editor.Cursors
                    .Where(w => w.Name == name)
                    .ToList();
        }


        public static void SendText(string sourceName, string targetName)
        {
            if (!HasTextNamed(targetName))
                throw new NoSuchTextException(targetName);

            if (HasCursorNamed(sourceName))
                editor.Texts
                    .Where(w => w.Name == targetName)
                    .FirstOrDefault()
                    .Data 
                    += 
                    editor.Cursors
                    .Where(w => w.Name == sourceName)
                    .FirstOrDefault()
                    .Data;
            
            else if(HasTextNamed(sourceName))
                editor.Texts
                    .Where(w => w.Name == targetName)
                    .FirstOrDefault()
                    .Data
                    +=
                    editor.Texts
                    .Where(w => w.Name == sourceName)
                    .FirstOrDefault()
                    .Data;
            
            else
                throw new NoSuchTextException(targetName);
        }

        public static void Format(string name, string[] separators = null)
        {
            if (!HasTextNamed(name))
                throw new NoSuchTextException(name);

            string data = GetTexts(name).FirstOrDefault().Data;
            data = Regex.Replace(data, @"\s+", " ");

            separators = separators ?? new string[0];

            foreach(string s in separators)
            {
                data = data.Replace($"{s}", $" {s} ");
            }

            editor.Texts
                .Where(w => w.Name == name)
                .FirstOrDefault()
                .Data = data;
        }
        
        private static Cursor SetCursorData(Cursor c)
        {
            if(c.From.Position == null)
                c.From.Position = FindWord(c.Target.Data, c.From.Word)
                    .OrderBy(o => o)
                    .FirstOrDefault();

            if (c.To.Position == null)
                c.To.Position = FindWord(c.Target.Data, c.To.Word)
                    .Where(w => c.Ahead ? w >= c.From.Position.Value : w <= c.From.Position.Value)
                    .OrderBy(o => o)
                    .FirstOrDefault();
            else
                c.To.Position = c.Ahead ? c.From.Position + c.To.Position :
                    c.From.Position - c.To.Position;

            if(c.From.Position.Value > c.To.Position.Value)
            {
                int tmp = c.From.Position.Value;
                c.From.Position = c.To.Position.Value;
                c.To.Position = tmp;
            }

            List<IndexedWord> text = GetNumeredWordsText(c.Target.Data);

            c.From.Position = c.From.Position < 0 ? 0 : c.From.Position;
            c.To.Position = c.To.Position > text.Count ? text.Count : c.To.Position;

            int startIndex = text
                .Where(w => w.Position == c.From.Position)
                .Select(s => s.WhiteSpacePosition)
                .FirstOrDefault();
            int len = text
                .Where(w => w.Position == c.To.Position)
                .Select(s => s.WhiteSpacePosition)
                .FirstOrDefault()
                - startIndex;

            c.Data = string.Concat(text.GetRange(startIndex, len));

            //return GetNumeredWordsText(c.Target.Data)


            //string[] wordsW = Regex.Split(c.Target.Data, @"(\s+)");
            //string[] words = Regex.Split(c.Target.Data, @"\s+");
            //int len = 0;
            //for(int i = 0; i < wordsW.Length; i++)
            //{
            //if(!string.IsNullOrWhiteSpace(words[i]))
            //{ }
            //}
            ////GetNumeredWordsText(c.Target.Data);

            //c.Data = string.Concat(text.ToList());

            return c;
        }

        private static List<int> FindWord(string text, string word)
        {
            List<int> indexes = new List<int>();
            string[] words = Regex.Split(text, @"\s+");

            for(int i = 0; i< words.Length; i++)
            {
                if (words[i] == word)
                    indexes.Add(i);
            }

            return indexes.OrderBy(o => o).ToList();

        }

        public static List<IndexedWord> GetNumeredWordsText(string text)
        {
            List<IndexedWord> words = new List<IndexedWord>();

            string[] wordsArray = Regex.Split(text, @"(\s+)");
            int k = 0;

            for (int i = 0; i < wordsArray.Length; i++)
            {
                int? pos = (int?)null;
                if(!string.IsNullOrWhiteSpace(wordsArray[i]))
                {
                    pos = k;
                    k++;
                }

                words.Add(new IndexedWord
                {
                    WhiteSpacePosition = i,
                    Position = pos,
                    Value = wordsArray[i]
                });
            }
            return words;
        }
        

    }
}
