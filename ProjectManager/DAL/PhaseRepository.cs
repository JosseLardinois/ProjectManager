using Dapper;
using MySql.Data.MySqlClient;
using ProjectManager.DTO;
using ProjectManager.DTOs;
using ProjectManager.Interfaces;
using ProjectManager.Models;
using System.Data;

namespace ProjectManager.DAL
{
    public class PhaseRepository : IPhaseRepository
    {
        private readonly string _connetionString;

        public PhaseRepository()
        {
            _connetionString = "Server=localhost;Database=projectmanagement;User ID=root;Password=password;";
        }
        private IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connetionString);
        }


        public async Task<IEnumerable<Phase>> GetAllPhasesAsync(Guid projectId)
        {
            const string query = @"SELECT * FROM Phase WHERE ProjectId = @ProjectId;";

            using var connection = CreateConnection();
            var phases = await connection.QueryAsync<Phase>(query, new { ProjectId = projectId });

            // Create a new list of Phase
            var phaseList = new List<Phase>();

            // Loop through the fetched rows and add them to the list of PhaseDTO
            foreach (var phase in phases)
            {
                phaseList.Add(phase);
            }

            // Writeline the PhaseDTO list
            foreach (var phaseDTO in phaseList)
            {
                Console.WriteLine(phaseDTO);
            }
            return phaseList;
        }

        public async Task<bool> IsAPhaseActive(Guid projectId)
        {
            const string query = @"SELECT COUNT(*) FROM Phase WHERE ProjectId = @ProjectId AND Status = 'Active'";
            using var connection = CreateConnection();
            int activePhaseCount = await connection.ExecuteScalarAsync<int>(query, new { ProjectId = projectId });
            return activePhaseCount >= 1;  // returns true if there's at least one active phase
        }


        public async Task<bool> UpdatePhaseAsync(Phase phase)
        {
            const string query = @"UPDATE Phase SET Completed_By = @Completed_By, Completed_At = @Completed_At, Status = @Status WHERE Id = @Id;";
            var affectedRows = await CreateConnection().ExecuteAsync(query, phase);
            return affectedRows > 0;
        }
    }
}
