using System;

namespace AareonTechnicalTest.Exceptions
{
    public class UserNotAuthorisedException : Exception
    {
        public UserNotAuthorisedException(string message) : base(message)
        { }
    }
}
