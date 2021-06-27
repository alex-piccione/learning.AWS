using System;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Jil;

namespace Learning.Portfolio {
    abstract class LambdaFunction {
        public abstract APIGatewayProxyResponse Handle(APIGatewayProxyRequest request, ILambdaContext context);

        protected APIGatewayProxyResponse CreateOkResponse<T>(T data) => CreateResponse(HttpStatusCode.OK, data);

        protected APIGatewayProxyResponse CreateResponse<T>(HttpStatusCode statusCode, T data) 
        => new APIGatewayProxyResponse
        {
            StatusCode = (int)statusCode,
            Body = JSON.Serialize(data)
        };


        protected T GetRequest<T>(string requestBody)
        {
            try
            {
                return JSON.Deserialize<T>(requestBody);
            }
            catch (Exception exc)
            {
                throw new Exception($"Failed to deserialize request body to {typeof(T)}", exc);
            }
        }
    }
}