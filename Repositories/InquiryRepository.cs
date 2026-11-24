using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RoomIQ.Models;
using RoomIQ.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomIQ.Repositories
{
    public class InquiryRepository : IInquiryRepository
    {
        private readonly string _connectionString;

        public InquiryRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IEnumerable<Inquiry>> GetAllAsync()
        {
            using var conn = new SqlConnection(_connectionString);
            var sql = @"SELECT i.Id, i.HostelId, h.Name AS HostelName, i.StudentName, i.Phone, i.Message, i.CreatedAt
                        FROM Inquiries i
                        LEFT JOIN Hostels h ON i.HostelId = h.Id
                        ORDER BY i.Id DESC";
            return await conn.QueryAsync<Inquiry>(sql);
        }

        public async Task<int> AddAsync(Inquiry inquiry)
        {
            using var conn = new SqlConnection(_connectionString);
            var sql = @"INSERT INTO Inquiries (HostelId, StudentName, Phone, Message)
                        VALUES (@HostelId, @StudentName, @Phone, @Message);
                        SELECT CAST(SCOPE_IDENTITY() as int);";
            return await conn.ExecuteScalarAsync<int>(sql, inquiry);
        }
    }
}