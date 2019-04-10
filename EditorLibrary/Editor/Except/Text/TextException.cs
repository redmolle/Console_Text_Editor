using EditorLibrary.Dict;

namespace EditorLibrary.Editor
{
    public class TextException : EditorException
    {
        protected string _message { get { return base._message; } }

        public override string Type { get { return ExceptionMessageDict.Text; } }
        public override string Message { get; }

        protected TextException(string m) : base(m) { }
    }
}
