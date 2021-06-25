using System;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;

namespace Learning.Portfolio {
    abstract class LambdaFunction {
        public abstract APIGatewayProxyResponse Handle(APIGatewayProxyRequest request, ILambdaContext context);
    }
}