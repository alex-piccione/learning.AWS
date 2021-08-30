using System;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Jil;
using Learning.Exceptions;

namespace Learning.Portfolio {
    class CreateFund : FundLambdaFunctionBase
    {
        private int CODE_MAX_LENGTH = 10;

        public CreateFund(IFundRepository repository) :base(repository) 
        { }

        public CreateFund() : base()
        { }

        public override APIGatewayProxyResponse Handle(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                var requestData = NormalizeRequest(GetRequest<CreateFundRequest>(request.Body));

                ValidateRequest(requestData);

                var id = Guid.NewGuid().ToString();
                var fund = new Fund(id, requestData.Name, requestData.Code);

                repository.Create(fund);

                return CreateResponse(
                    HttpStatusCode.Created,
                    new CreateFundResponse
                    {
                        Id = fund.Id,
                        Name = fund.Name,
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
                return CreateErrorResponse(exc.Message);
            }
        }

        /// Trim all the string properties and set the code uppercase
        private CreateFundRequest NormalizeRequest(CreateFundRequest request)
        {
            request.Code = request.Code?.Trim().ToUpperInvariant();
            request.Name = request.Name?.Trim();
            return request;
        }

        private void ValidateRequest(CreateFundRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new InvalidRequestDataException("Name", "must not be empty");

            if (string.IsNullOrWhiteSpace(request.Code))
                throw new InvalidRequestDataException("Code", "must not be empty");
            else if (request.Code.Length > CODE_MAX_LENGTH)
                throw new Exception($"Code is too long. The max allowed length is {CODE_MAX_LENGTH}.");
        }

        //private string ValidateCode(string code)
        //{
        //    if (code?.Length > CODE_MAX_LENGTH)
        //        throw new Exception($"Code is too long. The max allowed length is {CODE_MAX_LENGTH}.");

        //    return code.ToUpperInvariant();
        //}
    }
}