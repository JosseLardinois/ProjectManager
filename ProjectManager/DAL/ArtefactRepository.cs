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

        public async Task<bool> UpdateArtefactStatus(Artefact artefact)
        {
            const string query = @"UPDATE artefact SET Status = @Status, Completed_By = @Completed_By, Completed_At = @Completed_At WHERE Id = @Id;";
            var affectedRows = await CreateConnection().ExecuteAsync(query, artefact);
            return affectedRows > 0;
        }

        public async Task<IEnumerable<Artefact>> GetStatusArtefactsFromPhase(Guid phaseId, string status)
        {
            const string query = @"SELECT * FROM artefact WHERE PhaseId = @PhaseId AND Status = @Status;";
            using var connection = CreateConnection();
            return await connection.QueryAsync<Artefact>(query, new { PhaseId = phaseId, Status = status });
        }

        public async Task<IEnumerable<ProjectArtefactDTO>> GetArtefactsFromProject(Guid projectId)
        {
            const string query = @"SELECT a.Id, a.Status, a.Completed_By, a.Completed_At, da.Name AS DefaultArtefactName, da.Artefact_Type FROM artefact a JOIN defaultartefacts da ON a.DefaultArtefactId = da.Id
                                    JOIN Phase ph ON a.PhaseId = ph.Id JOIN Project p ON ph.ProjectId = p.Id WHERE p.Id = @ProjectId";
            using var connection = CreateConnection();
            return await connection.QueryAsync<ProjectArtefactDTO>(query, new { ProjectId = projectId});
        }
    }
}
