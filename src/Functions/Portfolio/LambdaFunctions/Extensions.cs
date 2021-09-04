using System;
using Amazon.Lambda.APIGatewayEvents;

namespace Learning.Portfolio
{
    public static class APIGatewayProxyRequestExtensions
    {
        public static string GetIdFromPath(this APIGatewayProxyRequest request)
        {
            if (request.PathParameters.TryGetValue("id", out string value))
                return value;

            throw new Exception("Path parameter \"id\" is missing.");
        }
    }
}
