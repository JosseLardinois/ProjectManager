using Dapper;
using MySql.Data.MySqlClient;
using ProjectManager.DTO;
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
        public async Task CreatePhasesAsync(Guid projectId)
        {
            var phases = new List<Phase>
            {
                new Phase { Name = "Commercial Phase", ProjectId = projectId, Id = Guid.NewGuid() },
                new Phase { Name = "Inception Phase", ProjectId = projectId, Id = Guid.NewGuid() },
                new Phase { Name = "Elaboration Phase", ProjectId = projectId, Id = Guid.NewGuid() },
                new Phase { Name = "Construction Phase", ProjectId = projectId, Id = Guid.NewGuid() },
                new Phase { Name = "Production Phase", ProjectId = projectId, Id = Guid.NewGuid() }
            };

            const string query = @"
                INSERT INTO Phase (Id, Name, ProjectID) 
                VALUES (@Id, @Name, @ProjectID);
            ";

            using var connection = CreateConnection();
            foreach (var phase in phases)
            {
                await connection.ExecuteAsync(query, phase);
            }
        }

    }
}
