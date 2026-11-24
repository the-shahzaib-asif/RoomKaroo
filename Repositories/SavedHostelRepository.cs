using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RoomIQ.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomIQ.Repositories
{
    public class SavedHostelRepository : ISavedHostelRepository
    {
        private readonly string _connectionString;

        public SavedHostelRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IEnumerable<int>> GetSavedIdsByUserAsync(string userEmail)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<int>(
                "SELECT HostelId FROM SavedHostels WHERE UserEmail = @UserEmail",
                new { UserEmail = userEmail });
        }

        public async Task<bool> ToggleSaveAsync(string userEmail, int hostelId)
        {
            using var conn = new SqlConnection(_connectionString);
            var exists = await conn.ExecuteScalarAsync<int>(
                "SELECT COUNT(1) FROM SavedHostels WHERE UserEmail = @UserEmail AND HostelId = @HostelId",
                new { UserEmail = userEmail, HostelId = hostelId });

            if (exists > 0)
            {
                await conn.ExecuteAsync(
                    "DELETE FROM SavedHostels WHERE UserEmail = @UserEmail AND HostelId = @HostelId",
                    new { UserEmail = userEmail, HostelId = hostelId });
                return false; // Unsaved
            }

            await conn.ExecuteAsync(
                "INSERT INTO SavedHostels (UserEmail, HostelId) VALUES (@UserEmail, @HostelId)",
                new { UserEmail = userEmail, HostelId = hostelId });
            return true; // Saved
        }
    }
}