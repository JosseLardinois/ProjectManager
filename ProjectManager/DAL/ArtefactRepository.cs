using Dapper;
using MySql.Data.MySqlClient;
using ProjectManager.DTO;
using ProjectManager.Interfaces;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.DAL
{
    public class ArtefactRepository : IArtefactRepository
    {
        private readonly string _connectionString;

        public ArtefactRepository()
        {
            _connectionString = "Server=localhost;Database=projectmanagement;User ID=root;Password=password;";
        }

        private IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public async Task<IEnumerable<Artefact>> GetArtefactsFromPhase(Guid phaseId)
        {
            const string query = @"SELECT * FROM artefact WHERE PhaseId = @PhaseId;";
            using var connection = CreateConnection();
            return await connection.QueryAsync<Artefact>(query, new { PhaseId = phaseId });
        }

        public async Task<bool> CreateArtefactsAsync(List<PhaseDTO> phases, List<DefaultArtefactDTO> defaultArtefacts)
        {
            var phaseIdMap = phases.ToDictionary(phase => phase.Name, phase => phase.Id);
            using var connection = CreateConnection();

            // Generate artefacts based on defaultartefacts
            var artefacts = defaultArtefacts.Select(da => new Artefact
            {
                Id = Guid.NewGuid(),
                PhaseId = phaseIdMap[da.Phase], // Assuming the name in defaultartefacts matches the phase name in phaseIdMap
                DefaultArtefactId = da.Id
            }).ToList();

            const string insertArtefactQuery = @"INSERT INTO artefact (Id, PhaseId, DefaultArtefactId) VALUES (@Id, @PhaseId, @DefaultArtefactId);";

            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                foreach (var artefact in artefacts)
                {
                    await connection.ExecuteAsync(insertArtefactQuery, artefact, transaction);
                }
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}
