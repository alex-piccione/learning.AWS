using System;
using Microsoft.Extensions.Configuration;

namespace Learning.Portfolio
{
    abstract class FundLambdaFunctionBase : LambdaFunction
    {
        protected IFundRepository repository;

        public FundLambdaFunctionBase(IFundRepository repository)
        {
            this.repository = repository;
        }

        public FundLambdaFunctionBase()
        {
            var configFile = "configuration.json";
            var variable = "MongoDB_connection_string";

            var configuration = new ConfigurationBuilder()
                .AddJsonFile(configFile)
                .Build();

            var connectionString = configuration[variable];
            if (connectionString == null) throw new Exception($@"Cannot find ""{variable}"" in ""{configFile}"".");

            repository = new MongoDBFundRepository(connectionString);
        }
    }
}
