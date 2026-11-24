using System;

namespace RoomIQ.Models
{
    public class SavedHostel
    {
        public int Id { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public int HostelId { get; set; }
        public DateTime SavedAt { get; set; } = DateTime.Now;
    }
}