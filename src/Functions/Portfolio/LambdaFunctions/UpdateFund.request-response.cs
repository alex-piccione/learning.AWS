namespace Learning.Portfolio
{
    internal class UpdateFundRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    internal class UpdateFundResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
