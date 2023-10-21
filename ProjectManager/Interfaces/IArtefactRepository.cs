using ProjectManager.DTO;
using ProjectManager.Models;

namespace ProjectManager.Interfaces
{
    public interface IArtefactRepository
    {
        Task<IEnumerable<Artefact>> GetArtefactsFromPhase(Guid phaseId);
        Task<bool> CreateArtefactsAsync(List<PhaseDTO> phases, List<DefaultArtefactDTO> defaultArtefacts);
    }
}
