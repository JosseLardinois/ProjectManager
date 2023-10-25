using ProjectManager.DAL;
using ProjectManager.DTO;
using ProjectManager.Models;

namespace ProjectManager.Interfaces
{
    public interface IArtefactService
    {
        Task<IEnumerable<ArtefactDTO>> GetArtefactsFromPhase(Guid phaseId);
        Task<bool> CreateArtefacts(List<PhaseDTO> phases, List<DefaultArtefactDTO> defaultArtefacts);
        Task<bool> UpdateArtefactStatusAsync(ArtefactDTO artefactDto);

        Task<IEnumerable<ArtefactDTO>> GetArtefactsFromProject(Guid projectId);

        Task<IEnumerable<ArtefactDTO>> GetStatusArtefactsFromPhase(Guid phaseId, string status);
    }
}
