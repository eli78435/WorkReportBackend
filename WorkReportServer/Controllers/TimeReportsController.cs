using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WorkReportServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeReportsController : ControllerBase
    {
        private readonly ILogger<TimeReportsController> _logger;
        private static List<TimeReport> _reports = new List<TimeReport>();

        public TimeReportsController(ILogger<TimeReportsController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IEnumerable<TimeReport> GetReports()
        {
            return _reports;
        }
        
        [HttpPost]
        public void AddReport(TimeReport report)
        {
            assertAddReport(report);

            _logger.LogInformation($"add");
            _reports.Add(report);
        }

        [HttpPut]
        public void UpdateReport(TimeReport report)
        {
            assertUpdateeport(report);
            var existing = _reports.First(r => r.Id == report.Id);
            existing.End = report.End;
        }

        private void assertAddReport(TimeReport report)
        {
            if (string.IsNullOrWhiteSpace(report.Id))
                throw new ArgumentNullException(nameof(report.Id));

            if (report.Start == null)
                throw new ArgumentNullException(nameof(report.Start));

            if (report.End.HasValue)
                throw new ArgumentException(nameof(report.End));

            if (_reports.Any(r => r.Id == report.Id))
                throw new ApplicationException($"report id {report.Id} already exist");
        }

        private void assertUpdateeport(TimeReport report)
        {
            if (string.IsNullOrWhiteSpace(report.Id))
                throw new ArgumentNullException(nameof(report.Id));

            if (report.Start == null)
                throw new ArgumentNullException(nameof(report.Start));

            if (report.End == null)
                throw new ArgumentException(nameof(report.End));

            if (! _reports.Any(r => r.Id == report.Id))
                throw new ApplicationException($"report id {report.Id} not exist, can't update");
        }
    }
}
