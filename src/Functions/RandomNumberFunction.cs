using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using System;

namespace Learning.Portfolio
{
    public class RandomNumber
    {
        Random fate = new Random(DateTime.Now.Millisecond);
        public APIGatewayProxyResponse Handle(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var max = request.GetIntFromQueryString("max", int.MaxValue);

            return new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Body = fate.Next(max).ToString()
            };
        }
    }
}