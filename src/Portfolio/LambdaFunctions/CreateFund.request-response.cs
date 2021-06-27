namespace Learning.Portfolio
{
    internal class CreateFundRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    internal class CreateFundResponse
    {
        public string Id { get; set; }
        public string Code { get; set; }
    }
}
