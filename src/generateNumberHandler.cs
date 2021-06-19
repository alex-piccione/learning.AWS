using System;
using System.IO;
using Amazon.Lambda.Core;
using Portfolio;
// Amazon.Lambda.Serialization.SystemTextJson

// ref: https://dzone.com/articles/under-the-hood-of-net-core-lambda-request

// learning::Learning.Portfolio.RandomNumberFunction::Handle
[assembly:LambdaSerializer(typeof(Request))]
namespace Portfolio
{
    public class RandomNumberFunction {
        Random fate = new Random(DateTime.Now.Millisecond);
        public string Handle(string input, ILambdaContext context)
        {
            // Read the stream into a string
            /*if (input != null)
            {
                StreamReader streamReader = new StreamReader(input);
                inputString = streamReader.ReadToEnd();
            }*/
            
            //var output = new StreamReader(new TextReader(""));
            //output.Write("123");
            
            return fate.Next().ToString();
        }
    }

    public class Request : ILambdaSerializer
    {
        public T Deserialize<T>(Stream requestStream)
        {
            throw new NotImplementedException();
        }

        public void Serialize<T>(T response, Stream responseStream)
        {
            throw new NotImplementedException();
        }
    }
}

