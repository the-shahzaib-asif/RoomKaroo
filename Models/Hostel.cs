using System.Collections.Generic;
using System.Linq;

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

    // Deliverable 2 Dynamic Data Source
    public static class HostelData
    {
        public static List<Hostel> Hostels = new List<Hostel>
        {
            new Hostel
            {
                Id = 1,
                Name = "The Courtyard Boys Hostel",
                Area = "Johar Town",
                Gender = "Boys",
                Price = 18000,
                Facilities = "WiFi, 3 Times Mess, Laundry, Backup Generator",
                OwnerName = "Tariq Mehmood",
                WhatsApp = "923004112233",
                ImageUrl = "https://images.unsplash.com/photo-1555854877-bab0e564b8d5?auto=format&fit=crop&w=800&q=80"
            },
            new Hostel
            {
                Id = 2,
                Name = "Grace Executive Girls Residency",
                Area = "Model Town",
                Gender = "Girls",
                Price = 23000,
                Facilities = "AC Rooms, Attached Bath, 24/7 Warden, Mess",
                OwnerName = "Mrs. Farah Zafar",
                WhatsApp = "923214998877",
                ImageUrl = "https://images.unsplash.com/photo-1595526114035-0d45ed16cfbf?auto=format&fit=crop&w=800&q=80"
            },
            new Hostel
            {
                Id = 3,
                Name = "Scholars Nest Student Living",
                Area = "Anarkali",
                Gender = "Boys",
                Price = 14500,
                Facilities = "WiFi, Mess, Filtered Water, Near PUCIT",
                OwnerName = "Abdul Rasheed",
                WhatsApp = "923334556677",
                ImageUrl = "https://images.unsplash.com/photo-1522771739844-6a9f6d5f14af?auto=format&fit=crop&w=800&q=80"
            }
        };

        public static void Add(Hostel hostel)
        {
            hostel.Id = Hostels.Any() ? Hostels.Max(h => h.Id) + 1 : 1;
            if (string.IsNullOrWhiteSpace(hostel.ImageUrl))
            {
                hostel.ImageUrl = "https://images.unsplash.com/photo-1555854877-bab0e564b8d5?auto=format&fit=crop&w=800&q=80";
            }
            Hostels.Insert(0, hostel);
        }

        public static void Delete(int id)
        {
            var item = Hostels.FirstOrDefault(h => h.Id == id);
            if (item != null) Hostels.Remove(item);
        }
    }
}