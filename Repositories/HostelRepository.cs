using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RoomIQ.Models;
using RoomIQ.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomIQ.Repositories
{
    public class HostelRepository : IHostelRepository
    {
        private readonly string _connectionString;

        public HostelRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IEnumerable<Hostel>> GetAllAsync()
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<Hostel>("SELECT * FROM Hostels ORDER BY Id DESC");
        }

        public async Task<IEnumerable<Hostel>> FilterAsync(string? area, string? gender, int? maxPrice)
        {
            using var conn = new SqlConnection(_connectionString);
            var sql = "SELECT * FROM Hostels WHERE 1=1";
            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(area) && area != "All")
            {
                sql += " AND Area = @Area";
                parameters.Add("Area", area);
            }

            if (!string.IsNullOrEmpty(gender) && gender != "All")
            {
                sql += " AND Gender = @Gender";
                parameters.Add("Gender", gender);
            }

            if (maxPrice.HasValue && maxPrice.Value > 0)
            {
                sql += " AND Price <= @MaxPrice";
                parameters.Add("MaxPrice", maxPrice.Value);
            }

            sql += " ORDER BY Id DESC";
            return await conn.QueryAsync<Hostel>(sql, parameters);
        }

        public async Task<int> AddAsync(Hostel hostel)
        {
            if (string.IsNullOrWhiteSpace(hostel.ImageUrl))
            {
                hostel.ImageUrl = "https://images.unsplash.com/photo-1555854877-bab0e564b8d5?auto=format&fit=crop&w=800&q=80";
            }

            using var conn = new SqlConnection(_connectionString);
            var sql = @"INSERT INTO Hostels (Name, Area, Gender, Price, Facilities, OwnerName, WhatsApp, ImageUrl)
                        VALUES (@Name, @Area, @Gender, @Price, @Facilities, @OwnerName, @WhatsApp, @ImageUrl);
                        SELECT CAST(SCOPE_IDENTITY() as int);";

            return await conn.ExecuteScalarAsync<int>(sql, hostel);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            var rows = await conn.ExecuteAsync("DELETE FROM Hostels WHERE Id = @Id", new { Id = id });
            return rows > 0;
        }
    }
}