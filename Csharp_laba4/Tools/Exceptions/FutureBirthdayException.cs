using System;

namespace Csharp_laba4.Tools.Exceptions
{
    internal class FutureBirthdayException : Exception
    {
        private string _message;
        private DateTime? _receivedDate;

        public override string Message
        {
            get => _message;
        }

        public FutureBirthdayException(string message)
        {
            _message = message;
        }

        public FutureBirthdayException(DateTime badDate)
        {
            _receivedDate = badDate;
            _message = $"A date from future was passed: {_receivedDate.ToString()}";
        }

        public FutureBirthdayException(DateTime badDate, string message)
        {
            _receivedDate = badDate;
            _message = message;
        }
    }
}
