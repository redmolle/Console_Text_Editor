using EditorLibrary.Dict;

namespace EditorLibrary.Editor
{
    class NoSuchTextException : TextException
    {
        protected string message { get { return base._message; } }

        public override string Message { get { return $"{ExceptionMessageDict.NoSuchText} {message}"; } }

        public NoSuchTextException(string m) : base(m) { }
    }
}
