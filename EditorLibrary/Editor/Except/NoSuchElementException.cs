using EditorLibrary.Dict;

namespace EditorLibrary.Editor {
    class NoSuchElementException : EditorException {
        protected string message { get { return base._message; } }

        public override string Message { get { return $"{ExceptionMessageDict.NoSuchElement} {message}"; } }

        public NoSuchElementException(string m) : base(m) { }
    }
}
