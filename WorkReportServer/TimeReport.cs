using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WorkReportServer
{
    public class TimeReport
    {
        [Required]
        public string Id { get; set; }
        
        [Required]
        public string UserId { get; set; }
        
        [Required]
        public DateTime Timestamp { get; set; }
        
        [Required]
        public DateTime Start { get; set; }
        
        public DateTime? End { get; set; }
    }
}
