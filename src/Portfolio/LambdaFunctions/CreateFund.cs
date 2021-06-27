using System;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Jil;
using Learning.Exceptions;

namespace Learning.Portfolio {
    class CreateFund : LambdaFunction {

        private int CODE_MAX_LENGTH = 10;

        public override APIGatewayProxyResponse Handle(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                var requestData = GetRequest<CreateFundRequest>(request.Body);

                ValidateRequest(requestData);
                // TODO: normalize strings

                var id = Guid.NewGuid().ToString();
                var name = requestData.Name?.Trim();



                var code = ValidateCode(requestData.Code?.Trim());

                var fund = new Fund(id, name, code);

                // TODO: store using Repository

                return CreateResponse(
                    HttpStatusCode.Created,
                    new CreateFundResponse
                    {
                        Id = fund.Id,
                        Code = fund.Code
                    }
                );
            }
            catch (DeserializationException exc)
            {
                context.Logger.LogLine(exc.ToString());
                return CreateResponse(HttpStatusCode.BadRequest, "Cannot deserialize request body");
            }
            catch (InvalidRequestDataException exc)
            {
                context.Logger.LogLine(exc.ToString());
                return CreateResponse(HttpStatusCode.BadRequest, exc.Message) ;
            }
            catch (Exception exc)
            {
                context.Logger.LogLine(exc.ToString());
                return CreateErrorResponse("Something went wrong");
            }
        }

        private void ValidateRequest(CreateFundRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new InvalidRequestDataException("Name", "must not be empty");

            if (string.IsNullOrWhiteSpace(request.Code))
                throw new InvalidRequestDataException("Code", "must not be empty");
        }

        private string ValidateCode(string code)
        {
            if (code?.Length > CODE_MAX_LENGTH)
                throw new Exception($"Code is too long. The max allowed length is {CODE_MAX_LENGTH}.");

            return code.ToUpperInvariant();
        }
    }
}