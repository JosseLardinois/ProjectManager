using ProjectManager.DTO;
using ProjectManager.Models;

namespace ProjectManager.Interfaces
{
    public interface IArtefactRepository
    {
        Task<IEnumerable<Artefact>> GetArtefactsFromPhase(Guid phaseId);
        Task<bool> CreateArtefactsAsync(List<PhaseDTO> phases, List<DefaultArtefactDTO> defaultArtefacts);
        Task<bool> UpdateArtefactStatus(Artefact artefact);
        Task<IEnumerable<Artefact>> GetStatusArtefactsFromPhase(Guid phaseId, string status);
        Task<IEnumerable<Artefact>> GetArtefactsFromProject(Guid projectId);
    }
}
