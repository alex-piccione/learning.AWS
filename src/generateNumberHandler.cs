using System;
using System.IO;
using Amazon.Lambda.Core;
// Amazon.Lambda.Serialization.SystemTextJson

// ref: https://dzone.com/articles/under-the-hood-of-net-core-lambda-request

// learning::Learning.Portfolio.RandomNumberFunction::Handle
[assembly:LambdaSerializer(typeof(String))]
namespace Learning.Portfolio
{    
    public class RandomNumberFunction {
        public string Handle(string input, Amazon.Lambda.Core.ILambdaContext context)
        {
            // Read the stream into a string
            /*if (input != null)
            {
                StreamReader streamReader = new StreamReader(input);
                inputString = streamReader.ReadToEnd();
            }*/
            
            //var output = new StreamReader(new TextReader(""));
            //output.Write("123");
            return "456";
        }
    }   
}