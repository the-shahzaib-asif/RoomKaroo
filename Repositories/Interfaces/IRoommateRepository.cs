using RoomIQ.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomIQ.Repositories.Interfaces
{
    public interface IRoommateRepository
    {
        Task<IEnumerable<RoommatePost>> GetAllAsync();
        Task<int> AddAsync(RoommatePost post);
    }
}