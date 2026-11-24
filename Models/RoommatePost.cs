namespace RoomIQ.Models
{
    public class RoommatePost
    {
        public int Id { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string University { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int Budget { get; set; }
        public string Preferences { get; set; } = string.Empty;
        public string WhatsApp { get; set; } = string.Empty;
    }
}