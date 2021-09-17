using System;
using System.Linq;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Learning.Exceptions;

namespace Learning.Portfolio {
    class UpdateFund : FundLambdaFunctionBase
    {
        private int CODE_MAX_LENGTH = 10;

        public UpdateFund()
        { }

        public UpdateFund(IFundRepository repository) : base(repository)
        { }

        public override APIGatewayProxyResponse Handle(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Body))
                    return CreateResponse(HttpStatusCode.BadRequest, "Request body cannot be empty.");

                var requestData = NormalizeRequest(GetRequest<UpdateFundRequest>(request.Body));

                ValidateRequest(requestData);

                var list = repository.List();
                if (list?.FirstOrDefault(f => IsSameName(f.Name, requestData.Name) ) != null)
                    return CreateResponse(HttpStatusCode.BadRequest, "A Fund with the same name already exists.");
                if (list?.FirstOrDefault(f => IsSameCode(f.Code, requestData.Code)) != null)
                    return CreateResponse(HttpStatusCode.BadRequest, "A Fund with the same code already exists.");

                if (repository.Get(requestData.Id) == null)
                    return CreateResponse(HttpStatusCode.NotFound, "A Fund with the passed id does not exists.");

                var fund = new Fund(requestData.Id, requestData.Name, requestData.Code);

                repository.Update(fund);

                return CreateResponse(
                    HttpStatusCode.OK,
                    new UpdateFundResponse
                    {
                        Id = fund.Id,
                        Name = fund.Name,
                        Code = fund.Code
                    }
                );
            }
            catch (DeserializationFailedException exc)
            {
                context.Logger.LogLine($"JSON:/n{exc.Json}/n/n{exc}");
                return CreateResponse(HttpStatusCode.BadRequest, $"Failed to deserialize. JSON: {exc.Json}");
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
        private UpdateFundRequest NormalizeRequest(UpdateFundRequest request)
        {
            request.Code = request.Code?.Trim().ToUpperInvariant();
            request.Name = request.Name?.Trim();
            return request;
        }

        private void ValidateRequest(UpdateFundRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new InvalidRequestDataException("Name", "must not be empty");

            if (string.IsNullOrWhiteSpace(request.Code))
                throw new InvalidRequestDataException("Code", "must not be empty");
            else if (request.Code.Length > CODE_MAX_LENGTH)
                throw new Exception($"Code is too long. The max allowed length is {CODE_MAX_LENGTH}.");
        }

        private bool IsSameName(string a, string b) => a.ToLowerInvariant() == b.ToLowerInvariant();
        private bool IsSameCode(string a, string b) => a.ToLowerInvariant() == b.ToLowerInvariant();
    }
}