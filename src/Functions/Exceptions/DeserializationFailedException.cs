using System;

namespace Learning.Exceptions
{
    internal class DeserializationFailedException : Exception
    {
        public string Json  { get; private set; }
        public DeserializationFailedException(string json, Type @type, Exception exc) 
            : base($@"Failed to deserialize JSON to type ""{@type}"".", exc)
        {
            Json = json;
        }
    }
}
