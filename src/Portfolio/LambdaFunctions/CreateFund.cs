using System;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;

namespace Learning.Portfolio {
    class CreateFund : LambdaFunction {

        private int CODE_MAX_LENGTH = 10;

        public override APIGatewayProxyResponse Handle(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var requestData = GetRequest<CreateFundRequest>(request.Body);

            var id = Guid.NewGuid().ToString();
            var name = requestData.Name.Trim();
            var code = ValidateCode(requestData.Code.Trim());

            var fund = new Fund(id, name, code);

            // TODO: store using Repository

            return CreateOkResponse(
                new CreateFundResponse
                {
                    Id = fund.Id,
                    Code = fund.Code
                }
            );
        }

        private string ValidateCode(string code)
        {
            if (code.Length > CODE_MAX_LENGTH)
                throw new Exception($"Code is too long. The max allowed length is {CODE_MAX_LENGTH}.");

            return code.ToUpperInvariant();
        }
    }
}