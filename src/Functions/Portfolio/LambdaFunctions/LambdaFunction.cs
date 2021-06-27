using System;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Jil;
using Learning.Exceptions;

namespace Learning.Portfolio {
    abstract class LambdaFunction {

        private static Options jsonOptions = Options.ISO8601CamelCase;

        public abstract APIGatewayProxyResponse Handle(APIGatewayProxyRequest request, ILambdaContext context);

        protected APIGatewayProxyResponse CreateOkResponse<T>(T data) => CreateResponse(HttpStatusCode.OK, data);
        protected APIGatewayProxyResponse CreateErrorResponse(string message = null) 
        => new APIGatewayProxyResponse
        {
            StatusCode = 500,
            Body = message
        };

        protected APIGatewayProxyResponse CreateResponse<T>(HttpStatusCode statusCode, T data) 
        => new APIGatewayProxyResponse
        {
            StatusCode = (int)statusCode,
            Body = data == null ? null : JSON.Serialize(data, jsonOptions)
        };


        protected T GetRequest<T>(string requestBody)
        {
            try
            {
                return JSON.Deserialize<T>(requestBody, jsonOptions);
            }
            catch (Exception exc)
            {
                throw new DeserializationFailedException<T>(requestBody, exc);
            }
        }
    }
}