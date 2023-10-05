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
        INSERT INTO Phase (Id, Name, ProjectId) 
        VALUES (@Id, @Name, @ProjectId);
    ";

            using var connection = CreateConnection();
            connection.Open();
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
