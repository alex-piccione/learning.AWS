using System;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Learning.Exceptions;

namespace Learning.Portfolio {
    class GetFund : LambdaFunction {

        public override APIGatewayProxyResponse Handle(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                var id = request.GetIdFromQueryString();

                // TODO: retrieve from Repository

                var fund = new Fund(id, "Test fund", "TEST");

                return CreateResponse(
                    HttpStatusCode.Created,
                    new CreateFundResponse
                    {
                        Id = fund.Id,
                        Code = fund.Code
                    }
                );
            }
            catch (InvalidRequestDataException exc)
            {
                context.Logger.LogLine(exc.ToString());
                return CreateResponse(HttpStatusCode.BadRequest, exc.Message) ;
            }
            catch (Exception exc)
            {
                context.Logger.LogLine(exc.ToString());
                return CreateErrorResponse($"Something went wrong. {exc.Message}");
            }
        }
    }
}