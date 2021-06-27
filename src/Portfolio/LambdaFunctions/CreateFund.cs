using System;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;

namespace Learning.Portfolio {
    class CreateFund {
        public APIGatewayProxyResponse Handle(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var id = Guid.NewGuid().ToString();
            var name = "Fund TEet";
            var code = "TEST";

            //request.Body

            //validateCode(code) // UPPERCASE, MAX LENGTH

            var fund = new Fund(id, name, code);

            // TODO store using Repository

            var responseBody = $"{fund.Id}";

            return new APIGatewayProxyResponse
            {
                StatusCode = 200,
                // TODO return Fund serialized as JSON
                Body = responseBody
            };
        }
    }
}