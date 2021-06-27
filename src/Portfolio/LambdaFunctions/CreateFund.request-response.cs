namespace Learning.Portfolio
{
    public class CreateFundRequest
    {
        public string FundName { get; set; }
        public string FundCode { get; set; }
    }

    public class CreateFundResponse
    {
        public string FundId { get; set; }
        public string FundCode { get; set; }
    }
}
