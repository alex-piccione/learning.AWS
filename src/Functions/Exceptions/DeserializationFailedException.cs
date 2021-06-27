using System;

namespace Learning.Exceptions
{
    internal class DeserializationFailedException<T> : Exception
    {
        public string Json  { get; private set; }

        public DeserializationFailedException(string json, Exception exc) 
            : base($@"Failed to deserialize JSON to type ""{typeof(T)}"".", exc)
        {
            Json = json;
        }
    }
}
