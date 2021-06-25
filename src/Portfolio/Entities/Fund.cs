namespace Learning.Portfolio {

    public class Fund {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }

        public Fund(string id, string name, string code) {
            Id = id;
            Name = name;
            Code = code;
        }
    }
}