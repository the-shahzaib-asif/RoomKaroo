using RoomIQ.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomIQ.Repositories.Interfaces
{
    public interface IInquiryRepository
    {
        Task<IEnumerable<Inquiry>> GetAllAsync();
        Task<int> AddAsync(Inquiry inquiry);
    }
}