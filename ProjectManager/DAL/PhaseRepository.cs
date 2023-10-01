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

        public async Task<IEnumerable<PhaseDTO>> GetAllPhasesAsync(Guid projectId)
        {
            const string query = @"SELECT * FROM Phase WHERE ProjectID = @ProjectID;";

            using var connection = CreateConnection();
            var phases = await connection.QueryAsync<PhaseDTO>(query, new { ProjectID = projectId });
            Console.WriteLine(phases);
            return phases;
        }
        public async Task<bool> CreatePhasesAsync(Guid projectId)
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
            using var transaction = connection.BeginTransaction();
            try
            {
                foreach (var phase in phases)
                {
                    await connection.ExecuteAsync(query, phase, transaction);
                }
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                //all the phases or none
                transaction.Rollback();
                Console.WriteLine(ex.Message);
                return false;
            }
        }


    }
}
