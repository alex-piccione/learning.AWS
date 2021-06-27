namespace Learning.Portfolio
{
    public class CreateFundRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class CreateFundResponse
    {
        public string Id { get; set; }
        public string Code { get; set; }
    }
}
