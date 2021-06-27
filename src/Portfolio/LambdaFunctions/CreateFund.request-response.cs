using Amazon.Lambda.APIGatewayEvents;

namespace Learning.Portfolio
{
    public class CreateFundRequest
    {
        public string FundName { get; set; }
        public string FundCode { get; set; }
    }

    public class CreateFundResponse : APIGatewayProxyResponse
    {
        public string FundId { get; set; }
        public string FundCode { get; set; }

    }
}
