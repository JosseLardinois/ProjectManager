using Dapper;
using MySql.Data.MySqlClient;
using ProjectManager.Interfaces;
using ProjectManager.Models;
using System.Data;

namespace ProjectManager.DAL
{
    public class DefaultArtefactRepository : IDefaultArtefactRepository
    {
        private readonly string _connectionString;

        public DefaultArtefactRepository()
        {
            _connectionString = "Server=localhost;Database=projectmanagement;User ID=root;Password=password;";
        }

        private IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }


        public async Task<List<DefaultArtefact>> GetAllArtefacts()
        {
            const string fetchDefaultArtefactsQuery = @"SELECT * FROM defaultartefacts;";
            using var connection = CreateConnection();
            return (await connection.QueryAsync<DefaultArtefact>(fetchDefaultArtefactsQuery)).ToList();
        }
        public async Task<int> CreateDefaultArtefactAsync(DefaultArtefact artefact)
        {
            const string query = @"INSERT INTO defaultartefacts (Name, Artefact_Type, Phase) VALUES (@Name, @Artefact_Type, @Phase);";
            return await CreateConnection().ExecuteScalarAsync<int>(query, artefact);
        }

        public async Task<bool> UpdateDefaultArtefactAsync(DefaultArtefact artefact)
        {
            const string query = @"UPDATE defaultartefacts SET Name = @Name WHERE Id = @Id;";
            var affectedRows = await CreateConnection().ExecuteAsync(query, artefact);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteDefaultArtefactAsync(int artefactId)
        {
            const string query = @"DELETE FROM defaultartefacts WHERE Id = @Id;";
            var affectedRows = await CreateConnection().ExecuteAsync(query, new { Id = artefactId });
            return affectedRows > 0;
        }



    }
}
