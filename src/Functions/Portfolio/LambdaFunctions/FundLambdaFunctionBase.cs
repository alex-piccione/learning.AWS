using System;

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
            var connectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING");
            if (connectionString == null) throw new ArgumentNullException("Environment variable \"MONGODB_CONNECTION_STRING\" not found.");
            repository = new MongoDBFundRepository(connectionString);
        }
    }
}
