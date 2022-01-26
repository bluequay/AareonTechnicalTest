using System;

namespace AareonTechnicalTest.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message) : base(message)
        { }
    }
}
