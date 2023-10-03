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

        public async Task<ProjectDTO> GetProjectAsync(Guid Id)
        {
            using (var dbConnection = CreateConnection())
                {
                    const string query = @"SELECT * FROM Project WHERE Id = @Id;";
                    return await dbConnection.QueryFirstOrDefaultAsync<ProjectDTO>(query, new { Id });           
                }
        }

        public async Task<int> CreateProjectAsync(Project project)
        {
            project.Id = Guid.NewGuid();
            project.Created_At = DateTime.Now;
            using (var dbConnection = CreateConnection())
            {
                const string query = @"INSERT INTO Project (Id, Name, Created_At) 
                                       VALUES (@Id, @Name, @created_At);
                                       SELECT LAST_INSERT_ID();";
                return await dbConnection.ExecuteScalarAsync<int>(query, project);
            }
        }

        public async Task<bool> UpdateProjectAsync(Project project)
        {
            const string query = @"UPDATE Project SET Name = @Name, Finished = @Finished WHERE Id = @Id;";
            var affectedRows = await CreateConnection().ExecuteAsync(query, project);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteProjectAsync(Guid projectId)
        {
            const string query = @"DELETE FROM Project WHERE ProjectId = @ProjectId;";
            var affectedRows = await CreateConnection().ExecuteAsync(query, new { Id = projectId });
            return affectedRows > 0;
        }
    }
}
