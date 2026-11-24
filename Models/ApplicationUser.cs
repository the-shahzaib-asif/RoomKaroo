using Microsoft.AspNetCore.Identity;

namespace RoomIQ.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string UserRole { get; set; } = "Finder"; // Finder or Lister
        public string WhatsAppNumber { get; set; } = string.Empty;
    }
}