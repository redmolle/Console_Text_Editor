using EditorLibrary.Dict;

namespace EditorLibrary.Cmd {
    public class MisstakeCmdException : CmdException {
        protected string message { get { return base._message; } }

        public override string Message { get { return $"{ExceptionMessageDict.MisstakeCmd} {message}"; } }

        public MisstakeCmdException(string m) : base(m) { }
    }
}
