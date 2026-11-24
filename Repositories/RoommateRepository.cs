using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RoomIQ.Models;
using RoomIQ.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomIQ.Repositories
{
    public class RoommateRepository : IRoommateRepository
    {
        private readonly string _connectionString;

        public RoommateRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IEnumerable<RoommatePost>> GetAllAsync()
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<RoommatePost>("SELECT * FROM Roommates ORDER BY Id DESC");
        }

        public async Task<int> AddAsync(RoommatePost post)
        {
            using var conn = new SqlConnection(_connectionString);
            var sql = @"INSERT INTO Roommates (StudentName, University, Area, Gender, Budget, Preferences, WhatsApp)
                        VALUES (@StudentName, @University, @Area, @Gender, @Budget, @Preferences, @WhatsApp);
                        SELECT CAST(SCOPE_IDENTITY() as int);";

            return await conn.ExecuteScalarAsync<int>(sql, post);
        }
    }
}