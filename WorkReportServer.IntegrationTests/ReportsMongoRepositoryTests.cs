using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using NUnit.Framework;
using WorkReportServer.Repositories;

namespace WorkReportServer.IntegrationTests
{
    [TestFixture]
    public class ReportsMongoRepositoryTests
    {
        private IReportOperations _repository;

        [SetUp]
        public void Setup()
        {
            var mongoClientSettings = new MongoClientSettings
            {
                Credential = MongoCredential.CreateCredential("admin", "root", "example"),
                Server = new MongoServerAddress("localhost", 27017),
            };
            _repository = new ReportsMongoRepositoryProvider(mongoClientSettings).CreateReportOperations();
        }

        [Test]
        public void TestConnectivityToMongo()
        {
            var names = _repository.GetDatabasesNames();
            Assert.IsTrue(0 < names?.Count);
        }

        [Test]
        public async Task TestGetReports()
        {
            var reports = await _repository.GetReports();
            Assert.NotNull(reports);
        }

        [Test]
        public async Task TestAddReport()
        {
            var report = GenerateReport();
            await _repository.AddReport(report);
            var newReport = await _repository.GetReportById(report.Id);

            Assert.NotNull(newReport);
            Assert.AreEqual(report.Id, newReport.Id);
        }

        private TimeReport GenerateReport()
        {
            var id = Guid.NewGuid().ToString();
            return new TimeReport
            {
                Id = id,
                UserId = "userid" + id,
                Timestamp = DateTime.Now,
                Start = DateTime.Now,
                End = DateTime.Now
            };
        }
    }
}
