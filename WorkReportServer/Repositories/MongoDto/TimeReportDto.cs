using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace WorkReportServer.Repositories.MongoDto
{
    public class TimeReportDto
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [Required]
        [BsonElement]
        public string ReportId { get; set; }

        [Required]
        [BsonElement]
        public string UserId { get; set; }

        [Required]
        [BsonElement]
        public DateTime Timestamp { get; set; }

        [Required]
        [BsonElement]
        public DateTime Start { get; set; }

        [BsonElement]
        public DateTime? End { get; set; }
    }

    public static class TimeReportMapperExtension
    {
        public static TimeReportDto ToTimeReportDto(this TimeReport timeReport) => new TimeReportDto
        {
            ReportId = timeReport.Id,
            UserId = timeReport.UserId,
            Timestamp = timeReport.Timestamp,
            Start = timeReport.Start,
            End = timeReport.End
        };

        public static IEnumerable<TimeReportDto> ToTimeReportDtos(this IEnumerable<TimeReport> timeReports) => timeReports.Select(ToTimeReportDto).ToList();

        public static List<TimeReportDto> ToTimeReportDtoList(this IEnumerable<TimeReport> timeReports) => timeReports.Select(ToTimeReportDto).ToList();

        public static TimeReport ToTimeReport(this TimeReportDto dto) => new TimeReport
        {
            Id = dto.ReportId,
            UserId = dto.UserId,
            Timestamp = dto.Timestamp,
            Start = dto.Start,
            End = dto.End
        };

        public static IEnumerable<TimeReport> ToTimeReports(this IEnumerable<TimeReportDto> dtos) => dtos.Select(ToTimeReport).ToList();

        public static List<TimeReport> ToTimeReportsList(this IAsyncCursor<TimeReportDto> dtos) => dtos.ToEnumerable().Select(ToTimeReport).ToList();
    }
}
