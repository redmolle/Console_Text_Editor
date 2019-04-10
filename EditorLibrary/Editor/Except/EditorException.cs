using EditorLibrary.Except;

namespace EditorLibrary.Editor
{
    public class EditorException : BaseException
    {
        protected string _message { get { return base._message; } }

        public override string Type { get; }
        public override string Message { get; }

        protected EditorException(string m) : base(m) { }
    }
}
