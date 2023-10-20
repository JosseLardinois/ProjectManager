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

        public async Task<bool> CreateArtefactsAsync(List<PhaseDTO> phases)
        {
            var phaseIdMap = phases.ToDictionary(phase => phase.Name, phase => phase.Id);
            var artefacts = GenerateArtefacts(phaseIdMap);

            const string query = @"
                INSERT INTO artefact (Id, Name, PhaseId, Artefact_Type) 
                VALUES (@Id, @Name, @PhaseId, @Artefact_Type);
            ";

            using var connection = CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                foreach (var artefact in artefacts)
                {
                    await connection.ExecuteAsync(query, artefact, transaction);
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



        private List<Artefact> GenerateArtefacts(Dictionary<string, Guid> phaseIdMap)
        {
            return new List<Artefact>
    {
        new Artefact { Name = "Create Product Vision Document", PhaseId = phaseIdMap["Commercial Phase"], Id = Guid.NewGuid(), Artefact_Type = "Project Documentation" },
        new Artefact { Name = "Create Project Plan for Inception Phase", PhaseId = phaseIdMap["Commercial Phase"], Id = Guid.NewGuid(), Artefact_Type = "Project Documentation" },
        new Artefact { Name = "Start Glossary Document", PhaseId = phaseIdMap["Commercial Phase"], Id = Guid.NewGuid(), Artefact_Type = "Project Documentation" },
        new Artefact { Name = "Create Initial Roadmap", PhaseId = phaseIdMap["Commercial Phase"], Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Document High-Level Requirements", PhaseId = phaseIdMap["Commercial Phase"], Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Broadband Estimate", PhaseId = phaseIdMap["Commercial Phase"], Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },

        new Artefact { Name = "Create Project Charter", PhaseId = phaseIdMap["Inception Phase"], Id = Guid.NewGuid(), Artefact_Type = "Project Documentation" },
        new Artefact { Name = "Create Project Design", PhaseId = phaseIdMap["Inception Phase"], Id = Guid.NewGuid(), Artefact_Type = "Project Documentation" },
        new Artefact { Name = "Create Architectural Vision", PhaseId = phaseIdMap["Inception Phase"], Id = Guid.NewGuid(), Artefact_Type = "Project Documentation" },
        new Artefact { Name = "Create Project Plan for Elaboration Phase", PhaseId = phaseIdMap["Inception Phase"], Id = Guid.NewGuid(), Artefact_Type = "Project Documentation" },
        new Artefact { Name = "Create Business Analyst Documentation", PhaseId = phaseIdMap["Inception Phase"], Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Business Process Document(s)", PhaseId = phaseIdMap["Inception Phase"], Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Application Landscape", PhaseId = phaseIdMap["Inception Phase"], Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Use Case Overview", PhaseId = phaseIdMap["Inception Phase"], Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create System Overview/Concept", PhaseId = phaseIdMap["Inception Phase"], Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Detailed High-Level Requirements Document", PhaseId = phaseIdMap["Inception Phase"], Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Low-Level Requirements Document", PhaseId = phaseIdMap["Inception Phase"], Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Functional Data Model Document", PhaseId = phaseIdMap["Inception Phase"], Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create User/Customer Journey Maps", PhaseId = phaseIdMap["Inception Phase"], Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Sitemap", PhaseId = phaseIdMap["Inception Phase"], Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Wireframes", PhaseId = phaseIdMap["Inception Phase"], Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create System Design", PhaseId = phaseIdMap["Inception Phase"], Id = Guid.NewGuid(), Artefact_Type = "Technical Documentation" },
        new Artefact { Name = "Create Investigation Folder", PhaseId = phaseIdMap["Inception Phase"], Id = Guid.NewGuid(), Artefact_Type = "Technical Documentation" },
        new Artefact { Name = "Create Initial Infrastructure Document", PhaseId = phaseIdMap["Inception Phase"], Id = Guid.NewGuid(), Artefact_Type = "Technical Documentation" },
        new Artefact { Name = "Create Detailed Design (Draft)", PhaseId = phaseIdMap["Inception Phase"], Id = Guid.NewGuid(), Artefact_Type = "Technical Documentation" },

        new Artefact { Name = "Create Test Strategy", PhaseId = phaseIdMap["Elaboration Phase"], Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Test Plan", PhaseId = phaseIdMap["Elaboration Phase"], Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Activity Diagram", PhaseId = phaseIdMap["Elaboration Phase"], Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Use Case Details", PhaseId = phaseIdMap["Elaboration Phase"], Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create UI Design", PhaseId = phaseIdMap["Elaboration Phase"], Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Clickable Prototype", PhaseId = phaseIdMap["Elaboration Phase"], Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Vertical Slice", PhaseId = phaseIdMap["Elaboration Phase"], Id = Guid.NewGuid(), Artefact_Type = "Technical Documentation" },
        new Artefact { Name = "Create Project Setup", PhaseId = phaseIdMap["Elaboration Phase"], Id = Guid.NewGuid(), Artefact_Type = "Technical Documentation" },

        new Artefact { Name = "Create Progress Updates", PhaseId = phaseIdMap["Construction Phase"], Id = Guid.NewGuid(), Artefact_Type = "Project Documentation" },
        new Artefact { Name = "Create Financial Progress Update Document", PhaseId = phaseIdMap["Construction Phase"], Id = Guid.NewGuid(), Artefact_Type = "Project Documentation" },
        new Artefact { Name = "Create Technical Manual", PhaseId = phaseIdMap["Construction Phase"], Id = Guid.NewGuid(), Artefact_Type = "Technical Documentation" },

        new Artefact { Name = "Modify Demo Material", PhaseId = phaseIdMap["Production Phase"], Id = Guid.NewGuid(), Artefact_Type = "Project Documentation" },
        new Artefact { Name = "Create Marketing Material", PhaseId = phaseIdMap["Production Phase"], Id = Guid.NewGuid(), Artefact_Type = "Project Documentation" },
        new Artefact { Name = "Create Test Reports", PhaseId = phaseIdMap["Production Phase"], Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" }
    };
        }

    }
}
