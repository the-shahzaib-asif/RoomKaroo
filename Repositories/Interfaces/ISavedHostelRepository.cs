using RoomIQ.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomIQ.Repositories.Interfaces
{
    public interface ISavedHostelRepository
    {
        Task<IEnumerable<int>> GetSavedIdsByUserAsync(string userEmail);
        Task<bool> ToggleSaveAsync(string userEmail, int hostelId);
    }
}