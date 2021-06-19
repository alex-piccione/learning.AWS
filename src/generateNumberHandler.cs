using System.IO;
// Amazon.Lambda.Serialization.SystemTextJson

// ref: https://dzone.com/articles/under-the-hood-of-net-core-lambda-request

namespace Learning.Portfolio
{
    public class RandomNumberFunction {
        public string Handle(string input, Amazon.Lambda.Core.ILambdaContext conext)
        {
            // Read the stream into a string
            /*if (input != null)
            {
                StreamReader streamReader = new StreamReader(input);
                inputString = streamReader.ReadToEnd();
            }*/
            
            //var output = new StreamReader(new TextReader(""));
            //output.Write("123");
            return "123";
        }
    }   
}