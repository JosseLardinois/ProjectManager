using ProjectManager.DAL;
using ProjectManager.DTO;
using ProjectManager.Models;

namespace ProjectManager.Interfaces
{
    public interface IArtefactService
    {
        Task<IEnumerable<ArtefactDTO>> GetArtefactsFromPhase(Guid phaseId);
        Task<bool> UpdateArtefactStatusAsync(ArtefactDTO artefactDto);

        Task<IEnumerable<ProjectArtefactDTO>> GetArtefactsFromProject(Guid projectId);

        Task<IEnumerable<ArtefactDTO>> GetStatusArtefactsFromPhase(Guid phaseId, string status);
    }
}
