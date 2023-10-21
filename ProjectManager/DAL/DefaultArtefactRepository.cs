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



    }
}
