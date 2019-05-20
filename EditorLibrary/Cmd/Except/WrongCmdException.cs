using EditorLibrary.Dict;

namespace EditorLibrary.Cmd {
    public class WrongCmdException : CmdException {
        protected string message { get { return base._message; } }

        public override string Message { get { return $"{ExceptionMessageDict.WrongCmd} {message}"; } }

        public WrongCmdException(string m) : base(m) { }
    }
}
