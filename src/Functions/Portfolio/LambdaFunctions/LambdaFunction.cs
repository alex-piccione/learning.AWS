using System;
using System.Net;
using System.Threading;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Jil;
using Learning.Exceptions;

namespace Learning.Portfolio {
    abstract class LambdaFunction {

        private static Options jsonOptions = Options.ISO8601CamelCase;

        public abstract APIGatewayProxyResponse Handle(APIGatewayProxyRequest request, ILambdaContext context);


        protected void CheckTimeout(ILambdaContext context, Action action) 
        {

        }
        /*
 function handler(event, context, callback) {
      const timer = setTimeout(() => {
        console.log("oh no i'm going to timeout in 3 seconds!");
        // &c.
      }, context.getRemainingTimeInMillis() - 3 * 1000);
      try {
        // rest of code...
      } finally {
        clearTimeout(timer);
      }
      callback(null, result);
    }
 */

        // TODO: use a timer to handle the Lambda function timeout
        private void HandleTimeout(ILambdaContext context)
        {
            var time = context.RemainingTime;
        }


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
                throw new DeserializationFailedException(requestBody, typeof(T), exc);
            }
        }
    }
}