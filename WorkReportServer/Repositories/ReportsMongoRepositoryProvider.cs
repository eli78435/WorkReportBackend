using System;
using MongoDB.Driver;

namespace WorkReportServer.Repositories
{
    public class ReportsMongoRepositoryProvider : IReportOperationsProvider
    {
        private readonly MongoClientSettings _mongoClientSettings;
        
        //private string _connectionString = "mongodb://root:example@localhost:27017";

        public ReportsMongoRepositoryProvider(MongoClientSettings mongoClientSettings)
        {
            _mongoClientSettings = mongoClientSettings ?? throw new ArgumentNullException(nameof(mongoClientSettings));
        }

        public IReportOperations CreateReportOperations()
        {
            var client = CreateClient();
            return new ReportsMongoRepository(client);
        }

        private MongoClient CreateClient() => new MongoClient(_mongoClientSettings);
    }
}