using System;

namespace WorkReportServer
{
    public class TimeReport
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
