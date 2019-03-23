using System;

namespace Csharp_laba4.Tools.Exceptions
{
    internal class EmailException : Exception
    {
        private string _message;

        public override string Message
        {
            get => _message;
        }

        public EmailException(string message)
        {
            _message = message;
        }
    }
}
