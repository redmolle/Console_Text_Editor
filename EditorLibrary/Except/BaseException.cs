using System;

namespace EditorLibrary.Except
{
    public class BaseException : Exception
    {
        protected string _message;

        public virtual string Type { get; }
        public virtual string Message { get; }

        protected BaseException(string m)
        {
            _message = m;
        }
    }
}
