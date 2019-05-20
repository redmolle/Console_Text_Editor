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
            //{
                //Cursor tmpC = editor.Cursors.Where(w => w.Name == c.Name).FirstOrDefault();
                //c.From.Position = tmpC.To.Position
            //}
                throw new AlreadyContainsTextException(c.Name);

            editor.Cursors.Add(SetCursorData(c));
        }

        public static List<Text> GetTexts(string name = null, bool isNeedThrow = false)
        {
            if (string.IsNullOrEmpty(name))
                return editor.Texts;
            else
            {
                if (isNeedThrow && !HasTextNamed(name))
                    throw new NoSuchTextException(name);
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


        public static void SendText(string sourceName, string targetName, CursorDestination after)
        {
            List<IndexedWord> text = new List<IndexedWord>();

            if (!HasTextNamed(targetName))
                editor.Texts.Add(new Text
                {
                    Name = targetName,
                    Data = string.Empty
                });
            //throw new NoSuchTextException(targetName);

            List<IndexedWord> target = GetNumeredWordsText(editor.Texts.FirstOrDefault(f => f.Name == targetName).Data);

            if (HasCursorNamed(sourceName))
            {
                text = GetNumeredWordsText(editor.Cursors.FirstOrDefault(f => f.Name == sourceName).Data);
            }
            else if(HasTextNamed(sourceName))
            {
                text = GetNumeredWordsText(editor.Texts.FirstOrDefault(f => f.Name == sourceName).Data);
            }
            else
                throw new NoSuchTextException(targetName);
            
            after.Position = after.Position ?? (string.IsNullOrEmpty(after.Word) ? target.Count : 
                target.FirstOrDefault(f => f.Value == after.Word).WhiteSpacePosition);

            List<IndexedWord> tmp = target.Where(w => w.WhiteSpacePosition > after.Position).ToList();
            target = target.Where(w => w.WhiteSpacePosition <= after.Position).ToList();
            target.Add(new IndexedWord
            {
                Value = " "
            });
            foreach(IndexedWord w in text)
            {
                target.Add(w);
            }
            foreach(IndexedWord w in tmp)
            {
                target.Add(w);
            }

            editor.Texts.Where(w => w.Name == targetName).Select(s => s.Data = string.Concat(target.Select(t => t.Value))).ToList();
            //s.Data = string.Concat(target.Select(t => t.Value)));

        }

        public static void Format(string name, string[] separators = null)
        {
            if (!HasTextNamed(name))
                throw new NoSuchTextException(name);

            string data = GetTexts(name).FirstOrDefault().Data;
            data = Regex.Replace(data, @"\s+", " ");

            separators = separators ?? new string[0];

            foreach (string s in separators)
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
            List<IndexedWord> text = GetNumeredWordsText(c.Target.Data);

            int lastWordIndex = text.OrderByDescending(o => o.Position).FirstOrDefault().Position.Value;

            if (c.From.Position == null)
            {
                var start = text.Where(w => w.Value == c.From.Word).OrderBy(o => o.Position);
                c.From.Position = start.Count() > 0 ? start.FirstOrDefault().Position : 0;

            }
            else
            {
                c.From.Position = (c.From.Position < 0 || c.From.Position > lastWordIndex) ? 0 : c.From.Position;
            }
            
            if (c.To.Position == null)
            {
                var end = c.Ahead ?
                    text.Where(w => w.Value == c.To.Word).OrderBy(o => o.Position) :
                    text.Where(w => w.Value == c.To.Word).OrderByDescending(o => o.Position);

                c.To.Position = end.Count() > 0 ?
                    end.FirstOrDefault().Position : (c.Ahead ? lastWordIndex : 0);
            }
            else
            {
                c.To.Position = c.Ahead ? c.From.Position + c.To.Position : c.From.Position - c.To.Position;
                c.To.Position = c.To.Position < 0 ? 0 : (c.To.Position > lastWordIndex ?lastWordIndex : c.To.Position);

            }

                if(c.From.Position > c.To.Position)
                {
                    int tmp = c.From.Position.Value;
                    c.From.Position = c.To.Position;
                    c.To.Position = tmp;
                }
            int startIndex = text.FirstOrDefault(f => f.Position == c.From.Position).WhiteSpacePosition;
            c.TargetSize = text.FirstOrDefault(f => f.Position == c.To.Position).WhiteSpacePosition - startIndex;
            c.From.Word = text.FirstOrDefault(f => f.Position == c.From.Position).Value;
            c.To.Word = text.FirstOrDefault(f => f.Position == c.To.Position).Value;
            c.Data = string.Concat(text.GetRange(startIndex, c.TargetSize).OrderBy(o => o.WhiteSpacePosition).Select(s => s.Value));
            
            return c;
        }

        private static List<int> FindWord(string text, string word)
        {
            List<int> indexes = new List<int>();
            string[] words = Regex.Split(text, @"\s+");

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] == word)
                    indexes.Add(i);
            }

            return indexes.OrderBy(o => o).ToList<int>();

        }

        public static List<IndexedWord> GetNumeredWordsText(string text)
        {
            List<IndexedWord> words = new List<IndexedWord>();

            string[] wordsArray = Regex.Split(text, @"(\s+)");
            int k = 0;

            for (int i = 0; i < wordsArray.Length; i++)
            {
                int? pos = (int?)null;
                if (!string.IsNullOrWhiteSpace(wordsArray[i]))
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