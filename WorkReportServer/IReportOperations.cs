using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkReportServer
{
    public interface IReportOperationsProvider
    {
        IReportOperations CreateReportOperations();
    }

    public interface IReportOperations
    {
        List<string> GetDatabasesNames();
        Task CreateReportCollection();
        Task AddReport(TimeReport timeReport);
        Task UpdateEndReportTime(string id, DateTime end);
        Task<TimeReport> GetReportById(string reportId);
        Task<IEnumerable<TimeReport>> GetReports();
    }
}