using System;

namespace Csharp_laba4.Tools.Exceptions
{
    internal class PastBirthdayException : Exception
    {
        private string _message;
        private DateTime? _receivedDate;

        public PastBirthdayException(string message)
        {
            _message = message;
        }

        public PastBirthdayException(DateTime badDate)
        {
            _receivedDate = badDate;
            _message = $"A date from future was passed: {_receivedDate.ToString()}";
        }

        public PastBirthdayException(DateTime badDate, string message)
        {
            _receivedDate = badDate;
            _message = message;
        }

        public override string Message
        {
            get => _message;
        }
    }
}
