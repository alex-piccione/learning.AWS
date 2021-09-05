using System;
using System.Linq;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Learning.Exceptions;

namespace Learning.Portfolio {
    class DeleteFund : FundLambdaFunctionBase
    {

        public DeleteFund()
        { }

        public DeleteFund(IFundRepository repository) : base(repository)
        { }

        public override APIGatewayProxyResponse Handle(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                var id = request.GetIdFromPath();
                repository.Delete(id);
                return CreateOkResponse();
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
    }
}