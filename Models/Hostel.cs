namespace RoomIQ.Models
{
    public class Hostel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Facilities { get; set; } = string.Empty;
        public string OwnerName { get; set; } = string.Empty;
        public string WhatsApp { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}