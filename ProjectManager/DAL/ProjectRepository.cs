using Dapper;
using MySql.Data.MySqlClient;
using ProjectManager.DTOs;
using ProjectManager.Models;
using System;
using System.Data;
using System.Threading.Tasks;

namespace ProjectManager.DAL
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly string _connectionString;

        public ProjectRepository()
        {
            // You should keep your connection strings secure and not hard-coded in real-world applications.
            _connectionString = "Server=localhost;Database=projectmanagement;User ID=root;Password=password;";
        }

        private IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public async Task<ProjectDTO> GetProjectAsync(int projectId)
        {
            const string query = @"SELECT * FROM Project WHERE ProjectID = @ProjectID;";
            using var dbConnection = CreateConnection();
            return await dbConnection.QueryFirstOrDefaultAsync<ProjectDTO>(query, new { ProjectID = projectId });
        }

        public async Task<int> CreateProjectAsync(Project project)
        {
            using (var dbConnection = CreateConnection())
            {
                const string query = @"INSERT INTO Project (Name, Created_At, Owners, PhaseID) 
                                       VALUES (@Name, @created_At, @Owners, @PhaseID);
                                       SELECT LAST_INSERT_ID();";
                return await dbConnection.ExecuteScalarAsync<int>(query, project);
            }
        }

        public async Task<bool> UpdateProjectAsync(Project project)
        {
            const string query = @"UPDATE Project SET Name = @Name, Created_At = @created_At, Owners = @Owners, PhaseID = @PhaseID WHERE ProjectID = @ProjectID;";
            var affectedRows = await CreateConnection().ExecuteAsync(query, project);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteProjectAsync(int projectId)
        {
            const string query = @"DELETE FROM Project WHERE ProjectID = @ProjectID;";
            var affectedRows = await CreateConnection().ExecuteAsync(query, new { ProjectID = projectId });
            return affectedRows > 0;
        }
    }
}
