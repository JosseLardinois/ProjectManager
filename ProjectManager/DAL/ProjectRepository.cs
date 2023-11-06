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

        public async Task<Project> GetProjectAsync(Guid Id)
        {
            using (var dbConnection = CreateConnection())
                {
                    const string query = @"SELECT * FROM Project WHERE Id = @Id;";
                    return await dbConnection.QueryFirstOrDefaultAsync<Project>(query, new { Id });           
                }
        }

        public async Task<bool> CreateProjectTransactional(Project project, Guid ownerId)
        {
                using (var connection = CreateConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Create project
                            const string createProjectQuery = @"INSERT INTO Project (Id, Name, Created_At) 
                                                    VALUES (@Id, @Name, @Created_At);";
                            await connection.ExecuteAsync(createProjectQuery, project, transaction);

                            // Create phases
                            var phases = new List<Phase>
                {
                    new Phase { Name = "Commercial Phase", ProjectId = project.Id, Id = Guid.NewGuid() },
                    new Phase { Name = "Inception Phase", ProjectId = project.Id, Id = Guid.NewGuid() },
                    new Phase { Name = "Elaboration Phase", ProjectId = project.Id, Id = Guid.NewGuid() },
                    new Phase { Name = "Construction Phase", ProjectId = project.Id, Id = Guid.NewGuid() },
                    new Phase { Name = "Production Phase", ProjectId = project.Id, Id = Guid.NewGuid() }
                };

                            const string createPhasesQuery = @"INSERT INTO Phase (Id, Name, ProjectId) 
                                                   VALUES (@Id, @Name, @ProjectId);";
                            foreach (var phase in phases)
                            {
                                await connection.ExecuteAsync(createPhasesQuery, phase, transaction);
                            }

                            // Get all default artefacts
                            const string fetchDefaultArtefactsQuery = @"SELECT * FROM defaultartefacts;";
                            var defaultArtefacts = (await connection.QueryAsync<DefaultArtefact>(fetchDefaultArtefactsQuery, transaction: transaction)).ToList();

                            // Create artefacts
                            var artefacts = defaultArtefacts.Select(da => new Artefact
                            {
                                Id = Guid.NewGuid(),
                                PhaseId = phases.Single(p => p.Name == da.Phase).Id,
                                DefaultArtefactId = da.Id
                            }).ToList();

                            const string insertArtefactQuery = @"INSERT INTO artefact (Id, PhaseId, DefaultArtefactId) 
                                                     VALUES (@Id, @PhaseId, @DefaultArtefactId);";
                            foreach (var artefact in artefacts)
                            {
                                await connection.ExecuteAsync(insertArtefactQuery, artefact, transaction);
                            }

                            // Add project owner
                            const string addProjectOwnerQuery = @"INSERT INTO ProjectOwner (ProjectId, OwnerId) 
                                                      VALUES (@ProjectId, @OwnerId);";
                            await connection.ExecuteAsync(addProjectOwnerQuery, new { ProjectId = project.Id, OwnerId = ownerId }, transaction);

                            // Commit the transaction
                            transaction.Commit();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            // Rollback the transaction if any exception occurs
                            transaction.Rollback();
                            // Log the exception (logging code should be here)
                            Console.WriteLine(ex.Message);
                            return false;
                        }
                    }
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
            using (var connection = CreateConnection())
            {
                connection.Open();
                // Start a transaction
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Delete artefacts related to the project
                        var deleteArtefactsQuery = @"
                    DELETE FROM artefact
                    WHERE PhaseId IN (
                        SELECT Id FROM Phase WHERE ProjectId = @ProjectId
                    );";
                        await connection.ExecuteAsync(deleteArtefactsQuery, new { ProjectId = projectId }, transaction);

                        // Delete phases related to the project
                        var deletePhasesQuery = @"
                    DELETE FROM Phase WHERE ProjectId = @ProjectId;";
                        await connection.ExecuteAsync(deletePhasesQuery, new { ProjectId = projectId }, transaction);

                        // Delete project-owner associations
                        var deleteProjectOwnersQuery = @"
                    DELETE FROM ProjectOwner WHERE ProjectId = @ProjectId;";
                        await connection.ExecuteAsync(deleteProjectOwnersQuery, new { ProjectId = projectId }, transaction);

                        // Finally, delete the project itself
                        var deleteProjectQuery = @"
                    DELETE FROM Project WHERE Id = @ProjectId;";
                        var affectedRows = await connection.ExecuteAsync(deleteProjectQuery, new { ProjectId = projectId }, transaction);

                        // Commit the transaction
                        transaction.Commit();
                        connection.Close();

                        return affectedRows > 0;
                    }
                    catch (Exception ex)
                    {
                        // Rollback the transaction if any exception occurs
                        transaction.Rollback();
                        // Log the exception (logging code should be here)
                        throw;
                    }
                }
            }
        }

    }
}
