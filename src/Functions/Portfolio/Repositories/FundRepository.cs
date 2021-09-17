using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Learning.Portfolio
{
    // interface is stored within the concrete class because it only exists to make possible injection for test purpose
    public interface IFundRepository
    {
        void Create(Fund fund);
        Fund Get(string id);
        void Update(Fund fund);
        void Delete(string id);
        ICollection<Fund> List();
    }

    public class MongoDBFundRepository : IFundRepository
    {
        private string DATABASE = "Portfolio";
        private string COLLECTION = "Fund";
        private IMongoDatabase database;

        public MongoDBFundRepository(string connectionString)
        {
            var client = new MongoClient(MongoClientSettings.FromConnectionString(connectionString));
            // setting the Timeout returns this error: MongoClientSettings is frozen
            //client.Settings.ConnectTimeout = TimeSpan.FromSeconds(30); 
            var databaseSettings = new MongoDatabaseSettings(); // default settings
            database = client.GetDatabase(DATABASE, databaseSettings);

            if (!BsonClassMap.IsClassMapRegistered(typeof(Fund)))
            {
                Action<BsonClassMap<Fund>> mapping = (map) =>
                {
                    map.AutoMap();
                    map.MapIdMember(fund => fund.Id);
                    //map.SetIgnoreExtraElements(true);
                };

                BsonClassMap.RegisterClassMap(mapping);
            }
        }

        public void Create(Fund fund)
        {
            var collection = database.GetCollection<Fund>(COLLECTION);
            collection.InsertOne(fund);
        }

        public Fund Get(string id)
        {
            var collection = database.GetCollection<Fund>(COLLECTION);
            return collection.Find(IdFilter(id)).SingleOrDefault();
        }

        public void Update(Fund fund)
        {
            var collection = database.GetCollection<Fund>(COLLECTION);
            var update = new UpdateDefinitionBuilder<Fund>()
                .Set(f => f.Code, fund.Code)
                .Set(f => f.Name, fund.Name);
            collection.UpdateOne(IdFilter(fund.Id), update);
        }

        public void Delete(string id)
        {
            var collection = database.GetCollection<Fund>(COLLECTION);
            collection.DeleteOne(IdFilter(id));
        }

        private static FilterDefinition<Fund> IdFilter(string id) => new FilterDefinitionBuilder<Fund>().Eq(fund => fund.Id, id);

        public ICollection<Fund> List()
        {
            var collection = database.GetCollection<Fund>(COLLECTION);
            return collection.Find(FilterDefinition<Fund>.Empty).ToList();
        }
    }

}