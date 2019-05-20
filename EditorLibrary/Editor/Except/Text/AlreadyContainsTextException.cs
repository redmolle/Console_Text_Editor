using EditorLibrary.Dict;

namespace EditorLibrary.Editor {
    class AlreadyContainsTextException : TextException {
        protected string message { get { return base._message; } }

        public override string Message { get { return $"{ExceptionMessageDict.AlreadyExistsText} {base._message}"; } }

        public AlreadyContainsTextException(string m) : base(m) { }

    }
}
