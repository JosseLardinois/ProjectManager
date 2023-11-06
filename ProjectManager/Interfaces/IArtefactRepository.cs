using ProjectManager.DTO;
using ProjectManager.Models;

namespace ProjectManager.Interfaces
{
    public interface IArtefactRepository
    {
        Task<IEnumerable<Artefact>> GetArtefactsFromPhase(Guid phaseId);
        Task<bool> UpdateArtefactStatus(Artefact artefact);
        Task<IEnumerable<Artefact>> GetStatusArtefactsFromPhase(Guid phaseId, string status);
        Task<IEnumerable<ProjectArtefactDTO>> GetArtefactsFromProject(Guid projectId);
    }
}
