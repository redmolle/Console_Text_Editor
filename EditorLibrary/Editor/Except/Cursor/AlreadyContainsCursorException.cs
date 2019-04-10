using EditorLibrary.Dict;

namespace EditorLibrary.Editor
{
    class AlreadyContainsCursorException : CursorException
    {
        protected string message { get { return base._message; } }

        public override string Message { get { return $"{ExceptionMessageDict.AlreadyExistsText} {message}"; } }

        public AlreadyContainsCursorException(string m) : base(m) { }
    }
}
