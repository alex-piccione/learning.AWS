using System;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Learning.Exceptions;

namespace Learning.Portfolio {
    class GetFund : FundLambdaFunctionBase {

        public GetFund(IFundRepository repository) :base(repository) {
         }

        public GetFund() 
        {
        }

        public override APIGatewayProxyResponse Handle(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                var id = request.GetIdFromQueryString();
                var fund = repository.Get(id);
                return (fund == null) ?
                    CreateResponse(HttpStatusCode.NoContent, fund) :
                    CreateOkResponse(fund);
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