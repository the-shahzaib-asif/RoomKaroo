using System;

namespace RoomIQ.Models
{
    public class Inquiry
    {
        public int Id { get; set; }
        public int HostelId { get; set; }
        public string HostelName { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}