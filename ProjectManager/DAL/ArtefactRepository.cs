using Dapper;
using MySql.Data.MySqlClient;
using ProjectManager.DTO;
using ProjectManager.Interfaces;
using ProjectManager.Models;
using System.Data;

namespace ProjectManager.DAL
{
    public class ArtefactRepository : IArtefactRepository
    {
        private readonly string _connetionString;
        public ArtefactRepository()
        {
            _connetionString = "Server=localhost;Database=projectmanagement;User ID=root;Password=password;";
        }
        private IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connetionString);
        }
        

        public async Task<IEnumerable<ArtefactDTO>> GetArtefactsFromPhase(Guid phaseId)
        {
            const string query = @"SELECT * FROM artefact WHERE PhaseId = @PhaseId;";

            using var connection = CreateConnection();
            var artefacts = await connection.QueryAsync<ArtefactDTO>(query, new { PhaseId = phaseId });

            // Create a new list of PhaseDTO
            var artefactDTOList = new List<ArtefactDTO>();

            // Loop through the fetched rows and add them to the list of PhaseDTO
            foreach (var artefact in artefacts)
            {
                artefactDTOList.Add(artefact);
            }

            return artefactDTOList;
        }

        


        public async Task<bool> CreateArtefactsAsync(Guid phaseId)
        {
            var artefacts = new List<Artefact>
            {
                        new Artefact { Name = "Create Product Vision Document", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Project Documentation" },
        new Artefact { Name = "Create Project Plan for Inception Phase", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Project Documentation" },
        new Artefact { Name = "Start Glossary Document", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Project Documentation" },
        new Artefact { Name = "Create Initial Roadmap", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Document High-Level Requirements", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Broadband Estimate", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Project Charter", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Project Documentation" },
        new Artefact { Name = "Create Project Design", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Project Documentation" },
        new Artefact { Name = "Create Risk Log", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Project Documentation" },
        new Artefact { Name = "Create Architectural Vision", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Project Documentation" },
        new Artefact { Name = "Create Project Plan for Elaboration Phase", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Project Documentation" },
        new Artefact { Name = "Create Business Analyst Documentation", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Business Process Document(s)", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Application Landscape", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Use Case Overview", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create System Overview/Concept", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Detailed High-Level Requirements Document", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Low-Level Requirements Document", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Functional Data Model Document", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create User/Customer Journey Maps", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Sitemap", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Wireframes", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create System Design", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Technical Documentation" },
        new Artefact { Name = "Create Investigation Folder", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Technical Documentation" },
        new Artefact { Name = "Create Initial Infrastructure Document", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Technical Documentation" },
        new Artefact { Name = "Create Detailed Design (Draft)", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Technical Documentation" },
        new Artefact { Name = "Create Test Strategy", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Test Plan", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Activity Diagram", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Use Case Details", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create UI Design", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Clickable Prototype", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" },
        new Artefact { Name = "Create Vertical Slice", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Technical Documentation" },
        new Artefact { Name = "Create Project Setup", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Technical Documentation" },
        new Artefact { Name = "Create Progress Updates", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Project Documentation" },
        new Artefact { Name = "Create Technical Manual", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Technical Documentation" },
        new Artefact { Name = "Modify Demo Material", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Project Documentation" },
        new Artefact { Name = "Create Marketing Material", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Project Documentation" },
        new Artefact { Name = "Create Test Reports", PhaseId = phaseId, Id = Guid.NewGuid(), Artefact_Type = "Functional Documentation" }
            };

            const string query = @"
                INSERT INTO Phase (Id, Name, PhaseId, Artefact_Type) 
                VALUES (@Id, @Name, @PhaseId, @Artefact_Type);
            ";
            using var connection = CreateConnection();
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
                //all artefacts or none
                transaction.Rollback();
                Console.WriteLine(ex.Message);
                return false;
            }
        }


    }
}
