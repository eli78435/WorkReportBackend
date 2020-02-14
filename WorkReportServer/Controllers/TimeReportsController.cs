using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly IReportOperations _reportOperations;

        public TimeReportsController(ILogger<TimeReportsController> logger, IReportOperationsProvider reportOperationsProvider)
        {
            _logger = logger;
            _reportOperations = reportOperationsProvider.CreateReportOperations();
        }


        [HttpGet]
        public async Task<IActionResult> GetReports()
        {
            var reports = await _reportOperations.GetReports();
            return Ok(reports);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddReport(TimeReport report)
        {
            AssertAddReport(report);

            _logger.LogInformation($"add");
            await _reportOperations.AddReport(report);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReport(TimeReport report)
        {
            AssertUpdateReport(report);
            Debug.Assert(report.End != null);
            await _reportOperations.UpdateEndReportTime(report.Id, report.End.Value);
            return Ok();
        }

        private void AssertAddReport(TimeReport report)
        {
            if (string.IsNullOrWhiteSpace(report.Id))
                throw new ArgumentNullException(nameof(report.Id));

            if (report.Start == null)
                throw new ArgumentNullException(nameof(report.Start));

            if (report.End.HasValue)
                throw new ArgumentException(nameof(report.End));
        }

        private void AssertUpdateReport(TimeReport report)
        {
            if (string.IsNullOrWhiteSpace(report.Id))
                throw new ArgumentNullException(nameof(report.Id));

            if (report.Start == null)
                throw new ArgumentNullException(nameof(report.Start));

            if (report.End == null)
                throw new ArgumentException(nameof(report.End));
        }
    }
}
