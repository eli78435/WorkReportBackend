using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using WorkReportServer.Repositories.MongoDto;

namespace WorkReportServer.Repositories
{
    public class ReportsMongoRepository : IReportOperations
    {
        private const string ReportDatabaseName = "reportDb";
        private const string ReportCollectionName = "report";

        private readonly MongoClient _mongoClient;

        public ReportsMongoRepository(MongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public List<string> GetDatabasesNames()
        {
            var names = _mongoClient.ListDatabaseNames();
            return names.ToList();
        }

        public Task CreateReportCollection()
        {
            Assert();

            var reportDatabase = _mongoClient.GetDatabase(ReportDatabaseName);
            return reportDatabase.CreateCollectionAsync(ReportCollectionName);
        }

        public async Task<IEnumerable<TimeReport>> GetReports()
        {
            Assert();

            var reportDatabase = _mongoClient.GetDatabase(ReportDatabaseName);
            var reports = reportDatabase.GetCollection<TimeReportDto>(ReportCollectionName);
            var allReports = await reports.FindAsync(Builders<TimeReportDto>.Filter.Empty);
            return allReports.ToTimeReportsList();
        }

        public async Task<TimeReport> GetReportById(string reportId)
        {
            Assert();

            var reportDatabase = _mongoClient.GetDatabase(ReportDatabaseName);
            var filter = Builders<TimeReportDto>.Filter.Eq(tr => tr.ReportId, reportId);
            var reportsByFilter = await reportDatabase.GetCollection<TimeReportDto>(ReportCollectionName)
                .FindAsync<TimeReportDto>(filter);
            return reportsByFilter.FirstOrDefault().ToTimeReport();
        }

        public Task AddReport(TimeReport timeReport)
        {
            Assert();

            var reportDatabase = _mongoClient.GetDatabase(ReportDatabaseName);
            var timeReportDto = timeReport.ToTimeReportDto();
            return reportDatabase.GetCollection<TimeReportDto>(ReportCollectionName)
                .InsertOneAsync(timeReportDto);
        }

        public Task UpdateEndReportTime(string reportId, DateTime end)
        {
            Assert();

            var reportDatabase = _mongoClient.GetDatabase(ReportDatabaseName);
            var filter = Builders<TimeReportDto>.Filter.Eq(tr => tr.ReportId, reportId);
            return reportDatabase.GetCollection<TimeReportDto>(ReportCollectionName)
                .FindOneAndUpdateAsync(filter,
                    Builders<TimeReportDto>.Update.Set(dto => dto.End, end));
        }

        private void Assert()
        {
            var reportDatabase = _mongoClient.GetDatabase(ReportDatabaseName);
            if(reportDatabase == null) throw new ApplicationException($"Data base {ReportDatabaseName} not exist");
        }
    }
}
