using RoomIQ.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomIQ.Repositories.Interfaces
{
    public interface IHostelRepository
    {
        Task<IEnumerable<Hostel>> GetAllAsync();
        Task<IEnumerable<Hostel>> FilterAsync(string? area, string? gender, int? maxPrice);
        Task<int> AddAsync(Hostel hostel);
        Task<bool> DeleteAsync(int id);
    }
}