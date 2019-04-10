using EditorLibrary.Dict;

namespace EditorLibrary.Editor
{
    public class CursorException : EditorException
    {
        protected string _message { get { return base._message; } }

        public override string Type { get { return ExceptionMessageDict.Cursor; } }
        public override string Message { get; }

        public CursorException(string m) : base(m) { }
    }
}