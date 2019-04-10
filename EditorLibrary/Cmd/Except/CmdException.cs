using EditorLibrary.Except;
using EditorLibrary.Dict;

namespace EditorLibrary.Cmd
{
    public  class CmdException : BaseException
    {
        protected string _message { get { return base._message; } }

        public override string Type { get { return ExceptionMessageDict.Cmd; } }
        public override string Message { get; }

        public CmdException(string m) : base(m) { }
    }
}
