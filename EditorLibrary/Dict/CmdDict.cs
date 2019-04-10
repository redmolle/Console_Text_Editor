using EditorLibrary.Cmd;

namespace EditorLibrary.Dict
{
    public static class CmdDict
    {
        public static string Input { get; set; }
        public static string Format { get; set; }
        public static string Cursor { get; set; }
        public static string Send { get; set; }
        public static string End { get; set; }
        public static string Print { get; set; }

        public static void SetUp(CmdList c)
        {
            Input = c.Input;
            Format = c.Format;
            Cursor = c.Cursor;
            Send = c.Send;
            End = c.End;
            Print = c.Print;
        }
    }
}
