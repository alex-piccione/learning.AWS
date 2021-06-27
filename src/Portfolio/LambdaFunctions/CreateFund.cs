using System;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;

namespace Learning.Portfolio {
    class CreateFund : LambdaFunction {

        public override APIGatewayProxyResponse Handle(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var id = Guid.NewGuid().ToString();
            var name = "Fund Test";
            var code = "TEST";

            //request.Body

            //validateCode(code) // UPPERCASE, MAX LENGTH

            var fund = new Fund(id, name, code);

            // TODO: store using Repository

            return CreateOkResponse(
                new CreateFundResponse
                {
                    FundId = fund.Id,
                    FundCode = fund.Code
                }
            );
        }
    }
}