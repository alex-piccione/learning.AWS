using System;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
//using Amazon.Lambda.Serialization.SystemTextJson;

// ref: https://dzone.com/articles/under-the-hood-of-net-core-lambda-request

// AWS documentation () suggest to use LambdaJsonSerializer because of betetr performance
// but it results deprecated because with some DEBUG session it return wrong casing JSON objects

//[assembly: LambdaSerializer(typeof(LambdaJsonSerializer))]
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace Learning.Portfolio
{
    public class RandomNumber
    {
        Random fate = new Random(DateTime.Now.Millisecond);
        public APIGatewayProxyResponse Handle(APIGatewayProxyRequest request, ILambdaContext context)
        {

            return new APIGatewayProxyResponse {
                StatusCode = 200,
                Body = fate.Next().ToString()
            };
        }
    }
}