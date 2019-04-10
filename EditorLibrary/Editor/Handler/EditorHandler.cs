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
            editor.Cursors.Add(c);
        }

        public static List<Text> GetTexts(string name = null)
        {
            if (string.IsNullOrEmpty(name))
                return editor.Texts;
            else
                return editor.Texts
                    .Where(w => w.Name == name)
                    .ToList();
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
    }
}
