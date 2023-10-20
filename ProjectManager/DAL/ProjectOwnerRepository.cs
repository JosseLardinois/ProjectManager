using Dapper;
using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using ProjectManager.Models;
using ProjectManager.Interfaces;

namespace ProjectManager.DAL
{
    public class ProjectOwnerRepository : IProjectOwnerRepository
    {
        private readonly string _connectionString;

        public ProjectOwnerRepository()
        {
            _connectionString = "Server=localhost;Database=projectmanagement;User ID=root;Password=password;";
        }

        private IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public async Task<IEnumerable<ProjectOwner>> GetProjectOwnersByProjectIdAsync(Guid projectId)
        {
            const string query = @"SELECT * FROM ProjectOwner WHERE ProjectId = @ProjectId;";
            return await CreateConnection().QueryAsync<ProjectOwner>(query, new { ProjectId = projectId });
        }

        public async Task<bool> AddProjectOwnerAsync(Guid projectId, Guid ownerId)
        {
            const string query = @"INSERT INTO ProjectOwner (ProjectId, OwnerId) VALUES (@ProjectId, @OwnerId);";
            var affectedRows = await CreateConnection().ExecuteAsync(query, new { ProjectId = projectId, OwnerId = ownerId });
            return affectedRows > 0;
        }

        public async Task<bool> RemoveProjectOwnerAsync(Guid projectId, Guid ownerId)
        {
            const string query = @"DELETE FROM ProjectOwner WHERE ProjectId = @ProjectId AND OwnerId = @OwnerId;";
            var affectedRows = await CreateConnection().ExecuteAsync(query, new { ProjectId = projectId, OwnerId = ownerId });
            return affectedRows > 0;
        }
    }
}
