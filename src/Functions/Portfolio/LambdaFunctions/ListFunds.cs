using System;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Learning.Exceptions;

namespace Learning.Portfolio {
    class ListFund : LambdaFunction {



        public override APIGatewayProxyResponse Handle(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                // TODO: retrieve from Repository

                var funds = new[] {
                    new Fund("test 01", "Test fund", "TEST"),
                    new Fund("test 02", "Test fund", "TEST"),
                    };

                return CreateOkResponse(funds);
            }
            /*catch (InvalidRequestDataException exc)
            {
                context.Logger.LogLine(exc.ToString());
                return CreateResponse(HttpStatusCode.BadRequest, exc.Message) ;
            }*/
            catch (Exception exc)
            {
                context.Logger.LogLine(exc.ToString());
                return CreateErrorResponse(exc.Message);
            }
        }
    }
}