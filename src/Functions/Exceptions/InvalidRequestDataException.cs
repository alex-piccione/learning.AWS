using System;
using System.Linq;

namespace Learning.Exceptions
{
    internal class InvalidRequestDataException :Exception
    {

        public InvalidRequestDataException(string field, string error)
            : base($"Invalid request data. Field:{field}, Error: {error}.")
        { 
            
        }


        public InvalidRequestDataException(params InvalidData[] errors)
            : base($"Invalid request data. " + GetError(errors))
        {

        }

        private static string GetError(InvalidData[] errors)
            => string.Join(", ", errors.Select(e => $"Field: {e.Field}, Error: {e.Error}"));
        

        internal struct InvalidData {
            public string Field { get; set; }
            public string Error { get; set; }
        }
    }
}
