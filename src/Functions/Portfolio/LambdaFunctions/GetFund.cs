using System;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Learning.Exceptions;

namespace Learning.Portfolio {
    class GetFund : LambdaFunction {

        private IFundRepository repository;

        public GetFund(IFundRepository repository) {
            this.repository = repository;
        }

        public GetFund()
        {
            var connectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING");
            repository = new MongoDBFundRepository(connectionString);
        }

        public override APIGatewayProxyResponse Handle(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                var id = request.GetIdFromQueryString();
                var fund = repository.Get(id);
                return CreateOkResponse(fund);
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